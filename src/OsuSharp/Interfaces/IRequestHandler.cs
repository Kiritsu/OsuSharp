using System;
using System.Threading.Tasks;
using OsuSharp.Models;

namespace OsuSharp.Interfaces
{
    public interface IRequestHandler : IDisposable
    {
        void UpdateAuthorization(
            OsuToken token);
        
        Task SendAsync(
            IOsuApiRequest request);

        Task<T> SendAsync<T, TModel>(
            IOsuApiRequest request)
            where T : class
            where TModel : class;
    }
}