using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPLATFORM.Entities.ViewModels
{
   public class MissionViewModel
    {

        public long MissionId { get; set; }

        public string? Theme { get; set; }

        public long CityId { get; set; }

        public string CityName { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string? ShortDescription { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool MissionType { get; set; }

        public string? OrganizationName { get; set; }

        public string? MediaName { get; set; }

        public int Rating { get; set; }

        public string? GoalObjectiveText { get; set; }

        public int? GoalValue { get; set; }

        public int? TotalSeat { get; set; }

        public DateTime? Deadline { get; set; }
    }
}
