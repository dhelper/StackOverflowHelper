using StackOverflowHelper.Repository.Data;
using StackOverflowHelper.ViewModels;

namespace StackOverflowHelper
{
    internal class UserFactory
    {
        private static bool SameUserAsPrevious(User input, UserViewModel previous)
        {
            return previous != null && previous.UserName == input.display_name;
        }

        public UserViewModel CreateAndInitializeViewModel(User input, UserViewModel previous)
        {
            var result = UserConvertor.CreateViewModel(input);

            result.ReputationChange = CalculateReputationChange(input, previous);

            if (SameUserAsPrevious(input, previous))
            {
                result.GoldBadges.Change = input.badge_counts.Gold - previous.GoldBadges.Count;
                result.SilverBadges.Change = input.badge_counts.Silver - previous.SilverBadges.Count;
                result.BronzeBadges.Change = input.badge_counts.Bronze - previous.BronzeBadges.Count;

            }

            return result;
        }

        private static int CalculateReputationChange(User input, UserViewModel previous)
        {
            if (!SameUserAsPrevious(input, previous))
            {
                return input.reputation_change_day;
            }

            return input.reputation - previous.Reputation;
        }
    }
}