using System.Collections.Generic;
using OsuSharp.Endpoints;

namespace OsuSharp.Analyzer.Interfaces
{
    public interface IAnalyzer<TKey, TValue>
    {
        /// <summary>
        ///     Represents every entities that are being updated.
        /// </summary>
        IReadOnlyDictionary<TKey, TValue> Entities { get; }

        /// <summary>
        ///     Adds an entity to analyze.
        /// </summary>
        /// <param name="key">Key of the entity to analyze.</param>
        /// <param name="entity">Entity to analyze.</param>
        void AddEntity(TKey key, TValue entity);

        /// <summary>
        ///     Removes an entity to stop analyzing it.
        /// </summary>
        /// <param name="key">Entity key to remove.</param>
        void RemoveEntity(TKey key);
    }
}
