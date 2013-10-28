using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Persistance;

namespace SentraWinFramework
{
    public interface IEntityForm
    {
        void ShowNew();
        void ShowView(string Condition, params FieldParam[] Parameters);
        void ShowViewWithKey(string Key);
        Type GetEntityType();
        void SetOwner(EntityForm EntityForm);
    }
}
