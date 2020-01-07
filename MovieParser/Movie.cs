﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MovieParser
{
    public class Movie
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public double? Rating { get; set; }
        public string MovieType { get; set; }
        public dynamic Description { get; internal set; }
    }
}
