using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Password_Storage.Api.Models
{
    public class PasswordModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string account_name { get; set; }

        public string username { get; set; }

        public string password_hash { get; set; }

        public DateTime created_at { get; set; }
    }
}
