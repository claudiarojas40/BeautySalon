using System.ComponentModel.DataAnnotations;
namespace BeautySalon.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El campo Login es obligatorio.")]
        public string Login { get; set; } = null!;

        [Required(ErrorMessage = "El campo Password es obligatorio.")]
        public string Password { get; set; } = null!;
    }
}

