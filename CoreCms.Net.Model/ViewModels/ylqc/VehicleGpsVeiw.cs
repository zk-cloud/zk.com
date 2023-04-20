using CoreCms.Net.Model.Entities;
using System;

namespace CoreCms.Net.Model.ViewModels.ylqc
{
    public class VehicleGpsVeiw
    {
        public string VIN { get; set; }

        public string Point { get; set; }
        public DateTime? GPSDate { get; set; }

        public vehicle_gbt32960 gbt32960info { get; set; }

        public vehicle vehicle { get; set; }

        public vehicle_type vtype { get; set; }

        public decimal DayMileage { get; set; }

    }

}
