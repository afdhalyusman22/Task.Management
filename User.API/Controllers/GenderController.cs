using Microsoft.AspNetCore.Mvc;
using User.API.Service.Interfaces;
using User.API.Services;

namespace User.API.Controllers
{
    public class GenderController : Controller
    {
        private readonly IGenderServices _genderServices;

        public GenderController(IGenderServices genderServices)
        {
            _genderServices = genderServices;

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {

            var res = await _genderServices.GetAsync(cancellationToken);
            return Ok(res);
        }
    }
}
