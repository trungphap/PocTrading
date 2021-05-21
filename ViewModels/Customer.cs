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
        public ICommand QueueCommand { get; }
        public IShell Shell { get; set; }
        public IShell Consummer { get; set; }
        public IShell QueueShell { get; set; }
        

        public Customer()
        {
           // MyCommand = new CustomerCommand(ExcuteMethod, CanExecuteMethod);           
            Consummer = new Shell();
            QueueShell = new Shell();
            OpenCommand = new OpenCommand(Shell);
            Shell = QueueShell;
            ProduceCommand = new ProduceCommand(Shell);
            ConsumeCommand = new ConsumeCommand(Consummer,QueueShell);
            QueueCommand = new QueueCommandG<string>(QueueShell);            
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
