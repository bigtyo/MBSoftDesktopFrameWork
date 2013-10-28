using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Persistance;
using SentraSolutionFramework.Entity;
using SentraSolutionFramework;
using System.Windows.Forms;
using SentraUtility;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.Utils;

namespace SentraWinFramework
{
    public delegate void NodeChanged(TreeListNode Node, object Entity);
    public delegate void NodeSelected(string KeyFieldValue);

    public class xTreeList
    {
        string OrderField;
        TableDef Td;
        DataPersistance Dp;
        TreeList TreeList;
        string[] Columns;

        public event NodeChanged onAfterAddNode;
        public event NodeChanged onAfterUpdateNode;
        public event NodeSelected onNodeSelected;

        public string SqlCondition;

        private IUINavigator _Navigator;

        private string PostingField;

        public xTreeList(DataPersistance Dp, IUINavigator Navigator, TreeList TreeList,
            string OrderField, string SqlCondition, string PostingField)
            : this(Dp, TreeList, OrderField, SqlCondition, PostingField)
        {
            _Navigator = Navigator;

            foreach (TreeListColumn tlc in TreeList.Columns)
                tlc.Caption = BaseUtility.SplitName(tlc.Caption);

            onNodeSelected += new NodeSelected(xTreeList_onNodeSelected);
            onAfterAddNode += new NodeChanged(xTreeList_onAfterAddNode);

            Navigator.onEntityAction += new EntityAction(Navigator_onEntityAction);
            Navigator.onDataMoving += new DataMoving(Navigator_onDataMoving);
            DrawTree(null);
        }

        void Navigator_onEntityAction(BaseEntity ActionEntity, enEntityActionMode ActionMode)
        {
            switch (ActionMode)
            {
                case enEntityActionMode.AfterSaveNew:
                    AddEntity(_Navigator.Entity);
                    break;
                case enEntityActionMode.AfterSaveUpdate:
                    ParentEntity pe = (ParentEntity)ActionEntity; 
                    UpdateEntity(pe.GetOriginal(), pe);
                    break;
                case enEntityActionMode.AfterSaveDelete:
                    RemoveEntity((BusinessEntity)ActionEntity);
                    break;
            }
        }

        void xTreeList_onNodeSelected(string KeyFieldValue)
        {
            _Navigator.FindData(TreeList.KeyFieldName + "=@0",
                new FieldParam("0", KeyFieldValue));
            TreeList.Focus();
        }
        void xTreeList_onAfterAddNode(TreeListNode Node, object Entity)
        {
            Node.HasChildren = !(bool)((ParentEntity)
                Entity)[PostingField];
        }

        private bool OnTreeClick = false;
        void Navigator_onDataMoving(MoveType MovingType, bool IsError)
        {
            if (OnTreeClick) return;
            OnTreeClick = true;
            try
            {
                FocusNode((string)_Navigator.Entity[
                    TreeList.KeyFieldName], true);
            }
            finally
            {
                OnTreeClick = false;
            }
        }

        public xTreeList(DataPersistance Dp, TreeList TreeList,
            string OrderField, string SqlCondition, string PostingField)
        {
            this.PostingField = PostingField;
            if (TreeList.RootValue.GetType() != typeof(string))
                TreeList.RootValue = string.Empty;

            this.Dp = Dp;
            this.OrderField = OrderField;
            this.TreeList = TreeList;
            this.SqlCondition = SqlCondition;

            BindingSource bs = TreeList.DataSource as BindingSource;
            if (bs == null) throw new ApplicationException(
                "DataSource dari TreeList belum diisi !");
            if (bs.DataSource.GetType().Name == "RuntimeType")
                Td = MetaData.GetTableDef((Type)bs.DataSource);
            else
                Td = MetaData.GetTableDef(bs.DataSource.GetType());

            TreeList.DataSource = null;
            TreeList.BeforeExpand += new BeforeExpandEventHandler(TreeList_BeforeExpand);
            TreeList.FocusedNodeChanged += new FocusedNodeChangedEventHandler(TreeList_FocusedNodeChanged);
            Columns = new string[TreeList.Columns.Count + 1];
            int i = 0;
            Columns[0] = TreeList.KeyFieldName;
            foreach (TreeListColumn col in TreeList.Columns)
            {
                FieldDef fld = Td.GetFieldDef(col.FieldName);
                col.Format.FormatType = FormatType.Custom;
                col.Format.FormatString = fld.FormatString;
                Columns[++i] = col.FieldName;
            }
        }

