using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SentraWinFramework
{
    public interface IFormBindingSource
    {
        BindingSource GetMainBindingSource();
    }
}
