using System;
using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;

namespace CoreNotes.AutoFac.Common.Cache
{
    /// <summary>
    /// Redis缓存接口实现
    /// </summary>
    public class RedisCacheService : ICacheService
    {
        protected RedisCache RedisCache = null;

        public RedisCacheService(RedisCacheOptions options)
        {
            RedisCache = new RedisCache(options);
        }



        public bool Add(string key, object value, int expirationTime = 20)
        {
            if (!string.IsNullOrEmpty(key))
            {
                RedisCache.Set(key, Encoding.UTF8.GetBytes(value.ToString()), new DistributedCacheEntryOptions()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(expirationTime)
                });
            }
            return true;
        }

        public bool Remove(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return false;
            }

            if (Exists(key))
            {
                RedisCache.Remove(key);
                return true;
            }
            return false;
        }


        public string GetValue(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }
            if (Exists(key))
            {
                return RedisCache.GetString(key);
            }
            return null;
        }


        public bool Exists(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return false;
            }
            return !string.IsNullOrEmpty(RedisCache.GetString(key)) ? true : false;
        }

    }
}