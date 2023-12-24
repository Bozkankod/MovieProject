using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MovieProject.Entitites.Base;

namespace MovieProject.Entitites
{
    public class Movie : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Title { get; set; }

        public double VoteAverage { get; set; }

        public int VoteCount { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }
    }
}
