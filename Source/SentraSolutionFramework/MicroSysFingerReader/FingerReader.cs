using System;
using System.Collections.Generic;
using System.Text;
using GrFingerXLib;
//using Microsoft.VisualBasic.Compatibility.VB6;
using System.Runtime.InteropServices;
using System.Data;
using System.Drawing;
using stdole;

namespace MicroSysFingerReader
{
    public enum enImageQuality
    {
        BadQuality,
        MediumQuality,
        HighQuality
    }

    public enum enSensorChanged
    {
        SensorPlugged,
        SensorUnplugged,
        SensorStarted,
        SensorStopped
    }

    public delegate void ImageAcquired(string SensorId, Image Img,
        byte[] Template, enImageQuality ImageQuality);
    public delegate void SensorChanged(string SensorId, enSensorChanged ChangeMode);

    public class FingerReader
    {
        public List<string> ListOfSensorId = new List<string>();

        private List<string> ListStartedCapture = new List<string>();
        private GrFingerXCtrl fgr = new GrFingerXCtrlClass();

        public event ImageAcquired onImageAcquired;
        public event SensorChanged onSensorChanged;

        public FingerReader()
        {
            fgr.ImageAcquired += new _IGrFingerXCtrlEvents_ImageAcquiredEventHandler(fgr_ImageAcquired);
            fgr.SensorPlug += new _IGrFingerXCtrlEvents_SensorPlugEventHandler(fgr_SensorPlug);
            fgr.SensorUnplug += new _IGrFingerXCtrlEvents_SensorUnplugEventHandler(fgr_SensorUnplug);
            fgr.Initialize();
            fgr.CapInitialize();
        }

        #region Reader Events
        void fgr_SensorUnplug(string idSensor)
        {
            ListOfSensorId.Remove(idSensor);
            ListStartedCapture.Remove(idSensor);

            if (onSensorChanged != null)
                onSensorChanged(idSensor, enSensorChanged.SensorUnplugged);
        }

        void fgr_SensorPlug(string idSensor)
        {
            if (idSensor != "File")
            {
                ListOfSensorId.Add(idSensor);
                if (onSensorChanged != null)
                    onSensorChanged(idSensor, enSensorChanged.SensorPlugged);
            }
        }

        void fgr_ImageAcquired(string idSensor, int width, int height,
            ref object rawImage, int res)
        {
            if (onImageAcquired != null)
            {
                enImageQuality ImageQuality;
                onImageAcquired(idSensor, GetImage(width, height, rawImage, res),
                    ExtractTemplate(width, height, rawImage, res,
                    out ImageQuality), ImageQuality);
            }
        }
        #endregion

        public bool IsSensorStarted(string SensorId)
        {
            return ListStartedCapture.Contains(SensorId);
        }
        public bool IsSensorStarted()
        {
            if (ListOfSensorId.Count == 0) return false;
            return ListStartedCapture.Contains(ListOfSensorId[0]);
        }

        public bool IsSensorPlugged(string SensorId)
        {
            return ListOfSensorId.Contains(SensorId);
        }
        public bool IsSensorPlugged()
        {
            return ListOfSensorId.Count > 0;
        }

        public bool StartCapture(string SensorId)
        {
            int i = fgr.CapStartCapture(SensorId);
            if (i < 0) return false;
            ListStartedCapture.Add(SensorId);
            if (onSensorChanged != null)
                onSensorChanged(SensorId, enSensorChanged.SensorStarted);
            return true;
        }
        public bool StartCapture()
        {
            if (ListOfSensorId.Count > 0)
            {
                string SensorId = ListOfSensorId[0];
                int i = fgr.CapStartCapture(SensorId);
                if (i < 0) return false;
                ListStartedCapture.Add(SensorId);
                if (onSensorChanged != null)
                    onSensorChanged(SensorId, enSensorChanged.SensorStarted);
                return true;
            }
            return false;
        }

        public void StopCapture(string SensorId)
        {
            if (ListStartedCapture.Contains(SensorId))
            {
                fgr.CapStopCapture(SensorId);
                ListStartedCapture.Remove(SensorId);
                if (onSensorChanged != null)
                    onSensorChanged(SensorId, enSensorChanged.SensorStopped);
            }
        }
        public void StopCapture()
        {
            if (ListOfSensorId.Count > 0)
            {
                string SensorId = ListOfSensorId[0];
                fgr.CapStopCapture(SensorId);
                ListStartedCapture.Remove(SensorId);
                if (onSensorChanged != null)
                    onSensorChanged(SensorId, enSensorChanged.SensorStopped);
            }
        }

        #region SearchFinger
        public bool SearchFinger(IDbConnection Connection, string KeyField,
            string TemplateField, string TableName, string DataFilter,
            byte[] FingerTemplate, out string KeyValue, out int Score)
        {
            Array TmpArry = FingerTemplate;
            fgr.IdentifyPrepare(ref TmpArry, 0);

            bool MustClose;
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
                MustClose = true;
            }
            else
                MustClose = false;

