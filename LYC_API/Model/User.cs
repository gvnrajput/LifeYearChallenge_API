using System.ComponentModel.DataAnnotations;

namespace LYC_API.Model
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        public string NickName { get; set; } = string.Empty;
        public string CreatedOn { get; set; }
    }
}
