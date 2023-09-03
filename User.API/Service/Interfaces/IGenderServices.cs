using Library.Backend.Helpers;

namespace User.API.Service.Interfaces
{
    public interface IGenderServices
    {
        Task<List<DropdownBase<int>>> GetListAsync(CancellationToken cancellationToken);
    }
}
