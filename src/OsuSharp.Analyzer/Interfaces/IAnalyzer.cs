using System.Collections.Generic;
using OsuSharp.Endpoints;

namespace OsuSharp.Analyzer.Interfaces
{
    public interface IAnalyzer<TKey, TValue> where TValue : Endpoint
    {
        /// <summary>
        ///     Represents every entities that are being updated.
        /// </summary>
        IReadOnlyDictionary<TKey, TValue> Entities { get; }

        /// <summary>
        ///     Adds an entity to analyze.
        /// </summary>
        /// <param name="entity">Entity to analyze.</param>
        void Add(TKey key, TValue entity);

        /// <summary>
        ///     Removes an entity to stop analyzing it.
        /// </summary>
        /// <param name="entity">Entity to remove.</param>
        void Remove(TKey key);
    }
}
