using Microsoft.AspNetCore.Mvc;
using WebApi.Service.CollectData;

namespace WebApi.Controllers.CollectSysData
{
    [ApiController]
    [Route("[controller]")]
    public class SysDataController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<SysData>> GetSysData()
        {
            try
            {
                return Ok(await GetSystemData.GetSysDataAsync());
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }
    }
}
