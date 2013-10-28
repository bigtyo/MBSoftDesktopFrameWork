using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SentraSolutionFramework.Entity;

namespace SentraSolutionFramework.Persistance
{
    public enum MoveType
    {
        FindFirst,
        FindLast,
        FindCriteria,
        MoveFirst,
        MovePrevious,
        MoveNext,
        MoveLast
    }

    public delegate void DataMoving(MoveType MovingType, bool IsError);
    public delegate void AfterLoad();

    public interface IEntityNavigator
    {
        bool MoveData(MoveType MovingType);
        bool MoveFirst();
        bool MovePrevious();
        bool MoveNext();
        bool MoveLast();
        void SetNew(bool CallSetDefault);
        void Save(bool CallValidateError);
        bool FindFirst();
        bool FindLast();
        bool FindCriteria(string Criteria);
        string Filter { get; set; }
        string OrderField { get; set; }
        int Delete(bool CallValidateError);
        SaveType SaveType { get; }
        TableDef TableDef { get; }
        object CurrentFieldValue { get; }
        object FindValue { set; }
        FieldDef OrderFieldDef { get; }
        bool Reload();
        bool ReloadFromOriginal();
        object Entity { get; }
        object Original { get; }
        event DataMoving onDataMoving;
        event AfterLoad onAfterLoad;
        DataPersistance DataPersistance { get; set; }
        void SetDefaultValue(string ExcludeFields);
    }

    public abstract class AbstractNavigator : IEntityNavigator
    {
        protected object _Entity;
        protected object _Original;

        private bool IsError = false;
        private string _Filter;
        private FieldDef _OrderFld;
        private int _CriteriaPos;
        protected TableDef td;
        private string strQuery;
        private SaveType _SaveType;

        private string _CurrentKeyValue = string.Empty;

        public object CurrentFieldValue
        {
            get
            {
                return _Original != null ?
                    _OrderFld.GetValue(_Original) : null;
            }
        }
        public FieldDef OrderFieldDef { get { return _OrderFld; } }

        public TableDef TableDef { get { return td; } }

        /// <summary>
        /// Jenis Simpan yg akan dilakukan ketika 
        /// dipanggil Perintah Save.
        /// </summary>
        public SaveType SaveType { get { return _SaveType; } }

        object IEntityNavigator.Entity { get { return _Entity; } }
        object IEntityNavigator.Original { get { return _Original; } }

        public void SetDefaultValue(string ExcludeFields)
        {
            td.SetDefault(_Entity, ExcludeFields);
        }

        /// <summary>
        /// Filter yg digunakan untuk menyaring data
        /// </summary>
        public string Filter
        {
            get { return _Filter; }
            set
            {
                _Filter = value;
                UpdateStrQuery();
            }
        }

        /// <summary>
        /// Field yg digunakan untuk Urutan Melihat Data, FindFirst, dan FindLast 
        /// </summary>
        public string OrderField
        {
            get { return _OrderFld.FieldName; }
            set
            {
                _OrderFld = td.GetFieldDef(value);
                UpdateStrQuery();
            }
        }

        private bool IsKeyFieldEqualOrderField;
        private void UpdateStrQuery()
        {
            string tmpFilter;

            if (_Filter.Length > 0)
                tmpFilter = _Filter;
            else
                tmpFilter = "1=1";

            string TmpStr = DataPersistance.BuildKeyField(td);
            if (_OrderFld._dtlsa == null)
            {
                strQuery = string.Concat("SELECT KeyField FROM (SELECT ",
                    TmpStr, " AS KeyField,", _OrderFld.FieldName, " FROM ",
                    td._TableName, " WHERE (", tmpFilter, ")) AS X");
                _CriteriaPos = strQuery.Length - 6;
            }
            else
            {
                strQuery = string.Concat("SELECT KeyField FROM (SELECT KeyField,",
                    DataPersistance.GetSqlCoalesceNoFormat(_OrderFld.FieldName,
                    DataPersistance.FormatSqlValue(string.Empty)),
                    " AS ", _OrderFld.FieldName, " FROM (SELECT ", TmpStr, " AS KeyField,(", _OrderFld._dtlsa._SqlQuery, ") AS ",
                    _OrderFld.FieldName, " FROM ",
                    td._TableName, " WHERE (", tmpFilter, ")) AS X) AS X");
                _CriteriaPos = strQuery.Length - 12;
            }
            IsKeyFieldEqualOrderField = TmpStr == _OrderFld.FieldName;
        }

