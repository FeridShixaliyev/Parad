using System.ComponentModel.DataAnnotations;

namespace Parad.ViewModels
{
    public class LoginVM
    {
        [Required,MaxLength(30)]
        public string UsernameOrEmail { get; set; }
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
