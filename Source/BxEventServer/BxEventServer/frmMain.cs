using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DotNetRemoting;
using BxEventServer.Properties;
using BxEventClient;
using BxEventClient.Warning;
using SentraSolutionFramework.Persistance;
using SentraSecurity;
using EngineAbsensiSDM.OrganisasiDanSDM.Karyawan;
using SentraSolutionFramework;
using System.IO;

namespace BxEventServer
{
    public partial class frmMain : Form
    {
        private Dictionary<string, List<int>> DictUserListener = 
            new Dictionary<string, List<int>>();

        private Dictionary<string, int> DictActionListener =
            new Dictionary<string, int>();

        private Dictionary<string, List<int>> DictUserClient =
            new Dictionary<string, List<int>>();

        private static Dictionary<string, string> DictSqlWarning = new Dictionary<string, string>();
        public static string CreateSqlWarning(string NamaTabel)
        {
            string RetVal;
            if (!DictSqlWarning.TryGetValue(NamaTabel, out RetVal))
            {
                string SqlQuery = "SELECT * FROM ViewWarningList WHERE TableSourceName LIKE @ts";

                DataPersistance Dp = BaseFramework.DefaultDp;

                IList<ViewWarningList> ListWl =
                    Dp.ListFastLoadEntitiesUsingSqlSelect<ViewWarningList>(
                    null, SqlQuery, string.Empty,
                    new FieldParam("ts", string.Concat("%", NamaTabel, "%")));

                if (ListWl.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (ViewWarningList vwl in ListWl)
                    {
                        string SqlFilter = vwl.KodeDepartemen.Length > 0 ?
                            string.Concat(" AND KodeDepartemen=",
                            Dp.FormatSqlValue(vwl.KodeDepartemen)) : string.Empty;

                        if (vwl.KodeBagian.Length > 0)
                            SqlFilter = string.Concat(SqlFilter, " AND KodeBagian=",
                                Dp.FormatSqlValue(vwl.KodeBagian));
                        if (vwl.KodeSeksi.Length > 0)
                            SqlFilter = string.Concat(SqlFilter, " AND KodeSeksi=",
                                Dp.FormatSqlValue(vwl.KodeSeksi));
                        if (vwl.KodeGudang.Length > 0)
                            SqlFilter = string.Concat(SqlFilter, " AND KodeGudang=",
                                Dp.FormatSqlValue(vwl.KodeGudang));

                        sb.Append(" UNION ALL SELECT ").Append(
                            Dp.FormatSqlValue(vwl.WarningName)).Append(
                            @" NamaPeringatan,NoDokumen,TglAkhir,Keterangan,KodeDepartemen,
                        KodeBagian,KodeSeksi,KodeGudang,Pembuat,")
                            .Append(Dp.FormatSqlValue(vwl.ResponsibleUser))
                            .Append(" PenanggungJawab,CAST(")
                            .Append(vwl.AutoWarningLetter ? "1" : "0")
                            .Append(" AS BIT) JenisWarning FROM (")
                            .Append(vwl.WarningQuery.Replace("@Tgl", vwl.NumDayToWarningLetter))
                            .Append(") x")
                            .Append(" WHERE NoDokumen LIKE @NoDok+'%'")
                            .Append(SqlFilter);
                    }
                    RetVal = sb.Remove(0, 11).ToString();
                }
                else
                    RetVal = string.Empty;
                DictSqlWarning[NamaTabel] = RetVal;
            }
            return RetVal;
        }

        private void DisconnectClient(int ClientId)
        {
            foreach (List<int> ListInt in DictUserListener.Values)
                ListInt.Remove(ClientId);

            string EventName = string.Empty;
            foreach (KeyValuePair<string,int> kvp in DictActionListener)
                if (kvp.Value == ClientId)
                {
                    EventName = kvp.Key;
                    break;
                }
            if (EventName.Length > 0)
                DictActionListener.Remove(EventName);

            foreach (List<int> ListInt in DictUserClient.Values)
                if (ListInt.Remove(ClientId))
                    break;
        }

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                Visible = false;
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Keluar")
                Close();
            else
            {
                Visible = true;
                WindowState = FormWindowState.Normal;
            }
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            syncServerCtl1.StartServer(Convert.ToInt32(numericUpDown1.Value));
            Application.DoEvents();
            WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Start")
                syncServerCtl1.StartServer(Convert.ToInt32(numericUpDown1.Value));
            else
            {
                if (syncServerCtl1.ClientsCount > 0 && MessageBox.Show(
                    "Ada " + syncServerCtl1.ClientsCount.ToString() +
                    " klien yang sedang terkoneksi. Matikan Server ?",
                    "Konfirmasi Mematikan Event Server", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.No) return;
                syncServerCtl1.Stop();
            }
        }

        private void syncServerCtl1_DataReceivedHandler(int ClientID, object Data)
        {
            #region PopupMessageSend
            if (Data is DtPopUpMessageSend)
            {
                DtPopUpMessageSend pms = (DtPopUpMessageSend)Data;

                if (pms.MessageType == enMessageType.InternalMessage &&
                    pms.Caption == "_Warning_")
                {
                    string strQuery = CreateSqlWarning(pms.MessageData.ToString());
                    //using (StreamWriter sw = new StreamWriter("DataLog.txt", false))
                    //{
                    //    sw.WriteLine("Events utk Tabel: " + pms.MessageData.ToString() +
                    //        ", NoDok: " + pms.Message);
                    //    sw.WriteLine(strQuery);
                    //}
                    IList<clsWarningSend> ListWarningSend = BaseFramework.DefaultDp
                        .ListFastLoadEntitiesUsingSqlSelect<clsWarningSend>(null,
                        strQuery, string.Empty, new FieldParam("NoDok", pms.Message));
                    DtPopUpMessageReceive pmr = new DtPopUpMessageReceive(
                        FindSender(ClientID), pms.Caption, pms.Message,
                        null, enMessageType.Information);
                    foreach (clsWarningSend ws in ListWarningSend)
                    {
                        //using (StreamWriter sw = new StreamWriter("DataLog.txt", true))
                        //{
                        //    sw.WriteLine(ws.NoDokumen + ":" + ws.NamaPeringatan);
                        //    sw.WriteLine("Penanggung Jawab: " + ws.PenanggungJawab);
                        //    sw.WriteLine("DictUserClient.Count=" + DictUserClient.Count);
                        //    foreach (string nm in DictUserClient.Keys)
                        //        sw.WriteLine(nm);
                        //}
                        pmr.Caption = ws.NamaPeringatan;
                        pmr.Message = string.Concat("NoDok: ", ws.NoDokumen,
                            "; Pembuat: ", ws.Pembuat, '\n', ws.Keterangan);
                        pmr.MessageType = ws.JenisWarning ?
                            enMessageType.Warning : enMessageType.Information;

                        List<int> ListClient;
                        if (DictUserClient.TryGetValue(
                            ws.PenanggungJawab.ToLower(), out ListClient))
                            foreach (int Id in ListClient)
                                syncServerCtl1.Send(Id, pmr);
                    }
                }
                else
                {
                    DtPopUpMessageReceive pmr = new DtPopUpMessageReceive(
                        FindSender(ClientID), pms.Caption, pms.Message,
                        pms.MessageData, pms.MessageType);

                    foreach (string UserName in pms.UserName)
                    {
                        List<int> ListClient;
                        if (DictUserClient.TryGetValue(UserName.Trim().ToLower(), out ListClient))
                            foreach (int Id in ListClient)
                                syncServerCtl1.Send(Id, pmr);
                    }
                }
                return;
            }
            #endregion

            #region RaiseEventData
            if (Data is DtRaiseEventData)
            {
                DtRaiseEventData red = (DtRaiseEventData)Data;

                int Id;
                if (DictActionListener.TryGetValue(red.EventName, out Id))
                    syncServerCtl1.Send(Id, red);

                List<int> ListClient;
                if (DictUserListener.TryGetValue(red.EventName, out ListClient))
                    foreach (int ClId in ListClient)
                        syncServerCtl1.Send(ClId, red);
                return;
            }
            #endregion

            #region RegisterActionListener
            if (Data is DtRegisterActionListener)
            {
                DtRegisterActionListener ral = (DtRegisterActionListener)Data;
                foreach (string EventName in ral.EventName)
                    if (!DictActionListener.ContainsKey(EventName))
                        DictActionListener.Add(EventName, ClientID);
                    else
                        syncServerCtl1.Send(ClientID, new DtPopUpMessageReceive("BxEventServer",
                            "Error Registrasi Action Listener", string.Concat(
                            "Event ", EventName, " sudah dihandle Action Listener lain !"),
                            null, enMessageType.Error));
                return;
            }
            #endregion

            #region RegisterUserListener
            if (Data is DtRegisterUserListener)
            {
                DtRegisterUserListener rul = (DtRegisterUserListener)Data;

                foreach (string EventName in rul.EventName)
                {
                    List<int> ListClient;
                    if (!DictUserListener.TryGetValue(EventName, out ListClient))
                    {
                        ListClient = new List<int>();
                        DictUserListener.Add(EventName, ListClient);
                    }
                    else
                    {
                        bool ClientExist = false;
                        foreach (int cl in ListClient)
                            if (cl == ClientID)
                            {
                                ClientExist = true;
                                break;
                            }
                        if (ClientExist) continue;
                    }
                    ListClient.Add(ClientID);
                }
                return;
            }
            #endregion

            #region DtLogin
            if (Data is DtLogin)
            {
                DtLogin lg = (DtLogin)Data;

                List<int> ListClient;

                if (!DictUserClient.TryGetValue(lg.UserName.ToLower(), out ListClient)) 
                {
                    ListClient = new List<int>();
                    DictUserClient.Add(lg.UserName.ToLower(), ListClient);
                }
                ListClient.Add(ClientID);
                syncServerCtl1.Send(ClientID, true);

                //using (StreamWriter sw = new StreamWriter("DataLog.txt", false))
                //{
                //    sw.WriteLine(lg.UserName + " Logged; ClientId=" + ClientID);
                //}

                //string Message = BuildWelcomeMessage();

                //if (Message.Length > 0)
                //{
                //    DateTime dt = DateTime.Now;
                //    string Caption;
                //    if (dt.Hour <= 10)
                //        Caption = "Selamat Pagi !";
                //    else if (dt.Hour <= 15)
                //        Caption = "Selamat Siang !";
                //    else if (dt.Hour <= 19)
                //        Caption = "Selamat Sore !";
                //    else
                //        Caption = "Selamat Malam !";
                //    syncServerCtl1.Send(ClientID, new DtPopUpMessageReceive(
                //        string.Empty, Caption,
                //        Message, null, enMessageType.Information));
                //}
            }
            #endregion
        }

        private string FindSender(int ClientID)
        {
            foreach (KeyValuePair<string, List<int>> kvp in DictUserClient)
                foreach (int Id in kvp.Value)
                    if (Id == ClientID)
                        return kvp.Key;
            return string.Empty;
        }

        private void syncServerCtl1_StatusHandler(StatusMessage sm)
        {
            switch (sm.Status)
            {
                case status.server_client_added:
                    label4.Text = syncServerCtl1.ClientsCount.ToString();
                    notifyIcon1.Text = "Server Started, #Client: " + label4.Text;
                    notifyIcon1.BalloonTipText = "New Client Joined, #Client: " + label4.Text;
                    notifyIcon1.ShowBalloonTip(3);
                    break;
                case status.server_client_removed:
                    DisconnectClient(sm.ID);
                    label4.Text = syncServerCtl1.ClientsCount.ToString();
                    notifyIcon1.Text = "Server Started, #Client: " + label4.Text;
                    notifyIcon1.BalloonTipText = "Client Disconnected, #Client: " + label4.Text;
                    notifyIcon1.ShowBalloonTip(3);
                    break;
                case status.info:
                    switch (sm.CodeMessage)
                    {
                        case "server_started":
                            label3.Text = "Server Started";
                            label4.Text = "0";
                            pictureBox1.Image = Resources.gear_run;
                            notifyIcon1.Icon = Resources.gear_run1;
                            notifyIcon1.Text = "BxEventServer Started, #Client: 0";
                            notifyIcon1.BalloonTipText = "Server Started";
                            numericUpDown1.Enabled = false;
                            button1.Text = "Stop";
                            notifyIcon1.ShowBalloonTip(3);
                            DictUserListener.Clear();
                            //CreateWarningLetter();
                            timer1.Enabled = true;
                            break;
                        case "method_Stop":
                            label3.Text = "Server Stopped";
                            button1.Text = "Start";
                            numericUpDown1.Enabled = true;
                            pictureBox1.Image = Resources.gear_stop;
                            notifyIcon1.Icon = Resources.gear_stop1;
                            notifyIcon1.BalloonTipText = "Server Stopped";
                            notifyIcon1.ShowBalloonTip(3);
                            timer1.Enabled = false;
                            break;
                    }
                    break;
                case status.error:
                    if (sm.CodeMessage == "method_StartServer")
                    {
                        pictureBox1.Image = Resources.gear_stop;
                        notifyIcon1.Icon = Resources.gear_stop1;
                        notifyIcon1.Text = "Server Stopped";
                        numericUpDown1.Enabled = true;
                        label3.Text = "Server Stopped";
                        label4.Text = "0";
                        button1.Text = "Start";
                        notifyIcon1.BalloonTipText = "Server Cannot started, Port Already Used !";
                        notifyIcon1.ShowBalloonTip(5);
                    }
                    break;
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (syncServerCtl1.ClientsCount > 0 && MessageBox.Show(
                    "Ada " + syncServerCtl1.ClientsCount.ToString() +
                    " klien yang sedang terkoneksi. Matikan Server ?",
                    "Konfirmasi Mematikan Event Server", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.No)
                e.Cancel = true;
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Visible = true;
            WindowState = FormWindowState.Normal;
            Activate();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //if (DateTime.Now.Hour == 12)
            //    CreateWarningLetter();
        }

        private void CreateWarningLetter()
        {
            string SqlQuery = "SELECT * FROM ViewWarningList WHERE AutoWarningLetter=1";

            DataPersistance Dp = BaseFramework.DefaultDp;
            IList<ViewWarningList> ListWl =
                Dp.ListFastLoadEntitiesUsingSqlSelect<ViewWarningList>(
                null, SqlQuery, string.Empty);

            if (ListWl.Count == 0) return;

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
                if (vwl.KodeGudang.Length > 0)
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
            SqlQuery = string.Concat("SELECT * FROM (",
                sb.ToString().Substring(10), @") x LEFT JOIN WarningLetter wl ON 
                x.NamaPeringatan=wl.WarningName AND x.NoDokumen=wl.ReffDocNumber 
                WHERE wl.WarningName IS NULL AND TglAkhir<",
                Dp.FormatSqlValue(DateTime.Today));
            IList<clsWarningList> ListWarning = Dp
                    .ListFastLoadEntitiesUsingSqlSelect<clsWarningList>(null,
                    SqlQuery, "NamaPeringatan,TglAkhir");
           try
            {
                //using (EntityTransaction tr = new EntityTransaction(Dp))
                //{
                //    foreach (clsWarningList wl in ListWarning)
                //    {
                //        SuratPeringatan sp = new SuratPeringatan(NoReg,
                //            wl.NamaPeringatan);
                //        sp.SaveNew();
                //        new WarningLetter(wl.NamaPeringatan,
                //            sp.NoSuratPeringatan, wl.NoDokumen).SaveNew();
                //    }
                //    tr.CommitTransaction();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Pembuatan SP", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach(KeyValuePair<string, List<int>> kvp in DictUserClient)
            {
                if (kvp.Value.Count > 0)
                {
                    sb.Append(kvp.Key).Append(": ");
                    foreach (int ClientId in kvp.Value)
                        sb.Append(ClientId).Append(",");
                    sb.Length = sb.Length - 1;
                    sb.AppendLine();
                }
            }
            if (sb.Length == 0) sb.Append("Tidak ada user yang aktif");
            MessageBox.Show(sb.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "*")
                foreach (KeyValuePair<string, List<int>> kvp in DictUserClient)
                {
                    foreach (int ClientId in kvp.Value)
                        syncServerCtl1.Send(ClientId, new DtPopUpMessageReceive("Server", "Tes Message",
                            "Hello " + kvp.Key, null, enMessageType.Information));
                }
            else
            {
                List<int> ListInt;
                if (DictUserClient.TryGetValue(textBox1.Text.Trim().ToLower(), 
                    out ListInt) && ListInt.Count > 0)
                    foreach (int ClientId in ListInt)
                        syncServerCtl1.Send(ClientId, new DtPopUpMessageReceive("Server", "Tes Message",
                            "Hello " + textBox1.Text, null, enMessageType.Information));
                else
                    MessageBox.Show("Nama User tidak ditemukan !");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DictSqlWarning.Clear();
        }

        //private string BuildWelcomeMessage()
        //{
        //    return string.Empty;
        //}
    }
}
