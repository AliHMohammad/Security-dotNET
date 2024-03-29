using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Security_CSharp.Security.Entitites
{
    public class Role
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Name { get; set; }

        public ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}
