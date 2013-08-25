using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MvvmCountriesDemo.Commands
{
    public class DelegateCommand : ICommand
    {
        private Func<object, bool> _canExecute;
        private Action<object> _executeAction;
        bool canExecuteCache;

        public DelegateCommand(Action<object> executeAction)
            : this(executeAction, null)
        {

        }

        public DelegateCommand(Action<object> executeAction, Func<object, bool> canExecute)
        {
            this._executeAction = executeAction;
            this._canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            bool temp = _canExecute(parameter);

            if (canExecuteCache != temp)
            {
                canExecuteCache = temp;

                if (CanExecuteChanged != null)
                {
                    CanExecuteChanged(this, new EventArgs());
                }
            }

            return canExecuteCache;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _executeAction.Invoke(parameter);
        }

        protected virtual void OnCanExecuteChanged(EventArgs e)
        {
            var canExecuteChanged = CanExecuteChanged;

            if (canExecuteChanged != null)
            {
                canExecuteChanged(this, e);
            }
        }

        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged(EventArgs.Empty);
        }
    }
}
