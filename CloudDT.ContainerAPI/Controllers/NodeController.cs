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

        public IActionResult Index(string code)
        {
            if(Request.Method == "Options")
                return Ok();
            
            nodeService.Save(code).Run();
            return Ok();
        }
    }
}
