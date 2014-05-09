using QMessenger.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace QMessenger.Repository
{
    public class LocalCacheRepository
    {
        private readonly static Lazy<LocalCacheRepository> instance =
            new Lazy<LocalCacheRepository>(() => new LocalCacheRepository());

        public static LocalCacheRepository Instance { get { return instance.Value; } }

        private ConcurrentDictionary<string, LocalCache> localRepo = new ConcurrentDictionary<string, LocalCache>();

        private LocalCacheRepository()
        {
            // Nothing in here...
        }

        public bool TryRepositoryAdd(string userId, User user)
        {
            return localRepo.TryAdd(userId, new LocalCache { UserInfo = user });
        }

        public void TryRepositoryAdd(string userId, string connectionId)
        {
            localRepo.AddOrUpdate(
                userId
                , (string k) =>
                {
                    using (RemoteMongoRepository remoteRepo = new RemoteMongoRepository())
                    {
                        User user = remoteRepo.GetUserById(k);

                        var localCache = new LocalCache();
                        localCache.UserInfo = user;
                        localCache.ConnectionIdCollection.Add(connectionId);

                        return localCache;
                    }
                }
                , (string k, LocalCache localCache) =>
                {
                    localCache.ConnectionIdCollection.Add(connectionId);

                    return localCache;
                });
        }

        public void TryRepositoryRemoveConnection(string userId, string connectionId)
        {
            LocalCache value;
            if (localRepo.TryGetValue(userId, out value))
            {
                value.ConnectionIdCollection.Remove(connectionId);
            }
        }

        public void TryRepositoryRemove(string userId)
        {
            LocalCache value;
            localRepo.TryRemove(userId, out value);
        }

        public List<string> TryRepositoryGetConnectionId(string userId)
        {
            LocalCache value;
            if (localRepo.TryGetValue(userId, out value))
            {
                return value.ConnectionIdCollection;
            }
            throw new Exception("Failed to TryGetValue");
        }

        public User TryRepositoryGetUser(string userId)
        {
            LocalCache value;
            if (localRepo.TryGetValue(userId, out value))
            {
                return value.UserInfo;
            }
            throw new Exception("Failed to TryGetValue");
        }
    }
}