using CloudDT.ContainerAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudDT.ContainerAPI.Controllers
{
    public class NodeController : ControllerBase
    {
        private readonly NodeService nodeService;

        public NodeController(NodeService nodeService)
        {
            this.nodeService = nodeService;
        }

        [HttpPost]
        public IActionResult Index(string code)
        {
            nodeService.Save(code).Run();
            return Ok();
        }
    }
}
