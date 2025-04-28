using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Investing.Models.DTO
{
    public class CredentialsDTO
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public bool IsRememberMe { get; set; }
    }
}
