using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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

        private Snippet selectedSnippet;
        public Snippet SelectedSnippet
        {
            get
            {
                return selectedSnippet;
            }
            set
            {
                selectedSnippet = value;
                TypedText = selectedSnippet?.ToString() ?? "";
            }
        }

        private string typedText;
        public string TypedText
        {
            get
            {
                return typedText;
            }
            set
            {
                typedText = value;
                RaisePropertyChanged("TypedText");
            }
        }

        public ICommand AddSnippetCommand { get; private set; }
        public ICommand EditSnippetCommand { get; private set; }
        public ICommand DeleteSnippetCommand { get; private set; }

        public ClickTypeViewModel()
        {
            AddSnippetCommand = new AddSnippetHandler(this);
            EditSnippetCommand = new EditSnippetHandler(this);
            DeleteSnippetCommand = new DeleteSnippetHandler(this);
            Snippets = SnippetLoader.LoadSnippets();
        }

        public void AddSnippet()
        {
            SnippetLoader.AddSnippet(TypedText);
            Snippets = SnippetLoader.LoadSnippets();
            TypedText = null; // Since adding a snippet with no prior selection does not clear this out
        }

        public void EditSelectedSnippet()
        {
            SnippetLoader.EditSnippet(SelectedSnippet.id, TypedText);
            Snippets = SnippetLoader.LoadSnippets();
        }

        public void DeleteSelectedSnippet()
        {
            SnippetLoader.DeleteSnippet(SelectedSnippet.id);
            Snippets = SnippetLoader.LoadSnippets();
        }

        public void TypeSnippet(Snippet snippet)
        {
            SnippetTyper.Type(snippet);
        }

        protected void RaisePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        class AddSnippetHandler : ICommand
        {
            private ClickTypeViewModel clickTypeViewModel;

            public AddSnippetHandler(ClickTypeViewModel clickTypeViewModel)
            {
                this.clickTypeViewModel = clickTypeViewModel;
            }

            public bool CanExecute(object parameter)
            {
                string typedText = clickTypeViewModel.TypedText;
                return (!string.IsNullOrEmpty(typedText) || !string.IsNullOrWhiteSpace(typedText));
            }

            public void Execute(object parameter)
            {
                clickTypeViewModel.AddSnippet();
            }

            // Solution from https://stackoverflow.com/a/42110060
            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

            public void RaiseCanExecuteChanged()
            {
                CommandManager.InvalidateRequerySuggested();
            }
        }

        class EditSnippetHandler : ICommand
        {
            private ClickTypeViewModel clickTypeViewModel;

            public EditSnippetHandler(ClickTypeViewModel clickTypeViewModel)
            {
                this.clickTypeViewModel = clickTypeViewModel;
            }

            public bool CanExecute(object parameter)
            {
                return clickTypeViewModel.SelectedSnippet != null &&
                        !string.IsNullOrEmpty(clickTypeViewModel.TypedText);
            }

            public void Execute(object parameter)
            {
                clickTypeViewModel.EditSelectedSnippet();
            }

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

            public void RaiseCanExecuteChanged()
            {
                CommandManager.InvalidateRequerySuggested();
            }
        }

        class DeleteSnippetHandler : ICommand
        {
            private ClickTypeViewModel clickTypeViewModel;

            public DeleteSnippetHandler(ClickTypeViewModel clickTypeViewModel)
            {
                this.clickTypeViewModel = clickTypeViewModel;
            }

            public bool CanExecute(object parameter)
            {
                return clickTypeViewModel.SelectedSnippet != null;
            }

            public void Execute(object parameter)
            {
                clickTypeViewModel.DeleteSelectedSnippet();
            }

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

            public void RaiseCanExecuteChanged()
            {
                CommandManager.InvalidateRequerySuggested();
            }
        }
    }
}
