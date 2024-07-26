using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class ProcessData
    {
        [Key]
        public long Pid { get; set; }
        public string Name { get; set; } = string.Empty;
        public double ProcessCpuUsage {  get; set; }
        public double ProcessMemoryUsage { get; set; }
        public bool Status { get; set; }

        public int SysDataId { get; set; }
        public SysData SysData { get; set; } = null!;
    }
}
