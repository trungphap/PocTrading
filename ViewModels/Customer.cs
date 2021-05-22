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
        public ICommand ProduceCommand2 { get; }
        public ICommand ConsumeCommand { get; }
        public ICommand QueueCommand { get; }
        public ICommand StopCommand { get; }
        public ICommand StopProduce { get; }
        public IShell ProducerShell2 { get; set; }
        public IShell ProducerShell { get; set; }
        public IShell Consummer { get; set; }
        public IShell QueueShell { get; set; }
        

        public Customer()
        {
           // MyCommand = new CustomerCommand(ExcuteMethod, CanExecuteMethod);           
            Consummer = new Shell() { StatusExecutable = true };
            QueueShell = new Shell() { StatusExecutable = true };
            ProducerShell = new Shell() { StatusExecutable =true};
            ProducerShell2 = new Shell() { StatusExecutable =true};
            OpenCommand = new OpenCommand(ProducerShell);

            ProduceCommand2 = new ProduceCommand(ProducerShell2, QueueShell);
            ProduceCommand = new ProduceCommand(ProducerShell, QueueShell);
            ConsumeCommand = new ConsumeCommand(Consummer,QueueShell);
            QueueCommand = new QueueCommandG<string>(QueueShell);
            StopCommand = new StopCommand(QueueShell);
            StopProduce = new StopTask(ProducerShell);            
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
