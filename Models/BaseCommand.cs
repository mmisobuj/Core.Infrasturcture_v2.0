using Core.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace Core.Infrastructure.Models
{
    public class BaseCommand
    {
        [JsonIgnore]
        public LoggedUserCommand? LoggedUser { get; set; }

        [BindNever]
        [JsonIgnore]
        public ActionCommand? commandType { get; set; }
    }
}
