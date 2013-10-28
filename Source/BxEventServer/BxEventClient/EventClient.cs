using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using DotNetRemoting;
using System.ComponentModel.Design;
using System.Windows.Forms;
using BxEventClient.Properties;
using System.Drawing;
using SentraWinFramework;
using SentraSolutionFramework.Persistance;
using BxEventClient.Warning;
using DevExpress.XtraBars.Alerter;
using SentraSolutionFramework;
using SentraSolutionFramework.Entity;
using DevExpress.XtraEditors;
using SentraSecurity;
using SentraUtility;

namespace BxEventClient
{
    public delegate void EventAction();
    public delegate void EventRaised(string EventName, object EventData);

    public partial class EventClient : Component
    {
        private ContainerControl _ContainerControl;
        [Browsable(false)]
        public ContainerControl ContainerControl
        {
            get { return _ContainerControl; }
            set { _ContainerControl = value; }
        }

        public EventClient()
        {
            InitializeComponent();
        }

        void BaseFramework_onEntityAction(BaseEntity ActionEntity, 
            enEntityActionMode ActionMode)
        {
            if (!BaseFramework.UseEventServer) return;
            switch (ActionMode)
            {
                case enEntityActionMode.AfterSaveNew:
                case enEntityActionMode.AfterSaveUpdate:
                    SendEntityChanged(ActionEntity);
                    break;
            }
        }

        public void SendEntityChanged(BaseEntity ActionEntity)
        {
            if (ActionEntity.GetType().Assembly.FullName
                .StartsWith("Sentra")) return;

            TableDef td = MetaData.GetTableDef(ActionEntity);
            if (td.KeyFields.Count == 0) return;
            int Ctr = 0;
            FieldDef KeyField = null;
            foreach (FieldDef fld in td.KeyFields.Values)
                if (Ctr++ == 0 ||
                    fld.FieldName.StartsWith("No",
                    StringComparison.OrdinalIgnoreCase))
                    KeyField = fld;
            if (KeyField == null) return;
            if (KeyField.DataType == DataType.VarChar)
            {
                string KeyValue = (string)KeyField.GetValue(ActionEntity);
                syncClientCtl1.Send(new DtPopUpMessageSend("_Warning_",
                    KeyValue, td.TableName, enMessageType.InternalMessage, true));
            }
        }

        public void TestEntityChanged(string KeyValue, string TableName)
        {
            syncClientCtl1.Send(new DtPopUpMessageSend("_Warning_",
                KeyValue, TableName, enMessageType.InternalMessage, true));
        }

        public EventClient(IContainer container)
        {
            container.Add(this);
            InitializeComponent();

            if (!BaseUtility.IsDebugMode)
            {
                BaseFramework.onEntityAction += new EntityAction(BaseFramework_onEntityAction);
                BaseFramework.onDefaultDPChanged += new EventHandler(BaseFramework_onDefaultDPChanged);
                BaseSecurity.CurrentLogin.onLogon += new LogonChanged(CurrentLogin_onLogon);
                BaseSecurity.CurrentLogin.OnLogout += new LogonChanged(CurrentLogin_OnLogout);
                BaseFramework.ConnectEventServer = true;
                onConnectTimeOut += new EventAction(EventClient_onDisconnected);
                onDisconnected += new EventAction(EventClient_onDisconnected);
                onConnected += new EventAction(EventClient_onConnected);
            }
        }

        void CurrentLogin_OnLogout()
        {
            DisconnectServer();
        }

        void EventClient_onConnected()
        {
            if (BaseFramework.ConnectEventServer && BaseFramework.UseEventServer)
                ShowWarningList();
        }

        void CurrentLogin_onLogon()
        {
            if (BaseFramework.ConnectEventServer && BaseFramework.UseEventServer)
            {
                _UserName = BaseSecurity.CurrentLogin.CurrentUser;
                _Password = BaseSecurity.CurrentLogin.CurrentPassword;
                ConnectServer();
            }
        }

