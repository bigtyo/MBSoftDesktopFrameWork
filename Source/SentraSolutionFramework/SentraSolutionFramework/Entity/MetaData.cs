using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using SentraSolutionFramework.Persistance;
using System.Collections;
using System.Diagnostics;
using SentraUtility;

namespace SentraSolutionFramework.Entity
{
    //[DebuggerNonUserCode]
    public static class MetaData
    {
        #region TypeCache
        private static string TChildName = typeof(ChildEntity<>).Name;
        private static string TColName = typeof(EntityCollection<>).Name;
        #endregion

        private static Dictionary<Type, TableDef> TableDefs =
            new Dictionary<Type, TableDef>();

        private static Dictionary<string, TableDef> NameTableDefs =
            new Dictionary<string, TableDef>();

        internal static Dictionary<Type, Type[]> ViewDependents =
            new Dictionary<Type, Type[]>();

        public static void ClearPersistanceFlag(DataPersistance dp)
        {
            foreach (TableDef td in TableDefs.Values)
                td.SetIsExist(dp, false);
        }

        private static void UpdateTableDef(TableDef td, MemberInfo mi)
        {
            FieldDef fld = null;
            bool IsCounterField = false;

            #region Query Primary Key, AutoNumber, AutoNested
            bool isPk;
            PrimaryKeyAttribute[] pkas = (PrimaryKeyAttribute[])
                mi.GetCustomAttributes(typeof(PrimaryKeyAttribute), true);
            int FieldLen = 0;
            if (pkas.Length > 0)
            {
                isPk = true;
                AutoNumberKeyAttribute aka = pkas[0] as AutoNumberKeyAttribute;
                if (aka != null)
                {
                    aka._FieldName = mi.Name;
                    if (td._AutoNumberKeyAtr == null)
                        td._AutoNumberKeyAtr = new List<AutoNumberKeyAttribute>();
                    td._AutoNumberKeyAtr.Add(aka);
                    FieldLen = aka._Template.Length;
                }
                else
                {
                    AutoNestedKeyAttribute ank = pkas[0] as AutoNestedKeyAttribute;
                    if (ank != null)
                    {
                        ank._FieldName = mi.Name;
                        if (td._AutoNestedKeyAtr != null)
                            throw new ApplicationException(string.Format(
                                ErrorMetaData.MultiNestedKey, mi.Name));
                        td._AutoNestedKeyAtr = ank;
                    }
                    else
                    {
                        CounterKeyAttribute cka = pkas[0] as CounterKeyAttribute;
                        if (cka != null)
                        {
                            if (td._fldCounterField != null)
                                throw new ApplicationException(string.Format(
                                    ErrorMetaData.MultiCounter, mi.Name));
                            IsCounterField = true;
                        }
                    }
                }
            }
            else isPk = false;

            AutoNumberAttribute[] anas = (AutoNumberAttribute[])
                mi.GetCustomAttributes(typeof(AutoNumberAttribute), true);
            if (anas.Length > 0)
            {
                anas[0]._FieldName = mi.Name;
                if (td._AutoNumberAtr == null)
                    td._AutoNumberAtr = new List<AutoNumberAttribute>();
                td._AutoNumberAtr.Add(anas[0]);
                FieldLen = anas[0]._Template.Length;
            }
            #endregion

            DataTypeLoadSqlAttribute[] dtls = (DataTypeLoadSqlAttribute[])
                mi.GetCustomAttributes(typeof(DataTypeLoadSqlAttribute), true);
            if (dtls.Length > 0)
            {
                #region Query DataTypeLoadSql
                if (isPk)
                    throw new ApplicationException(string.Format(
                        ErrorMetaData.DataTypeLoadSqlPk, mi.Name));

                if (dtls[0].GetSqlQueryLen() == 0)
                {
                    TableDef tdP;

                    if (dtls[0]._ParentType != td._ClassType)
                        tdP = MetaData.GetTableDef(dtls[0]._ParentType);
                    else
                        tdP = td;
                    if (dtls[0].ParentFieldName.Length == 0)
                        dtls[0].ParentFieldName = mi.Name;

                    FieldDef fldP = tdP.GetFieldDef(dtls[0].ParentFieldName);
                    if (fldP == null && !object.ReferenceEquals(tdP, td))
                        throw new ApplicationException(string.Format(
                            ErrorMetaData.DataTypeLoadSqlParentNotFound,
                            mi.Name, td.TableName));
                    fld = new FieldDef(mi, td._TableName, fldP, dtls[0]);
                }
                else
                    fld = new FieldDef(mi, td._TableName, null, dtls[0]);
                td.NonKeyFields.Add(mi.Name, fld);
                #endregion
            }
            else
            {
                #region Query DataType
                DataTypeAttribute[] dtas = (DataTypeAttribute[])
                    mi.GetCustomAttributes(typeof(DataTypeAttribute), true);
                if (dtas.Length == 1)
                {
                    if (isPk)
                    {
                        fld = new FieldDef(mi, dtas[0], true);
                        td.KeyFields.Add(mi.Name, fld);
                    }
                    else
                    {
                        fld = new FieldDef(mi, dtas[0], false);
                        td.NonKeyFields.Add(mi.Name, fld);
                    }
                }
                else if (dtas.Length > 1)
                    throw new ApplicationException(string.Format(
                        ErrorMetaData.MultiDataType, mi.Name));
                else if (isPk)
                {
                    // If Auto DataTypeAttribute :
                    if (IsCounterField)
                    {
                        fld = new FieldDef(mi,
                            new DataTypeIntegerAttribute(), true);
                        td._fldCounterField = fld;
                    }
                    else if (td._AutoNestedKeyAtr != null &&
                        td._AutoNestedKeyAtr._FieldName == mi.Name)
                        fld = new FieldDef(mi,
                            new DataTypeVarCharAttribute(0), true);
                    else if (mi.GetCustomAttributes(typeof(TransactionDateAttribute), true).Length > 0)
                    {
                        fld = new FieldDef(mi,
                              new DataTypeDateAttribute(), true);
                        td.KeyFields.Add(mi.Name, fld);
                        td.fldTransactionDate = fld;
                        return;
                    }
                    else if (FieldLen > 0)
                        fld = new FieldDef(mi,
                            new DataTypeVarCharAttribute(FieldLen), true);
                    else
                        throw new ApplicationException(string.Format(
                            ErrorMetaData.DataTypeNotFound, mi.Name));
                    td.KeyFields.Add(mi.Name, fld);
                }
                else if (mi.GetCustomAttributes(typeof(TransactionDateAttribute), true).Length > 0)
                {
                    fld = new FieldDef(mi,
                        new DataTypeDateAttribute(), false);
                    td.NonKeyFields.Add(mi.Name, fld);
                }
                else if (FieldLen > 0)
                {
                    fld = new FieldDef(mi,
                        new DataTypeVarCharAttribute(FieldLen), false);
                    td.NonKeyFields.Add(mi.Name, fld);
                }
                #endregion
            }

            if (fld != null)
            {
                #region Query EmptyError
                EmptyErrorAttribute[] eeas = (EmptyErrorAttribute[])
                    mi.GetCustomAttributes(typeof(EmptyErrorAttribute), true);
                if (eeas.Length > 0)
                {
                    fld.EmptyErrorAtr = eeas[0];
                    if (fld.EmptyErrorAtr.ErrorMessage.Length == 0)
                        fld.EmptyErrorAtr.ErrorMessage = string.Concat("'",
                            BaseUtility.SplitName(fld._FieldName), "' tidak boleh kosong");
                }
                #endregion

                DescriptionAttribute[] dsa = (DescriptionAttribute[])
                    mi.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (dsa.Length > 0)
                {
                    fld.Description = dsa[0].Description;
                }

                PrintCounterAttribute[] pcas = (PrintCounterAttribute[])
                    mi.GetCustomAttributes(typeof(PrintCounterAttribute), true);
                if (pcas.Length > 0)
                    td.fldPrintCounter = fld;

                if (mi.GetCustomAttributes(typeof(TransactionDateAttribute),
                    true).Length > 0) td.fldTransactionDate = fld;

                fld.CheckFieldType(td._TableName);
            }
        }

