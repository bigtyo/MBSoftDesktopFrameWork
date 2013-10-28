//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Net;
//using System.Windows.Forms;
//using System.Diagnostics;

//namespace SentraUtility
//{
//    public class AutoUpdate
//    {
//        // <File Path>;<MinimumVersion>   [' comments    ]
//        // <File Path>;=<ExactVersion>    [' comments    ]
//        // <File Path>;                   [' comments    ]
//        // <File Path>;?                  [' comments    ]
//        // <File Path>;delete             [' comments    ]
//        // ...
//        // Blank lines and comments are ignored
//        // First parameter - file path (eg. Dir\file.dll)
//        // Second parameter:
//        // If the version is specified, the file is updated if:
//        //   - it doesn't exist or
//        //   - the actual version number is smaller than the update version number
//        // If the version is specified precedeed by a "=", the file is updated if:
//        //   - it doesn't exist or
//        //   - the actual version doesn't match the update version
//        // If it's an interrogation mark "?" the file is updated only if it doesn't exist
//        // If the second parameter is not specified, the file is updated only if it doesn't exist (just like "?")
//        // If it's "delete" the system tries to delete the file
//        // "'" (chr(39)) start a line (or part line) comment (like VB)

//        // Method return values
//        //   - True - the update was done with no errors
//        //   - False - the update didn't complete successfully: either there is no update to be done
//        //             or there was an error during the update

//        // NB - "Version" refers to the AssemblyFileVersion as configured in file AssemblyInfo.cs

//        private const string ToDeleteExtension = ".ToDelete";
//        private const string UpdateFileName = "Update.txt";
//        private const string ErrorMessageCheck = "There was a problem checking the update config file.";
//        private const string ErrorMessageUpdate = "There was a problem runing the Auto Update.";
//        private const string ErrorMessageDelete = "There was a problem deleting files.";

//        public static bool CleanUp()
//        {
//            try
//            {
//                string file;

//                DirectoryInfo dir = new DirectoryInfo(Application.StartupPath);
//                FileInfo[] infos = dir.GetFiles("*" + ToDeleteExtension, SearchOption.AllDirectories);
//                foreach (FileInfo info in infos)
//                {
//                    file = info.FullName;
//                    File.SetAttributes(file, FileAttributes.Normal);
//                    File.Delete(file);
//                }
//                return true;
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ErrorMessageDelete + "\r" + ex.Message);
//                return false;
//            }
//        }

//        public static bool UpdateFiles(string remotePath, bool DoUpdate)
//        {
//            if (remotePath == string.Empty)
//                return false;

//            if (DoUpdate)
//            {
//                // Delete files before updating them
//                CleanUp();
//            }

//            // execute the following line even for check runs
//            List<AutoUpdateRollback> rollBackList =
//                new List<AutoUpdateRollback>();

//            string remoteUri = remotePath;
//            WebClient myWebClient = new WebClient();

//            bool retValue = false;
//            try
//            {
//                // Get the remote file
//                string contents = myWebClient.DownloadString(remoteUri + UpdateFileName);
//                // Strip the "LF" from CR+LF and break it down by line
//                contents = contents.Replace("\n", "");
//                string[] fileList = contents.Split(Convert.ToChar("\r"));

//                // Parse the file list to strip comments
//                contents = string.Empty;
//                foreach (string file in fileList)
//                {
//                    string fileAux;
//                    if ((file.IndexOf("\'") + 1 != 0))
//                        fileAux = file.Substring(0, ((file.IndexOf("\'") + 1) - 1));
//                    else
//                        fileAux = file;
//                    if (fileAux.Trim() != string.Empty)
//                    {
//                        if (!string.IsNullOrEmpty(contents))
//                            contents = contents + (char)(Keys.Return);
//                        contents = contents + fileAux.Trim();
//                    }
//                }

//                // Parse the file list again
//                fileList = contents.Split((char)(Keys.Return));
//                string[] info;
//                string infoFilePath;
//                String infoParam;
//                List<string> fileNameList = new List<string>();
//                Version Version1, Version2;
//                FileVersionInfo fv;
//                bool IsToDelete;
//                bool IsToUpgrade;
//                foreach (string file in fileList)
//                {
//                    info = file.Split(Convert.ToChar(";"));
//                    infoFilePath = info[0].Trim();
//                    infoParam = info[1].Trim();
//                    while (infoFilePath[0] == '.' || infoFilePath[0] == '\\')
//                    {
//                        infoFilePath = infoFilePath.Substring(1, infoFilePath.Length - 1);
//                    }

//                    // ignore path names already on list (duplicates)
//                    if (fileNameList.Contains(infoFilePath.ToLowerInvariant()))
//                    {
//                        continue;
//                    }

