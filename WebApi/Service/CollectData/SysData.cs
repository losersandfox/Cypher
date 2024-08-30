using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Service.CollectData
{
    public class SysData
    {
        private double cpuUsage;
        private double[]? perCoreUsage;

        // Memory data
        private long totalMemory;
        private long availableMemory;

        // Disk I/O data
        private FrozenDictionary<string, double> diskReadBytes;
        private FrozenDictionary<string, double> diskWriteBytes;

        // Network data
        private FrozenDictionary<string, double> networkReceivedBytes;
        private FrozenDictionary<string, double> networkSentBytes;

        // Processes data
        private int processCount;
        private FrozenDictionary<int, string> processes;

        // GPU data (if applicable)
        private double gpuUsage;
        private long gpuMemoryUsed;

        // System uptime
        private TimeSpan upTime;

        /*
         * ------------Get System Data-------------
         */

        public double CpuUsage
        {
            get
            {
                var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                
                cpuCounter.NextValue(); // 第一次调用通常返回0，需要延时调用第二次
                Task.Delay(1000);
                cpuUsage = cpuCounter.NextValue();
                return cpuUsage;
            }
        }

        public double[] PerCpuUsage
        {
            get
            {
                var coreCount = Environment.ProcessorCount;
                perCoreUsage = new double[coreCount];

                for (int i = 0; i < coreCount; i++)
                {
                    var coreCounter = new PerformanceCounter("Processor", "% Processor Time", i.ToString());
                    _ =  coreCounter.NextValue();
                    Task.Delay(1000);
                    perCoreUsage[i] = coreCounter.NextValue();
                }
                return perCoreUsage;
            }
        }

        public long TotalMemory
        {
            get
            {
                var counter = new PerformanceCounter("Memory", "Committed Bytes");
                totalMemory = counter.RawValue;
                return totalMemory;
            }
        }
        public long AvailableMemory
        {
            get
            {
                var counter = new PerformanceCounter("Memory", "Available Bytes");
                availableMemory = counter.RawValue;
                return availableMemory;
            }
        }

        public FrozenDictionary<string, double> DiskReadBytes
        {
            get
            {
                string categoryName = "PhysicalDisk";
                string counterName = "Disk Read Bytes/sec";

                var category = new PerformanceCounterCategory(categoryName);
                var instanceNames = category.GetInstanceNames();
                var temp = new Dictionary<string, double>();

                for (int i = 0; i < instanceNames.Length; i++)
                {
                    var diskReadBytesCounter = new PerformanceCounter(categoryName , counterName, instanceNames[i]);
                    diskReadBytesCounter.NextValue();
                    Task.Delay(1000);
                    temp.Add(instanceNames[i], diskReadBytesCounter.NextValue());
                }

                diskReadBytes = temp.ToFrozenDictionary<string, double>();
                return diskReadBytes;
            }
        }
        public FrozenDictionary<string, double> DiskWriteBytes
        {
            get
            {
                string categoryName = "PhysicalDisk";
                string counterName = "Disk Write Bytes/sec";

                var category = new PerformanceCounterCategory(categoryName);
                var instanceNames = category.GetInstanceNames();
                var temp = new Dictionary<string, double>();

                for (int i = 0; i < instanceNames.Length; i++)
                {
                    var diskWriteBytesCounter = new PerformanceCounter(categoryName, counterName, instanceNames[i]);
                    diskWriteBytesCounter.NextValue();
                    Task.Delay(1000);
                    temp.Add(instanceNames[i], diskWriteBytesCounter.NextValue());
                }

                diskWriteBytes = temp.ToFrozenDictionary<string, double>();
                return diskWriteBytes;
            }
        }
        public FrozenDictionary<string, double> NetworkRecieveBytes
        {
            get
            {
                string categoryName = "Network Interface";
                string counterNameReceived = "Bytes Received/sec";

                var category = new PerformanceCounterCategory(categoryName);
                var instanceNames = category.GetInstanceNames();
                var temp = new Dictionary<string, double>();

                for (int i = 0; i < instanceNames.Length; i++)
                {
                    PerformanceCounter networkReceivedCounter = new PerformanceCounter(categoryName, counterNameReceived, instanceNames[i]);
                    networkReceivedCounter.NextValue();
                    Task.Delay(1000);
                    temp.Add(instanceNames[i], networkReceivedCounter.NextValue());
                }
                networkReceivedBytes = temp.ToFrozenDictionary<string, double>();
                return networkReceivedBytes; 
            }
        }
        public FrozenDictionary<string, double> NetworkSentBytes
        {
            get
            {
                string categoryName = "Network Interface";
                string counterNameSent = "Bytes Sent/sec";

                var category = new PerformanceCounterCategory(categoryName);
                var instanceNames = category.GetInstanceNames();
                var temp = new Dictionary<string, double>();

                for (int i = 0; i < instanceNames.Length; i++)
                {
                    PerformanceCounter networkSentCounter = new PerformanceCounter(categoryName, counterNameSent, instanceNames[i]);
                    networkSentCounter.NextValue();
                    Task.Delay(1000);
                    temp.Add(instanceNames[i], networkSentCounter.NextValue());
                }
                networkSentBytes = temp.ToFrozenDictionary<string, double>();
                return networkSentBytes;
            }
        }

        public int ProcessCount
        {
            get
            {
                var temp = Process.GetProcesses();
                Dictionary<int, string> keyValuePairs = new Dictionary<int, string>();
                foreach (var process in temp)
                {
                    //保留前台进程
                    if (!(process.ProcessName.ToLower().Equals("explorer")
                       || process.MainWindowHandle == IntPtr.Zero))
                        keyValuePairs.Add(process.Id, process.ProcessName);
                }
                processCount = keyValuePairs.Count;
                return processCount;
            }
        }

        public FrozenDictionary<int, string> Processes
        {
            get
            {
                var temp = Process.GetProcesses();
                Dictionary<int, string> keyValuePairs = new Dictionary<int, string>();
                foreach (var process in temp)
                {
                    //保留前台进程
                    if(!(process.ProcessName.ToLower().Equals("explorer")
                       || process.MainWindowHandle == IntPtr.Zero))
                        keyValuePairs.Add(process.Id, process.ProcessName);
                }
                processes = keyValuePairs.ToFrozenDictionary<int, string>();
                return processes;
            }
        }

        public TimeSpan UpTime
        {
            get
            {
                upTime = TimeSpan.FromMilliseconds(Environment.TickCount64);
                return upTime;
            }
        }
    }
}
