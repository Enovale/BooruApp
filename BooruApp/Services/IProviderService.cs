using System.Collections.Generic;
using System.Threading.Tasks;
using BooruApp.Api;
using BooruApp.Backend;
using Glitonea.Mvvm;

namespace BooruApp.Services
{
    public interface IProviderService : IService
    {
        IReadOnlyCollection<IProvider> Providers { get; }

        public IProvider? GetProvider(string idName);
    }
}