//                    // add path name to list
//                    fileNameList.Add(infoFilePath.ToLowerInvariant());
//                    IsToDelete = false;
//                    IsToUpgrade = false;
//                    string fileName = Application.StartupPath + @"\" + infoFilePath;
//                    string tempFileName = Application.StartupPath + @"\" + infoFilePath + DateTime.Now.TimeOfDay.TotalMilliseconds;
//                    bool FileExists = File.Exists(fileName);
//                    if ((infoParam == "delete"))
//                    {
//                        IsToDelete = FileExists; // The second parameter is "delete"
//                        if (DoUpdate)
//                            if (IsToDelete)
//                                rollBackList.Add(new AutoUpdateRollback(fileName, tempFileName + ToDeleteExtension, "Delete"));
//                    }
//                    else if ((infoParam == "?"))
//                    {
//                        // The second parameter is "?" (check if the file exists and it TRUE, do not update
//                        IsToUpgrade = !FileExists;
//                    }
//                    else if (infoParam != string.Empty && (infoParam[0] == '=' && FileExists))
//                    {
//                        // The second parameter starts by "=" 
//                        // Check the version of local and remote files
//                        fv = FileVersionInfo.GetVersionInfo(fileName);
//                        Version1 = new Version(infoParam.Substring(1, infoParam.Length - 1));
//                        Version2 = new Version(fv.FileVersion);
//                        IsToUpgrade = Version1 != Version2;
//                        IsToDelete = IsToUpgrade;
//                        if (DoUpdate)
//                            if (IsToUpgrade)
//                                rollBackList.Add(
//                                    new AutoUpdateRollback(fileName, tempFileName + ToDeleteExtension, "Upgrade"));
//                    }
//                    else if (FileExists)
//                    {
//                        // Check the version of local and remote files
//                        fv = FileVersionInfo.GetVersionInfo(fileName);
//                        // If 2nd parameter is empty, do nothing as file already exists
//                        if (infoParam != string.Empty)
//                        {
//                            Version1 = new Version(infoParam);
//                            Version2 = new Version(fv.FileVersion);
//                            IsToUpgrade = Version1 > Version2;
//                            IsToDelete = IsToUpgrade;
//                            if (DoUpdate)
//                                if (IsToUpgrade)
//                                    rollBackList.Add(
//                                        new AutoUpdateRollback(fileName, tempFileName + ToDeleteExtension, "Upgrade"));
//                        }
//                    }
//                    else
//                    {
//                        IsToUpgrade = true;
//                    }

//                    if (DoUpdate)
//                    {
//                        if (IsToUpgrade)
//                            myWebClient.DownloadFile(remoteUri + infoFilePath, tempFileName);
//                        // Rename the file for future deletion
//                        if (IsToDelete)
//                            File.Move(fileName, tempFileName + ToDeleteExtension);
//                        // Rename the temporary file name to the real file name
//                        if (IsToUpgrade)
//                            File.Move(tempFileName, fileName);
//                    }

//                    if (IsToUpgrade || IsToDelete)
//                        retValue = true;
//                }

//                // This reruns the updated application
//                //Process.Start(Application.ExecutablePath);
//                // Don't use it here or you will end in an endless loop.
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("There was an error. Trying to roll back");
//                if (DoUpdate)
//                {
//                    foreach (AutoUpdateRollback rollBack in rollBackList)
//                    {
//                        if (rollBack.Operation == "Delete" || rollBack.Operation == "Upgrade")
//                        {
//                            if (File.Exists(rollBack.Renamed))
//                                File.Move(rollBack.Renamed, rollBack.Original);
//                        }
//                    }
//                    MessageBox.Show(ErrorMessageUpdate + "\r" + ex.Message + "\r" + "Remote URI: " + remoteUri);
//                }
//                else
//                    MessageBox.Show(ErrorMessageCheck + "\r" + ex.Message + "\r" + "Remote URI: " + remoteUri);

//                retValue = false;
//            }
//            finally
//            {
//                myWebClient.Dispose();
//                // execute the following line even for check runs
//                rollBackList.Clear();
//            }

//            return retValue;
//        }
//    }

//    public class AutoUpdateRollback
//    {
//        #region Properties

//        private string _renamed, _original, _operation;

//        public string Operation
//        {
//            get { return _operation; }
//            set { _operation = value; }
//        }

//        public string Original
//        {
//            get { return _original; }
//            set { _original = value; }
//        }

//        public string Renamed
//        {
//            get { return _renamed; }
//            set { _renamed = value; }
//        }

//        #endregion

//        #region Constructor

//        public AutoUpdateRollback(string Original, string Renamed, string Operation)
//        {
//            _original = Original;
//            _renamed = Renamed;
//            _operation = Operation;
//        }

//        #endregion
//    }
//}