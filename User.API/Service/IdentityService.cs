namespace User.API.Services
{
    public class IdentityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? UserId => _httpContextAccessor?.HttpContext?
                                            .User
                                            .Claims
                                            .FirstOrDefault(Q => Q.Type == "userId")
                                            ?.Value;
    }
}
