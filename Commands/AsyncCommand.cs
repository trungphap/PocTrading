using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Commands
{
    public abstract class AsyncCommand : IAsyncCommand
    {
        readonly ObservableCollection<Task> runningTasks;
        protected AsyncCommand()
        {
            runningTasks = new ObservableCollection<Task>();
            runningTasks.CollectionChanged += OnRunningTaskChanged;
        }

        private void OnRunningTaskChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CommandManager.InvalidateRequerySuggested();
        }

        public IEnumerable<Task> RunningTasks { get => runningTasks; }


        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }


        public abstract bool CanExecute(object parameter);


        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute(parameter);
        }

        async void ICommand.Execute(object parameter)
        {
            Task runningTask = ExecuteAsync(parameter);
            runningTasks.Add(runningTask);
            try
            {
                await runningTask;
            }
            finally
            {
                runningTasks.Remove(runningTask);
            }

        }

        public abstract Task ExecuteAsync(object parameter);

    }
}
