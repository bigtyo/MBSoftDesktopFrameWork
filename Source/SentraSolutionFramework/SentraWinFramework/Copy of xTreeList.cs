using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using SentraSolutionFramework.Persistance;
using SentraSolutionFramework.Entity;
using SentraSolutionFramework;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Columns;
using DevExpress.Utils;

namespace SentraWinFramework
{
    public delegate void AfterNodeChanged(TreeListNode Node, object Entity);

    public class xTreeList
    {
        private class MyNode
        {
            public string KeyField;
            public bool IsLoad;
            public TreeListNode Node;

            public MyNode(TreeListNode Node, string KeyField)
            {
                this.Node = Node;
                this.KeyField = KeyField;
                this.IsLoad = false;
            }
        }

        SortedList<string, MyNode> ListNodes = new SortedList<string,MyNode>();

        string OrderField;
        TableDef Td;
        DataPersistance Dp;
        TreeList TreeList;
        string[] Columns;

        public event AfterNodeChanged onAfterAddNode;
        public event AfterNodeChanged onAfterUpdateNode;

        public xTreeList(TreeList TreeList,
            string OrderField)
            : this(null, TreeList, OrderField) { }
        public xTreeList(DataPersistance Dp, TreeList TreeList,
            string OrderField)
        {
            this.Dp = Dp ?? BaseFramework.DefaultDataPersistance;
            this.OrderField = OrderField;
            this.TreeList = TreeList;
            BindingSource bs = TreeList.DataSource as BindingSource;
            if (bs == null) throw new ApplicationException(
                "DataSource dari TreeList belum diisi !");
            if (bs.DataSource.GetType().Name == "RuntimeType")
                Td = MetaData.GetTableDef((Type)bs.DataSource);
            else
                Td = MetaData.GetTableDef(bs.DataSource.GetType());

            TreeList.DataSource = null;
            TreeList.BeforeExpand += new BeforeExpandEventHandler(TreeList_BeforeExpand);
            TreeList.NodeChanged += new NodeChangedEventHandler(TreeList_NodeChanged);
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

        void TreeList_NodeChanged(object sender, NodeChangedEventArgs e)
        {
            if (e.ChangeType == NodeChangeTypeEnum.Remove)
                ListNodes.Remove(((MyNode)e.Node.Tag).KeyField);
        }

        void TreeList_BeforeExpand(object sender, BeforeExpandEventArgs e)
        {
            //if (!(bool)((object[])e.Node.Tag)[1]) DrawTree(e.Node);
            if (!((MyNode)e.Node.Tag).IsLoad) DrawTree(e.Node);
        }

        public void DrawTree(TreeListNode ParentNode)
        {
            //string ParentValue = ParentNode == null ?
            //    (string)TreeList.RootValue : (string)((object[])ParentNode.Tag)[0];
            string ParentValue = ParentNode == null ?
                (string)TreeList.RootValue : ((MyNode)ParentNode.Tag).KeyField;
            
            IList<object> ListEntity =
                Dp.FastLoadEntities(Td.ClassType, null,
                    string.Join(",", Columns), string.Concat(
                    TreeList.ParentFieldName, "=",
                    Dp.FormatSqlValue(ParentValue)), OrderField);

            //if (ParentNode != null) ((object[])ParentNode.Tag)[1] = true;
            if (ParentNode != null) ((MyNode)ParentNode.Tag).IsLoad = true;

            if (ListEntity == null || ListEntity.Count == 0)
            {
                if (ParentNode != null) ParentNode.HasChildren = false;
                return;
            }
            object[] NodeValue = new object[Columns.Length - 1];
            TreeList.BeginUnboundLoad();
            try
            {
                foreach (object Entity in ListEntity)
                {
                    for (int i = 1; i < Columns.Length; i++)
                        NodeValue[i - 1] = Td.GetFieldDef(Columns[i])
                            .GetValue(Entity);
                    TreeListNode Node = TreeList.AppendNode(NodeValue, ParentNode);
                    MyNode nd = new MyNode(Node,
                        (string)Td.GetFieldDef(Columns[0])
                        .GetValue(Entity));
                    Node.Tag = nd;

                    //Node.Tag = new object[2] { 
                    //    Td.GetFieldDef(Columns[0]).GetValue(Entity), false };

                    if (onAfterAddNode != null) onAfterAddNode(Node, Entity);
                }
            }
            finally
            {
                TreeList.EndUnboundLoad();
            }
        }

        public TreeListNode FindNode(string KeyField)
        {
            MyNode mn;
            ListNodes.TryGetValue(KeyField, out mn);
            return mn == null ? null : mn.Node;

            //string[] KeyFields = KeyField.Split('.');
            //TreeListNode nd = null;
            //TreeListNodes nds;
//            string CurrField = string.Empty;

            //foreach (string KField in KeyFields)
            //{
            //    if (CurrField.Length == 0)
            //        CurrField = KField;
            //    else
            //        CurrField = string.Concat(CurrField, ".", KField);
            //    nds = nd == null ? TreeList.Nodes : nd.Nodes;

                //bool NodeFound = false;
                //foreach (TreeListNode n in nds)
                //    if (((object[])n.Tag)[0].Equals(CurrField))
                //    {
                //        nd = n;
                //        NodeFound = true;
                //        break;
                //    }
                //if (!NodeFound) return null;
            //}
            //return nd;
        }
        public TreeListNode FocusNode(string KeyField, bool SelectNode)
        {
            MyNode mn;
            ListNodes.TryGetValue(KeyField, out mn);
            if (mn != null)
            {
                if (SelectNode) TreeList.FocusedNode = mn.Node;
                return mn.Node;
            }
            else
                return null;

            //string[] KeyFields = KeyField.Split('.');
            //TreeListNode nd = null;
            //TreeListNodes nds;
            //string CurrField = string.Empty;

            //foreach (string KField in KeyFields)
            //{
            //    if (CurrField.Length == 0)
            //        CurrField = KField;
            //    else
            //        CurrField = string.Concat(CurrField, ".", KField);
            //    if (nd == null)
            //        nds = TreeList.Nodes;
            //    else
            //    {
            //        //if (!(bool)((object[])nd.Tag)[1]) DrawTree(nd);
            //        if (!((MyNode)nd.Tag).IsLoad) DrawTree(nd);

            //        nds = nd.Nodes;
            //    }

            //    //bool NodeFound = false;
            //    //foreach (TreeListNode n in nds)
            //    //    if (((object[])n.Tag)[0].Equals(CurrField))
            //    //    {
            //    //        nd = n;
            //    //        NodeFound = true;
            //    //        break;
            //    //    }
            //    //if (!NodeFound) return null;
            //}
            //if (SelectNode) TreeList.FocusedNode = nd;
            //return nd;
        }

        public void AddEntity(object Entity)
        {
            string KeySearch = (string)Td.GetFieldDef(
                TreeList.ParentFieldName).GetValue(Entity);
            AddEntity(FindNode(KeySearch), Entity);
        }
        public void AddEntity(TreeListNode ParentNode, object Entity)
        {
            if (ParentNode == null)
            {
                if (((string)Td.GetFieldDef(TreeList.ParentFieldName)
                    .GetValue(Entity)).Length > 0) return;
            }
            else
                //if (!(bool)((object[])ParentNode.Tag)[1]) return;
                if (!((MyNode)ParentNode.Tag).IsLoad) return;

            object[] NodeValue = new object[Columns.Length - 1];
            for (int i = 1; i < Columns.Length; i++)
                NodeValue[i - 1] = Td.GetFieldDef(Columns[i])
                    .GetValue(Entity);
            TreeList.BeginUnboundLoad();
            try
            {
                TreeListNode Node = TreeList.AppendNode(NodeValue, ParentNode);
                MyNode nd = new MyNode(Node, (string)
                    Td.GetFieldDef(Columns[0]).GetValue(Entity));
                Node.Tag = nd;
                //Node.Tag = new object[] { Td.GetFieldDef(Columns[0])
                //    .GetValue(Entity), false };

                if (onAfterAddNode != null) onAfterAddNode(Node, Entity);
            }
            finally
            {
                TreeList.EndUnboundLoad();
            }
        }

        public void RemoveEntity(object Entity)
        {
            string KeySearch = (string)Td.GetFieldDef(
                TreeList.KeyFieldName).GetValue(Entity);
            RemoveNode(FindNode(KeySearch));
        }
        public void RemoveNode(TreeListNode Node)
        {
            if (Node != null) TreeList.Nodes.Remove(Node);
        }

        public void UpdateEntity(object OriginalEntity, object NewEntity)
        {
            string KeySearch = (string)Td.GetFieldDef(
                TreeList.KeyFieldName).GetValue(OriginalEntity);
            UpdateNode(FindNode(KeySearch), NewEntity);

        }
        public void UpdateNode(TreeListNode Node, object Entity)
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
                    //IsUpdate = (bool)((object[])Node.Tag)[1];
                    IsUpdate = ((MyNode)Node.Tag).IsLoad;
                else
                    IsUpdate = false;

                MyNode mn = new MyNode(Node, (string)
                    Td.GetFieldDef(Columns[0])
                    .GetValue(Entity));
                Node.Tag = mn;

                //Node.Tag = new object[] { Td.GetFieldDef(Columns[0])
                //    .GetValue(Entity), IsUpdate };
                if (onAfterUpdateNode != null) onAfterUpdateNode(Node, Entity);
            }
            finally
            {
                TreeList.EndUnboundLoad();
            }
        }

        public void Refresh()
        {
            List<string> OpenKey = new List<string>();
            //string SelKey = (string)((object[])TreeList.FocusedNode.Tag)[0];
            string SelKey = ((MyNode)TreeList.FocusedNode.Tag).KeyField;

            foreach (TreeListNode nd in TreeList.Nodes)
                if (nd.Expanded)
                {
                    //OpenKey.Add((string)((object[])nd.Tag)[0]);
                    OpenKey.Add(((MyNode)nd.Tag).KeyField);
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
            }
        }

        private void FindOpenNode(TreeListNode ParentNode,
            List<string> OpenKey)
        {
            foreach (TreeListNode nd in ParentNode.Nodes)
                if (nd.Expanded)
                {
                    //OpenKey.Add((string)((object[])nd.Tag)[0]);
                    OpenKey.Add(((MyNode)nd.Tag).KeyField);
                    FindOpenNode(nd, OpenKey);
                }
        }

        public string GetKeyFieldValue(TreeListNode Node)
        {
            return ((MyNode)Node.Tag).KeyField;
            //return (string)((object[])Node.Tag)[0];
        }
    }
}
