namespace ClickType
{
    class Snippet
    {
        public readonly long id;

        public string SnippetText
        {
            get;
            private set;
        }

        public Snippet()
        {
        }

        public Snippet(long id, string text)
        {
            this.id = id;
            SnippetText = text;
        }
               
        public override string ToString()
        {
            return SnippetText;
        }
    }
}
