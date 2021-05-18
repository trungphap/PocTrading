using System;
using System.Windows.Input;

namespace Commands
{
    public class CustomerCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        Action<Object> _executeMethod;
        Func<Object, bool> _canExecuteMethod;
        public CustomerCommand(Action<Object> executeMethod, Func<Object, bool> canExecuteMethod)
        {
            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecuteMethod(parameter);
        }

        public void Execute(object parameter)
        {
            _executeMethod(parameter);
        }
    }
}
