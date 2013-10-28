using System;
using System.Collections.Generic;
using System.Text;

namespace SentraSolutionFramework.Entity
{
    public interface ICancelEntity
    {
        string CancelStatus { get; set; }
        DateTime CancelDateTime { get; set; }
        string CancelUser { get; set; }
        string CancelNotes { get; set; }
    }
}
