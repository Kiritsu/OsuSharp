using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OsuSharp.Analyzer.Entities;
using OsuSharp.Analyzer.Interfaces;
using OsuSharp.Interfaces;

namespace OsuSharp.Analyzer
{
    public abstract class Analyzer<TKey, TValue> : IAnalyzer<TKey, TValue>
    {
        protected readonly ConcurrentDictionary<TKey, TValue> _entities;
        protected IOsuApi Api { get; }

        /// <summary>
        ///     Represents every entities that are being updated.
        /// </summary>
        public IReadOnlyDictionary<TKey, TValue> Entities => _entities;

        /// <summary>
        ///     Adds an entity to analyze.
        /// </summary>
        /// <param name="key">Key of the entity to analyze.</param>
        /// <param name="entity">Entity to analyze.</param>
        public void AddEntity(TKey key, TValue entity)
        {
            if (!_entities.TryAdd(key, entity))
            {
                throw new InvalidOperationException($"The given entity is already being analyzed. If you wish to force update it, please use UpdateValueAsync(T)");
            }
        }

        /// <summary>
        ///     Removes an entity to stop analyzing it.
        /// </summary>
        /// <param name="key">Entity key to remove.</param>
        public void RemoveEntity(TKey key)
        {
            if (!_entities.TryRemove(key, out _))
            {
                throw new InvalidOperationException($"The given entity is not being analyzed yet.");
            }
        }

        /// <summary>
        ///     Updates the entity associated with the given key.
        /// </summary>
        /// <param name="key">Key associated with the TValue.</param>
        /// <returns></returns>
        public abstract Task<TValue> UpdateEntityAsync(TKey key, CancellationToken token = default);

        /// <summary>
        ///     Fires when a value has been updated.
        /// </summary>
        public abstract event EventHandler<UpdateEventArgs<TValue>> EntityUpdated;

        public Analyzer(IOsuApi api)
        {
            Api = api;
            _entities = new ConcurrentDictionary<TKey, TValue>();
        }
    }
}
