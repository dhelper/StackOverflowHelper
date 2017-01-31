using System;
using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;
using StackOverflowHelper.Repository;
using StackOverflowHelper.Repository.Data;

namespace StackOverflowHelperTests
{
    [TestFixture]
    public class UserRepositoryTests
    {
        [Test]
        public async Task GetUser_ReturnedEmptyStringFromClient_ReturnNull()
        {
            var fakeJsonClient = new FakeJsonClient("");

            var userRepository = new UserRepository(fakeJsonClient);

            var result = await userRepository.GetUser("");

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetUser_ReturnedValidStringFromClient_ReturnUser()
        {
            var input = "{\"items\":[{\"badge_counts\":{\"bronze\":7610,\"silver\":6728,\"gold\":513},\"account_id\":11683,\"is_employee\":false,\"last_modified_date\":1485376893,\"last_access_date\":1485880825,\"age\":40,\"reputation_change_year\":6698,\"reputation_change_quarter\":6698,\"reputation_change_month\":6698,\"reputation_change_week\":630,\"reputation_change_day\":215,\"reputation\":924041,\"creation_date\":1222430705,\"user_type\":\"registered\",\"user_id\":22656,\"accept_rate\":89,\"location\":\"Reading, United Kingdom\",\"website_url\":\"http://csharpindepth.com\",\"link\":\"http://stackoverflow.com/users/22656/jon-skeet\",\"profile_image\":\"https://www.gravatar.com/avatar/6d8ebb117e8d83d74ea95fbdd0f87e13?s=128&d=identicon&r=PG\",\"display_name\":\"Jon Skeet\"}],\"has_more\":false,\"quota_max\":300,\"quota_remaining\":289}";
            var fakeJsonClient = new FakeJsonClient(input);

            var userRepository = new UserRepository(fakeJsonClient);

            var result = await userRepository.GetUser("");


            var expected = new User
            {
                display_name = "Jon Skeet",
                reputation = 924041
            };
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public async Task GetUser_ClientThrowsCommunicationException_ThrowsException()
        {
            var fakeJsonClient = new FakeJsonClientThatThrowsException();

            var userRepository = new UserRepository(fakeJsonClient);

            Assert.ThrowsAsync<ApplicationException>(() => userRepository.GetUser(""));
        }
    }

    public class FakeJsonClientThatThrowsException : IJsonWebClient
    {
        public Task<string> HttpGetUncompressedAsync(string url)
        {
            throw new ApplicationException();
        }
    }

    public class FakeJsonClient : IJsonWebClient
    {
        private readonly string _result;

        public FakeJsonClient(string result)
        {
            _result = result;
        }
        public Task<string> HttpGetUncompressedAsync(string url)
        {
            return Task.FromResult(_result);
        }
    }
}