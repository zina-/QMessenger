using MongoDB.Bson;

namespace QMessenger.Models
{
    public class User
    {
        public ObjectId Id { get; set; }
        public string UserId { get; set; }
        public string PasswordHash { get; set; }
        public string Alias { get; set; }
    }
}