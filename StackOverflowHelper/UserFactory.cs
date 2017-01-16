using StackOverflowHelper.Repository.Data;
using StackOverflowHelper.ViewModels;

namespace StackOverflowHelper
{
    internal class UserFactory
    {
        public UserViewModel CreateAndInitializeViewModel(User input, UserViewModel previous)
        {
            var result = UserConvertor.CreateViewModel(input);

            result.ReputationChange = CalculateReputationChange(input, previous);

            if (previous != null)
            {
                result.GoldBadges.Change = input.badge_counts.Gold - previous.GoldBadges.Count;
                result.SilverBadges.Change = input.badge_counts.Silver - previous.SilverBadges.Count;
                result.BronzeBadges.Change = input.badge_counts.Bronze - previous.BronzeBadges.Count;

            }

            return result;
        }

        private static int CalculateReputationChange(User input, UserViewModel previous)
        {
            if (previous == null)
            {
                return input.reputation_change_day;
            }

            return input.reputation - previous.Reputation;
        }
    }
}