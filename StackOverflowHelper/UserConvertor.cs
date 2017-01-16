using StackOverflowHelper.Repository.Data;
using StackOverflowHelper.ViewModels;

namespace StackOverflowHelper
{
    public static class UserConvertor
    {
        public static UserViewModel CreateViewModel(User input)
        {
            var newUser = new UserViewModel
            {
                UserName = input.display_name,
                Reputation = input.reputation,
                Image = input.profile_image,
                GoldBadges = new MedalViewModel { Count = input.badge_counts.Gold },
                SilverBadges = new MedalViewModel { Count = input.badge_counts.Silver },
                BronzeBadges = new MedalViewModel { Count = input.badge_counts.Bronze },
            };

            return newUser;
        }
    }
}
