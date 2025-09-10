using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPW
{
    internal static class Extensions
    {
        public static void InvokeIfRequired(this Control value,Action action)
        {
            if (value.InvokeRequired)
                value.Invoke(action);
            else
                action();

        }
    }
}
