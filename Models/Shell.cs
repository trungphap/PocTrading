using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Models
{
    public class Shell : IShell, INotifyPropertyChanged
    {
        string _statusText;
        bool _statusExecutable;
        public string StatusText
        {
            get => _statusText;
            set
            {
                _statusText = value;
                OnPropertyChanged();
            }
        }

        public bool StatusExecutable
        {
            get => _statusExecutable;
            set
            {
                _statusExecutable = value;
                OnPropertyChanged();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