        /// <summary>
        /// Nilai yg dicari oleh FindCriteria
        /// </summary>
        public string Criteria;

        /// <summary>
        /// Nilai yg dicari oleh FindFirst/ FindLast
        /// </summary>
        public object FindValue;

        object IEntityNavigator.FindValue { set { FindValue = value; } }

        protected DataPersistance _DataPersistance;
        protected ParentEntity e;

        /// <summary>
        /// ObjectContext yang digunakan
        /// </summary>
        public DataPersistance DataPersistance
        {
            get
            {
                return e != null ?
                    e.DataPersistance : _DataPersistance ??
                    BaseFramework.DefaultDataPersistance;
            }
            set
            {
                if (e != null)
                    e.DataPersistance = value;
                else
                    _DataPersistance = value;
            }
        }

        protected void DoInit()
        {
            _Filter = string.Empty;

            IsError = true;

            td = MetaData.GetTableDef(_Entity.GetType());
            DataPersistance.ValidateTableDef(td);
            _SaveType = SaveType.SaveNew;
            if (e != null) e._SaveType = SaveType.SaveNew;
            td.SetDefault(_Entity);
            foreach (FieldDef fld in td.KeyFields.Values)
            {
                _OrderFld = fld;
                break;
            }
            UpdateStrQuery();
        }

        public event DataMoving onDataMoving;
        public event AfterLoad onAfterLoad;

        /// <summary>
        /// Reload dari Database Entity sesuai Key Asal yang dimasukkan
        /// </summary>
        public bool Reload()
        {
            if (SaveType == SaveType.SaveNew) return false;
            string NewCriteria = string.Empty;

            foreach (FieldDef fld in td.KeyFields.Values)
                NewCriteria = string.Concat(NewCriteria, " AND ",
                    DataPersistance.FormatSqlObject(fld.FieldName), "=",
                    DataPersistance.FormatSqlValue(fld.GetValue(_Original)));
            Criteria = NewCriteria.Substring(5);
            return MoveData(MoveType.FindCriteria);
        }

        /// <summary>
        /// Reload dari Entity Original
        /// </summary>
        /// <returns></returns>
        public bool ReloadFromOriginal()
        {
            if (SaveType == SaveType.SaveNew) return false;
            MetaData.Clone(_Entity, _Original, true);
            if (onAfterLoad != null) onAfterLoad();
            return true;
        }

        /// <summary>
        /// Mengeset Entity Baru
        /// </summary>
        public void SetNew(bool CallSetDefault)
        {
            if (CallSetDefault) td.SetDefault(_Entity);
            _SaveType = SaveType.SaveNew;
            if (e != null) e._SaveType = SaveType.SaveNew;
        }

        /// <summary>
        /// Menyimpan Entity Aktif
        /// </summary>
        public void Save(bool CallRule, bool CallValidateError)
        {
            if (_SaveType == SaveType.SaveNew)
                DataPersistance.SaveNewEntity(_Entity, CallRule, CallValidateError);
            else
                DataPersistance.SaveUpdateEntity(_Original, _Entity, CallRule, CallValidateError);
        }
        /// <summary>
        /// Menyimpan Entity Aktif
        /// </summary>
        public void Save(bool CallValidateError)
        {
            if (_SaveType == SaveType.SaveNew)
                DataPersistance.SaveNewEntity(_Entity, true, CallValidateError);
            else
                DataPersistance.SaveUpdateEntity(_Original, _Entity, true, CallValidateError);
        }

        /// <summary>
        /// Menghapus Entity Aktif
        /// </summary>
        public int Delete(bool CallRule, bool CallValidateError)
        {
            if (SaveType != SaveType.SaveUpdate) return 0;
            return DataPersistance.DeleteEntity(_Original, CallRule, CallValidateError);
        }
        /// <summary>
        /// Menghapus Entity Aktif
        /// </summary>
        public int Delete(bool CallValidateError)
        {
            if (SaveType != SaveType.SaveUpdate) return 0;
            return DataPersistance.DeleteEntity(_Original, true, CallValidateError);
        }