        void TreeList_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            if (OnTreeClick) return;
            OnTreeClick = true;
            try
            {
                if (onNodeSelected != null && e.Node != null && e.Node.Tag != null)
                    onNodeSelected(GetKeyFieldValue(TreeList.FocusedNode));
            }
            finally
            {
                OnTreeClick = false;
            }
        }

        void TreeList_BeforeExpand(object sender, BeforeExpandEventArgs e)
        {
            if (!(bool)((object[])e.Node.Tag)[1]) DrawTree(e.Node);
        }

        public void DrawTree(TreeListNode ParentNode)
        {
            using (new WaitCursor())
            {
                string ParentValue = ParentNode == null ?
                    (string)TreeList.RootValue : (string)((object[])ParentNode.Tag)[0];

                IList<BaseEntity> ListEntity =
                    Dp.ListFastLoadEntities(Td.ClassType, null,
                        string.Join(",", Columns), string.Concat(
                        TreeList.ParentFieldName, "=@0",
                        SqlCondition.Length > 0 ?
                        " AND " + SqlCondition :
                        string.Empty), OrderField,
                        new FieldParam("0", ParentValue));

                if (ParentNode != null) ((object[])ParentNode.Tag)[1] = true;

                if (ListEntity == null || ListEntity.Count == 0)
                {
                    if (ParentNode != null) ParentNode.HasChildren = false;
                    return;
                }
                object[] NodeValue = new object[Columns.Length - 1];
                TreeList.BeginUnboundLoad();
                try
                {
                    OnTreeClick = true;
                    foreach (BusinessEntity Entity in ListEntity)
                    {
                        for (int i = 1; i < Columns.Length; i++)
                            NodeValue[i - 1] = Td.GetFieldDef(Columns[i])
                                .GetValue(Entity);
                        TreeListNode Node = TreeList.AppendNode(NodeValue, ParentNode);

                        Node.Tag = new object[2] { 
                        Td.GetFieldDef(Columns[0]).GetValue(Entity), false };

                        if (onAfterAddNode != null) onAfterAddNode(Node, Entity);
                    }
                }
                finally
                {
                    TreeList.EndUnboundLoad();
                    OnTreeClick = false;
                }
            }
        }

        public TreeListNode FindNode(string KeyField)
        {
            string[] KeyFields = KeyField.Split('.');
            TreeListNode nd = null;
            TreeListNodes nds;
            string CurrField = string.Empty;

            foreach (string KField in KeyFields)
            {
                if (CurrField.Length == 0)
                    CurrField = KField;
                else
                    CurrField = string.Concat(CurrField, ".", KField);
                nds = nd == null ? TreeList.Nodes : nd.Nodes;

                bool NodeFound = false;
                foreach (TreeListNode n in nds)
                    if (((object[])n.Tag)[0].Equals(CurrField))
                    {
                        nd = n;
                        NodeFound = true;
                        break;
                    }
                if (!NodeFound) return null;
            }
            return nd;
        }
        public TreeListNode FocusNode(string KeyField, bool SelectNode)
        {
            string[] KeyFields = KeyField.Split('.');
            TreeListNode nd = null;
            TreeListNodes nds;
            string CurrField = string.Empty;

            foreach (string KField in KeyFields)
            {
                if (CurrField.Length == 0)
                    CurrField = KField;
                else
                    CurrField = string.Concat(CurrField, ".", KField);
                if (nd == null)
                    nds = TreeList.Nodes;
                else
                {
                    if (!(bool)((object[])nd.Tag)[1]) DrawTree(nd);
                    nds = nd.Nodes;
                }

                bool NodeFound = false;
                foreach (TreeListNode n in nds)
                    if (((object[])n.Tag)[0].Equals(CurrField))
                    {
                        nd = n;
                        NodeFound = true;
                        break;
                    }
                if (!NodeFound) return null;
            }
            if (SelectNode) TreeList.FocusedNode = nd;
            return nd;
        }

