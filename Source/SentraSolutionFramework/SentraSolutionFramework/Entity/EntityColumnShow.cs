using System;
using System.Collections.Generic;
using System.Text;

namespace SentraSolutionFramework.Entity
{
    public class EntityColumnShow
    {
        public string ColumnShow;
        public List<ChildColumnShow> ListChild = new List<ChildColumnShow>();

        public EntityColumnShow(string ColumnShow)
        {
            this.ColumnShow = ColumnShow;
        }
    }

    public class ChildColumnShow
    {
        public string ChildName;
        public string ColumnShow;

        public ChildColumnShow(string ChildName, string ColumnShow)
        {
            this.ChildName = ChildName;
            this.ColumnShow = ColumnShow;
        }
    }
}
