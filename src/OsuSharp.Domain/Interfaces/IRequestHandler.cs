using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace OsuSharp.Interfaces;

/// <summary>
/// Interfaces the handler that creates and make request to the osu! api.
/// </summary>
public interface IRequestHandler : IDisposable
{
    /// <summary>
    /// Sends a request that doesn't return a model.
    /// </summary>
    /// <param name="request">Details of the request to make.</param>
    /// <param name="token">Cancellation token.</param>
    Task SendAsync(
        IOsuApiRequest request,
        CancellationToken token = default);

    /// <summary>
    /// Sends a GET request and gets the raw response as a stream.
    /// </summary>
    /// <param name="request">Details of the request to make.</param>
    /// <param name="token">Cancellation token.</param>
    Task<Stream> GetStreamAsync(
        IOsuApiRequest request,
        CancellationToken token = default);

    /// <summary>
    /// Sends a request and return the deserialized model from it.
    /// </summary>
    /// <param name="request">Details of the request to make.</param>
    /// <param name="token">Cancellation token.</param>
    /// <typeparam name="T">Type of the final object.</typeparam>
    Task<T> SendAsync<T>(
        IOsuApiRequest request,
        CancellationToken token = default)
        where T : class;

    /// <summary>
    /// Sends a request and return the deserialized models from it.
    /// </summary>
    /// <param name="request">Details of the request to make.</param>
    /// <param name="token">Cancellation token.</param>
    /// <typeparam name="TImplementation">Type of the final object.</typeparam>
    /// <typeparam name="TModel">Type of the json model.</typeparam>
    Task<IReadOnlyList<TImplementation>> SendMultipleAsync<TImplementation, TModel>(
        IOsuApiRequest request,
        CancellationToken token = default)
        where TModel : class;

    /// <summary>
    /// Sends a request and return the deserialized model from it.
    /// </summary>
    /// <param name="request">Details of the request to make.</param>
    /// <param name="token">Cancellation token.</param>
    /// <typeparam name="TImplementation">Type of the final object.</typeparam>
    /// <typeparam name="TModel">Type of the json model.</typeparam>
    Task<TImplementation> SendAsync<TImplementation, TModel>(
        IOsuApiRequest request, 
        CancellationToken token = default)
        where TModel : class;
}