        public void AddEntity(BusinessEntity Entity)
        {
            string KeySearch = (string)Td.GetFieldDef(
                TreeList.ParentFieldName).GetValue(Entity);
            AddEntity(FindNode(KeySearch), Entity);
        }
        public void AddEntity(TreeListNode ParentNode, BusinessEntity Entity)
        {
            if (ParentNode == null)
            {
                if (((string)Td.GetFieldDef(TreeList.ParentFieldName)
                    .GetValue(Entity)).Length > 0) return;
            }
            else
                if (!(bool)((object[])ParentNode.Tag)[1]) return;

            object[] NodeValue = new object[Columns.Length - 1];
            for (int i = 1; i < Columns.Length; i++)
                NodeValue[i - 1] = Td.GetFieldDef(Columns[i])
                    .GetValue(Entity);
            TreeList.BeginUnboundLoad();
            try
            {
                TreeListNode Node = TreeList.AppendNode(NodeValue, ParentNode);
                Node.Tag = new object[] { Td.GetFieldDef(Columns[0])
                    .GetValue(Entity), false };

                if (onAfterAddNode != null) onAfterAddNode(Node, Entity);
            }
            finally
            {
                TreeList.EndUnboundLoad();
            }
        }

        public void RemoveEntity(BusinessEntity Entity)
        {
            string KeySearch = (string)Td.GetFieldDef(
                TreeList.KeyFieldName).GetValue(Entity);
            RemoveNode(FindNode(KeySearch));
        }
        public void RemoveNode(TreeListNode Node)
        {
            if (Node != null) TreeList.Nodes.Remove(Node);
        }

        public void UpdateEntity(BusinessEntity OriginalEntity, BusinessEntity NewEntity)
        {
            string KeySearch = (string)Td.GetFieldDef(
                TreeList.KeyFieldName).GetValue(OriginalEntity);
            UpdateNode(FindNode(KeySearch), NewEntity);

        }
        public void UpdateNode(TreeListNode Node, BusinessEntity Entity)
        {
            if (Node == null) return;
            TreeList.BeginUnboundLoad();
            try
            {
                for (int i = 1; i < Columns.Length; i++)
                    Node.SetValue(Columns[i],
                        Td.GetFieldDef(Columns[i]).GetValue(Entity));
                bool IsUpdate;
                if (Node.Tag != null)
                    IsUpdate = (bool)((object[])Node.Tag)[1];
                else
                    IsUpdate = false;

                Node.Tag = new object[] { Td.GetFieldDef(Columns[0])
                    .GetValue(Entity), IsUpdate };
                if (onAfterUpdateNode != null) onAfterUpdateNode(Node, Entity);
            }
            finally
            {
                TreeList.EndUnboundLoad();
            }
        }

        public void Refresh()
        {
            using (new WaitCursor())
            {
                if (TreeList.Nodes.Count == 0)
                {
                    DrawTree(null);
                    OnTreeClick = false;
                    return;
                }
                List<string> OpenKey = new List<string>();
                string SelKey = (string)((object[])TreeList.FocusedNode.Tag)[0];

                foreach (TreeListNode nd in TreeList.Nodes)
                    if (nd.Expanded)
                    {
                        OpenKey.Add((string)((object[])nd.Tag)[0]);
                        FindOpenNode(nd, OpenKey);
                    }
                TreeList.BeginUnboundLoad();
                try
                {
                    TreeList.Nodes.Clear();
                    DrawTree(null);
                    foreach (string Key in OpenKey)
                    {
                        TreeListNode nd = FocusNode(Key, false);
                        if (nd != null) nd.Expanded = true;
                    }
                    FocusNode(SelKey, true);
                }
                finally
                {
                    TreeList.EndUnboundLoad();
                    OnTreeClick = false;
                }
            }
        }

        private void FindOpenNode(TreeListNode ParentNode,
            List<string> OpenKey)
        {
            foreach (TreeListNode nd in ParentNode.Nodes)
                if (nd.Expanded)
                {
                    OpenKey.Add((string)((object[])nd.Tag)[0]);
                    FindOpenNode(nd, OpenKey);
                }
        }

        public string GetKeyFieldValue(TreeListNode Node)
        {
            return (string)((object[])Node.Tag)[0];
        }
    }
}