            try
            {
                IDbCommand Cmd = Connection.CreateCommand();
                if (DataFilter.Length > 0) DataFilter = " WHERE " + DataFilter;
                Cmd.CommandText = string.Concat("SELECT ", KeyField, ",", TemplateField,
                    " FROM ", TableName, DataFilter);
                Cmd.CommandType = CommandType.Text;
                Cmd.CommandTimeout = 1000;

                IDataReader rdr = Cmd.ExecuteReader();
                try
                {
                    Score = 0;
                    while (rdr.Read())
                    {
                        TmpArry = (Array)rdr[1];
                        if (TmpArry == null) continue;

                        int Result = fgr.Identify(ref TmpArry, ref Score, 0);

                        if (Result == (int)GRConstants.GR_MATCH)
                        {
                            KeyValue = rdr.GetString(0);
                            return true;
                        }
                    }
                }
                finally
                {
                    rdr.Close();
                }
                KeyValue = string.Empty;
                return false;
            }
            finally
            {
                if (MustClose) Connection.Close();
            }
        }

        public bool SearchFinger(DataTable dt, byte[] FingerTemplate,
            out string KeyValue, out int Score)
        {
            Array TmpArry = FingerTemplate;
            fgr.IdentifyPrepare(ref TmpArry, 0);

            Score = 0;
            foreach (DataRow dr in dt.Rows)
            {
                TmpArry = (Array)dr[1];
                if (TmpArry == null) continue;

                int Result = fgr.Identify(ref TmpArry, ref Score, 0);

                if (Result == (int)GRConstants.GR_MATCH)
                {
                    KeyValue = (string)dr[0];
                    return true;
                }
            }
            KeyValue = string.Empty;
            return false;
        }

        public bool SearchFinger(ICollection<FingerTemplate> ListFinger,
            byte[] FingerTemplate, out FingerTemplate FoundFinger, out int Score)
        {
            Array TmpArry = FingerTemplate;
            fgr.IdentifyPrepare(ref TmpArry, 0);

            Score = 0;
            foreach (FingerTemplate fg in ListFinger)
            {
                TmpArry = (Array)fg.TemplateValue;
                int Result = fgr.Identify(ref TmpArry, ref Score, 0);

                if (Result == (int)GRConstants.GR_MATCH)
                {
                    FoundFinger = fg;
                    return true;
                }
            }
            FoundFinger = null;
            return false;
        }
        #endregion

        #region SearchAllFinger
        public bool SearchAllFinger(IDbConnection Connection, string KeyField,
            string FingerTemplateFields, string TableName, string DataFilter,
            byte[] FingerTemplate, out string KeyValue, out int Score, 
            out enFingerName FingerName)
        {
            Array TmpArry = FingerTemplate;
            fgr.IdentifyPrepare(ref TmpArry, 0);

            bool MustClose;
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
                MustClose = true;
            }
            else
                MustClose = false;

