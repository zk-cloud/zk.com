using CoreCms.Net.Models;
using System.Threading.Tasks;

namespace CoreCms.Net.IService
{
    public interface IWareService
    {
        public Task<ResponseBase> WareChange(string warename);

        public Task<ResponseBase> FileChange(string filename);
    }
}
