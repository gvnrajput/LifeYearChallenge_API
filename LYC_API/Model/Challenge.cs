using System.ComponentModel.DataAnnotations;

namespace LYC_API.Model
{
    public class Challenge
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; } = string.Empty;
        [Required]
        public string DailyTargetInMins { get; set; }
        [Required]
        public string StartDate { get; set; }
        [Required]
        public string EndDate { get; set; }
        public string Color { get; set; }
        public string CreatedOn { get; set; }
    }
}
