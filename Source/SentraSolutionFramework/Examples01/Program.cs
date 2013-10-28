using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SentraSolutionFramework;
using SentraSolutionFramework.Persistance;
using Examples01.Entity;
using SentraWinFramework;

namespace Examples01
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            BaseFramework.DefaultDataPersistance =
                new AccessPersistance("C:\\Data", "Examples01", true);

            BaseWinFramework.Init();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmStart());
        }
    }
}