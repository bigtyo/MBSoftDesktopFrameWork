using System;
using System.Xml;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace SentraUtility
{
    [DebuggerNonUserCode]
    public class XmlConfig
    {
        XmlDocument doc;
        string fileName;
        string rootName;

        public XmlConfig(string fileName, string rootName)
        {
            this.fileName = fileName;
            this.rootName = rootName.Replace(" ", string.Empty);

            doc = new XmlDocument();

            try
            {
                doc.Load(fileName);
            }
            catch
            {
                CreateSettingsDocument();
            }
        }

        public XmlConfig(string fileName)
            : this(fileName, "Settings") { }

        //Deletes all entries of a section
        public void ClearSection(string section)
        {
            XmlNode root = doc.DocumentElement;
            XmlNode s = root.SelectSingleNode('/' + rootName + '/' + section);

            if (s != null) s.RemoveAll();
        }

        //initializes a new settings file with the XML version
        //and the root node
        protected void CreateSettingsDocument()
        {
            doc.AppendChild(doc.CreateXmlDeclaration("1.0", null, null));
            doc.AppendChild(doc.CreateElement(rootName));
        }

        //removes a section and all its entries
        public void RemoveSection(string section)
        {
            XmlNode root = doc.DocumentElement;
            XmlNode s = root.SelectSingleNode('/' + rootName + '/' + section);

            if (s != null)
                root.RemoveChild(s);
        }

        public void RemoveVariable(string section, string name)
        {
            XmlNode root = doc.DocumentElement;

            XmlNode s = root.SelectSingleNode('/' + rootName + '/' + section);
            if (s == null) return;

            XmlNode n = s.SelectSingleNode(name);
            if (n != null) s.RemoveChild(n);
        }

        public void Save()
        {
            string Folder = fileName.Substring(0, 
                fileName.LastIndexOf('\\'));

            try
            {
                if (!Directory.Exists(Folder))
                    Directory.CreateDirectory(Folder);
            }
            catch { }
            int i = 0;
            Ulang:
            try
            {
                doc.Save(fileName);
            }
            catch
            {
                if (i++ < 10)
                {
                    Thread.Sleep(250);
                    goto Ulang;
                }
            }
        }

        #region Read methods
        public bool ReadBool(string section, string name, bool defaultValue)
        {
            string s = ReadString(section, name, string.Empty);

            if (s == Boolean.TrueString)
                return true;
            else if (s == Boolean.FalseString)
                return false;
            else
                return defaultValue;
        }
        public DateTime ReadDateTime(string section, string name, DateTime defaultValue)
        {
            string s = ReadString(section, name, string.Empty);

            if (s.Length == 0)
                return defaultValue;
            else
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(s);
                    return dt;
                }
                catch (FormatException)
                {
                    return defaultValue;
                }
            }
        }
        public double ReadDouble(string section, string name, double defaultValue)
        {
            string s = ReadString(section, name, string.Empty);

            if (s.Length == 0)
                return defaultValue;
            else
            {
                try
                {
                    double d = Convert.ToDouble(s);
                    return d;
                }
                catch (FormatException)
                {
                    return defaultValue;
                }
            }
        }
        public float ReadFloat(string section, string name, float defaultValue)
        {
            string s = ReadString(section, name, string.Empty);

            if (s.Length == 0)
                return defaultValue;
            else
            {
                try
                {
                    float f = Convert.ToSingle(s);
                    return f;
                }
                catch (FormatException)
                {
                    return defaultValue;
                }
            }
        }
        public int ReadInt(string section, string name, int defaultValue)
        {
            string s = ReadString(section, name, string.Empty);

            if (s.Length == 0)
                return defaultValue;
            else
            {
                try
                {
                    int n = Convert.ToInt32(s);
                    return n;
                }
                catch (FormatException)
                {
                    return defaultValue;
                }
            }
        }
        public long ReadLong(string section, string name, long defaultValue)
        {
            string s = ReadString(section, name, string.Empty);

            if (s.Length == 0)
                return defaultValue;
            else
            {
                try
                {
                    long l = Convert.ToInt64(s);
                    return l;
                }
                catch (FormatException)
                {
                    return defaultValue;
                }
            }
        }
        public short ReadShort(string section, string name, short defaultValue)
        {
            string s = ReadString(section, name, string.Empty);

            if (s.Length == 0)
                return defaultValue;
            else
            {
                try
                {
                    short n = Convert.ToInt16(s);
                    return n;
                }
                catch (FormatException)
                {
                    return defaultValue;
                }
            }
        }
        public string[] GetAllKeys(string section)
        {
            XmlNode root = doc.DocumentElement;
            XmlNode s = root.SelectSingleNode('/' + rootName + '/' + section);

            if (s == null) return new string[0];
            string[] retVal = new string[s.ChildNodes.Count];
            for (int i = 0; i < s.ChildNodes.Count; i++)
                retVal[i] = s.ChildNodes[i].Name;
            return retVal;
        }
        public string ReadString(string section, string name, string defaultValue)
        {
            XmlNode root = doc.DocumentElement;
            XmlNode s = root.SelectSingleNode('/' + rootName + '/' + section);

            if (s == null)
                return defaultValue;  //not found

            XmlNode n = s.SelectSingleNode(name);

            if (n == null)
                return defaultValue;  //not found

            XmlAttributeCollection attrs = n.Attributes;

            foreach (XmlAttribute attr in attrs)
            {
                if (attr.Name == "value")
                    return attr.Value;
            }

            return defaultValue;
        }
        public uint ReadUInt(string section, string name, uint defaultValue)
        {
            string s = ReadString(section, name, string.Empty);

            if (s.Length == 0)
                return defaultValue;
            else
            {
                try
                {
                    uint n = Convert.ToUInt32(s);
                    return n;
                }
                catch (FormatException)
                {
                    return defaultValue;
                }
            }
        }
        public ulong ReadULong(string section, string name, ulong defaultValue)
        {
            string s = ReadString(section, name, string.Empty);

            if (s.Length == 0)
                return defaultValue;
            else
            {
                try
                {
                    ulong l = Convert.ToUInt64(s);
                    return l;
                }
                catch (FormatException)
                {
                    return defaultValue;
                }
            }
        }
        public ushort ReadUShort(string section, string name, ushort defaultValue)
        {
            string s = ReadString(section, name, string.Empty);

            if (s.Length == 0)
                return defaultValue;
            else
            {
                try
                {
                    ushort n = Convert.ToUInt16(s);
                    return n;
                }
                catch (FormatException)
                {
                    return defaultValue;
                }
            }
        }
        #endregion

        #region Write methods
        public void WriteBool(string section, string name, bool value)
        {
            WriteString(section, name, value.ToString());
        }
        public void WriteDateTime(string section, string name, DateTime value)
        {
            WriteString(section, name, value.ToString());
        }
        public void WriteDouble(string section, string name, double value)
        {
            WriteString(section, name, value.ToString());
        }
        public void WriteFloat(string section, string name, float value)
        {
            WriteString(section, name, value.ToString());
        }
        public void WriteInt(string section, string name, int value)
        {
            WriteString(section, name, value.ToString());
        }
        public void WriteLong(string section, string name, long value)
        {
            WriteString(section, name, value.ToString());
        }
        public void WriteShort(string section, string name, short value)
        {
            WriteString(section, name, value.ToString());
        }
        public void WriteString(string section, string name, string value)
        {
            XmlNode root = doc.DocumentElement;
            XmlNode s = root.SelectSingleNode('/' + rootName + '/' + section);

            if (s == null)
                s = root.AppendChild(doc.CreateElement(section));

            XmlNode n = s.SelectSingleNode(name);

            if (n == null)
                n = s.AppendChild(doc.CreateElement(name));

            XmlAttribute attr = ((XmlElement)n).SetAttributeNode("value", string.Empty);
            attr.Value = value;
        }
        public void WriteUInt(string section, string name, uint value)
        {
            WriteString(section, name, value.ToString());
        }
        public void WriteULong(string section, string name, ulong value)
        {
            WriteString(section, name, value.ToString());
        }
        public void WriteUShort(string section, string name, ushort value)
        {
            WriteString(section, name, value.ToString());
        }
        #endregion
    }

    [DebuggerNonUserCode]
    public class XmlConfigLocal : XmlConfig
    {
        public XmlConfigLocal(string AppName, string RootName) :
            base(string.Concat(Environment.GetFolderPath(
            Environment.SpecialFolder.CommonApplicationData),
            "\\",AppName, "\\Config.xml"), RootName)
        { }
        public XmlConfigLocal(string AppName) :
            base(string.Concat(Environment.GetFolderPath(
            Environment.SpecialFolder.CommonApplicationData),
            "\\", AppName, "\\Config.xml"))
        { }
    }

    [DebuggerNonUserCode]
    public class XmlConfigLocalUser : XmlConfig
    {
        public XmlConfigLocalUser(string AppName, string RootName) :
            base(string.Concat(Environment.GetFolderPath(
            Environment.SpecialFolder.ApplicationData),
            "\\", AppName, "\\Config.xml"), RootName)
        { }
        public XmlConfigLocalUser(string AppName) :
            base(string.Concat(Environment.GetFolderPath(
            Environment.SpecialFolder.ApplicationData),
            "\\", AppName, "\\Config.xml"))
        { }
    }
}
