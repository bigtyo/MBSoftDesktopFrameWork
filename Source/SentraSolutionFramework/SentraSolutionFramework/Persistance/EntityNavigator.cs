using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SentraSolutionFramework.Entity;
using System.Diagnostics;
using SentraUtility;
using System.Reflection;

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
    
    //[DebuggerNonUserCode]
    public sealed class EntityNavigator
    {
        private ParentEntity _Entity;

        private bool IsError = false;
        private string _Filter;
        private string _strOrderAsc;
        private string _strOrderDesc;

        private FieldDef _OrderFld;
        private TableDef td;
        private string strQuery;
        private bool WhereExist;

        private FieldDef[] _CurrentKeyField;
        private FieldParam[] _ParamCurrentKeyField;

        private string _ModuleName;
        public string ModuleName
        {
            get { return _ModuleName; }
            set { _ModuleName = value; }
        }

        public object CurrentFieldValue
        {
            get
            {
                if (_Entity != null && _OrderFld != null)
                    return _OrderFld.GetValue(_Entity._Original);
                return null;
            }
        }
        private FieldDef OrderFieldDef { get { return _OrderFld; } }

        public DataType OrderFieldDataType
        {
            get { return _OrderFld.DataType; }
        }

        public TableDef TableDef { get { return td; } }

        public ParentEntity Entity { get { return _Entity; } }
        public ParentEntity Original { get { return _Entity._Original; } }

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
            get
            {
                return _OrderFld != null ? _OrderFld._FieldName : null;
            }
            set
            {
                _OrderFld = td.GetFieldDef(value);
                UpdateStrQuery();
            }
        }

        private bool OrderInKey;
        private void UpdateStrQuery()
        {
            if (_OrderFld == null) return;
            int i = 0;
            string tmpFilter;
            string TmpStr2;

            _strOrderAsc = string.Empty;
            _strOrderDesc = string.Empty;

            OrderInKey = false;
            foreach(FieldDef fld in td.KeyFields.Values)
                if (object.ReferenceEquals(fld, _OrderFld))
                {
                    OrderInKey = true;
                    break;
                }

            int numKey = OrderInKey ? 
                td.KeyFields.Count - 1 : td.KeyFields.Count;

            _CurrentKeyField = new FieldDef[numKey];
            _ParamCurrentKeyField = new FieldParam[numKey];

            foreach (FieldDef fld in td.KeyFields.Values)
            {
                if (!object.ReferenceEquals(fld, _OrderFld))
                {
                    _CurrentKeyField[i] = fld;
                    _ParamCurrentKeyField[i++] = new FieldParam(fld);
                    _strOrderAsc = string.Concat(_strOrderAsc, ",", 
                        fld._FieldName);
                    _strOrderDesc = string.Concat(_strOrderDesc, ",",
                        fld._FieldName, " DESC");
                }
            }
            if (_Filter.Length > 0)
            {
                tmpFilter = string.Concat(
                    " WHERE (", _Filter, ")");
                WhereExist = true;
            }
            else
            {
                tmpFilter = string.Empty;
                WhereExist = false;
            }
            TmpStr2 = ((IRuleInitUI)_Entity).GetBrowseFilter();
            if (TmpStr2.Length > 0)
            {
                if (WhereExist)
                    tmpFilter = string.Concat(tmpFilter, " AND (", TmpStr2, ")");
                else
                {
                    WhereExist = true;
                    tmpFilter = string.Concat(" WHERE (", TmpStr2, ")");
                }
            }
            else
            {
                string SqlSelect, SqlCondition, SqlOrderBy;

                ((IRuleInitUI)_Entity).GetBrowseSql(out SqlSelect, 
                    out SqlCondition, out SqlOrderBy);
                if (SqlSelect.Length > 0)
                {
                    if (SqlCondition.Length == 0)
                        strQuery = string.Concat("SELECT ",
                            _OrderFld._FieldName,
                            _strOrderAsc, " FROM (",
                            SqlSelect, ") AS X", tmpFilter);
                    else
                    {
                        tmpFilter = string.Concat(" WHERE (", SqlCondition, ")");
                        WhereExist = true;
                        strQuery = string.Concat("SELECT ",
                            _OrderFld._FieldName,
                            _strOrderAsc, " FROM (",
                            SqlSelect, ") AS X", tmpFilter);
                    }
                    return;
                }
            }
            if (_OrderFld._dtlsa == null)
                strQuery = string.Concat("SELECT ", 
                    _OrderFld._FieldName, 
                    _strOrderAsc, " FROM ",
                    td._TableName, tmpFilter);
            else
                strQuery = string.Concat("SELECT ", 
                    _OrderFld._FieldName, _strOrderAsc,
                    " FROM (SELECT (", _Entity.Dp
                    .GetSqlCoalesceNoFormat(
                    string.Concat("(", _OrderFld._dtlsa.GetSqlQuery(), ")"), 
                    _Entity.Dp
                    .FormatSqlValue(_OrderFld.GetDataTypeDefault())), 
                    ") AS " , _OrderFld._FieldName, 
                    _strOrderAsc, " FROM ",
                    td._TableName, tmpFilter, ") AS X");
        }

        /// <summary>
        /// Nilai yg dicari oleh FindCriteria
        /// </summary>
        public string Criteria;
        public FieldParam[] CriteriaParams;

        /// <summary>
        /// Nilai yg dicari oleh FindFirst/ FindLast
        /// </summary>
        public object FindValue;

        public DataPersistance Dp
        {
            get
            {
                return _Entity == null ? null : _Entity.Dp;
            }
            set
            {
                if (_Entity != null) _Entity.Dp = value;
            }
        }

        private void DoInit(bool CallSetDefault)
        {
            _Filter = string.Empty;

            _Entity.OnEntityAction += new EntityAction(_Entity_OnEntityAction);
            IsError = true;

            td = MetaData.GetTableDef(_Entity.GetType());
            Dp.ValidateTableDef(td);
            _Entity.FormMode = FormMode.FormAddNew;
            if (CallSetDefault) td.SetDefault(_Entity);
            foreach (FieldDef fld in td.KeyFields.Values)
            {
                _OrderFld = fld;
                break;
            }
        }

        void _Entity_OnEntityAction(BaseEntity ActionEntity, enEntityActionMode ActionMode)
        {
            if (onEntityAction != null) onEntityAction(ActionEntity, ActionMode);
        }

        public event DataMoving onDataMoving;
        public event EntityAction onEntityAction;

        /// <summary>
        /// Reload dari Database Entity sesuai Key Asal yang dimasukkan
        /// </summary>
        public bool Reload()
        {
            if (_Entity.FormMode == FormMode.FormAddNew) return false;

            int i = 0;

            CriteriaParams = new FieldParam[td.KeyFields.Count];
            Criteria = string.Empty;
            foreach (FieldDef fld in td.KeyFields.Values)
            {
                Criteria = string.Concat(Criteria,
                    " AND ", fld._FieldName, "=@", fld._FieldName);
                CriteriaParams[i++] = new FieldParam(fld,
                    _Entity._Original);
            }

            Criteria = Criteria.Substring(5);
            return MoveData(MoveType.FindCriteria);
        }

        /// <summary>
        /// Mengeset Entity Baru
        /// </summary>
        public void SetNew(bool CallSetDefault)
        {
            _Entity.FormMode = FormMode.FormAddNew;
            if (CallSetDefault) td.SetDefault(_Entity);
        }

        /// <summary>
        /// Menyimpan Entity Aktif
        /// </summary>
        public int Save(bool CallRule, bool CallValidateError)
        {
            return _Entity.Save(Dp, CallRule, CallValidateError);
        }

        /// <summary>
        /// Menyimpan Entity Aktif
        /// </summary>
        public int Save(bool CallValidateError)
        {
            return _Entity.Save(true, CallValidateError);
        }

        /// <summary>
        /// Menghapus Entity Aktif
        /// </summary>
        public int Delete(bool CallRule)
        {
            if (_Entity.FormMode == FormMode.FormAddNew) return 0;
            return _Entity.SaveDelete(Dp, CallRule);
        }

        #region Sql Generator
        private string GetOderFieldName()
        {
            return _OrderFld._FieldName;
        }

        private string SqlMoveFirst()
        {
            //SELECT TOP 1 KeyField FROM View ORDER BY 
            //  OrdField,KeyField
            return Dp.GetSqlSelectTopN(
                strQuery, 1, GetOderFieldName() + _strOrderAsc);
        }
        private string SqlMoveLast()
        {
            //SELECT TOP 1 KeyField FROM View ORDER BY 
            //   OrdField DESC,KeyField DESC
            return Dp.GetSqlSelectTopN(
                strQuery, 1, string.Concat(
                GetOderFieldName(), " DESC", _strOrderDesc));
        }

        private FieldParam[] ParamMovePrevNext;

        /// <summary>
        /// Digunakan untuk MovePrev dan MoveNext
        /// </summary>
        /// <param name="Op"></param>
        /// <returns></returns>
        private string GenKeyCondition(string Op)
        {
            string strKey;

            FieldDef PrevField;

            if (_CurrentKeyField.Length == 0)
                PrevField = _OrderFld;
            else
                PrevField = _CurrentKeyField[0];

            strKey = string.Concat(PrevField._FieldName,
                Op, "@XCurrKeyX");

            ParamMovePrevNext = (FieldParam[])_ParamCurrentKeyField.Clone();
            int NumKeyField = ParamMovePrevNext.Length;
            Array.Resize<FieldParam>(ref ParamMovePrevNext,
                NumKeyField + 2);
            ParamMovePrevNext[NumKeyField] =
                new FieldParam("XCurrKeyX", PrevField,
                PrevField.GetValue(_Entity._Original));

            string strTemp = string.Empty;

            if (_CurrentKeyField.Length > 0)
            {
                ParamMovePrevNext[0].Value = _CurrentKeyField[0]
                    .GetValue(_Entity._Original);
                for (int i = 1; i < _CurrentKeyField.Length; i++)
                {
                    strTemp = string.Concat(strTemp,
                        PrevField._FieldName, "=@",
                        PrevField._FieldName, " AND ");

                    FieldDef TmpFld = _CurrentKeyField[i];

                    ParamMovePrevNext[i].Value = TmpFld
                        .GetValue(_Entity._Original);

                    strKey = string.Concat(strKey, " OR ", strTemp,
                        TmpFld._FieldName,
                        Op, "@", TmpFld._FieldName);
                    PrevField = TmpFld;
                }
            }

            strKey = string.Concat("(", strKey, ")");

            return strKey;
        }

        private string SqlMovePrevious()
        {
            //SELECT TOP 1 KeyField FROM View WHERE
            //   ordField=@CurrField AND KeyField<@CurrKey OR
            //   ordField<@CurrField ORDER BY OrdField DESC, KeyField DESC
            string strTemp = WhereExist ? " AND (" : " WHERE (";
            string strOrderFld = GetOderFieldName();

            string RetVal = Dp.GetSqlSelectTopN(
                string.Concat(strQuery, strTemp,
                strOrderFld, "=@XCurrFieldX AND ", GenKeyCondition("<"),
                " OR ", strOrderFld, "<@XCurrFieldX)"), 1,
                string.Concat(strOrderFld, " DESC",
                _strOrderDesc));

            ParamMovePrevNext[ParamMovePrevNext.Length - 1] =
                new FieldParam("XCurrFieldX", _OrderFld,
                _OrderFld.GetValue(_Entity._Original));

            return RetVal;
        }

        private string SqlMoveNext()
        {
            //SELECT TOP 1 KeyField FROM View WHERE
            //  (OrdField=@CurrField AND KeyField>@CurrKey) OR
            //   ordField>@CurrField ORDER BY OrdField, KeyField
            string strTemp = WhereExist ? " AND (" : " WHERE (";
            string strOrderFld = GetOderFieldName();

            string RetVal = Dp.GetSqlSelectTopN(
                string.Concat(strQuery, strTemp, 
                strOrderFld,
                "=@XCurrFieldX  AND ", GenKeyCondition(">"),
                " OR ", strOrderFld, ">@XCurrFieldX)"), 1,
                strOrderFld + _strOrderAsc);
            ParamMovePrevNext[ParamMovePrevNext.Length - 1] =
                new FieldParam("XCurrFieldX", _OrderFld,
                _OrderFld.GetValue(_Entity._Original));

            return RetVal;
        }

        private FieldParam ParamFind;

        private string SqlFindFirst()
        {
            string strTemp = WhereExist ? " AND " : " WHERE ";
            ParamFind = new FieldParam(_OrderFld, FindValue);
            string strOrderFld = GetOderFieldName();

            //SELECT TOP 1 KeyField FROM View WHERE 
            //  OrdField LIKE @FindData 
            //  ORDER BY OrdField, KeyField
            switch (_OrderFld.DataType)
            {
                case DataType.Boolean:
                    return Dp.GetSqlSelectTopN(
                        string.Concat(strQuery, strTemp,
                        strOrderFld, "=@", strOrderFld), 1,
                        strOrderFld + _strOrderAsc);
                case DataType.Char:
                case DataType.VarChar:
                    return Dp.GetSqlSelectTopN(
                        string.Concat(strQuery, strTemp,
                        strOrderFld, " LIKE @",
                        strOrderFld), 1,
                        strOrderFld + _strOrderAsc);
                default:
                    return Dp.GetSqlSelectTopN(
                        string.Concat(strQuery, strTemp,
                        strOrderFld, ">=@",
                        strOrderFld), 1,
                        strOrderFld + _strOrderAsc);
            }
        }
        private string SqlFindLast()
        {
            string strTemp = WhereExist ? " AND " : " WHERE ";
            ParamFind = new FieldParam(_OrderFld, FindValue);

            string strOrderFld = GetOderFieldName();
            
            //SELECT TOP 1 KeyField FROM View WHERE 
            //  OrdField LIKE @FindData ORDER BY 
            //  OrdField DESC, Key DESC
            switch (_OrderFld.DataType)
            {
                case DataType.Boolean:
                    return Dp.GetSqlSelectTopN(
                        string.Concat(strQuery, strTemp,
                        strOrderFld,
                        "=@", strOrderFld), 1, string.Concat(
                        strOrderFld, " DESC", _strOrderDesc));
                case DataType.Char:
                case DataType.VarChar:
                    return Dp.GetSqlSelectTopN(
                        string.Concat(strQuery, strTemp,
                        strOrderFld, " LIKE @",
                        strOrderFld), 1,
                        string.Concat(strOrderFld, 
                        " DESC", _strOrderDesc));
                default:
                    return Dp.GetSqlSelectTopN(
                        string.Concat(strQuery, strTemp,
                        strOrderFld, "<=@",
                        strOrderFld), 1,
                        string.Concat(strOrderFld, 
                        " DESC", _strOrderDesc));
            }
        }
        private string SqlFindCriteria()
        {
            string strTemp = WhereExist ? " AND " : " WHERE ";

            //SELECT TOP 1 KeyField FROM View WHERE 
            //  OrdField LIKE @FindData ORDER BY 
            //  OrdField DESC, Key DESC
            return Dp.GetSqlSelectTopN(string.Concat(
                strQuery, strTemp, Criteria), 1, string.Concat(
                GetOderFieldName(), " DESC", _strOrderDesc));
        }
        #endregion

        #region MoveData
        public bool MoveData(MoveType MovingType)
        {
            if (_OrderFld == null) return false;

            IDataReader rdr;

            #region Cek MovingType
            switch (MovingType)
            {
                case MoveType.MoveFirst:
                    rdr = Dp.ExecuteReader(
                        SqlMoveFirst());
                    break;
                case MoveType.MovePrevious:
                    if (!IsError)
                        rdr = Dp.ExecuteReader(
                            SqlMovePrevious(), ParamMovePrevNext);
                    else
                        rdr = Dp.ExecuteReader(
                            SqlMoveFirst());
                    break;
                case MoveType.MoveNext:
                    if (!IsError)
                        rdr = Dp.ExecuteReader(
                            SqlMoveNext(), ParamMovePrevNext);
                    else
                        rdr = Dp.ExecuteReader(
                            SqlMoveLast());
                    break;
                case MoveType.FindCriteria:
                    rdr = Dp.ExecuteReader(
                        SqlFindCriteria(), CriteriaParams);
                    break;
                case MoveType.FindFirst:
                    rdr = Dp.ExecuteReader(
                        SqlFindFirst(), ParamFind);
                    break;
                case MoveType.FindLast:
                    rdr = Dp.ExecuteReader(
                        SqlFindLast(), ParamFind);
                    break;
                default:    // MoveLast
                    rdr = Dp.ExecuteReader(
                            SqlMoveLast());
                    break;
            }
            #endregion
            try
            {
                if (rdr.Read())
                {
                    string strKeyWhere = string.Empty;
                    FieldParam[] LoadParam = (FieldParam[])
                        _ParamCurrentKeyField.Clone();

                    for (int i = 0; i < _CurrentKeyField.Length; i++)
                    {
                        strKeyWhere = string.Concat(strKeyWhere, " AND ",
                            _CurrentKeyField[i]._FieldName, "=@",
                            _CurrentKeyField[i]._FieldName);
                        LoadParam[i].Value = rdr.GetValue(i + 1);
                    }

                    if (OrderInKey)
                    {
                        int SizeLoadParam = LoadParam.Length;
                        Array.Resize<FieldParam>(ref LoadParam,
                            SizeLoadParam + 1);
                        LoadParam[SizeLoadParam] = new FieldParam(
                            _OrderFld, rdr.GetValue(0));

                        strKeyWhere = string.Concat(
                            _OrderFld._FieldName, "=@",
                            _OrderFld._FieldName, strKeyWhere);
                    }
                    else
                        strKeyWhere = strKeyWhere.Substring(5);

                    _Entity.LoadEntity(strKeyWhere, true, LoadParam);

                    IsError = false;
                    if (_Entity.FormMode != FormMode.FormEdit &&
                        _Entity.FormMode != FormMode.FormView)
                        _Entity.FormMode = FormMode.FormEdit;
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
                    _Entity.FormMode = FormMode.FormError;
                }
                //if (!IsError && onAfterLoad != null) onAfterLoad();
                if (onDataMoving != null) 
                    onDataMoving(MovingType, IsError);
                return !IsError;
            }
            finally
            {
                if (rdr != null) rdr.Close();
            }
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
        /// Mencari Data dengan Kondisi Tertentu
        /// </summary>
        /// <param name="NewCriteria"></param>
        /// <returns></returns>
        public bool FindCriteria(string NewCriteria, 
            params FieldParam[] Parameters)
        {
            Criteria = NewCriteria;
            CriteriaParams = Parameters;
            return MoveData(MoveType.FindCriteria);
        }

        /// <summary>
        /// Find Key, Format: Key1,Key2,...
        /// </summary>
        /// <param name="NewKey"></param>
        /// <returns></returns>
        public bool FindKey(string NewKey)
        {
            string[] Keys = NewKey.Split(',');
            Criteria = string.Empty;
            CriteriaParams = new FieldParam[td.KeyFields.Count];
            int i = 0;
            foreach (FieldDef fld in td.KeyFields.Values)
            {
                Criteria = string.Concat(Criteria, " AND ",
                    fld._FieldName, "=@", fld._FieldName);
                CriteriaParams[i] = new FieldParam(fld, Keys[i]);
                i++;
            }
            Criteria = Criteria.Substring(5);
            return MoveData(MoveType.FindCriteria);
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

        public EntityNavigator(Type ObjType, bool CallSetDefault)
            : this((ParentEntity)BaseFactory.CreateInstance(ObjType), 
            CallSetDefault) { }

        public EntityNavigator(ParentEntity Entity, bool CallSetDefault)
        {
            _Entity = Entity;
            _Entity.CurrentNavigator = this;
            _Entity._Original = (ParentEntity)MetaData.Clone(_Entity); 
            DoInit(CallSetDefault);
        }

        public EntityNavigator(IBaseUINavigator BaseUINavigator,
            ParentEntity Entity, bool CallSetDefault)
        {
            _Entity = Entity;
            _Entity.CurrentNavigator = this;
            _Entity.BaseUINavigator = BaseUINavigator;
            _Entity._Original = (ParentEntity)MetaData.Clone(_Entity);
            DoInit(CallSetDefault);
        }
        
        public EntityNavigator(Type ObjType, DataPersistance Dp, 
            bool CallSetDefault) : this((ParentEntity)BaseFactory
            .CreateInstance(ObjType), Dp, CallSetDefault) { }
        public EntityNavigator(ParentEntity Entity, 
            DataPersistance Dp, bool CallSetDefault)
        {
            _Entity = Entity;
            _Entity.CurrentNavigator = this;
            _Entity._Original = (ParentEntity)MetaData.Clone(_Entity); 
            _Entity.Dp = Dp;
            DoInit(CallSetDefault);
        }
    }
}
