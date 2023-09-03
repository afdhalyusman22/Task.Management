using Library.Backend.Helpers;
using Microsoft.EntityFrameworkCore;
using User.API.Entities;
using User.API.Service.Interfaces;

namespace User.API.Service
{
    public class GenderService : IGenderServices
    {
        private readonly DataContext _context;

        public GenderService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<DropdownBase<int>>> GetListAsync(CancellationToken cancellationToken)
        {
            var genders = await _context.Genders
                .AsNoTracking()
                .Select(Q => new DropdownBase<int>
                {
                    Id = Q.GenderId,
                    Label = Q.Name
                }).ToListAsync(cancellationToken);

            return genders;
        }
    }
}
