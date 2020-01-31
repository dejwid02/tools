using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Data
{
    public class MovieUserData
    {
        public int Id { get; set; }
        public Movie Movie { get; set; }
        public bool DontShow { get; set; }
    }
}
