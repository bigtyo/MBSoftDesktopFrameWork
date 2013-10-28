using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Examples01.Entity;
using SentraSolutionFramework;

namespace Examples01
{
    public partial class frmAkun : Form
    {
        public frmAkun()
        {
            InitializeComponent();
            akunBindingSource1.DataSource = BaseFramework.DefaultDataPersistance
                .FastLoadEntities<Akun>(
                "NoAkun,NamaAkun", 
                "NonPosting=true", "NoAkun");
            akunBindingSource.DataSource = new Akun();
        }

        private void uiNavigator1_onAfterSaveEdit()
        {
            akunBindingSource1.DataSource = BaseFramework.DefaultDataPersistance
                .FastLoadEntities<Akun>(
                "NoAkun,NamaAkun", 
                "NonPosting=true", "NoAkun");
        }

        private void uiNavigator1_onAfterSaveNew()
        {
            akunBindingSource1.DataSource = BaseFramework.DefaultDataPersistance
                .FastLoadEntities<Akun>("NoAkun,NamaAkun", "NonPosting=true", "NoAkun");
        }
    }
}