        private static void CheckAutoNumber(TableDef td, Type EntityType,
            ICollection Colls)
        {
            if (!BaseUtility.IsDebugMode) return;
            if (Colls != null)
                foreach (IAutoNumberAttribute anka in Colls)
                {
                    string DateField = anka.GetDateField();
                    if (DateField.Length > 0)
                    {
                        PropertyInfo pi = EntityType.GetProperty(DateField, 
                            BindingFlags.NonPublic | BindingFlags.Public |
                            BindingFlags.Instance);
                        if (pi == null)
                        {
                            FieldInfo fi = EntityType.GetField(DateField, 
                                BindingFlags.NonPublic | BindingFlags.Public |
                                BindingFlags.Instance);
                            if (fi == null)
                                throw new ApplicationException(string.Format(
                                    ErrorMetaData.DateFieldNotFound, DateField));
                            if (fi.FieldType != typeof(DateTime))
                                throw new ApplicationException(string.Format(
                                    ErrorMetaData.DateFieldError, DateField));
                        }
                        else if (pi.PropertyType != typeof(DateTime))
                            throw new ApplicationException(string.Format(
                                ErrorMetaData.DateFieldError, DateField));
                    }
                    if (anka.GetCounterText().Length == 0 ||
                        anka.GetTemplate().IndexOf(
                        anka.GetCounterText()) < 0)
                        throw new ApplicationException(string.Format(
                            ErrorMetaData.CounterTextNotFound,
                            td._TableName, anka.GetTemplate()));
                }
        }

