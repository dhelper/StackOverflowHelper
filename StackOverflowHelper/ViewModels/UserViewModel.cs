using System.ComponentModel;
using System.Runtime.CompilerServices;
using StackOverflowHelper.Annotations;

namespace StackOverflowHelper.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private string _userName;
        private int _reputation;
        private int _goldMedals;
        private int _silverMedals;
        private int _bronzeMedals;
        private string _image;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
     
        public string Image
        {
            get { return _image; }
            set
            {
                if (value == _image) return;
                _image = value;
                OnPropertyChanged();
            }
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                if (value == _userName) return;
                _userName = value;
                OnPropertyChanged();
            }
        }

        public int Reputation
        {
            get { return _reputation; }
            set
            {
                if (value == _reputation) return;
                _reputation = value;
                OnPropertyChanged();
            }
        }

        public int GoldMedals
        {
            get { return _goldMedals; }
            set
            {
                if (value == _goldMedals) return;
                _goldMedals = value;
                OnPropertyChanged();
            }
        }
        public int SilverMedals
        {
            get { return _silverMedals; }
            set
            {
                if (value == _silverMedals) return;
                _silverMedals = value;
                OnPropertyChanged();
            }
        }
        public int BronzeMedals
        {
            get { return _bronzeMedals; }
            set
            {
                if (value == _bronzeMedals) return;
                _bronzeMedals = value;
                OnPropertyChanged();
            }
        }
    }
}