using System;
using System.Windows.Input;

namespace WinMpcTrayIcon
{
    public class DelegateCommand : ICommand
    {
        public Action<string> CommandAction { get; set; }

        public Func<bool> CanExecuteFunc { get; set; }

        public void Execute(object parameter = null)
        {
            CommandAction((string)parameter);
        }

        public bool CanExecute(object parameter)
        {
            return CanExecuteFunc == null  || CanExecuteFunc();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}