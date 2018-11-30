using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using OsuSharp.Analyzer.Interfaces;
using OsuSharp.Endpoints;
using OsuSharp.Interfaces;

namespace OsuSharp.Analyzer
{
    public abstract class Analyzer<TKey, TValue> : IAnalyzer<TKey, TValue> where TValue : Endpoint
    {
        protected readonly ConcurrentDictionary<TKey, TValue> _entities;
        protected IOsuApi Api { get; }

        public IReadOnlyDictionary<TKey, TValue> Entities => _entities;

        public void Add(TKey key, TValue entity)
        {
            if (!_entities.TryAdd(key, entity))
            {
                throw new InvalidOperationException($"The given entity is already being analyzed. If you wish to force update it, please use UpdateValueAsync(T)");
            }
        }

        public void Remove(TKey key)
        {
            if (!_entities.TryRemove(key, out _))
            {
                throw new InvalidOperationException($"The given entity is not being analyzed yet.");
            }
        }

        public abstract Task<TValue> UpdateValueAsync(TKey key);

        /// <summary>
        ///     Fires when a value has been updated.
        /// </summary>
        public abstract event EventHandler<TValue> EntityUpdated;

        public Analyzer(IOsuApi api)
        {
            Api = api;
            _entities = new ConcurrentDictionary<TKey, TValue>();
        }
    }
}
