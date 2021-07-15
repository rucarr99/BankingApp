using System;
using ConsoleMenuComponent.Abstractions;

namespace ConsoleMenuComponent
{
    public class ConsoleMenuItem : BaseMenuItem
    {
        private readonly Action<object> _executeAction;

        public ConsoleMenuItem(int shortCut, string text, Action<object> executeAction) : base(shortCut, text)
        {
            _executeAction = executeAction;
        }
        public override void Execute(object parentObject)
        {
            _executeAction?.Invoke(parentObject);
        }
    }
}
