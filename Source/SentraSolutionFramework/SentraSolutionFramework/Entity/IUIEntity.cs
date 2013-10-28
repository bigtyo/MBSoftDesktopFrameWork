using System;
using System.Collections.Generic;
using System.Text;

namespace SentraSolutionFramework.Entity
{
    public interface IUIEntity
    {
        bool TryGetFocusedRowValue<TType>(string TableName,
            string FieldName, out TType Value);
    }
}
