using Library.Backend.Helpers;
using Microsoft.EntityFrameworkCore;
using User.API.Entities;
using User.API.Models.User;
using User.API.Services.Interfaces;

namespace User.API.Service
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        public UserService(DataContext context)
        {
            _context = context;
        }

        public async Task<ValidationError> UpdateAsync(Guid userId, UpdateDto dto, CancellationToken cancellationToken)
        {
            var user = await _context.UserAccounts
                .Where(Q => Q.UserId == userId && Q.DeletedAt == null)
                .FirstOrDefaultAsync(cancellationToken);

            if (user == null)
            {
                return new ValidationError
                {
                    Field = nameof(dto.GenderId),
                    Message = "User not found",
                };
            }

            var gender = await _context.Genders
                .Where(Q => Q.GenderId == dto.GenderId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (gender == null)
            {
                return new ValidationError
                {
                    Field = nameof(dto.GenderId),
                    Message = "Gender not valid",
                };
            }

            var phoneNumberExist = await _context.UserAccounts
                .AnyAsync(Q => Q.PhoneNumber == dto.PhoneNumber && Q.UserId != userId, cancellationToken);

            if (phoneNumberExist)
            {
                return new ValidationError
                {
                    Field = nameof(dto.GenderId),
                    Message = "Phone number already exist",
                };
            }

            user.PhoneNumber = dto.PhoneNumber;
            user.FullName = dto.FullName;
            user.GenderId = dto.GenderId;

            _context.Update(user);
            await _context.SaveChangesAsync();

            return null;
        }


        public async Task<(ValidationError error, DetailDto user)> GetAsync(Guid userId, CancellationToken cancellationToken)
        {
            var user = await _context.UserAccounts
                .Where(Q => Q.UserId == userId && Q.DeletedAt == null)
                .AsNoTracking()
                .Select(Q => new DetailDto
                {
                    Email = Q.Email,
                    FullName = Q.FullName,
                    GenderId = Q.GenderId,
                    GenderName = Q.Gender.Name,
                    LastLogin = Q.LastLogin,
                    RegisterAt = Q.RegisterAt,
                    PhoneNumber = Q.PhoneNumber,
                    UserId = Q.UserId
                }).FirstOrDefaultAsync(cancellationToken);

            if (user == null)
            {
                return (new ValidationError
                {
                    Field = nameof(userId),
                    Message = "User not found",
                }, null);
            }

            return (null, user);
        }

        public async Task<ValidationError> DeleteAsync(Guid userId, CancellationToken cancellationToken)
        {
            var user = await _context.UserAccounts
                .Where(Q => Q.UserId == userId && Q.DeletedAt == null)
                .FirstOrDefaultAsync(cancellationToken);

            if (user == null)
            {
                return new ValidationError
                {
                    Field = nameof(userId),
                    Message = "User not found",
                };
            }
            user.DeletedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return null;
        }
    }
}
