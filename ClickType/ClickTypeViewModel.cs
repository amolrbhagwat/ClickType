using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickType
{
    class ClickTypeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private List<Snippet> snippets;
        public List<Snippet> Snippets
        {
            private set
            {
                snippets = value;
                RaisePropertyChanged("Snippets");
            }
            get
            {
                return snippets;
            }
        }

        public ClickTypeViewModel()
        {
            Snippets = SnippetLoader.LoadSnippets();
        }

        protected void RaisePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
