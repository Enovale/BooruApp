using System.Collections.Generic;
using BooruApp.Api.Interfaces;
using BooruApp.Api.Models;
using Glitonea.Mvvm;

namespace BooruApp.Services
{
    public interface IServerService : IService, IServerProvider
    {
        IReadOnlyDictionary<string, RegisteredServerProvider> Providers { get; }
    }
}