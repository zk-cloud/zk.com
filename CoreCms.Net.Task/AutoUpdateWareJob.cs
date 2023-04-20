using CoreCms.Net.IService;

namespace CoreCms.Net.TaskJob
{
    public class AutoUpdateWareJob
    {
        private readonly IWareService _IWareService;

        public AutoUpdateWareJob(IWareService IWareService) {
            _IWareService = IWareService;
        }

        public async System.Threading.Tasks.Task Execute() {
            await _IWareService.WareChange("123123");
        }

    }
}
