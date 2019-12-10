using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediaMonkey.DataAccess.Models
{
    public class ProfileDislikedVideo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public virtual Profile Profile { get; set; }
        public int VideoId { get; set; }
        public virtual Video Video { get; set; }
    }
}