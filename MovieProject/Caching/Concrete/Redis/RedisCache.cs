using Microsoft.Extensions.Options;
using MovieProject.Caching.Abstract;
using MovieProject.Settings;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace MovieProject.Caching.Concrete.Redis
{
    public class RedisCache : ICacheService
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        private readonly RedisSetting _redisSetting;

        public RedisCache(IOptions<RedisSetting> redisSetting)
        {
            _redisSetting = redisSetting.Value;
            _redis = ConnectionMultiplexer.Connect(_redisSetting.RedisConnectionString);
            _database = _redis.GetDatabase();
        }

        public T Get<T>(string key)
        {

            var serializedValue = _database.StringGet(key);
            if (!serializedValue.HasValue)
            {
                return default;
            }
            return JsonConvert.DeserializeObject<T>(serializedValue);
        }
        public void Set<T>(string key, T value, TimeSpan expiration)
        {
            var serializedValue = JsonConvert.SerializeObject(value);
            _database.StringSet(key, serializedValue, expiration);
        }

        public void Remove(string key)
        {
            _database.KeyDelete(key);
        }

        public bool IsSet(string key)
        {
            return _database.KeyExists(key);
        }
    }
}
