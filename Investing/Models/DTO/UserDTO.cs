using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Investing.Models.DTO
{
    public class UserDTO
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
     
        public string Name { get; set; }

        public int Age { get; set; }

    }
}
