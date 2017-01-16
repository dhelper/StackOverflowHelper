using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using StackOverflowHelper.Annotations;
using StackOverflowHelper.Repository;

namespace StackOverflowHelper.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly UserRepository _userRepository = new UserRepository(new JsonWebClient());
        private string _userId = "22656";
        private UserViewModel _userActiveUser;
        private string _status;

        public event PropertyChangedEventHandler PropertyChanged;


        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand LoadUserDetailsCommand
        {
            get { return new DelegateCommand(LoadUserDetails); }
        }

        private async void LoadUserDetails()
        {

            var result = await _userRepository.GetUser(_userId);
            var newViewModel = new UserViewModel
            {
                UserName = result.display_name,
                Reputation = result.reputation,
                Image = result.profile_image,
                GoldMedals = result.badge_counts.Gold,
                SilverMedals = result.badge_counts.Silver,
                BronzeMedals = result.badge_counts.Bronze
            };

            ActiveUser = newViewModel;
        }


        public string UserId
        {
            get { return _userId; }
            set
            {
                if (value == _userId) return;
                _userId = value;
                OnPropertyChanged();
            }
        }

        public string Status
        {
            get { return _status; }
            set
            {
                if (value == _status) return;
                _status = value;
                OnPropertyChanged();
            }
        }

        public UserViewModel ActiveUser
        {
            get { return _userActiveUser; }
            set
            {
                if (Equals(value, _userActiveUser)) return;
                _userActiveUser = value;
                OnPropertyChanged();
            }
        }
    }
}
