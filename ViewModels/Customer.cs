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
        public ICommand ConsumeCommand2 { get; }
        public ICommand QueueCommand { get; }
        public ICommand StopCommand { get; }
        public ICommand StopProduce { get; }
        public ICommand StopProduce2 { get; }
        public ICommand StopConsumer1 { get; }
        public ICommand StopConsumer2 { get; }
        public IShell ProducerShell2 { get; set; }
        public IShell ProducerShell { get; set; }
        public IShell Consummer { get; set; }
        public IShell Consummer2 { get; set; }
        public IShell QueueShell { get; set; }
        

        public Customer()
        {
           // MyCommand = new CustomerCommand(ExcuteMethod, CanExecuteMethod);           
            Consummer = new Shell() { StatusExecutable = true };
            Consummer2 = new Shell() { StatusExecutable = true };
            QueueShell = new Shell() { StatusExecutable = true };
            ProducerShell = new Shell() { StatusExecutable =true};
            ProducerShell2 = new Shell() { StatusExecutable =true};
            OpenCommand = new OpenCommand(ProducerShell);

            ProduceCommand2 = new ProduceCommand(ProducerShell2, QueueShell);
            ProduceCommand = new ProduceCommand(ProducerShell, QueueShell);
            ConsumeCommand = new ConsumeCommand(Consummer,QueueShell);
            ConsumeCommand2 = new ConsumeCommand(Consummer2,QueueShell);
            QueueCommand = new QueueCommandG<string>(QueueShell);
            StopCommand = new StopCommand(QueueShell);

            StopProduce = new StopTask(ProducerShell);            
            StopProduce2 = new StopTask(ProducerShell2);            
            StopConsumer1 = new StopTask(Consummer);            
            StopConsumer2 = new StopTask(Consummer2);            
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
