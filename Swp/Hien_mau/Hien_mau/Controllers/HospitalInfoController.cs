using Hien_mau.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hien_mau.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalInfoController : ControllerBase
    {
        private readonly IHospitalInfo _service;
        public HospitalInfoController(IHospitalInfo service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetHospitalInfo()
        {
            var hospitalInfo = await _service.GetHospitalInfo();
            return Ok(hospitalInfo);
        }
    }
}
