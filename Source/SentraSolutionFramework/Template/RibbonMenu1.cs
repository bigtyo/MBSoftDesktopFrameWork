using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars.Ribbon;
using SentraWinSecurity;
using SentraSolutionFramework;
using SentraSolutionFramework.Persistance;
using SentraWinFramework;
using SentraSecurity;

namespace Template
{
    public partial class RibbonMenu1 : RibbonForm
    {
        public RibbonMenu1()
        {
            InitializeComponent();

            BaseWinFramework.ShowProgressBar(Text);
            BaseSecurity.LoginWithRole = false;

            #region Register Persistance Engine
            BaseFramework.DpEngine
                .RegisterEngine<SqlServerPersistance>();
            BaseFramework.DpEngine
                .RegisterEngine<AccessPersistance>();
            #endregion

            // WARNING !!!
            // You Must Set your Company Name & Product Name on 
            //   Project->[ProjectName] Properties->Application->
            //   Assembly Information->Product...

            BaseWinSecurity.Init(this, DoLogin, DoLogout, true, pgSistem);

            // Register your Document here...
        }

        private void DoLogin()
        {
        }

        private void DoLogout()
        {
        }
    }
}