        private static void CheckIndexes(TableDef td, IndexAttribute[] ias)
        {
            if (!BaseUtility.IsDebugMode) return;
            foreach (IndexAttribute ia in ias)
            {
                string[] Index = ia._IndexedFields.Split(',');
                foreach (string idx in Index)
                {
                    string TmpIdx = idx.Trim();
                    int i = TmpIdx.IndexOf(' ');
                    if (i > 0) TmpIdx = TmpIdx.Substring(0, i);
                    if (!td.KeyFields.ContainsKey(TmpIdx) &&
                        !td.NonKeyFields.ContainsKey(TmpIdx))
                        throw new ApplicationException(string.Format(
                            ErrorMetaData.IndexFieldNotFound, TmpIdx));
                }
            }
        }

        private static void CheckChildRelation(RelationAttribute ra, TableDef tdParent, TableDef td)
        {
            if (!BaseUtility.IsDebugMode) return;
            if (ra._ParentFields.Length != ra._ChildFields.Length)
                throw new ApplicationException(string.Format(
                    ErrorMetaData.RelationFieldCount,
                    ra._ParentType.Name, ra.ChildType.Name));
            if (ra._ParentFields.Length != tdParent.KeyFields.Count)
                throw new ApplicationException(string.Format(
                    ErrorMetaData.RelationParentField,
                    ra._ParentType.Name, ra.ChildType.Name));
            for (int i = 0; i < ra._ParentFields.Length; i++)
            {
                FieldDef fldP;
                if (!tdParent.KeyFields.TryGetValue(
                    ra._ParentFields[i], out fldP))
                    throw new ApplicationException(string.Format(
                        ErrorMetaData.RelParentFieldNotFound,
                        ra._ParentType.Name, ra.ChildType.Name,
                        ra._ParentFields[i]));

                FieldDef fldC;
                if (!td.KeyFields.TryGetValue(ra._ChildFields[i],
                    out fldC))
                    if (!td.NonKeyFields.TryGetValue(ra._ChildFields[i],
                        out fldC))
                        throw new ApplicationException(string.Format(
                            ErrorMetaData.RelChildFieldNotFound,
                            ra._ParentType.Name, ra.ChildType.Name,
                            ra._ChildFields[i]));
                if (fldP.DataType != fldC.DataType ||
                    fldP.Length < fldC.Length)
                    throw new ApplicationException(string.Format(
                        ErrorMetaData.RelationFieldTypeNotMatch,
                        ra._ParentType.Name, ra.ChildType.Name,
                        ra._ParentFields[i], ra._ChildFields[i]));
            }
        }

        private static void CheckFields(TableDef td, string[] FKFields, int FKCount)
        {
            if (!BaseUtility.IsDebugMode) return;
            foreach (FieldDef fd in td.KeyFields.Values)
                if (fd.DataType == DataType.TimeStamp)
                    throw new ApplicationException(string.Format(
                        ErrorMetaData.KeyTimeStamp, fd._FieldName));
            if (FKFields != null)
                foreach (string FKField in FKFields)
                    if (td.NonKeyFields.ContainsKey(FKField))
                        throw new ApplicationException(string.Format(
                            ErrorMetaData.MultiFieldName, FKField));

            if (td._AutoNestedKeyAtr != null)
                if (!td.NonKeyFields.ContainsKey(td._AutoNestedKeyAtr._ParentField))
                    throw new ApplicationException(string.Format(
                        ErrorMetaData.ParentFieldNotFound,
                        td._AutoNestedKeyAtr._ParentField));

            if (FKCount == 0)   // Not child
            {
                if (td._fldCounterField != null)
                    throw new ApplicationException(string.Format(
                        ErrorMetaData.CounterFieldOnParent,
                        td._fldCounterField._FieldName));
            }
            else
            {
                if (td._fldCounterField == null && td.KeyFields.Count != FKCount)
                    throw new ApplicationException(
                        ErrorMetaData.CounterFieldNotExist);
                if (td._AutoNestedKeyAtr != null)
                    throw new ApplicationException(string.Format(
                        ErrorMetaData.AutoNestedFieldOnChild,
                        td._AutoNestedKeyAtr._FieldName));
            }
        }

