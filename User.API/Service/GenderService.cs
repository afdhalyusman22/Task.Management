using Microsoft.EntityFrameworkCore;
using User.API.Entities;
using User.API.Models.Gender;
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

        public async Task<List<GenderDto>> GetListAsync(CancellationToken cancellationToken)
        {
            var genders = await _context.Genders
                .AsNoTracking()
                .Select(Q => new GenderDto
                {
                    Id = Q.GenderId,
                    Name = Q.Name
                }).ToListAsync(cancellationToken);

            return genders;
        }
    }
}
