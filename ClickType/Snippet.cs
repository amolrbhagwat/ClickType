using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickType
{
    class Snippet
    {
        public string Text
        {
            get;
            private set;
        }

        public Snippet(string text)
        {
            Text = text;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
