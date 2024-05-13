using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObject
{
    public class BookDto
    {
        public int BookId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [Range(100, 1000000)]
        public decimal Price { get; set; }
    }
}
