using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using SentraSecurity;
using SentraSolutionFramework;

namespace SentraGL
{
    [EnableLog]
    public abstract class DocumentEntity : ParentEntity
    {
        private DateTime _TglJamUpdate;
        [DataTypeTimeStamp]
        public DateTime TglJamUpdate
        {
            get { return _TglJamUpdate; }
        }

        private string _NamaOperator;
        [DataTypeVarChar(50, Default = null)]
        public string NamaOperator
        {
            get { return _NamaOperator; }
        }

        public string TerakhirUpdateOleh
        {
            get
            {
                return (string.Concat(_NamaOperator, " - ",
                    string.Format("{0:dd MMM yyyy HH:mm}", _TglJamUpdate)));
            }
        }

        static DocumentEntity()
        {
            BaseFramework.onEntityAction += new EntityAction(BaseFramework_onEntityAction);
        }

        static void BaseFramework_onEntityAction(BaseEntity ActionEntity, enEntityActionMode ActionMode)
        {
            DocumentEntity de = ActionEntity as DocumentEntity;
            if (de == null) return;

            switch (ActionMode)
            {
                case enEntityActionMode.BeforeSaveNew:
                case enEntityActionMode.BeforeSaveUpdate:
                    de._NamaOperator = BaseSecurity.CurrentLogin.CurrentUser;
                    break;
            }
        }
    }
}
