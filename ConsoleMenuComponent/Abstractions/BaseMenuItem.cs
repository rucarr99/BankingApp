namespace ConsoleMenuComponent.Abstractions
{
    public abstract class BaseMenuItem : IExecutable
    {
        public int Shortcut { get; }
        public string Text { get; set; }
        protected BaseMenuItem(int shortcut, string text)
        {
            Shortcut = shortcut;
            Text = text;
        }
        public abstract void Execute(object parentObject);
    }
}