        #region Sql Generator
        private string SqlMoveFirst()
        {
            //SELECT TOP 1 KeyField FROM View ORDER BY 
            //  OrdField,KeyField
            string OrdKeyField = IsKeyFieldEqualOrderField ?
                string.Empty : ",KeyField";
            return DataPersistance.GetSqlSelectTopN(
                strQuery, 1, _OrderFld.FieldName + OrdKeyField);
        }
        private string SqlMoveLast()
        {
            //SELECT TOP 1 KeyField FROM View ORDER BY 
            //   OrdField DESC,KeyField DESC
            string OrdKeyField = IsKeyFieldEqualOrderField ?
                string.Empty : ",KeyField DESC";
            return DataPersistance.GetSqlSelectTopN(
                strQuery, 1, string.Concat(_OrderFld.FieldName,
                " DESC", OrdKeyField));
        }

        private string SqlMovePrevious(string CurrField, string CurrKey)
        {
            //SELECT TOP 1 KeyField FROM View WHERE
            //   ordField=@CurrField AND KeyField<@CurrKey OR
            //   ordField<@CurrField ORDER BY OrdField DESC, KeyField DESC
            string OrdKeyField = IsKeyFieldEqualOrderField ?
                string.Empty : ",KeyField DESC";
            return DataPersistance.GetSqlSelectTopN(
                string.Concat(strQuery, " WHERE ", _OrderFld.FieldName,
                "=", CurrField, " AND KeyField<", CurrKey, " OR ",
                _OrderFld.FieldName, "<", CurrField), 1,
                string.Concat(_OrderFld.FieldName, " DESC", OrdKeyField));
        }
        private string SqlMoveNext(string CurrField, string CurrKey)
        {
            //SELECT TOP 1 KeyField FROM View WHERE
            //  (OrdField=@CurrField AND KeyField>@CurrKey) OR
            //   ordField>@CurrField ORDER BY OrdField, KeyField
            string OrdKeyField = IsKeyFieldEqualOrderField ?
                string.Empty : ",KeyField";
            return DataPersistance.GetSqlSelectTopN(
                string.Concat(strQuery, " WHERE ", _OrderFld.FieldName,
                "=", CurrField, " AND KeyField>", CurrKey, " OR ",
                _OrderFld.FieldName, ">", CurrField), 1,
                _OrderFld.FieldName + OrdKeyField);
        }

        private string SqlFindFirst(string FindData)
        {
            //SELECT TOP 1 KeyField FROM View WHERE 
            //  OrdField LIKE @FindData 
            //  ORDER BY OrdField, KeyField
            string OrdKeyField = IsKeyFieldEqualOrderField ?
                string.Empty : ",KeyField";
            switch (_OrderFld.DataType)
            {
                case DataType.Boolean:
                    return DataPersistance.GetSqlSelectTopN(
                        string.Concat(strQuery, " WHERE ",
                        _OrderFld.FieldName, "=", FindData), 1,
                        _OrderFld.FieldName + OrdKeyField);
                case DataType.Char:
                case DataType.VarChar:
                    return DataPersistance.GetSqlSelectTopN(
                        string.Concat(strQuery, " WHERE ",
                        _OrderFld.FieldName, " LIKE ", FindData), 1,
                        _OrderFld.FieldName + OrdKeyField);
                default:
                    return DataPersistance.GetSqlSelectTopN(
                        string.Concat(strQuery, " WHERE ",
                        _OrderFld.FieldName, ">=", FindData), 1,
                        _OrderFld.FieldName + OrdKeyField);
            }
        }
        private string SqlFindLast(string FindData)
        {
            //SELECT TOP 1 KeyField FROM View WHERE 
            //  OrdField LIKE @FindData ORDER BY 
            //  OrdField DESC, Key DESC
            string OrdKeyField = IsKeyFieldEqualOrderField ?
                string.Empty : ",KeyField DESC";
            switch (_OrderFld.DataType)
            {
                case DataType.Boolean:
                    return DataPersistance.GetSqlSelectTopN(
                        string.Concat(strQuery, " WHERE ",
                        _OrderFld.FieldName, "=", FindData), 1,
                        string.Concat(_OrderFld.FieldName, " DESC", OrdKeyField));
                case DataType.Char:
                case DataType.VarChar:
                    return DataPersistance.GetSqlSelectTopN(
                        string.Concat(strQuery, " WHERE ",
                        _OrderFld.FieldName, " LIKE ", FindData), 1,
                        string.Concat(_OrderFld.FieldName, " DESC", OrdKeyField));
                default:
                    return DataPersistance.GetSqlSelectTopN(
                        string.Concat(strQuery, " WHERE ",
                        _OrderFld.FieldName, "<=", FindData), 1,
                        string.Concat(_OrderFld.FieldName, " DESC", OrdKeyField));
            }
        }
        private string SqlFindCriteria()
        {
            //SELECT TOP 1 KeyField FROM View WHERE 
            //  OrdField LIKE @FindData ORDER BY 
            //  OrdField DESC, Key DESC
            string OrdKeyField = IsKeyFieldEqualOrderField ?
                string.Empty : ",KeyField DESC";
            return DataPersistance.GetSqlSelectTopN(
                strQuery.Insert(_CriteriaPos, " AND " +
                Criteria), 1, string.Concat(_OrderFld.FieldName,
                " DESC", OrdKeyField));
        }