        void EventClient_onDisconnected()
        {
            if (!BaseSecurity.CurrentLogin.IsLogged()) return;
            if (XtraMessageBox.Show("Server Gagal dihubungi. Ulangi Koneksi ?",
                "Gagal Koneksi ke server", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
                ConnectServer();
            else
            {
                if (XtraMessageBox.Show("Keluar Aplikasi ?", "Konfirmasi Keluar Aplikasi",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    ((Form)ContainerControl).Close();
                else
                    ConnectServer();
            }
        }

        void BaseFramework_onDefaultDPChanged(object sender, EventArgs e)
        {
            SqlServerPersistance Sqlp = (SqlServerPersistance)BaseFramework.DefaultDp;

            frmWarningList.SDMDp = new SqlServerPersistance(Sqlp.ServerName,
                "Barindo SDM", false, string.Empty, Sqlp.IntegratedSecurity,
                Sqlp.UserName, Sqlp.Password);
            _ServerAddress = Sqlp.ServerName.Replace("\\SqlExpress", string.Empty);
            if (_ServerAddress.Length <= 1) _ServerAddress = "127.0.0.1";
        }

        public override ISite Site
        {
            set
            {
                base.Site = value;
                if (value != null)
                {
                    IDesignerHost service = value.GetService(typeof(IDesignerHost)) as IDesignerHost;
                    if (service != null)
                    {
                        IComponent rootComponent = service.RootComponent;
                        if (rootComponent is ContainerControl)
                            _ContainerControl = (ContainerControl)rootComponent;
                    }
                }
            }
        }

        public event EventAction onDisconnected;
        public event EventAction onConnected;
        public event EventAction onConnectTimeOut;
        public event EventRaised onEventRaised;
        public event EventRaised onMessageReceived;

        private string _ServerAddress = "127.0.0.1";
        [DefaultValue("127.0.0.1")]
        public string ServerAddress
        {
            get { return _ServerAddress; }
            set { _ServerAddress = value; }
        }

        private int _ServerPort = 4000;
        [DefaultValue(4000)]
        public int ServerPort
        {
            get { return _ServerPort; }
            set { _ServerPort = value; }
        }

        private static string _UserName = "Guest";
        [DefaultValue("Guest")]
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        private string _Password = string.Empty;
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        public void SendPopupMessage(string Caption, string Message, 
            enMessageType MessageType, bool GuarantedDelivery,
            params string[] UserName)
        {
            syncClientCtl1.Send(new DtPopUpMessageSend(Caption, Message, null, 
                MessageType, GuarantedDelivery, UserName));
        }

        public void RaiseEvent(string EventName, object EventData)
        {
            syncClientCtl1.Send(new DtRaiseEventData(EventName, EventData));
        }

        public void RegisterActionListener(params string[] EventName)
        {
            syncClientCtl1.Send(new DtRegisterActionListener(EventName));
        }

        public void RegisterUserListener(params string[] EventName)
        {
            syncClientCtl1.Send(new DtRegisterUserListener(EventName));
        }

        private void CreateSyncClient()
        {
            syncClientCtl1 = new SyncClientCtl();
            syncClientCtl1.AutoReconnect = true;
            syncClientCtl1.DataReceivedHandler += new ObjDelegate(syncClientCtl1_DataReceivedHandler);
            syncClientCtl1.StatusHandler += new StatDelegate(syncClientCtl1_StatusHandler);
            _ContainerControl.Controls.Add(syncClientCtl1);
        }

        public void ConnectServer(string ServerAddress, int ServerPort, 
            string UserName, string Password)
        {
            if (GetDp().Find.IsExists(@"SELECT UserName FROM _System_User 
                WHERE UserName=@nm AND UserPassword=@pwd AND IsActive=1",
                new FieldParam("nm", UserName),
                new FieldParam("pwd", Password)))
            {

                _ServerAddress = ServerAddress;
                _ServerPort = ServerPort;
                _UserName = UserName;
                _Password = Password;
                ConnectServer();
            }
            else
                throw new ApplicationException("Nama User/ Password tidak sesuai");
        }
        public void ConnectServer()
        {
            if (syncClientCtl1 == null) CreateSyncClient();
            syncClientCtl1.Connect(_ServerAddress, _ServerPort);
        }
        public void DisconnectServer()
        {
            //_UserName = string.Empty;
            if (syncClientCtl1 != null)
                syncClientCtl1.Disconnect();
        }

        private void syncClientCtl1_DataReceivedHandler(object Data)
        {
            if (Data is bool)
            {
                if ((bool)Data && onConnected != null)
                    onConnected();
                return;
            }
            if (Data is DtPopUpMessageReceive)
            {
                DtPopUpMessageReceive dmr = (DtPopUpMessageReceive)Data;
                Image Img = null;

                switch (dmr.MessageType)
                {
                    case enMessageType.Error:
                        Img = Resources.scroll_error;
                        break;
                    case enMessageType.Information:
                        Img = Resources.scroll_information;
                        break;
                    case enMessageType.Warning:
                        Img = Resources.scroll_warning;
                        break;
                    case enMessageType.Chat:
                        Img = Resources.text_loudspeaker;
                        break;
                }
                if (Img != null)
                    alertControl1.Show((Form)_ContainerControl.Parent,
                        dmr.Caption, dmr.Message, Img);
                if (onMessageReceived != null) onMessageReceived(dmr.Caption, dmr);
                return;
            }

            if (Data is DtRaiseEventData && onEventRaised != null)
            {
                DtRaiseEventData red = (DtRaiseEventData)Data;
                onEventRaised(red.EventName, red.EventData);
                return;
            }

        }

        private enum enStatus
        {
            OnNone,
            OnTimeOut,
            OnConnected,
            OnDisconnected
        }

        private enStatus ConStatus = enStatus.OnNone;
        private void syncClientCtl1_StatusHandler(StatusMessage sm)
        {
            switch (sm.Status)
            {
                case status.timeout:
                    ConStatus = enStatus.OnTimeOut;
                    break;
                case status.disconnected:
                    if (ConStatus == enStatus.OnTimeOut)
                    {
                        if (onConnectTimeOut != null)
                            onConnectTimeOut();
                        ConStatus = enStatus.OnNone;
                    }
                    else
                        ConStatus = enStatus.OnDisconnected;
                    //_UserName = string.Empty;
                    break;
                case status.connected:
                    ConStatus = enStatus.OnConnected;
                    break;
                case status.server_handshake_complete_for_client:
                    if (ConStatus == enStatus.OnConnected)
                        syncClientCtl1.Send(new DtLogin(_UserName, _Password));
                    ConStatus = enStatus.OnNone;
                    break;
                case status.socket_dispose_request:
                    //if (ConStatus == enStatus.OnDisconnected && onDisconnected != null)
                    //    onDisconnected();
                    ConStatus = enStatus.OnNone;
                    break;
                case status.error:
                    if(sm.CodeMessage == "Connect")
                        ConStatus = enStatus.OnTimeOut;
                    break;
            }
        }

        public void ShowWarningList()
        {
            using (new WaitCursor())
            {
                frmWarningList frm = new frmWarningList();
                frm.Show();
            }
        }

        public DataPersistance GetDp()
        {
            return frmWarningList.SDMDp;
        }
    }
}
