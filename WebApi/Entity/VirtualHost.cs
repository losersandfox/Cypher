using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class VirtualHost
    {
        [Key]
        public int VirtualHostId { get; set; }
        public string VirtualHostName { get; set; } = string.Empty;
        public string IpAddr { get; set; } = string.Empty;
        public bool IsBoot { get; set; }

        public int HostId { get; set; }
        public PhyHost Host { get; set; } = null!;
    }
}
