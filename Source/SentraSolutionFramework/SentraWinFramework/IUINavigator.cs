using System;
using SentraUtility.Expression;
using System.Windows.Forms;
using SentraSolutionFramework.Entity;
using SentraSolutionFramework.Persistance;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;
using SentraSecurity;
using SentraSolutionFramework;

namespace SentraWinFramework
{
    public interface IUINavigator : IBaseUINavigator
    {
        EntityForm EntityForm { set; }
        void SetGridLookupEditDisplayMember(GridView gv, 
            RepositoryItemLookUpEditBase Le, string DisplayMember);
        void SetLookupEditDisplayMember(LookUpEdit Le, string DisplayMember);
        void SetGridAsNavigator(GridView gv);
        void SetSecurity(ModuleAccess ma);
    }
}