        private static TableDef BuildEntityMetaData(Type EntityType)
        {
            if (EntityType.IsAbstract) return null;

            TableDef td;

            TableDefs.TryGetValue(EntityType, out td);
            if (td != null) return td;

            ViewEntityAttribute[] dvas = (ViewEntityAttribute[])EntityType.GetCustomAttributes(
                typeof(ViewEntityAttribute), true);
            bool dvaExist = dvas.Length > 0;

            if (dvaExist)
            {
                td = new TableDef(dvas[0], EntityType);
                Type[] tds = dvas[0].TypeDependends;
                if (tds != null && tds.Length > 0)
                    ViewDependents.Add(EntityType, tds);
            }
            else
                td = new TableDef(EntityType);
            
            string[] FKFields = null;

            #region Check Inheritance
            if (!EntityType.IsSubclassOf(typeof(ParentEntity)))
            {
                Type BaseType = EntityType.BaseType;

                while (BaseType != null && BaseType.Name != TChildName)
                    BaseType = BaseType.BaseType;
                if (BaseType != null)
                {
                    td._ParentClassType = BaseType.GetGenericArguments()[0];
                    TableDef entParent = GetTableDef(td._ParentClassType);
                    FKFields = new string[entParent.KeyFields.Count];
                    int i = 0;
                    foreach (FieldDef fd in entParent.KeyFields.Values)
                    {
                        td.KeyFields.Add(fd._FieldName, new FieldDef(fd));
                        FKFields[i++] = fd._FieldName;
                    }
                }
            }
            #endregion

            int FKCount = td.KeyFields.Count;

            #region Read TableName
            TableNameAttribute[] tnas = (TableNameAttribute[])EntityType.GetCustomAttributes(
                typeof(TableNameAttribute), true);
            if (tnas.Length > 0)
                td.TableName = tnas[0]._TableName;
            else
                td.TableName = EntityType.Name;
            #endregion

            #region Read Fields/ Properties
            foreach(MemberInfo mi in EntityType.FindMembers(
                MemberTypes.Field | MemberTypes.Property, 
                BindingFlags.NonPublic | BindingFlags.Public |
                BindingFlags.Instance, null, null))
                if (mi.MemberType == MemberTypes.Property)
                {
                    if (((PropertyInfo)mi).PropertyType.Name == TColName)
                        td.ChildEntities.Add(new EntityCollDef(mi));
                    else
                        UpdateTableDef(td, mi);
                }
                else
                {
                    if (((FieldInfo)mi).FieldType.Name == TColName)
                        td.ChildEntities.Add(new EntityCollDef(mi));
                    else
                        UpdateTableDef(td, mi);
                }
            #endregion

            if (!dvaExist)
            {
                if (FKCount == td.KeyFields.Count && (FKCount > 0 ||
                    td.NonKeyFields.Count > 0))
                {
                    NoKeyEntityAttribute[] nkeas = (NoKeyEntityAttribute[])EntityType.GetCustomAttributes(
                        typeof(NoKeyEntityAttribute), true);
                    if (nkeas.Length == 0)
                        throw new ApplicationException(ErrorMetaData.KeyNotFound);
                }
                if (td._AutoNestedKeyAtr != null)
                    td.GetFieldDef(td._AutoNestedKeyAtr._FieldName)
                        ._DataTypeAtr._Length = td.GetFieldDef(
                        td._AutoNestedKeyAtr._ParentField)._DataTypeAtr._Length;

                CheckAutoNumber(td, EntityType, td._AutoNumberKeyAtr);
                CheckAutoNumber(td, EntityType, td._AutoNumberAtr);

                #region Read Indexes
                IndexAttribute[] ias = (IndexAttribute[])EntityType.GetCustomAttributes(
                    typeof(IndexAttribute), true);

                foreach (IndexAttribute ia in ias)
                    if (ia._Unique)
                        td.IndexedFields.Add("Unique|" + ia._IndexedFields);
                    else
                        td.IndexedFields.Add("NotUnique|" + ia._IndexedFields);

                CheckIndexes(td, ias);
                #endregion

                #region Read ParentRelation
                //ParentRelationAttribute[] pras = (ParentRelationAttribute[])EntityType.GetCustomAttributes(
                //    TPRA, true);
                //foreach (ParentRelationAttribute pra in pras)
                //{
                //    pra._ParentType = EntityType;
                //    td.ParentRelations.Add(pra);

                //    if (pra._ParentFields == null)
                //    {
                //        string[] Fields = new string[td.KeyFields.Count];
                //        int i = 0;
                //        foreach (FieldDef fld in td.KeyFields.Values)
                //            Fields[i++] = fld._FieldName;
                //        pra._ParentFields = Fields;
                //        pra._ChildFields = Fields;
                //    }
                //}
                #endregion

                CheckFields(td, FKFields, FKCount);
                bool TSExist = false;
                foreach (FieldDef fd in td.NonKeyFields.Values)
                {
                    // Parent adalah diri sendiri...
                    if (fd._DataTypeAtr == null)
                    {
                        fd._DataTypeAtr = td.GetFieldDef(
                            fd._dtlsa.ParentFieldName)._DataTypeAtr;
                        fd.CheckFieldType(td._TableName);
                    }
                    if (fd.DataType == DataType.TimeStamp)
                    {
                        if (TSExist)
                            throw new ApplicationException(string.Format(
                                ErrorMetaData.DuplicateTimeStamp, td._TableName));
                        td.fldTimeStamp = fd;
                        TSExist = true;
                    }
                }
            }

            EnableCancelEntityAttribute[] ddes = (EnableCancelEntityAttribute[])
                EntityType.GetCustomAttributes(typeof(EnableCancelEntityAttribute), true);
            if (ddes.Length > 0)
            {
                Type tpi = EntityType.GetInterface("ICancelEntity");
                td.EnableCancelEntityAtr = ddes[0];

                #region Cek Cancel Field
                string strFldName = td.EnableCancelEntityAtr.GetCancelDateTimeFieldName();
                FieldDef TmpFld = td.GetFieldDef(strFldName);
                if (TmpFld == null)
                {
                    DataTypeDateTimeAttribute dta = new DataTypeDateTimeAttribute();
                    dta.Default = "1/1/1900";
                    td.NonKeyFields.Add(strFldName, new FieldDef(strFldName,
                        tpi.GetProperty("CancelDateTime"), dta, false));
                }
                else
                    if (TmpFld.DataType != DataType.DateTime)
                        throw new ApplicationException(string.Format(
                            ErrorMetaData.DataTypeCancelDateTimeIsIncorrect, td._TableName,
                            strFldName));

                strFldName = td.EnableCancelEntityAtr.GetCancelStatusFieldName();
                TmpFld = td.GetFieldDef(strFldName);
                if (TmpFld == null)
                    td.NonKeyFields.Add(strFldName, new FieldDef(strFldName,
                        tpi.GetProperty("CancelStatus"), 
                        new DataTypeVarCharAttribute(10), false));
                else
                    if (TmpFld.DataType != DataType.VarChar)
                        throw new ApplicationException(string.Format(
                            ErrorMetaData.DataTypeCancelStatusIsIncorrect, td._TableName,
                            strFldName));

                strFldName = td.EnableCancelEntityAtr.GetCancelNotesFieldName();
                TmpFld = td.GetFieldDef(strFldName);
                if (TmpFld == null)
                    td.NonKeyFields.Add(strFldName, new FieldDef(strFldName,
                        tpi.GetProperty("CancelNotes"), 
                        new DataTypeVarCharAttribute(100), false));
                else
                    if (TmpFld.DataType != DataType.VarChar)
                        throw new ApplicationException(string.Format(
                            ErrorMetaData.DataTypeCancelNotesIsIncorrect, td._TableName,
                            strFldName));

                strFldName = td.EnableCancelEntityAtr.GetCancelUserFieldName();
                TmpFld = td.GetFieldDef(strFldName);
                if (TmpFld == null)
                    td.NonKeyFields.Add(strFldName, new FieldDef(strFldName,
                        tpi.GetProperty("CancelUser"), 
                        new DataTypeVarCharAttribute(20), false));
                else
                    if (TmpFld.DataType != DataType.VarChar)
                        throw new ApplicationException(string.Format(
                            ErrorMetaData.DataTypeCancelUserIsIncorrect, td._TableName,
                            strFldName));
                #endregion
            }
            return td;
        }

