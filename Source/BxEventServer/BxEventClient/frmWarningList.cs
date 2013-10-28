using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BxEventClient.Warning;
using SentraSolutionFramework;
using SentraSolutionFramework.Persistance;
using SentraSecurity;
using DevExpress.XtraGrid.Views.Grid;

namespace BxEventClient
{
    public partial class frmWarningList : XtraForm
    {
        public static DataPersistance SDMDp;

        public frmWarningList()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        public IList<clsWarningList> CreateDataSource(bool Awl)
        {
            string SqlQuery = @"SELECT * FROM ViewWarningList 
                WHERE ResponsibleUser=@User AND AutoWarningLetter=@awl";

            DataPersistance Dp = SDMDp;

            IList<ViewWarningList> ListWl =
                Dp.ListFastLoadEntitiesUsingSqlSelect<ViewWarningList>(
                null, SqlQuery, string.Empty,
                new FieldParam("User", BaseSecurity.CurrentLogin.CurrentUser),
                new FieldParam("awl", Awl));

            if (ListWl.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (ViewWarningList vwl in ListWl)
                {
                    string SqlFilter = vwl.KodeDepartemen.Length > 0 ?
                        string.Concat("KodeDepartemen=",
                        Dp.FormatSqlValue(vwl.KodeDepartemen)) : string.Empty;

                    if (vwl.KodeBagian.Length > 0)
                        SqlFilter = string.Concat(SqlFilter, " AND KodeBagian=",
                            Dp.FormatSqlValue(vwl.KodeBagian));
                    if (vwl.KodeSeksi.Length > 0)
                        SqlFilter = string.Concat(SqlFilter, " AND KodeSeksi=",
                            Dp.FormatSqlValue(vwl.KodeSeksi));
                    if (vwl.KodeGudang.Length>0)
                        SqlFilter = string.Concat(SqlFilter, " AND KodeGudang=",
                            Dp.FormatSqlValue(vwl.KodeGudang));

                    if (SqlFilter.StartsWith(" AND"))
                        SqlFilter = " WHERE " + SqlFilter.Substring(5);
                    else if (SqlFilter.Length > 0)
                        SqlFilter = " WHERE " + SqlFilter;

                    sb.Append(" UNION ALL SELECT ").Append(
                        Dp.FormatSqlValue(vwl.WarningName)).Append(
                        @" NamaPeringatan,NoDokumen,TglAkhir,Keterangan,KodeDepartemen,
                        KodeBagian,KodeSeksi,KodeGudang,Pembuat FROM (")
                        .Append(vwl.WarningQuery.Replace("@Tgl", vwl.NumDayToWarningLetter))
                        .Append(") x").Append(SqlFilter);
                }
                sb.Remove(0, 11);
                return Dp.ListFastLoadEntitiesUsingSqlSelect<clsWarningList>(null,
                        sb.ToString(), "NamaPeringatan,TglAkhir");
            }
            else
                return new List<clsWarningList>();
        }

        private void RefreshData()
        {
            try
            {
                gridControl1.DataSource = CreateDataSource(true);
                gridControl2.DataSource = CreateDataSource(false);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error Refresh Data", 
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void frmWarningList_Load(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void gridView1_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Appearance.ForeColor = DateTime.Today <=
                    ((DateTime)gridView1.GetRowCellValue(e.RowHandle, gridColumn3)) ?
                    Color.Black : Color.Red;
        }
    }

    public class clsWarningList
    {
        private string _NamaPeringatan;
        public string NamaPeringatan
        {
            get { return _NamaPeringatan; }
            set { _NamaPeringatan = value; }
        }

        private string _NoDokumen;
        public string NoDokumen
        {
            get { return _NoDokumen; }
            set { _NoDokumen = value; }
        }

        private DateTime _TglAkhir;
        public DateTime TglAkhir
        {
            get { return _TglAkhir; }
            set { _TglAkhir = value; }
        }

        private string _Keterangan;
        public string Keterangan
        {
            get { return _Keterangan; }
            set { _Keterangan = value; }
        }

        private string _KodeDepartemen;
        public string KodeDepartemen
        {
            get { return _KodeDepartemen; }
            set { _KodeDepartemen = value; }
        }

        private string _KodeBagian;
        public string KodeBagian
        {
            get { return _KodeBagian; }
            set { _KodeBagian = value; }
        }

        private string _KodeSeksi;
        public string KodeSeksi
        {
            get { return _KodeSeksi; }
            set { _KodeSeksi = value; }
        }

        private string _Pembuat;
        public string Pembuat
        {
            get { return _Pembuat; }
            set { _Pembuat = value; }
        }

        private string _Pesan;
        public string Pesan
        {
            get { return _Pesan; }
            set { _Pesan = value; }
        }
    }
}