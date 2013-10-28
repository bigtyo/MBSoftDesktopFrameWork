using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SistemBukuBesar
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] Parameters)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MdiForm());
            //Application.Run(new SistemBukuBesar.TesSDI.frmUtama());
        }
    }

}