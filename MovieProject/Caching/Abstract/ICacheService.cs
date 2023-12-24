namespace MovieProject.Caching.Abstract
{
    public interface ICacheService
    {
        T Get<T>(string key);
        void Set<T>(string key, T value, TimeSpan expiration);

        bool IsSet(string key);

        void Remove(string key);
    }
}