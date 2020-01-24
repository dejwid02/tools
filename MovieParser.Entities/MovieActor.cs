using System;
using System.Collections.Generic;
using System.Text;

namespace MovieParser.Entities
{
    public class MovieActor
    {
        public int MovieId { get; set; }
        public int ActorId { get; set; }

        public Movie Movie { get; set; }
        public Actor Actor { get; set; }
    }
}
