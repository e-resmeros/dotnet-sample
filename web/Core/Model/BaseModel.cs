using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace api.Core.Model
{
    public class BaseModel
    {
        [JsonIgnore]
        [NotMapped]
        public string Error { get; set; }
    }
}