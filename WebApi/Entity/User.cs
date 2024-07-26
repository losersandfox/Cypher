using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class User
    {
        public long UserId { get; set; }

        [Range(1,50)] 
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        [Range(8,40)]
        public string password {  get; set; } = string.Empty;
        public List<PhyHost> LinksHosts { get; set; } = new List<PhyHost>();
        public PhyHost NowHost { get; set; } = new PhyHost();
    }
}
