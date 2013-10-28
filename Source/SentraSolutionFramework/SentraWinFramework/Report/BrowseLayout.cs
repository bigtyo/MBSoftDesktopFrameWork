using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using SentraSolutionFramework;
using System.Data;
using SentraSolutionFramework.Persistance;
using System.IO;
using SentraUtility;

namespace SentraWinFramework.Report
{
    // Menyimpan seting default Laporan dari Entity
    [TableName("_System_DocDefault")]
    internal class DocDefault : ParentEntity
    {
        [PrimaryKey, DataTypeVarChar(50)]
        public string DocId;
        [DataTypeVarChar(50)]
        public string DefaultBrowseLayoutId;
        [DataTypeVarChar(50)]
        public string DefaultPrintLayoutId;
        [DataTypeVarChar(50)]
        public string DefaultPrintBrowseLayoutId;
        [DataTypeBoolean]
        public bool DefaultPrintOnSave;
        [DataTypeBoolean]
        public bool DefaultUsePrintPreview;

        public static void GetDefaultLayout(string DocId, out string BrowseLayoutId,
            out string PrintBrowseLayoutId, out bool DefaultUsePrintPreview)
        {
            DocDefault dd = new DocDefault();
            dd.DocId = DocId;
            if (dd.LoadEntity(true))
            {
                BrowseLayoutId = dd.DefaultBrowseLayoutId;
                PrintBrowseLayoutId = dd.DefaultPrintBrowseLayoutId;
                DefaultUsePrintPreview = dd.DefaultUsePrintPreview;
            }
            else
            {
                BrowseLayoutId = string.Empty;
                PrintBrowseLayoutId = string.Empty;
                DefaultUsePrintPreview = true;
            }
        }

        public static void UpdateDefaultLayout(string DocId,
            string BrowseLayoutId, string PrintBrowseLayoutId,
            bool DefaultUsePrintPreview)
        {
            DocDefault dd = new DocDefault();
            dd.DocId = DocId;
            dd.DefaultBrowseLayoutId = BrowseLayoutId;
            dd.DefaultPrintLayoutId = string.Empty;
            dd.DefaultPrintBrowseLayoutId = PrintBrowseLayoutId;
            dd.DefaultPrintOnSave = false;
            dd.DefaultUsePrintPreview = DefaultUsePrintPreview;
            dd.Save();
        }

        public static void GetDefaultLayout(string DocId, out string BrowseLayoutId,
            out string PrintLayoutId, out bool DefaultPrintOnSave,
            out bool DefaultUsePrintPreview)
        {
            DocDefault dd = new DocDefault();
            dd.DocId = DocId;

            if (dd.LoadEntity(true))
            {
                BrowseLayoutId = dd.DefaultBrowseLayoutId;
                PrintLayoutId = dd.DefaultPrintLayoutId;
                DefaultPrintOnSave = dd.DefaultPrintOnSave;
                DefaultUsePrintPreview = dd.DefaultUsePrintPreview;
            }
            else
            {
                BrowseLayoutId = string.Empty;
                PrintLayoutId = string.Empty;
                DefaultPrintOnSave = false;
                DefaultUsePrintPreview = true;
            }
        }

        public static void UpdateDefaultLayout(string DocId,
            string BrowseLayoutId, string PrintLayoutId,
            bool DefaultPrintOnSave, bool DefaultUsePrintPreview)
        {
            DocDefault dd = new DocDefault();
            dd.DocId = DocId;
            dd.DefaultBrowseLayoutId = BrowseLayoutId;
            dd.DefaultPrintLayoutId = PrintLayoutId;
            dd.DefaultPrintBrowseLayoutId = string.Empty;
            dd.DefaultPrintOnSave = DefaultPrintOnSave;
            dd.DefaultUsePrintPreview = DefaultUsePrintPreview;
            dd.Save();
        }

        public static void UpdateSelectedLayout(string DocId,
            string PrintLayoutId)
        {
            DocDefault dd = new DocDefault();
            dd.DocId = DocId;
            if (dd.LoadEntity())
            {
                dd.DefaultPrintLayoutId = PrintLayoutId;
                dd.SaveUpdate();
            }
            else
            {
                dd.SetDefaultValue();
                dd.DocId = DocId;
                dd.DefaultPrintLayoutId = PrintLayoutId;
                dd.DefaultPrintOnSave = false;
                dd.SaveNew();
            }
        }

