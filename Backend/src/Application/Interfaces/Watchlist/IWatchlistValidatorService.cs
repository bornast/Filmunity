using Application.Dtos.Watchlist;
using System.Threading.Tasks;

namespace Application.Interfaces.Watchlist
{
    public interface IWatchlistValidatorService
    {
        Task ValidateForCreation(WatchlistForCreationDto watchlistForCreation);
        Task ValidateForUpdate(int id, WatchlistForUpdateDto watchlistForUpdate);
    }
}
