﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StackOverflowHelper.Repository.Data;

namespace StackOverflowHelper.Repository
{
    class UserRepository
    {
        private readonly IJsonWebClient _client;

        public UserRepository(IJsonWebClient client)
        {
            _client = client;
        }

        public async Task<User> GetUser(string userId)
        {
            var url = string.Format("https://api.stackexchange.com/2.1/users/{0}?site=stackoverflow", userId);

            var response = await _client.HttpGetUncompressedAsync(url);
            if (String.IsNullOrEmpty(response))
            {
                return null;
            }

            var jobject = JObject.Parse(response);

            var userJsonString = jobject["items"].First();

            var result = JsonConvert.DeserializeObject<User>(userJsonString.ToString());

            return result;
        }
    }
}
