using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework;
using SentraSolutionFramework.Persistance;
using SentraSecurity;
using SentraGL;
using System.Drawing;

namespace SentraGL.Report
{
    public class ReportUmum
    {
        private DataPersistance Dp
        {
            get { return BaseFramework.DefaultDp; }
        }

        private SetingPerusahaan sp = BaseGL.SetingPerusahaan;

        public string TglLaporan
        {
            get
            {
                return string.Concat(DateTime.Today.Day.ToString(), " ",
                        NamaBulan((decimal)DateTime.Today.Month),
                        " ", DateTime.Today.Year.ToString());
            }
        }

        private static string[] ArrBulan = {"Januari", "Februari", "Maret",
            "April", "Mei", "Juni", "Juli", "Agustus", "September",
            "Oktober", "Nopember", "Desember" };

        public string NamaBulan(decimal Bulan)
        {
            return ArrBulan[(int)Bulan-1];
        }

        public string JamLaporan
        {
            get
            {
                return DateTime.Now.ToString("HH:mm");
            }
        }

        public string NamaOperator
        {
            get
            {
                return BaseSecurity.CurrentLogin.CurrentUser;
            }
        }

        public string NamaPeran
        {
            get
            {
                return BaseSecurity.CurrentLogin.CurrentRole;
            }
        }

        public string FormatTanggal(DateTime TglLaporan, string Format)
        {
            return TglLaporan.ToString(Format);
        }

        public string NamaPerusahaan
        {
            get
            {
                return sp.NamaPerusahaan;
            }
        }

        public string KodePerusahaan
        {
            get
            {
                return sp.KodePerusahaan;
            }
        }

        public string AlamatPerusahaan
        {
            get
            {
                return sp.AlamatPerusahaan;
            }
        }

        public string Kota
        {
            get
            {
                return sp.Kota;
            }
        }

        public string Telepon
        {
            get
            {
                return sp.Telpon;
            }
        }

        public Image LambangPerusahaan
        {
            get
            {
                return sp.LogoPerusahaan;
            }
        }

        public int JumlahHariBulan(decimal Tahun, decimal Bulan)
        {
            return (DateTime.DaysInMonth((int)Tahun, (int)Bulan));
        }
    }
}
