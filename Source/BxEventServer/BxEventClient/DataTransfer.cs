using System;
using System.Collections.Generic;
using System.Text;

namespace BxEventClient
{
    public enum enMessageType
    {
        Information,
        Warning,
        Error,
        Chat,
        InternalMessage
    }

    [Serializable]
    public sealed class DtLogin
    {
        public readonly string UserName;
        public readonly string Password;

        public DtLogin() { }

        public DtLogin(string UserName, string Password)
        {
            this.UserName = UserName;
            this.Password = Password;
        }
    }

    [Serializable]
    public sealed class DtPopUpMessageReceive
    {
        public string Sender;
        public string Caption;
        public string Message;
        public object MessageData;
        public enMessageType MessageType;

        public DtPopUpMessageReceive() { }

        public DtPopUpMessageReceive(string Sender, string Caption, 
            string Message, object MessageData, enMessageType MessageType)
        {
            this.Sender = Sender;
            this.Caption = Caption;
            this.Message = Message;
            this.MessageData = MessageData;
            this.MessageType = MessageType;
        }
    }

    [Serializable]
    public sealed class DtPopUpMessageSend
    {
        public readonly string[] UserName;
        public readonly string Caption;
        public readonly string Message;
        public readonly enMessageType MessageType;
        public readonly bool GuarantedDelivery;
        public readonly object MessageData;

        public DtPopUpMessageSend() { }

        public DtPopUpMessageSend(string Caption, string Message,
            object MessageData, enMessageType MessageType,  
            bool GuarantedDelivery, params string[] UserName)
        {
            this.Caption = Caption;
            this.Message = Message;
            this.MessageData = MessageData;
            this.MessageType = MessageType;
            this.GuarantedDelivery = GuarantedDelivery;
            this.UserName = UserName;
        }
    }

    [Serializable]
    public sealed class DtRegisterUserListener
    {
        public readonly string[] EventName;

        public DtRegisterUserListener() { }

        public DtRegisterUserListener(params string[] EventName)
        {
            this.EventName = EventName;
        }
    }

    [Serializable]
    public sealed class DtRegisterActionListener
    {
        public readonly string[] EventName;

        public DtRegisterActionListener() { }

        public DtRegisterActionListener(params string[] EventName)
        {
            this.EventName = EventName;
        }
    }

    [Serializable]
    public sealed class DtRaiseEventData
    {
        public readonly string EventName;
        public readonly object EventData;

        public DtRaiseEventData() { }

        public DtRaiseEventData(string EventName, object EventData)
        {
            this.EventName = EventName;
            this.EventData = EventData;
        }
    }
}
