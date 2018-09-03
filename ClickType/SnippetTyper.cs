using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClickType
{
    static class SnippetTyper
    {
        public static void Type(Snippet snippet)
        {
            if(snippet != null)
            {
                SendKeys.SendWait(snippet.ToString());
            }            
        }
    }
}
