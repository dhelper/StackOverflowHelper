using System.ComponentModel;
using System.Runtime.CompilerServices;
using StackOverflowHelper.Annotations;

namespace StackOverflowHelper.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private string _userName;
        private int _reputation;
        private MedalViewModel _goldBadges;
        private MedalViewModel _silverBadges;
        private MedalViewModel _bronzeBadges;
        private string _image;
        private int _reputationChange;

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

        public MedalViewModel GoldBadges
        {
            get { return _goldBadges; }
            set
            {
                if (value == _goldBadges) return;
                _goldBadges = value;
                OnPropertyChanged();
            }
        }
        public MedalViewModel SilverBadges
        {
            get { return _silverBadges; }
            set
            {
                if (value == _silverBadges) return;
                _silverBadges = value;
                OnPropertyChanged();
            }
        }
        public MedalViewModel BronzeBadges
        {
            get { return _bronzeBadges; }
            set
            {
                if (value == _bronzeBadges) return;
                _bronzeBadges = value;
                OnPropertyChanged();
            }
        }

        public int ReputationChange
        {
            get { return _reputationChange; }
            set
            {
                if (value == _reputationChange) return;
                _reputationChange = value;
                OnPropertyChanged();
            }
        }
    }
}