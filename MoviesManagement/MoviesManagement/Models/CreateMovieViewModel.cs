using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesManagement.Models
{
    public class CreateMovieViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public int? Year { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public bool IsRecorded { get; set; }
        [Required]
        public DateTime RecordingDate { get; set; }
        [Required]
        [Remote("VerifyImageFile", "Movies")]
        public string ImageFile { get; set; }
        public double? Rating { get; internal set; }
        public long Id { get; set; }
    }
}
