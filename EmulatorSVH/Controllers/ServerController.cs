using EmulatorSVH.Contracts;
using Microsoft.AspNetCore.Mvc;
using EmulatorSVH.Application.Interface;



namespace EmulatorSVH.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServerController(IEmulatorServices srvService) : ControllerBase
    {


        private readonly IEmulatorServices _srvService = srvService;


        [HttpPost("GetPackage")]
        public async Task<IActionResult> GetPkgAll(PackageResponse pkgSend)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _srvService.GetPackageList();

            return Ok(result);
        }
        [HttpPost("GetDocsPackage")]
        public async Task<IActionResult> GetDocsPkg(DocumentResponse docSend)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _srvService.GetDocumentList(docSend.Pid);

            return Ok(result);
        }
        [HttpPost("GetDocRecord")]
        public async Task<IActionResult> GetRecord(DocRequest docSend)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var Doc = await _srvService.GetDocument(docSend.Id);
            var result = await _srvService.GetRecord(Doc.DocId);

            return Ok(result);
        }
    }
}