        private static void CheckTableDef(TableDef newTd)
        {
            if (!BaseUtility.IsDebugMode || newTd._dva != null) return;

            foreach (RelationAttribute pra in newTd.ParentRelations)
            {
                if (pra._ParentFields.Length != pra._ChildFields.Length)
                    throw new ApplicationException(string.Format(
                        ErrorMetaData.RelationFieldCount,
                        pra._ParentType.Name, pra.ChildType.Name));
                if (pra._ParentFields.Length != newTd.KeyFields.Count)
                    throw new ApplicationException(string.Format(
                        ErrorMetaData.RelationParentField,
                        pra._ParentType.Name, pra.ChildType.Name));
                TableDef tdChild = MetaData.GetTableDef(pra.ChildType);
                for (int i = 0; i < pra._ParentFields.Length; i++)
                {
                    FieldDef fldP;
                    if (!newTd.KeyFields.TryGetValue(pra._ParentFields[i], out fldP))
                        throw new ApplicationException(string.Format(
                            ErrorMetaData.RelParentFieldNotFound,
                            pra._ParentType.Name, pra.ChildType.Name,
                            pra._ParentFields[i]));

                    FieldDef fldC;
                    if (!tdChild.KeyFields.TryGetValue(pra._ChildFields[i], out fldC))
                        if (!tdChild.NonKeyFields.TryGetValue(pra._ChildFields[i], out fldC))
                            throw new ApplicationException(string.Format(
                                ErrorMetaData.RelChildFieldNotFound,
                                pra._ParentType.Name, pra.ChildType.Name,
                                pra._ChildFields[i]));
                    if (fldP.DataType != fldC.DataType ||
                        fldP.Length < fldC.Length)
                        throw new ApplicationException(string.Format(
                            ErrorMetaData.RelationFieldTypeNotMatch,
                            pra._ParentType.Name, pra.ChildType.Name,
                            pra._ParentFields[i], pra._ChildFields[i]));
                }
            }
        }

