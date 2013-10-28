using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework;
using SentraSolutionFramework.Persistance;
using System.Data;

namespace SentraSolutionFramework.Persistance
{
    public class RepeatCommand
    {
        public IDbCommand Cmd;
        public object[] Values;

        public RepeatCommand(IDbCommand Cmd, object[] Values)
        {
            this.Cmd = Cmd;
            this.Values = Values;
        }

        public int ExecuteNonQuery()
        {
            int Cnt = Cmd.Parameters.Count;
            for (int i = 0; i < Cnt; i++)
                ((IDataParameter)Cmd.Parameters[i]).Value = Values[i];
            return Cmd.ExecuteNonQuery();
        }
    }
}
