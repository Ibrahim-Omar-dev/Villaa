using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Magic_villa.Model
{
    public class AppUser : IdentityUser
    {
        [Column("Name")]
        public string Name { get; set; }
    }
}
