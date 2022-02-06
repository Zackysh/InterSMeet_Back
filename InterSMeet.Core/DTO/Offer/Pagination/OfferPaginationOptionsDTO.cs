using System.ComponentModel.DataAnnotations;

namespace InterSMeet.Core.DTO.Offer
{
    public class OfferPaginationOptionsDTO
    {
        [Range(0, int.MaxValue)]
        public int Page { get; set; }
        [Range(1, int.MaxValue)]
        public int Size { get; set; }
        public bool SkipExpired { get; set; } = true;

        /// <summary>
        /// If true:
        ///     - Students will retrieve its applications
        ///     - Companies will retrieve its offers with applicant data
        /// </summary>
        public bool PrivateData { get; set; } = false;

        /// <summary> Search by offer name + description </summary>
        public string? Search { get; set; }

        // @ Filters
        public int? CompanyId { get; set; }
        public int? DegreeId { get; set; }
        public int? FamilyId { get; set; }
        public int? LevelId { get; set; }

        // @ Salary Filter
        [Range(0, int.MaxValue)]
        public double? Min { get; set; }
        [Range(0, int.MaxValue)]
        public double? Max { get; set; }
    }
}
