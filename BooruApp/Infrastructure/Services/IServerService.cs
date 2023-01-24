using System.Collections.Generic;
using BooruApp.Api.Interfaces;
using BooruApp.Infrastructure.Models;
using Glitonea.Mvvm;

namespace BooruApp.Infrastructure.Services
{
    public interface IServerService : IService, IServerProvider
    {
        IReadOnlyDictionary<string, RegisteredServerProvider> Providers { get; }
    }
}