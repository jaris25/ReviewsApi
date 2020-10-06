using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Reviews.Data.Models
{
    public class ReviewForCreationDto
    {
        [MaxLength(300)]
        public string Feedback { get; set; }
        [Required]
        [Range(1, 10, ErrorMessage = "Rating value must be between 1 and 10")]
        public int Rating { get; set; }
    }
}