        #region GetTableDef
        public static TableDef GetTableDef(BaseEntity Entity)
        { return GetTableDef(Entity.GetType()); }
        public static TableDef GetTableDef<TEntity>()
            where TEntity : BaseEntity
        {
            return GetTableDef(typeof(TEntity));
        }
        public static TableDef GetTableDef(Type EntityType)
        {
            EntityType = BaseFactory.GetObjType(EntityType);

            TableDef newTd;
            if (TableDefs.TryGetValue(EntityType, out newTd))
                return newTd;

            try
            {
                newTd = BuildEntityMetaData(EntityType);
                if (TableDefs.ContainsKey(EntityType))
                    return newTd;
                else
                {
                    TableDefs.Add(EntityType, newTd);
                    if (!NameTableDefs.ContainsKey(newTd._TableName))
                        NameTableDefs.Add(newTd._TableName, newTd);
 
                    CheckTableDef(newTd);

                    foreach (RelationAttribute ra in (RelationAttribute[])
                        EntityType.GetCustomAttributes(typeof(RelationAttribute), true))
                    {
                        if (ra.ChildType == null)
                            ra.ChildType = EntityType;
                        newTd.ChildRelations.Add(ra);

                        TableDef tdParent = MetaData.GetTableDef(ra._ParentType);
                        tdParent.ParentRelations.Add(ra);

                        if (ra._ParentFields == null)
                        {
                            string[] Fields = new string[tdParent.KeyFields.Count];
                            int i = 0;
                            foreach (FieldDef fld in tdParent.KeyFields.Values)
                                Fields[i++] = fld._FieldName;
                            ra._ParentFields = Fields;
                            ra._ChildFields = Fields;
                            ra._ParentFields = Fields;
                            ra._ChildFields = Fields;
                        }

                        CheckChildRelation(ra, tdParent, newTd);
                    }

                    return newTd;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format(
                    ErrorMetaData.BuildMetaData, EntityType.Name,
                    ex.Message));
            }
        }
        public static TableDef GetTableDef(string TableName)
        {
            TableDef td;

            NameTableDefs.TryGetValue(TableName, out td);
            return td;
        }
        #endregion

