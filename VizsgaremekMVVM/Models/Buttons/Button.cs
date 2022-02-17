using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VizsgaremekMVVM.Models.Buttons
{
    public class Button : ICommand
    {
        Action<object?> execute;
        public event EventHandler? CanExecuteChanged;
        public Button(Action<object?> execute)
        {
            this.execute = execute;
        }


        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            execute.Invoke(parameter);
        }
    }
}
