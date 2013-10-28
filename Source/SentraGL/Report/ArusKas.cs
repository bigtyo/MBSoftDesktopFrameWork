using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using SentraSolutionFramework.Persistance;

namespace SentraGL.Report
{
    public class ArusKas : ReportEntity
    {
        private DateTime _TglAwal;
        public DateTime TglAwal
        {
            get { return _TglAwal; }
            set { _TglAwal = value; }
        }

        private DateTime _TglAkhir;
        public DateTime TglAkhir
        {
            get { return _TglAkhir; }
            set { _TglAkhir = value; }
        }

        protected override void GetDataSource(out string DataSource,
            out string DataSourceOrder, List<FieldParam> Parameters)
        {
            DataSource = string.Empty;
            DataSourceOrder = string.Empty;
        }
    }

    public class DataArusKas
    {
        private int _UrutanJenisArusKas;
        public int UrutanJenisArusKas
        {
            get { return _UrutanJenisArusKas; }
            set { _UrutanJenisArusKas = value; }
        }

        private string _JenisArusKas;
        public string JenisArusKas
        {
            get { return _JenisArusKas; }
            set { _JenisArusKas = value; }
        }

        private string _JenisTransaksi;
        public string JenisTransaksi
        {
            get { return _JenisTransaksi; }
            set { _JenisTransaksi = value; }
        }

        private string _JenisMutasi;
        public string JenisMutasi
        {
            get { return _JenisMutasi; }
            set { _JenisMutasi = value; }
        }

        private decimal _Total;
        public decimal Total
        {
            get { return _Total; }
            set { _Total = value; }
        }

        public DataArusKas(int UrutanJenisArusKas, string JenisArusKas,
            string JenisTransaksi, string Menambah, decimal Total)
        {
            _UrutanJenisArusKas = UrutanJenisArusKas;
            _JenisArusKas = JenisArusKas;
            _JenisTransaksi = JenisTransaksi;
            _JenisMutasi = Menambah;
            _Total = Total;
        }
    }
}