        public static void UpdateSelectedPrintBrowseLayout(string DocId,
            string PrintBrowseLayoutId)
        {
            DocDefault dd = new DocDefault();
            dd.DocId = DocId;
            if (dd.LoadEntity())
            {
                dd.DefaultPrintBrowseLayoutId = PrintBrowseLayoutId;
                dd.SaveUpdate();
            }
            else
            {
                dd.SetDefaultValue();
                dd.DocId = DocId;
                dd.DefaultPrintBrowseLayoutId = PrintBrowseLayoutId;
                dd.DefaultPrintOnSave = false;
                dd.SaveNew();
            }
        }
    }

    // Menyimpan Layout Browse dari Entity
    [TableName("_System_DocBrowseLayout")]
    internal class DocBrowseLayout : ParentEntity
    {
        [PrimaryKey, DataTypeVarChar(50)]
        public string DocId;
        [PrimaryKey, DataTypeVarChar(50)]
        public string LayoutId;
        [DataTypeBinary]
        public byte[] LayoutData;
        [DataTypeVarChar(1000)]
        public string QueryFilter;
        [DataTypeVarChar(1000)]
        public string QueryFormFilter;

        public static List<string> GetListLayout(string DocId)
        {
            List<string> RetVal = new List<string>();

            IList<DocBrowseLayout> ListDbl = BaseFramework
                .DefaultDp.ListFastLoadEntities<DocBrowseLayout>(
                null, "LayoutId", "DocId=@0", "LayoutId", 
                new FieldParam("0", DocId));

            foreach (DocBrowseLayout dbl in ListDbl)
                RetVal.Add(dbl.LayoutId);

            return RetVal;
        }

        public static void GetLayoutData(string DocId, string LayoutId,
            out MemoryStream LayoutData, out string QueryFilter,
            Dictionary<string, object>QueryFormFilter)
        {
            DocBrowseLayout dbl = new DocBrowseLayout();
            dbl.DocId = DocId;
            dbl.LayoutId = LayoutId;
            if (dbl.LoadEntity()) 
            {
                LayoutData = new MemoryStream(dbl.LayoutData);
                QueryFilter = dbl.QueryFilter;
                BaseUtility.String2Dictionary(
                    dbl.QueryFormFilter, QueryFormFilter);
            }
            else
            {
                LayoutData = null;
                QueryFilter = string.Empty;
                QueryFormFilter = null;
            }
        }

        public static void SaveUpdateLayout(string DocId, string LayoutId,
            MemoryStream LayoutData, string QueryFilter,
            Dictionary<string, object>QueryFormFilter)
        {
            DocBrowseLayout dbl = new DocBrowseLayout();
            dbl.DocId = DocId;
            dbl.LayoutId = LayoutId;
            dbl.LayoutData = LayoutData.ToArray();
            dbl.QueryFilter = QueryFilter;
            dbl.QueryFormFilter = BaseUtility.Dictionary2String(QueryFormFilter);
            dbl.SaveUpdate();
        }

        public static void SaveNewLayout(string DocId, string LayoutId,
            MemoryStream LayoutData, string QueryFilter,
            Dictionary<string, object>QueryFormFilter)
        {
            DocBrowseLayout dbl = new DocBrowseLayout();
            dbl.DocId = DocId;
            dbl.LayoutId = LayoutId;
            dbl.LayoutData = LayoutData.ToArray();
            dbl.QueryFilter = QueryFilter;
            dbl.QueryFormFilter = BaseUtility.Dictionary2String(QueryFormFilter);
            dbl.SaveNew();
        }

        public static int DeleteLayout(string DocId, string LayoutId)
        {
            DocBrowseLayout dbl = new DocBrowseLayout();
            dbl.DocId = DocId;
            dbl.LayoutId = LayoutId;
            return dbl.SaveDelete();
        }
    }

