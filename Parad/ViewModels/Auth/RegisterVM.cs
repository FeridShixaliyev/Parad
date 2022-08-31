using System.ComponentModel.DataAnnotations;

namespace Parad.ViewModels
{
    public class RegisterVM
    {
        [Required,MaxLength(30)]
        public string FirstName { get; set; }
        [Required,MaxLength(30)]
        public string LastName { get; set; }
        [Required,MaxLength(20)]
        public string Username { get; set; }
        [Required,DataType(DataType.EmailAddress),MaxLength(30)]
        public string Email { get; set; }
        [Required,DataType(DataType.Password),MaxLength(30)]
        public string Password { get; set; }
        [Required,DataType(DataType.Password),Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
