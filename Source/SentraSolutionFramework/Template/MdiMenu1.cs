using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SentraWinFramework;
using SentraWinSecurity;
using DevExpress.XtraBars;
using SentraSolutionFramework;
using SentraSolutionFramework.Persistance;
using SentraSecurity;

namespace Template
{
    public partial class MdiMenu1 : XtraForm
    {
        public MdiMenu1()
        {
            InitializeComponent();

            BaseWinFramework.ShowProgressBar(Text);

            #region Register Persistance Engine
            BaseFramework.DpEngine
                .RegisterEngine<SqlServerPersistance>();
            BaseFramework.DpEngine
                .RegisterEngine<AccessPersistance>();
            #endregion

            BaseSecurity.LoginWithRole = false;
            BaseWinSecurity.Init(this, DoLogin, DoLogout, true);

            #region register button menu
            BaseWinSecurity.ListLoginButton.Add(btnLogout);
            BaseWinSecurity.ListLoginButton.Add(btnUbahPassword);

            BaseWinSecurity.ListAdminButton.Add(btnUserDanHakAkses);
            BaseWinSecurity.ListAdminButton.Add(btnLogAktivitas);
            BaseWinSecurity.ListAdminButton.Add(btnManajemenDatabase);
            #endregion

            #region Place Init Security Module here...
            #endregion
        }

        private void DoLogin()
        {
        }

        private void DoLogout()
        {
        }
    }
}