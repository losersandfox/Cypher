namespace WebApi.Service.CollectData
{
    public class GetSystemData
    {
        public async static Task<SysData> GetSysDataAsync()
        {
            return await Task.FromResult(new SysData());
        }

        public static SysData GetSysData()
        {
            return new SysData();
        }
    }
}
