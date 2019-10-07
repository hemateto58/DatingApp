using System.ComponentModel.DataAnnotations;

namespace DatingApp.api.Deto
{
    public class registerUser
    {
        [Required]
        public string name { get; set; }
        [Required]
        [StringLength(8,MinimumLength=4,ErrorMessage="4 to 8")]
        public string password { get; set; }

    }
}