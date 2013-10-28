using System;
using System.Collections.Generic;
using System.Text;
using SentraUtility.Expression;
using SentraSolutionFramework;
using SentraSecurity;
using SentraGL.Report;
using SentraGL.Document;
using SentraSolutionFramework.Entity;
using SentraGL.Master;
using SentraSolutionFramework.Persistance;
using SentraUtility;

namespace SentraGL
{
    public enum enNamaBulan
    {
        Januari = 1, Februari, Maret, April, Mei, Juni,
        Juli, Agustus, September, Oktober, Nopember, Desember
    }

    public static class BaseGL
    {
        static BaseGL()
        {
            if (BaseSecurity.CurrentLogin.IsLogged())
                CurrentLogin_onLogon();

            BaseSecurity.CurrentLogin.onLogon += new LogonChanged(CurrentLogin_onLogon);
            BaseFactory.RegisterObjType<Evaluator, GLEvaluator>();
        }

        static void CurrentLogin_onLogon()
        {
            RingkasanAkun = new RingkasanAkun();

            using (EntityTransaction tr = new EntityTransaction(
                BaseFramework.DefaultDp))
            {
                if (!BaseFramework.DefaultDp.Find
                    .IsExists<MataUang>(string.Empty))
                {
                    MataUang mu = new MataUang();
                    mu.KodeMataUang = "IDR";
                    mu.NamaMataUang = "Rupiah";
                    mu.Aktif = true;
                    mu.SaveNew();
                }
                if (!BaseFramework.DefaultDp.Find
                    .IsExists<JenisDokSumberJurnal>("Otomatis=@0",
                    new FieldParam("0", false)))
                {
                    JenisDokSumberJurnal jds = new JenisDokSumberJurnal();
                    jds.JenisDokSumber = "Umum";
                    jds.Aktif = true;
                    jds.SaveNew();
                }

                if (!BaseFramework.DefaultDp.Find
                    .IsExists<SetingPerusahaan>(string.Empty))
                {
                    SetingPerusahaan sp = new SetingPerusahaan();
                    sp.SetDefaultValue();
                    sp.TglMulaiSistemBaru = BaseFramework
                        .TransDate.StartDate;
                    sp.SaveNew();
                }

                tr.CommitTransaction();
            }
            SetingPerusahaan.LoadEntity();
            RegisterAsPostedDocument("Penerimaan Kas Umum");
            RegisterAsPostedDocument("Pengeluaran Kas Umum");
            RegisterAsPostedDocument("Transfer Antar Kas");
        }

        public static RingkasanAkun RingkasanAkun;
        public static SetingPerusahaan SetingPerusahaan = new SetingPerusahaan();
        public static ReportUmum ReportUmum = new ReportUmum();
        public static FungsiGL FungsiGL = new FungsiGL();
        
        /// <summary>
        /// Save Module Name into [JenisDokSumberJurnal] if does not exist
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        public static void RegisterAsPostedDocument(string ModuleName)
        {
            if (!BaseFramework.DefaultDp
                .Find.IsExists<JenisDokSumberJurnal>(
                "JenisDokSumber=@0", 
                new FieldParam("0", ModuleName)))
                new JenisDokSumberJurnal(ModuleName, true)
                    .Save(true, true);
        }
    }

    public class GLEvaluator : Evaluator
    {
        public GLEvaluator()
        {
            ObjValues.Add("Umum", BaseGL.ReportUmum);
            ObjValues.Add("SetingPerusahaan", BaseGL.SetingPerusahaan);
        }
    }
}
