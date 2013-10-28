using System;
using System.Collections.Generic;
using System.Text;

namespace SentraSecurity
{
    public interface IModuleAccessForm
    {
        bool ShowDialog(ModuleAccess ma, Dictionary<string, 
            List<string>> ListKey, ref bool AllDocumentData);
    }
}
