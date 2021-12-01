using System.Collections.Generic;

namespace CloudDT.Shared.Models
{
    public class Container
    {
        public Container()
        {
            ContainerId = string.Empty;
            ForwardPorts = new();
        }

        public string ContainerId { get; set; }

        public List<int> ForwardPorts { get ;set; }
    }
}