    // Menyimpan Layout Cetak Daftar dari Entity
    [TableName("_System_DocPrintBrowseLayout")]
    internal class DocPrintBrowseLayout : ParentEntity
    {
        [PrimaryKey, DataTypeVarChar(50)]
        public string DocId;
        [PrimaryKey, DataTypeVarChar(50)]
        public string LayoutId;
        [DataTypeBinary]
        public byte[] LayoutData;
        [DataTypeVarChar(1000)]
        public string QueryFormFilter;  // Untuk FreeForm

        public static List<string> GetListLayout(string DocId)
        {
            List<string> RetVal = new List<string>();

            IList<DocPrintBrowseLayout> Listdpbl = BaseFramework.DefaultDp
                .ListFastLoadEntities<DocPrintBrowseLayout>(null, "LayoutId",
                "DocId=@0", "LayoutId", new FieldParam("0", DocId));
            foreach (DocPrintBrowseLayout dpbl in Listdpbl)
                RetVal.Add(dpbl.LayoutId);

            return RetVal;
        }

        public static void GetLayoutData(string DocId, string LayoutId,
            out MemoryStream LayoutData)
        {
            DocPrintBrowseLayout dpbl = new DocPrintBrowseLayout();
            dpbl.DocId = DocId;
            dpbl.LayoutId = LayoutId;
            if (dpbl.FastLoadEntity("LayoutData"))
                LayoutData = new MemoryStream(dpbl.LayoutData);
            else
                LayoutData = null;
        }

        public static void SaveUpdateLayout(string DocId, string LayoutId,
            MemoryStream LayoutData)
        {
            DocPrintBrowseLayout dpbl = new DocPrintBrowseLayout();
            dpbl.DocId = DocId;
            dpbl.LayoutId = LayoutId;
            if (dpbl.FastLoadEntity("QueryFormFilter"))
            {
                dpbl.LayoutData = LayoutData.ToArray();
                dpbl.SaveUpdate();
            }
        }

        public static void SaveNewLayout(string DocId, string LayoutId,
            MemoryStream LayoutData)
        {
            DocPrintBrowseLayout dpbl = new DocPrintBrowseLayout();
            dpbl.DocId = DocId;
            dpbl.LayoutId = LayoutId;
            dpbl.LayoutData = LayoutData.ToArray();
            dpbl.SaveNew();
        }

        public static int DeleteLayout(string DocId, string LayoutId)
        {
            DocPrintBrowseLayout dpbl = new DocPrintBrowseLayout();
            dpbl.DocId = DocId;
            dpbl.LayoutId = LayoutId;
            return dpbl.SaveDelete();
        }

        public static void GetFreeLayoutData(string DocId, string LayoutId,
            out MemoryStream LayoutData, 
            Dictionary<string, object> QueryFormFilter)
        {
            DocPrintBrowseLayout dpbl = new DocPrintBrowseLayout();
            dpbl.DocId = DocId;
            dpbl.LayoutId = LayoutId;
            if (dpbl.LoadEntity())
            {
                LayoutData = new MemoryStream(dpbl.LayoutData);
                BaseUtility.String2Dictionary(dpbl.QueryFormFilter, QueryFormFilter);
            }
            else
                LayoutData = null;
        }

        public static void SaveNewFreeLayout(string DocId, string LayoutId,
            MemoryStream LayoutData, Dictionary<string, object> QueryFormFilter)
        {
            DocPrintBrowseLayout dpbl = new DocPrintBrowseLayout();
            dpbl.DocId = DocId;
            dpbl.LayoutId = LayoutId;
            dpbl.LayoutData = LayoutData.ToArray();
            dpbl.QueryFormFilter = BaseUtility.Dictionary2String(QueryFormFilter);
            dpbl.SaveNew();
        }

        public static void SaveUpdateFreeLayout(string DocId, string LayoutId,
            MemoryStream LayoutData, Dictionary<string, object> QueryFormFilter)
        {
            DocPrintBrowseLayout dpbl = new DocPrintBrowseLayout();
            dpbl.DocId = DocId;
            dpbl.LayoutId = LayoutId;
            dpbl.LayoutData = LayoutData.ToArray();
            dpbl.QueryFormFilter = BaseUtility.Dictionary2String(QueryFormFilter);
            dpbl.SaveUpdate();
        }
    }
}
