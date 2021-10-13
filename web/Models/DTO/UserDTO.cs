using api.Core.Model;

namespace api.Models.DTO
{
    public class UserDTO : BaseModel
    {
        public string mobileLogin { get; set; }
        public string username { get; set; }

        public string usertype { get; set; }

        public string company { get; set; }

        public string name { get; set; }

        public string ccc { get; set; }

        public string authToken { get; set; }

        public string refreshToken { get; set; }
    }
}