            try
            {
                IDbCommand Cmd = Connection.CreateCommand();
                if (DataFilter.Length > 0) DataFilter = " WHERE " + DataFilter;

                Cmd.CommandText = string.Concat("SELECT ", KeyField, ",",
                    FingerTemplateFields, " FROM ", TableName, DataFilter);
                Cmd.CommandType = CommandType.Text;
                Cmd.CommandTimeout = 1000;

                IDataReader rdr = Cmd.ExecuteReader();
                try
                {
                    Score = 0;
                    while (rdr.Read())
                    {
                        for (int i = 1; i <= 10; i++)
                        {
                            TmpArry = (Array)rdr[i];
                            if (TmpArry == null) continue;

                            int Result = fgr.Identify(ref TmpArry, ref Score, 0);

                            if (Result == (int)GRConstants.GR_MATCH)
                            {
                                KeyValue = rdr.GetString(0);
                                FingerName = (enFingerName)i;
                                return true;
                            }
                        }
                    }
                }
                finally
                {
                    rdr.Close();
                }
                KeyValue = string.Empty;
                FingerName = enFingerName.Undefined;
                return false;
            }
            finally
            {
                if (MustClose) Connection.Close();
            }
        }

        /// <summary>
        /// DataRow Field Must be : KeyField, Finger1,...Finger10
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="FingerTemplate"></param>
        /// <param name="KeyValue"></param>
        /// <param name="Score"></param>
        /// <returns></returns>
        public bool SearchAllFinger(DataTable dt, byte[] FingerTemplate,
            out string KeyValue, out int Score, out enFingerName FingerName)
        {
            Array TmpArry = FingerTemplate;
            fgr.IdentifyPrepare(ref TmpArry, 0);

            Score = 0;
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 1; i <= 10; i++)
                {
                    TmpArry = (Array)dr[i];
                    if (TmpArry == null) continue;

                    int Result = fgr.Identify(ref TmpArry, ref Score, 0);

                    if (Result == (int)GRConstants.GR_MATCH)
                    {
                        KeyValue = (string)dr[0];
                        FingerName = (enFingerName)i;
                        return true;
                    }
                }
            }
            KeyValue = string.Empty;
            FingerName = enFingerName.Undefined;
            return false;
        }

        public bool SearchAllFinger(ICollection<AllFingerTemplate> ListFinger,
            byte[] FingerTemplate, out AllFingerTemplate FoundFinger, 
            out int Score, out enFingerName FingerName)
        {
            Array TmpArry = FingerTemplate;
            fgr.IdentifyPrepare(ref TmpArry, 0);

            Score = 0;
            foreach (AllFingerTemplate fgs in ListFinger)
            {
                int i = 1;
                foreach (Array Arry in fgs.TemplateValues)
                {
                    if (Arry == null) continue;

                    TmpArry = Arry;
                    int Result = fgr.Identify(ref TmpArry, ref Score, 0);
                    if (Result == (int)GRConstants.GR_MATCH)
                    {
                        FoundFinger = fgs;
                        FingerName = (enFingerName)i;
                        return true;
                    }
                    i++;
                }
            }
            FoundFinger = null;
            FingerName = enFingerName.Undefined;
            return false;
        }
        #endregion

        public bool FindMatchFinger(out int Score, out int Index1, out int Index2,
            params byte[][] Fingers)
        {
            int NumData = Fingers.Length;
            Score = -1;
            for (int i = 0; i < NumData - 1; i++)
            {
                Array TmpArry = Fingers[i];
                if (TmpArry == null) continue;
                fgr.IdentifyPrepare(ref TmpArry, 0);

                for (int j = i + 1; j < NumData; j++)
                {
                    TmpArry = Fingers[j];
                    if (fgr.Identify(ref TmpArry, ref Score, 0) == (int)GRConstants.GR_MATCH)
                    {
                        Index1 = i;
                        Index2 = j;
                        return true;
                    }
                }
            }
            Index1 = -1;
            Index2 = -1;
            return false;
        }

        public bool VerifyFinger(byte[] Finger1, byte[] Finger2, out int Score)
        {
            Array arr1 = Finger1, arr2 = Finger2;
            Score = 0;
            return fgr.Verify(ref arr1, ref arr2, ref Score, 0) ==
                (int)GRConstants.GR_MATCH;
        }

        #region private Image GetImage(...)
        [DllImport("user32.dll", EntryPoint = "GetDC")]
        private static extern IntPtr GetDC(IntPtr ptr);

        [DllImport("user32.dll", EntryPoint = "ReleaseDC")]
        private static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);

        private Image GetImage(int width, int height,
            object rawImage, int res)
        {
            Image RetImage;

            // screen HDC
            IntPtr hdc = GetDC(System.IntPtr.Zero);

            IPictureDisp ImagePict = null;

            // get raw image
            fgr.CapRawImageToHandle(ref rawImage, width,
                height, hdc.ToInt32(), ref ImagePict);

            //RetImage = Support.IPictureDispToImage(ImagePict);
            RetImage = ImageConverter.IpictureToImage(ImagePict);

            // release screen HDC
            ReleaseDC(System.IntPtr.Zero, hdc);

            return RetImage;
        }
        #endregion

        private byte[] ExtractTemplate(int width, int height,
            object rawImage, int res, out enImageQuality ImageQuality)
        {
            int size = (int)GRConstants.GR_MAX_SIZE_TEMPLATE;
            Array TmpArray = new byte[(int)GRConstants.GR_MAX_SIZE_TEMPLATE];
            switch ((GRConstants)fgr.Extract(ref rawImage, width, height, res,
                ref TmpArray, ref size,
                (int)GRConstants.GR_DEFAULT_CONTEXT))
            {
                case GRConstants.GR_HIGH_QUALITY:
                    ImageQuality = enImageQuality.HighQuality;
                    break;
                case GRConstants.GR_MEDIUM_QUALITY:
                    ImageQuality = enImageQuality.MediumQuality;
                    break;
                default:
                    ImageQuality = enImageQuality.BadQuality;
                    break;
            }

            byte[] retVal = (byte[])TmpArray;
            Array.Resize<byte>(ref retVal, size);
            return retVal;
        }
    }

    public enum enFingerName
    {
        Undefined,
        LeftPinkie,
        LeftRing,
        LeftMiddle,
        LeftIndex,
        LeftThumb,
        RightThumb,
        RightIndex,
        RightMiddle,
        RightRing,
        RightPinkie
    }

    public class FingerTemplate
    {
        public string FingerId;
        public enFingerName FingerName;
        public byte[] TemplateValue;

        public FingerTemplate(string FingerId, enFingerName FingerName,
            byte[] TemplateValue)
        {
            this.FingerId = FingerId;
            this.FingerName = FingerName;
            this.TemplateValue = TemplateValue;
        }
    }

    public class AllFingerTemplate
    {
        public string FingerId;
        public ICollection<byte[]> TemplateValues;

        public AllFingerTemplate(string FingerId, ICollection<byte[]> TemplateValues)
        {
            this.FingerId = FingerId;
            this.TemplateValues = TemplateValues;
        }
    }
}
