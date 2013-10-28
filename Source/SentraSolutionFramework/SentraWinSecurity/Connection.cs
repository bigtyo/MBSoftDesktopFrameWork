using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using System.Data;
using SentraSolutionFramework.Persistance;
using SentraWinFramework;
using SentraUtility;

namespace SentraWinSecurity
{
    internal static class Connection
    {
        private const string mdlName = "Connection";
        private const string mdlData = "Data";
        private const string cEngineName = "EngineName";
        private const string cConnectionString = "ConnStr";
        private const string cAutoCreateDb = "AutoCreateDb";
        private const string cFolderLocation = "FolderLoc";

        public static string[] GetListConnection()
        {
            string[] retVal = BaseWinFramework.LocalConfig
                .GetAllKeys(mdlName);

            if (retVal != null)
                for (int i = 0; i < retVal.Length; i++)
                    retVal[i] = retVal[i].Replace("_", " ");
            return retVal;
        }

        public static bool IsConnectionExist()
        {
            string[] ListConn = BaseWinFramework.LocalConfig.GetAllKeys(mdlName);

            if (ListConn == null) return false;
            return BaseWinFramework.LocalConfig
                .GetAllKeys(mdlName).Length > 0;
        }

        public static void GetConnectionData(
            string ConnectionName,
            out string EngineName, out string ConnectionString,
            out bool AutoCreateDb, out string FolderLocation)
        {
            ConnectionName = ConnectionName.Replace(" ", "_");

            XmlConfig cfg = BaseWinFramework.LocalConfig;
            string thisMdl = cfg.ReadString(mdlName,
                ConnectionName, ConnectionName + mdlData);

            EngineName = cfg.ReadString(thisMdl, 
                cEngineName, string.Empty);
            ConnectionString = cfg.ReadString(thisMdl,
                cConnectionString, string.Empty);
            AutoCreateDb = cfg.ReadBool(thisMdl,
                cAutoCreateDb, false);
            FolderLocation = cfg.ReadString(thisMdl,
                cFolderLocation, string.Empty);
        }

        public static void AddConnection(string ConnectionName,
            string EngineName, string ConnectionString, bool AutoCreateDb,
            string FolderLocation)
        {
            ConnectionName = ConnectionName.Replace(" ", "_");

            XmlConfig cfg = BaseWinFramework.LocalConfig;
            string thisMdl = ConnectionName + mdlData;

            cfg.WriteString(mdlName, ConnectionName, thisMdl);
            
            cfg.WriteString(thisMdl,
                cEngineName, EngineName);
            cfg.WriteString(thisMdl,
                cConnectionString, ConnectionString);
            cfg.WriteBool(thisMdl,
                cAutoCreateDb, AutoCreateDb);
            cfg.WriteString(thisMdl,
                cFolderLocation, FolderLocation);
            cfg.Save();
        }

        public static void EditConnection(string ConnectionName,
            string NewConnectionName, string EngineName,
            string ConnectionString, bool AutoCreateDb,
            string FolderLocation)
        {
            ConnectionName = ConnectionName.Replace(" ", "_");
            XmlConfig cfg = BaseWinFramework.LocalConfig;
            string thisMdl = ConnectionName + mdlData;

            cfg.WriteString(mdlName, ConnectionName, thisMdl);

            cfg.WriteString(thisMdl,
                cEngineName, EngineName);
            cfg.WriteString(thisMdl,
                cConnectionString, ConnectionString);
            cfg.WriteBool(thisMdl,
                cAutoCreateDb, AutoCreateDb);
            cfg.WriteString(thisMdl,
                cFolderLocation, FolderLocation);
            cfg.Save();
        }

        public static void RemoveConnection(string ConnectionName)
        {
            ConnectionName = ConnectionName.Replace(" ", "_");

            XmlConfig cfg = BaseWinFramework.LocalConfig;
            cfg.RemoveVariable(mdlName, ConnectionName);
            cfg.RemoveSection(ConnectionName + mdlData);
            cfg.Save();
        }
    }
}
