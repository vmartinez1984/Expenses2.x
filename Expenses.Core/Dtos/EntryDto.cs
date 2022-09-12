using System.ComponentModel.DataAnnotations;

namespace Expenses.Core.Dtos
{
    public class EntryDtoIn
    {
        [Required]
        [StringLength(25)]
        public string Name { get; set; }

        [Required]
        [Range(1, 13000)]
        public decimal Amount { get; set; }
        
        [Required]
        public string PeriodId { get; set; }
    }

    public class EntryDto : EntryDtoIn
    {
        public string Id { get; set; }
        
        public DateTime DateRegistration { get; set; }

    }
}