        private string SqlReload()
        {
            //SELECT TOP 1 KeyField FROM View WHERE 
            //  KeyField=@CurrKey
            return DataPersistance.GetSqlSelectTopN(
                strQuery + " WHERE KeyField=@CurrKey", 1, string.Empty);
        }
        #endregion

        #region MoveData
        public bool MoveData(MoveType MovingType)
        {
            IDataReader rdr;

            #region Cek MovingType
            switch (MovingType)
            {
                case MoveType.MoveFirst:
                    rdr = DataPersistance.ExecuteReader(SqlMoveFirst());
                    break;
                case MoveType.MovePrevious:
                    if (!IsError)
                        rdr = DataPersistance.ExecuteReader(SqlMovePrevious(
                            DataPersistance.FormatSqlValue(_OrderFld.GetValue(_Original), _OrderFld.DataType),
                            DataPersistance.FormatSqlValue(_CurrentKeyValue, DataType.VarChar)));
                    else
                        rdr = DataPersistance.ExecuteReader(SqlMoveFirst());
                    break;
                case MoveType.MoveNext:
                    if (!IsError)
                        rdr = DataPersistance.ExecuteReader(SqlMoveNext(
                            DataPersistance.FormatSqlValue(_OrderFld.GetValue(_Original), _OrderFld.DataType),
                            DataPersistance.FormatSqlValue(_CurrentKeyValue, DataType.VarChar)));
                    else
                        rdr = DataPersistance.ExecuteReader(SqlMoveLast());
                    break;
                case MoveType.FindCriteria:
                    rdr = DataPersistance.ExecuteReader(SqlFindCriteria());
                    break;
                case MoveType.FindFirst:
                    rdr = DataPersistance.ExecuteReader(
                        SqlFindFirst(DataPersistance.FormatSqlValue(
                        FindValue, _OrderFld.DataType)));
                    break;
                case MoveType.FindLast:
                    rdr = DataPersistance.ExecuteReader(
                        SqlFindLast(DataPersistance.FormatSqlValue(
                        FindValue, _OrderFld.DataType)));
                    break;
                default:    // MoveLast
                    rdr = DataPersistance.ExecuteReader(
                            SqlMoveLast());
                    break;
            }
            #endregion

            if (rdr.Read())
            {
                _CurrentKeyValue = rdr.GetString(0);

                string Criteria = string.Concat(DataPersistance
                    .BuildKeyField(td), "=", DataPersistance
                    .FormatSqlValue(_CurrentKeyValue));

                DataPersistance.LoadEntity(_Entity, Criteria, true);
                MetaData.Clone(_Original, _Entity, true);
                if (e != null) e._Original = (ParentEntity)_Original;

                IsError = false;
                _SaveType = SaveType.SaveUpdate;
                if (e != null) e._SaveType = SaveType.SaveUpdate;
            }
            else
            {
                switch (MovingType)
                {
                    case MoveType.MovePrevious:
                        return MoveFirst();
                    case MoveType.MoveNext:
                        return MoveLast();
                }
                IsError = true;
            }
            if (onAfterLoad != null) onAfterLoad();
            if (onDataMoving != null) onDataMoving(MovingType, IsError);

            return !IsError;
        }
        /// <summary>
        /// Pergi Ke Data Pertama
        /// </summary>
        /// <returns></returns>
        public bool MoveFirst() { return MoveData(MoveType.MoveFirst); }
        /// <summary>
        /// Pergi Ke Data Berikutnya
        /// </summary>
        /// <returns></returns>
        public bool MoveNext() { return MoveData(MoveType.MoveNext); }
        /// <summary>
        /// Pergi Ke Data Sebelumnya
        /// </summary>
        /// <returns></returns>
        public bool MovePrevious() { return MoveData(MoveType.MovePrevious); }
        /// <summary>
        /// Pergi Ke Data Terakhir
        /// </summary>
        /// <returns></returns>
        public bool MoveLast() { return MoveData(MoveType.MoveLast); }
        /// <summary>
        /// Mencari Data dgn Kriteria Tertentu (Lihat: Criteria)
        /// </summary>
        /// <returns></returns>
        public bool FindCriteria() { return MoveData(MoveType.FindCriteria); }
        /// <summary>
        /// Mencari Data dengan Kondisi Tertentu
        /// </summary>
        /// <param name="NewCriteria"></param>
        /// <returns></returns>
        public bool FindCriteria(string NewCriteria)
        {
            Criteria = NewCriteria;
            return MoveData(MoveType.FindCriteria);
        }
        /// <summary>
        /// Mencari data dengan Parameter Tertentu
        /// </summary>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        public bool FindCriteria(params FieldParam[] Parameters)
        {
            string NewCriteria = string.Empty;
            foreach (FieldParam Param in Parameters)
                NewCriteria = string.Concat(NewCriteria, " AND ",
                    DataPersistance.FormatSqlObject(Param.FieldName), "=",
                    DataPersistance.FormatSqlValue(Param.Value));
            if (NewCriteria.Length > 0)
            {
                Criteria = NewCriteria.Substring(5);
                return MoveData(MoveType.FindCriteria);
            }
            else
                return false;
        }

