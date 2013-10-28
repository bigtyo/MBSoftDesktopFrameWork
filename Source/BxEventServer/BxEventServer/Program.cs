using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SentraSolutionFramework;
using SentraSolutionFramework.Persistance;
using BxEventClient.Warning;
using SentraUtility;

namespace BxEventServer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(params string[] Params)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BaseFramework.AutoUpdateMetaData = false;

            string ServerAddress = Params.Length > 0 ? Params[0] : ".";
            try
            {
                BaseFramework.DefaultDp = new SqlServerPersistance(ServerAddress,
                    "Barindo SDM", false, string.Empty, false, "sa", "Adm1n");
            }
            catch
            {
                BaseFramework.DefaultDp = new SqlServerPersistance(ServerAddress,
                    "Barindo SDM", false, string.Empty, false, "sa", string.Empty);
            }
            DataPersistance Dp = BaseFramework.DefaultDp;

            try
            {
                Dp.ValidateTableDef<GetJumlahJamDPB>();
                Dp.ValidateTableDef<WarningLetter>();
                Dp.ValidateTableDef<WarningMaster>();
                Dp.ValidateTableDef<WarningResponsible>();
                Dp.ValidateTableDef<ViewWarningList>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Menjalankan BxServer", 
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Application.Run(new frmMain());
        }
    }
}
