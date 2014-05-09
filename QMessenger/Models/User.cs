using System;
using MongoDB.Bson;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace QMessenger.Models
{
    public class User
    {
        [JsonIgnore]
        public ObjectId Id { get; set; }

        public string UserId { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }

        public string Alias { get; set; }

        public List<Friend> Friends { get; set; }

        public User()
        {
            Friends = new List<Friend>();
        }
    }
}