        /// <summary>
        /// Mencari Data Pertama sesuai Urutan Data 
        /// (Lihat: OrderField, FindValue)
        /// </summary>
        /// <returns></returns>
        public bool FindFirst() { return MoveData(MoveType.FindFirst); }
        /// <summary>
        /// Mencari Data Terakhir sesuai Urutan Data
        /// (Lihat: OrderField, FindValue)
        /// </summary>
        /// <returns></returns>
        public bool FindLast() { return MoveData(MoveType.FindLast); }
        #endregion
    }

    public sealed class EntityNavigator : AbstractNavigator
    {
        public EntityNavigator(Type ObjType)
            : this(Activator.CreateInstance(ObjType)) { }
        public EntityNavigator(object Entity)
        {
            _Entity = Entity;
            e = _Entity as ParentEntity;
            _Original = Activator.CreateInstance(_Entity.GetType());
            if (e == null)
                _DataPersistance = BaseFramework.DefaultDataPersistance;
            else
                e._Original = (ParentEntity)_Original;
            DoInit();
        }
        public EntityNavigator(Type ObjType, DataPersistance DataPersistance)
            : this(Activator.CreateInstance(ObjType), DataPersistance) { }
        public EntityNavigator(object Entity, DataPersistance DataPersistance)
        {
            _Entity = Entity;
            e = _Entity as ParentEntity;
            _Original = Activator.CreateInstance(e.GetType());
            this.DataPersistance = DataPersistance;
            if (e != null) e._Original = (ParentEntity)_Original;
            DoInit();
        }

        /// <summary>
        /// Entity Aktif yang digunakan
        /// </summary>
        public object Entity { get { return _Entity; } }

        /// <summary>
        /// Entity Original dari database (mode Lihat/ Edit)
        /// </summary>
        object Original { get { return _Original; } }
    }

    public sealed class EntityNavigator<TEntity> : AbstractNavigator
        where TEntity : new()
    {
        public EntityNavigator()
            : this(new TEntity()) { }
        public EntityNavigator(TEntity Entity)
        {
            _Entity = Entity;
            _Original = new TEntity();
            e = _Entity as ParentEntity;
            if (e == null)
                _DataPersistance = BaseFramework.DefaultDataPersistance;
            else
                e._Original = (ParentEntity)_Original;
            DoInit();
        }
        public EntityNavigator(DataPersistance DataPersistance)
            : this(new TEntity(), DataPersistance) { }
        public EntityNavigator(TEntity Entity, DataPersistance DataPersistance)
        {
            _Entity = Entity;
            _Original = new TEntity();
            e = _Entity as ParentEntity;
            this.DataPersistance = DataPersistance;
            if (e != null) e._Original = (ParentEntity)_Original;
            DoInit();
        }

        /// <summary>
        /// Entity Aktif yang digunakan
        /// </summary>
        public TEntity Entity { get { return (TEntity)_Entity; } }

        /// <summary>
        /// Entity Original dari database (mode Lihat/ Edit)
        /// </summary>
        public TEntity Original { get { return (TEntity)_Original; } }
    }
}
