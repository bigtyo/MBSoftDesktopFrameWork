using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace SentraSolutionFramework.Persistance
{
    [DebuggerNonUserCode]
    public class BatchCommand
    {
        private class internalCmd
        {
            public string SqlCommand;
            public object Parameters;

            public internalCmd(string SqlCommand, object Parameters)
            {
                this.SqlCommand = SqlCommand;
                this.Parameters = Parameters;
            }

            public override string ToString()
            {
                return SqlCommand;
            }
        }

        List<internalCmd> CmdList = new List<internalCmd>();

        public void ClearCommand()
        {
            CmdList.Clear();
        }

        public void AddCommand(string SqlCommand,
            params FieldParam[] Parameters)
        {
            CmdList.Add(new internalCmd(SqlCommand, Parameters));
        }
        public void AddCommand(string SqlCommand,
            List<FieldParam> Parameters)
        {
            CmdList.Add(new internalCmd(SqlCommand, Parameters));
        }

        public void InsertCommand(int Index, string SqlCommand,
            params FieldParam[] Parameters)
        {
            CmdList.Insert(Index, new internalCmd(SqlCommand, 
                Parameters));
        }
        public void InsertCommand(int Index, string SqlCommand,
            List<FieldParam> Parameters)
        {
            CmdList.Insert(Index, new internalCmd(SqlCommand, 
                Parameters));
        }

        public int CommandCount { get { return CmdList.Count; } }
        public string GetSqlCommand(int index)
        {
            return CmdList[index].SqlCommand;
        }
        public FieldParam[] GetParameters(int index)
        {
            object p = CmdList[index].Parameters;
            if (p as List<FieldParam> != null)
                return ((List<FieldParam>)p).ToArray();
            else
                return (FieldParam[])p;
        }

        /// <summary>
        /// Cek apakah semua command merupakan DELETE Command/ bukan
        /// </summary>
        /// <returns></returns>
        internal bool OnlyDeleteCommand()
        {
            foreach (internalCmd Cmd in CmdList)
                if (!Cmd.SqlCommand.StartsWith("DELETE")) return false;
            return true;
        }

    }
}
