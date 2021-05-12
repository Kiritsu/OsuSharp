using System;
using System.Threading.Tasks;

namespace OsuSharp.Interfaces
{
    /// <summary>
    /// Interfaces the handler that creates and make request to the osu! api.
    /// </summary>
    public interface IRequestHandler : IDisposable
    {
        /// <summary>
        /// Sends a request that doesn't return a model.
        /// </summary>
        /// <param name="request">Details of the request to make.</param>
        Task SendAsync(
            IOsuApiRequest request);

        /// <summary>
        /// Sends a request and return the deserialized model from it.
        /// </summary>
        /// <param name="request">Details of the request to make.</param>
        /// <typeparam name="T">Type of the final object.</typeparam>
        /// <typeparam name="TModel">Type of the json model.</typeparam>
        Task<T> SendAsync<T, TModel>(
            IOsuApiRequest request)
            where T : class
            where TModel : class;
    }
}