using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Security_CSharp.Security.Entitites
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public string Username { get; set; }


        [EmailAddress]
        [Required]
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public ICollection<Role> Roles { get; set; } = new List<Role>();
    }
}
