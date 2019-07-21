using System.Collections.Generic;
using System.Threading.Tasks;
using Romulox.Core.Entities;

namespace Romulox.Core.Interfaces
{
    // Interface that any API handling class can implement in order to be injected
    public interface IGameProvider
    { 
        Task<List<Game>> ProvideGamesAsync(Platform platform, IGameNameTransformer gameNameTransformer);
    }
}