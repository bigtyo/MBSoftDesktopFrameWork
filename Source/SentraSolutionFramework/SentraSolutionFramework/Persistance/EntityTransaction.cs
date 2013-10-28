using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Data;

namespace SentraSolutionFramework.Persistance
{
    //[DebuggerNonUserCode]
    public sealed class EntityTransaction : IDisposable
    {
        DataPersistance dp;
        private bool IsTransactionExist;

        public EntityTransaction(DataPersistance dp)
        {
            this.dp = dp;
            IsTransactionExist = (dp.Trx != null);
            if (!IsTransactionExist) 
                dp.BeginTransaction();
        }

        public void CommitTransaction() 
        {
            if (!IsTransactionExist)
            {
                dp.CommitTransaction();
                IsTransactionExist = true;
            }
        }

        public void RollbackTransaction() 
        {
            if (!IsTransactionExist)
            {
                dp.RollbackTransaction();
                IsTransactionExist = true;
            }
        }

        void IDisposable.Dispose()
        {
            if (!IsTransactionExist)
            {
                dp.RollbackTransaction();
                IsTransactionExist = true;
            }
        }
    }
}
