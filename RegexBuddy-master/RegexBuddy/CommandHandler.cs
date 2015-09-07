using System;
using System.Linq.Expressions;
using System.Windows.Input;

namespace RegexBuddy
{
    public class CommandHandler : ICommand
    {
        private readonly Action _action;
        private readonly Func<bool> _canExecute;

        public CommandHandler(Action action, bool canExecute)
        {
            _action = action;

            var expression = Expression.Constant(canExecute, typeof (bool));
            var lambda = Expression.Lambda<Func<bool>>(expression);

            _canExecute = lambda.Compile();
        }

        public CommandHandler(Action action, Func<bool> canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            var value = _canExecute.Invoke();

            return value;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _action();
        }
    }
}