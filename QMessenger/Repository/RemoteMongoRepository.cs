using MongoDB.Driver;
using MongoDB.Driver.Linq;
using QMessenger.Models;
using System;
using System.Linq;

namespace QMessenger.Repository
{
    public class RemoteMongoRepository : IRemoteRepository, IDisposable
    {
        private MongoClient client;
        private MongoServer server;
        private MongoDatabase db;

        public RemoteMongoRepository()
        {
            //string mongoConnectionString = ConfigurationManager.ConnectionStrings["MongoDbConnectionString"].ConnectionString;
            string mongoConnectionString = "mongodb://admin:dnjem!1@localhost/";

            client = new MongoClient(mongoConnectionString);
            server = client.GetServer();
            db = server.GetDatabase("QMessenger");
        }

        public void Dispose()
        {
            // Nothing in here...
        }

        public User GetUserById(string userId)
        {
            MongoCollection<User> users = db.GetCollection<User>("Users");

            return users.AsQueryable()
                .Where(user => user.UserId == userId)
                .SingleOrDefault();
        }

        public User GetUserByIdAndPasswordHash(string userId, string passwordHash)
        {
            MongoCollection<User> users = db.GetCollection<User>("Users");

            return users.AsQueryable()
                .Where(user => user.UserId == userId && user.PasswordHash == passwordHash)
                .SingleOrDefault();
        }

        //public bool HasUserIdAndPasswordHash(string userId, string passwordHash)
        //{
        //    MongoCollection<User> users = db.GetCollection<User>("Users");

        //    return users.AsQueryable()
        //        .Any(user => user.UserId == userId && user.PasswordHash == passwordHash);
        //}
    }
}