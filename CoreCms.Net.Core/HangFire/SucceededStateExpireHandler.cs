using Hangfire.States;
using Hangfire.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreCms.Net.Core.HangFire
{
    //1、添加SucceededStateExpireHandler 继承接口 IStateHandler
  public class SucceededStateExpireHandler : IStateHandler
    {
        public TimeSpan JobExpirationTimeout;
        public SucceededStateExpireHandler(int jobExpirationTimeout)
        {
            JobExpirationTimeout = TimeSpan.FromMinutes(jobExpirationTimeout);
        }

        public string StateName => SucceededState.StateName;


        public void Apply(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {
            context.JobExpirationTimeout = JobExpirationTimeout;
        }

        public void Unapply(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {

        }

    }
}
