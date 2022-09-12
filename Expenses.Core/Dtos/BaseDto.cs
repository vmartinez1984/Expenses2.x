using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Expenses.Core.Dtos
{
    public class BaseBDto : BaseADto
    {
        public DateTime DateRegister { get; set; } = DateTime.Now;
    }

    public class BaseADto
    {        
        public string Id { get; set; }
    }    

    public class BaseCDto
    {       
        public DateTime DateRegister { get; set; } = DateTime.Now;        
    }   
}