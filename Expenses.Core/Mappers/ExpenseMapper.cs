using AutoMapper;
using Expenses.Core.Dtos;
using Expenses.Core.Entities;

namespace Expenses.Core.Mappers
{
   public class ExpenseMapper: Profile
    {
        public ExpenseMapper()
        {
            CreateMap<CategoryDtoIn, CategoryEntity>();
            CreateMap<CategoryEntity,CategoryDto>();

            CreateMap<SubcategoryDtoIn, SubcategoryEntity>();
            CreateMap<SubcategoryEntity,SubcategoryDto>();

            CreateMap<PeriodDtoIn, PeriodEntity>();
            CreateMap<PeriodEntity,PeriodDto>(); 

            CreateMap<EntryDtoIn, EntryEntity>();
            CreateMap<EntryEntity,EntryDto>();   

            CreateMap<ExpenseDtoIn, ExpenseEntity>();
            CreateMap<ExpenseEntity,ExpenseDto>();           
        }
    }
}