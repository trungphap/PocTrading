using Commands;
using Models;
using System;
using System.Windows;
using System.Windows.Input;

namespace ViewModels
{
    public class Customer
    {
        public ICommand MyCommand { get; }
        public ICommand OpenCommand { get; }
        public ICommand ProduceCommand { get; }
        public ICommand ConsumeCommand { get; }
        public IShell Shell { get; set; }
        public IShell Consummer { get; set; }
        

        public Customer()
        {
           // MyCommand = new CustomerCommand(ExcuteMethod, CanExecuteMethod);
            Shell = new Shell();
            Consummer = new Shell();
            OpenCommand = new OpenCommand(Shell);
            ProduceCommand = new ProduceCommand(Shell);
            ConsumeCommand = new ConsumeCommand(Consummer);            
        }


        public bool CanExecuteMethod(Object p)
        {
            return true;
        }

        public void ExcuteMethod(Object p)
        {
            MessageBox.Show("Command executed");

        }
    }
}
