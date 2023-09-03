using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.API.Service.Interfaces;

namespace User.API.Controllers
{
    [Authorize]
    [Route("api/gender")]
    [ApiController]
    public class GenderController : Controller
    {
        private readonly IGenderServices _genderServices;

        public GenderController(IGenderServices genderServices)
        {
            _genderServices = genderServices;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var res = await _genderServices.GetListAsync(cancellationToken);
            return Ok(res);
        }
    }
}
