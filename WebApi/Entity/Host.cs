using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class PhyHost
    {
        [Key]
        public int HostId { get; set; }
        public string HostName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string IpAddr { get; set; } = string.Empty;
        public string MacAddr { get; set; } = string.Empty;
        public SysData SysData { get; set; } = new SysData();
        public List<VirtualHost> VirtualHosts { get; set; } = new List<VirtualHost>();
        public bool IsBoot { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

    }
}
