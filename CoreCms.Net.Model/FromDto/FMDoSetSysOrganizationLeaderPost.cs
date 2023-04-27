using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCms.Net.Model.FromDto
{
    public class FMDoSetSysOrganizationLeaderPost
    {
        public int leaderId { get; set; }
        public int organizationId { get; set; }

        public string leaderName { get; set; }

        public string leaderPhone { get; set; }
    }
}
