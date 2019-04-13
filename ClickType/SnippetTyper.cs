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
