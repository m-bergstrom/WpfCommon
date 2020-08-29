using System;
using System.Windows.Input;
using JetBrains.Annotations;

namespace WpfCommon.Commands
{
    public class DelegateCommand : ICommand
    {
        [NotNull] private readonly Action<object> _Execute;
        [NotNull] private readonly Func<object, bool> _CanExecute;

        public DelegateCommand([NotNull] Action<object> execute, [NotNull] Func<object, bool> canExecute)
        {
            _Execute = execute;
            _CanExecute = canExecute;
        }

        public DelegateCommand([NotNull] Action execute, [NotNull] Func<bool> canExecute) : this(o => execute(),
            o => canExecute())
        {
        }

        public bool CanExecute(object parameter)
        {
            return _CanExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _Execute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}