        #region GetEntityInfo
        public static string GetEntityInfo(BaseEntity Entity) 
        { return GetEntityInfo(Entity, 0); }
        internal static string GetEntityInfo(BaseEntity Entity, int Level)
        {
            TableDef td = MetaData.GetTableDef(Entity.GetType());
            StringBuilder tmpStr = new StringBuilder();
            string TmpSpace = tmpStr.Append(' ', Level * 5).ToString();
            tmpStr.Length = 0;
            tmpStr.Append("<").Append(Entity.GetType().Name)
                .Append(">").AppendLine(":");
            foreach (FieldDef fld in td.Fields)
            {
                object obj = fld.GetValue(Entity);
                string tmpStr2 = obj == null ? 
                    "(null)" : obj.ToString();
                tmpStr.Append(TmpSpace).Append(
                    fld._FieldName)
                    .Append("=").AppendLine(tmpStr2);
            }
            ParentEntity pe = Entity as ParentEntity;
            foreach (EntityCollDef ecd in td.ChildEntities)
            {
                int i = 1;
                ICollection<BusinessEntity> List = (ICollection<BusinessEntity>)
                    ecd.GetValue(pe);
                if (List.Count > 0)
                    tmpStr.Append(TmpSpace).Append(ecd.FieldName)
                        .AppendLine("=");
                else
                    tmpStr.Append(TmpSpace).Append(ecd.FieldName)
                        .AppendLine("= Empty)");

                foreach (BusinessEntity objItem in List)
                {
                    tmpStr.Append(TmpSpace).Append(i.ToString())
                        .Append(" - ").AppendLine(
                        objItem.ToString(Level + 1));
                    i++;
                }
            }
            return tmpStr.ToString().TrimEnd();
        }
        #endregion

        public static void SetDefault(BaseEntity Entity)
        { GetTableDef(Entity.GetType()).SetDefault(Entity); }

        #region Clone, CloneAll, FastClone
        public static BaseEntity Clone(BaseEntity SourceObj)
        {
            TableDef td = GetTableDef(SourceObj.GetType());
            BusinessEntity DestObj = (BusinessEntity)BaseFactory
                .CreateInstance(td._ClassType);
            IRuleInitUI px = DestObj as IRuleInitUI;

            //CallLoadRule = CallLoadRule && px != null;
            
            DestObj.EntityOnLoad = true;
            try
            {
                foreach (FieldDef fd in td.KeyFields.Values)
                    fd.SetLoadValue(DestObj, fd.GetValue(SourceObj));
                foreach (FieldDef fd in td.NonKeyFields.Values)
                    fd.SetLoadValue(DestObj, fd.GetValue(SourceObj));

                if (td.ChildEntities.Count > 0)
                {
                    foreach (EntityCollDef ecd in td.ChildEntities)
                    {
                        IList SrcCols = ecd.GetValue(SourceObj);
                        IList DestCols = ecd.GetValue(DestObj);
                        int NumDest = DestCols.Count;
                        ((IEntityCollection)DestCols).OnLoad = true;
                        int i = 0;
                        foreach (BaseEntity obj in SrcCols)
                            if (i < NumDest)
                                Clone((BaseEntity)DestCols[i++], obj);
                            else
                                DestCols.Add(Clone(obj));
                        ((IEntityCollection)DestCols).OnLoad = false;
                    }
                }

                //if (CallLoadRule)
                //{
                    //px.AfterLoadFound();
                    //BaseFramework.DoEntityAction(DestObj, enEntityActionMode.AfterLoadFound);
                //}
                BusinessEntity be = DestObj as BusinessEntity;
                if (be != null)
                    be.AfterClone((BusinessEntity)SourceObj);
            }
            finally
            {
                DestObj.EntityOnLoad = false;
                DestObj.DataChanged();
            }
            return DestObj;
        }
        public static void Clone(BaseEntity DestObj, 
            BaseEntity SourceObj)
        {
            if (DestObj == null)
                throw new ApplicationException("DestObj tidak boleh null");
            TableDef td = GetTableDef(SourceObj.GetType());

            //IRuleInitUI px = DestObj as IRuleInitUI;
            //CallLoadRule = CallLoadRule && px != null;

            DestObj.EntityOnLoad = true;
            try
            {
                foreach (FieldDef fd in td.KeyFields.Values)
                    fd.SetLoadValue(DestObj, fd.GetValue(SourceObj));
                foreach (FieldDef fd in td.NonKeyFields.Values)
                    fd.SetLoadValue(DestObj, fd.GetValue(SourceObj));

                if (td.ChildEntities.Count > 0)
                {
                    foreach (EntityCollDef ecd in td.ChildEntities)
                    {
                        IList SrcCols = ecd.GetValue(SourceObj);
                        IList DestCols = ecd.GetValue(DestObj);
                        int NumDest = DestCols.Count;
                        ((IEntityCollection)DestCols).OnLoad = true;

                        int i = 0;
                        while (DestCols.Count > SrcCols.Count)
                            DestCols.RemoveAt(0);
                        foreach (BaseEntity obj in SrcCols)
                            if (i < NumDest)
                                Clone((BaseEntity)DestCols[i++], obj);
                            else
                                DestCols.Add(Clone(obj));
                        ((IEntityCollection)DestCols).OnLoad = false;
                    }
                }
                //if (CallLoadRule)
                //{
                //    px.AfterLoadFound();
                //    BaseFramework.DoEntityAction(DestObj, enEntityActionMode.AfterLoadFound);
                //}
                BusinessEntity be = DestObj as BusinessEntity;
                if (be != null)
                    be.AfterClone((BusinessEntity)SourceObj);
            }
            finally
            {
                DestObj.EntityOnLoad = false;
                DestObj.DataChanged();
            }
        }
        public static void CloneToOriginal(ParentEntity Entity)
        {
            Entity._Original = (ParentEntity)CloneAll(Entity);
        }

