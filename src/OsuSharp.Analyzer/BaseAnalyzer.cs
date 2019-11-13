using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace OsuSharp.Analyzer
{
    public abstract class BaseAnalyzer<TKey, TValue>
    {
        protected readonly ConcurrentDictionary<TKey, TValue> _entities;
        protected OsuClient _client;

        /// <summary>
        ///     Entities and their current value that are being tracked by this Analyzer.
        /// </summary>
        public IReadOnlyDictionary<TKey, TValue> Entities;

        /// <summary>
        ///     Adds an entity to analyze.
        /// </summary>
        /// <param name="key">Key of the entity to track.</param>
        /// <param name="entity">Entity to track.</param>
        public virtual void AddEntity(TKey key, TValue entity)
        {
            if (!_entities.TryAdd(key, entity))
            {
                throw new InvalidOperationException($"The given entity is already being tracked. If you wish to force update it, please use UpdateValueAsync(T)");
            }
        }

        /// <summary>
        ///     Removes an entity to stop tracking it.
        /// </summary>
        /// <param name="key">Entity key to remove.</param>
        public virtual void RemoveEntity(TKey key)
        {
            if (!_entities.TryRemove(key, out _))
            {
                throw new InvalidOperationException($"The given entity is not being tracked yet.");
            }
        }

        /// <summary>
        ///     Updates the entity associated with the given key. If the result is different from the current stored value, it will fire the EntityUpdated event.
        /// </summary>
        /// <param name="key">Key associated with the TValue.</param>
        /// <param name="token">Cancellation Token to cancel the current requests.</param>
        /// <returns></returns>
        public abstract Task<TValue> UpdateEntityAsync(TKey key, CancellationToken token = default);

        /// <summary>
        ///     Events that fire each time an entity has been updated.
        /// </summary>
        public Func<EntityUpdateEventArgs<TValue>, Task> EntityUpdated;

        protected BaseAnalyzer(OsuClient client)
        {
            _client = client;
            _entities = new ConcurrentDictionary<TKey, TValue>();
            Entities = new ReadOnlyDictionary<TKey, TValue>(_entities);
        }
    }
}
