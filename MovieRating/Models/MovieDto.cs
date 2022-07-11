using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRating.Models
{
    public class MovieDto
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public int RateCount { get; set; }
        public int RateTotal { get; set; }
    }
}
