using System;

namespace Maple.Api.Models
{
    public class CoveragePlanModel
    {
        public string CoveragePlan { get; set; }
        public DateTime EligibilityDateFrom { get; set; }
        public DateTime EligibilityDateTo { get; set; }
        public string EligibilityCountry { get; set; }
    }
}
