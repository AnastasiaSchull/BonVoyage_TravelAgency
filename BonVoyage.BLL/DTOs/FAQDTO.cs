using System.ComponentModel.DataAnnotations;

namespace BonVoyage.BLL.DTOs
{
    public class FAQDTO
    {
        public int FAQId { get; set; }

        [Required(ErrorMessage = "Question is required")]
        public string? Question { get; set; }

        [Required(ErrorMessage = "Answer is required")]
        public string? Answer { get; set; }
    }
}
