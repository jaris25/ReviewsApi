using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Reviews.Data.Entities
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Feedback { get; set; }
        [Required]
        [Range(1,10, ErrorMessage = "Rating value must be between 1 and 10")]
        public int Rating { get; set; }
        [ForeignKey("ItemId")]
        public Item Item { get; set; }
        public int ItemId { get; set; }
    }
}
