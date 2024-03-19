using alamos_Backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DPCtlUruNet;
using DPUruNet;
using System.Diagnostics;
using alamos_Backend.Models;

namespace alamos_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FingerprintReaderController : ControllerBase
    {
        private readonly AplicationDbContext _context;

        public FingerprintReaderController(AplicationDbContext dbContext) 
        {
            _context = dbContext;   
        }

        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody] List<string> dataFingers)
        {
            try
            {
                List<Fmd> listFinger = new List<Fmd>();

                if (dataFingers.Count() == 0) 
                {
                    return BadRequest("Datos Vacios");
                }

                for (int i = 0; i < dataFingers.Count(); i++) 
                {
                    var dataB64 = dataFingers[i].Replace('-', '+').Replace('_', '/').PadRight(4 * ((dataFingers[i].Length + 3) / 4), '=');
                    DataResult<Fmd> importerResult = Importer.ImportFmd(Convert.FromBase64String(dataB64), Constants.Formats.Fmd.DP_PRE_REGISTRATION, Constants.Formats.Fmd.DP_PRE_REGISTRATION);
                    
                    listFinger.Add(importerResult.Data);
                }

                var enrollment = Enrollment.CreateEnrollmentFmd(Constants.Formats.Fmd.DP_REGISTRATION, listFinger);

                var xml = Fmd.SerializeXml(enrollment.Data);

                return Ok(xml.ToString());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        
        }

        [HttpPost("find")]
        public async Task<IActionResult> findUser([FromBody] List<string> dataFingers)
        {
            try
            {
                Users userFinded = new Users();

                var users = await _context.users.ToListAsync();

                string dataB64 = dataFingers[0].Replace('-', '+').Replace('_', '/').PadRight(4 * ((dataFingers[0].Length + 3) / 4), '=');
                DataResult<Fmd> importerResult = Importer.ImportFmd(Convert.FromBase64String(dataB64), Constants.Formats.Fmd.DP_VERIFICATION, Constants.Formats.Fmd.DP_VERIFICATION);
                Fmd compare = importerResult.Data;

                users.ForEach(user =>
                {
                    if (user.registroHuellaDigital == null) return;
                    if (user.registroHuellaDigital == "") return;

                    var extraction = Fmd.DeserializeXml(user.registroHuellaDigital);
                    var result = Comparison.Compare(compare, 0, extraction, 0);

                    if (result.ResultCode == Constants.ResultCode.DP_SUCCESS) 
                    {
                        if (result.Score == 0) 
                        {
                            userFinded = user;
                        }
                    }
                }
                );

                return Ok(userFinded);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
}
