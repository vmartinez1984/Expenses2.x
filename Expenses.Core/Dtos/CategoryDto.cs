using System.ComponentModel.DataAnnotations;

namespace Expenses.Core.Dtos
{
    public class CategoryDto : CategoryDtoIn
    {
        public string Id { get; set; }

        //public DateTime DateRegister { get; set; } = DateTime.Now;
    }

    public class CategoryDtoIn
    {
        public string Name { get; set; }
    }
}