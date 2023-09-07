using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Core.Infrastructure.Models
{
    public class BaseQuery
    {
        [BindNever]
        public LoggedUserCommand? LoggedUser { get; set; }


    }

}
