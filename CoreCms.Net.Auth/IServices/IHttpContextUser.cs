using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreCms.Net.Auth.IServices
{
    public interface IHttpContextUser
    {
        public string GetToken();

        public string GetClaim(string Claimtype);
    }
}
