using CoreCms.Net.Model.ViewModels.ylqc;
using System;
using System.Collections.Generic;

namespace CoreProject.Net.Models.FromDate
{
    public class FMTrackDto
    {
        public string VIN { get; set; }

        public DateTime begindate { get; set; }

        public DateTime enddate { get; set; }
    }

    public class FMTracksDto
    {
        public List<VehicleTreeVeiw> dtolist { get; set; }

        public DateTime begindate { get; set; }

        public DateTime enddate { get; set; }

        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        
    }
}
