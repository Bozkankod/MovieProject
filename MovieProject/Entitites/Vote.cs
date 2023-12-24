using MovieProject.Entitites.Base;

namespace MovieProject.Entitites
{
    public class Vote : BaseEntity
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string UserId { get; set; }
        public double Point { get; set; }
        public string Comment { get; set; }
    }
}