        public static BaseEntity CloneAll(BaseEntity SourceObj)
        {
            TableDef td = GetTableDef(SourceObj.GetType());
            BaseEntity DestObj = (BaseEntity)BaseFactory
                .CreateInstance(td._ClassType);

            IRuleInitUI px = DestObj as IRuleInitUI;
            //CallLoadRule = CallLoadRule && px != null;

            DestObj.EntityOnLoad = true;
            try
            {
                DestObj = SourceObj.ShallowClone();

                if (td.ChildEntities.Count > 0)
                {
                    foreach (EntityCollDef ecd in td.ChildEntities)
                    {
                        //IEntityCollection cols = (IEntityCollection)
                        //    Activator.CreateInstance(ecd.ConstructionType);
                        //if (ecd.mi.MemberType == MemberTypes.Field)
                        //    ((FieldInfo)ecd.mi).SetValue(DestObj, cols);
                        //else
                        //    ((PropertyInfo)ecd.mi).SetValue(DestObj, cols, null);

                        IList SrcCols = ecd.GetValue(SourceObj);
                        IList DestCols = (IList)ecd.CreateNew((BusinessEntity)DestObj);

                        ((IEntityCollection)DestCols).OnLoad = true;
                        foreach (BusinessEntity obj in SrcCols)
                            DestCols.Add(CloneAll(obj));
                        ((IEntityCollection)DestCols).OnLoad = false;
                    }
                }

                //if (CallLoadRule)
                //{
                //    px.AfterLoadFound();
                //    BaseFramework.DoEntityAction(DestObj, enEntityActionMode.AfterLoadFound);
                //}
                ParentEntity pe = DestObj as ParentEntity;
                if (pe != null)
                    pe.AfterClone((ParentEntity)SourceObj);
            }
            finally
            {
                DestObj.EntityOnLoad = false;
                DestObj.DataChanged();
            }
            return DestObj;
        }

        public static BaseEntity FastClone(BaseEntity SourceObj)
        {
            TableDef td = GetTableDef(SourceObj.GetType());
            BusinessEntity DestObj = (BusinessEntity)BaseFactory
                .CreateInstance(td._ClassType);

            DestObj.EntityOnLoad = true;
            try
            {
                foreach (FieldDef fd in td.KeyFields.Values)
                    fd.SetLoadValue(DestObj, fd.GetValue(SourceObj));
                foreach (FieldDef fd in td.NonKeyFields.Values)
                    fd.SetLoadValue(DestObj, fd.GetValue(SourceObj));
            }
            finally
            {
                DestObj.EntityOnLoad = false;
            }
            return DestObj;
        }
        public static void FastClone(BaseEntity DestObj, BaseEntity SourceObj)
        {
            if (DestObj == null)
                DestObj = (BusinessEntity)BaseFactory
                    .CreateInstance(SourceObj.GetType());
            TableDef td = GetTableDef(SourceObj.GetType());
            BusinessEntity be = (BusinessEntity)DestObj;
            be.EntityOnLoad = true;
            try
            {
                foreach (FieldDef fd in td.KeyFields.Values)
                    fd.SetLoadValue(DestObj, fd.GetValue(SourceObj));
                foreach (FieldDef fd in td.NonKeyFields.Values)
                    fd.SetLoadValue(DestObj, fd.GetValue(SourceObj));
            }
            finally
            {
                be.EntityOnLoad = false;
            }
        }
        #endregion
    }
}
