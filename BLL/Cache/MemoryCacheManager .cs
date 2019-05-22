using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL.Cache
{
    /// <summary>
    /// Represents a MemoryCacheCache
    /// </summary>
    //public partial class MemoryCacheManager : ICacheManager
    //{
    //    protected ObjectCache Cache
    //    {
    //        get
    //        {
    //            return MemoryCache.Default;
    //        }
    //    }

    //    /// <summary>
    //    /// Gets or sets the value associated with the specified key.
    //    /// </summary>
    //    /// <typeparam name="T">Type</typeparam>
    //    /// <param name="key">The key of the value to get.</param>
    //    /// <returns>The value associated with the specified key.</returns>
    //    public T Get<T>(string key)
    //    {
    //        return (T)Cache[key];
    //    }

    //    /// <summary>
    //    /// Adds the specified key and object to the cache.
    //    /// </summary>
    //    /// <param name="key">key</param>
    //    /// <param name="data">Data</param>
    //    /// <param name="cacheTime">Cache time</param>
    //    public void Set(string key, object data, int cacheTime)
    //    {
    //        if (data == null)
    //            return;

    //        var policy = new CacheItemPolicy();
    //        policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime);
    //        Cache.Add(new CacheItem(key, data), policy);
    //    }

    //    /// <summary>
    //    /// Gets a value indicating whether the value associated with the specified key is cached
    //    /// </summary>
    //    /// <param name="key">key</param>
    //    /// <returns>Result</returns>
    //    public bool IsSet(string key)
    //    {
    //        return (Cache.Contains(key));
    //    }

    //    /// <summary>
    //    /// Removes the value with the specified key from the cache
    //    /// </summary>
    //    /// <param name="key">/key</param>
    //    public void Remove(string key)
    //    {
    //        Cache.Remove(key);
    //    }

    //    /// <summary>
    //    /// Removes items by pattern
    //    /// </summary>
    //    /// <param name="pattern">pattern</param>
    //    public void RemoveByPattern(string pattern)
    //    {
    //        var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
    //        var keysToRemove = new List<String>();

    //        foreach (var item in Cache)
    //            if (regex.IsMatch(item.Key))
    //                keysToRemove.Add(item.Key);

    //        foreach (string key in keysToRemove)
    //        {
    //            Remove(key);
    //        }
    //    }

    //    /// <summary>
    //    /// Clear all cache data
    //    /// </summary>
    //    public void Clear()
    //    {
    //        foreach (var item in Cache)
    //            Remove(item.Key);
    //    }
    //}
    /// <summary>
    /// Extensions
    /// </summary>
    public static class CacheExtensions
    {
        public static T Get<T>(this ICacheManager cacheManager, string key, Func<T> acquire)
        {
            return Get(cacheManager, key, 60, acquire);
        }

        public static T Get<T>(this ICacheManager cacheManager, string key, int cacheTime, Func<T> acquire)
        {
            if (cacheManager.IsSet(key))
            {
                return cacheManager.Get<T>(key);
            }
            else
            {
                var result = acquire();
                //if (result != null)
                cacheManager.Set(key, result, cacheTime);
                return result;
            }
        }
    }
}
