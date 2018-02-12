using System.ComponentModel.DataAnnotations;

namespace SMSGateway.Models
{
    public class TextMessage
    {
        [Required]
        [Phone]
        public string FromNumber { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(140)]
        public string Content { get; set; }

        [Required]
        [Phone]
        public string ToNumber { get; set; }
    }
}