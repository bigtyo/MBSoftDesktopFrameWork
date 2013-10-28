using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using System.Diagnostics;

namespace SentraUtility
{
    [DebuggerNonUserCode]
    public static class HardwareIdentification
    //Fingerprints the hardware
    {
        public static string Value()
        {
            try
            {
                //return Pack(string.Concat(CpuId(), BiosId(),
                //    DiskId(), BaseId(), MacId()));
                return DiskId();
            }
            catch
            {
                return "StandarDiskDrive";
                //return "IC25N040ATMR04-0(Standard disk drives)3743211292255";
            }
        }

        //Return a hardware identifier
        private static string identifier(string wmiClass, string wmiProperty, string wmiMustBeTrue)
        {
            string result = string.Empty;
            ManagementClass mc = new ManagementClass(wmiClass);
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (mo[wmiMustBeTrue].ToString() == "True")
                {
                    //Only get the first one
                    try
                    {
                        result = mo[wmiProperty].ToString();
                        break;
                    }
                    catch { }
                }
            }
            return result;
        }

        //Return a hardware identifier
        private static string identifier(string wmiClass, string wmiProperty)
        {
            string result = string.Empty;
            ManagementClass mc = new ManagementClass(wmiClass);
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {

                //Only get the first one
                try
                {
                    result = mo[wmiProperty].ToString();
                    break;
                }
                catch { }
            }
            return result;
        }

        private static string _CpuId = string.Empty;
        public static string CpuId()
        {
            if (_CpuId.Length == 0)
            {
                //Uses first CPU identifier available in order of preference
                //Don't get all identifiers, as very time consuming
                string retVal = identifier("Win32_Processor", "UniqueId");
                if (retVal == string.Empty) //If no UniqueID, use ProcessorID
                {
                    retVal = identifier("Win32_Processor", "ProcessorId");

                    if (retVal == string.Empty) //If no ProcessorId, use Name
                    {
                        retVal = identifier("Win32_Processor", "Name");


                        if (retVal == string.Empty) //If no Name, use Manufacturer
                        {
                            retVal = identifier("Win32_Processor", "Manufacturer");
                        }

                        //Add clock speed for extra security
                        retVal += identifier("Win32_Processor", "MaxClockSpeed");
                    }
                }
                _CpuId = retVal;
            }
            return _CpuId;

        }

        //BIOS Identifier
        private static string _BiosId = string.Empty;
        public static string BiosId()
        {
            if (_BiosId.Length == 0)
                _BiosId = string.Concat(
                    identifier("Win32_BIOS", "Manufacturer"),
                    identifier("Win32_BIOS", "SMBIOSBIOSVersion"),
                    identifier("Win32_BIOS", "IdentificationCode"),
                    identifier("Win32_BIOS", "SerialNumber"),
                    identifier("Win32_BIOS", "ReleaseDate"),
                    identifier("Win32_BIOS", "Version"));
            return _BiosId;
        }

        //Main physical hard drive ID
        private static string _DiskId = string.Empty;
        public static string DiskId()
        {
            if (_DiskId.Length == 0)
                _DiskId = string.Concat(
                    identifier("Win32_DiskDrive", "Model"),
                    identifier("Win32_DiskDrive", "Manufacturer"),
                    identifier("Win32_DiskDrive", "Signature"),
                    identifier("Win32_DiskDrive", "TotalHeads"));
            return _DiskId;
        }

        //Motherboard ID
        private static string _BaseId = string.Empty;
        public static string BaseId()
        {
            if (_BaseId.Length == 0)
                _BaseId = string.Concat(
                    identifier("Win32_BaseBoard", "Model"),
                    identifier("Win32_BaseBoard", "Manufacturer"),
                    identifier("Win32_BaseBoard", "Name"),
                    identifier("Win32_BaseBoard", "SerialNumber"));
            return _BaseId;
        }

        //Primary video controller ID
        private static string _VideoId = string.Empty;
        public static string VideoId()
        {
            if (_VideoId.Length == 0)
                _VideoId = identifier(
                    "Win32_VideoController", "DriverVersion")
                    + identifier("Win32_VideoController", "Name");
            return _VideoId;
        }

        //First enabled network card ID
        private static string _MacId = string.Empty;
        public static string MacId()
        {
            if (_MacId.Length == 0)
                _MacId = identifier("Win32_NetworkAdapterConfiguration",
                    "MACAddress", "IPEnabled");
            return _MacId;
        }

        //Packs the string to 12 digits
        public static string Pack(string text)
        {
            string retVal;
            int x = 0;
            int y = 0;
            foreach (char n in text)
            {
                y += 5732;
                x += (n * y);
            }

            retVal = x.ToString() + "000000000000";

            return string.Concat(
                retVal.Substring(0, 4), "-",
                retVal.Substring(4, 4), "-", 
                retVal.Substring(8, 4));
        }

        public static new string ToString()
        {
            return string.Concat(
                "CPU\t\t: ", CpuId(),
                "\nBIOS\t\t: ", BiosId(),
                "\nHD\t\t: ", DiskId(),
                "\nMB\t\t: ", BaseId(),
                "\nVideo\t\t: ", VideoId(),
                "\nMAC\t\t: ", MacId());
        }
    }
}
