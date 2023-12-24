using System.Collections.Generic;
using MovieProject.Entitites;
using Newtonsoft.Json;

namespace MovieProject.Models.Responses
{
    public class ResultMovie
    {
        public ResultMovie()
        {
            Movies = new List<Movie>();
        }

        [JsonProperty("results")]
        public virtual List<Movie> Movies { get; set; }
    }
}
