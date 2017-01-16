using System.ComponentModel;
using System.Runtime.CompilerServices;
using StackOverflowHelper.Annotations;

namespace StackOverflowHelper.ViewModels
{
    public class MedalViewModel : INotifyPropertyChanged
    {
        private int _count;
        private int _change;
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public int Count
        {
            get { return _count; }
            set
            {
                if (value == _count) return;
                _count = value;
                OnPropertyChanged();
            }
        }

        public int Change
        {
            get { return _change; }
            set
            {
                if (value == _change) return;
                _change = value;
                OnPropertyChanged();
            }
        }
    }
}