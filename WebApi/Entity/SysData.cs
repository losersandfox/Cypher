using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class SysData
    {
        // CPU data
        [Key]
        public int Id { get; set; } 
        public double CpuUsage { get; set; }
        public double[] PerCoreUsage { get; set; }

        // Memory data
        public long TotalMemory { get; set; }
        public long UsedMemory { get; set; }
        public long AvailableMemory { get; set; }
        public long CachedMemory { get; set; }
        public long SwapTotal { get; set; }
        public long SwapUsed { get; set; }

        // Disk I/O data
        public long DiskReadBytes { get; set; }
        public long DiskWriteBytes { get; set; }
        public long DiskReadOps { get; set; }
        public long DiskWriteOps { get; set; }

        // Network data
        public long NetworkReceivedBytes { get; set; }
        public long NetworkSentBytes { get; set; }
        public Dictionary<string, long> InterfaceUsage { get; set; } = new Dictionary<string, long>();

        // Processes data
        public int ProcessCount { get; set; } 
        public LinkedList<ProcessData> processes { get; set; } = new LinkedList<ProcessData>();

        // GPU data (if applicable)
        public double GpuUsage { get; set; }
        public long GpuMemoryUsed { get; set; }

        // System logs (example, can be more detailed)
        public List<string> SystemLogs { get; set; } = new List<string>();

        // System uptime
        public TimeSpan UpTime { get; set; }

        public int HostId { get; set; }
        public PhyHost Host { get; set; } = null!;

        public int VirtualHostId { get; set; }
        public VirtualHost VirtualHost { get; set; } = null!;
    }
}
