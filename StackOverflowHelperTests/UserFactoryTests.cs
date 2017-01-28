using NUnit.Framework;
using StackOverflowHelper;
using StackOverflowHelper.Repository.Data;

namespace StackOverflowHelperTests
{
    [TestFixture]
    public class UserFactoryTests
    {
        [Test]
        public void CreateAndInitializeViewModel_PreviousModelWasNullAndNoReputationGainedThatDay_ReputationChnageZero()
        {
            var user = new User
            {
                reputation_change_day = 0,
                badge_counts = new BadgeCounts()
            };

            var userFactory = new UserFactory();

            var result = userFactory.CreateAndInitializeViewModel(user, null);

            Assert.That(result.ReputationChange, Is.EqualTo(0));
        }
    }
}
