using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SentraSolutionFramework;
using SentraSolutionFramework.Persistance;

namespace Examples03
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            BaseFramework.DefaultDataPersistance = new
                AccessPersistance(@"C:\Data", "Luna", true);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}