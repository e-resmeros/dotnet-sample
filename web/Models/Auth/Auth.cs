using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Core.Model;

namespace api.Models.Auth
{
    [Table("u_auth")]
    public class Auth : BaseModel
    {
        [Column("auth_id")]
        [Key]
        public int Id { get; set; }

        [Column("mobile_login")]
        [Required]
        public string MobileLogin { get; set; }

        [Column("refresh_token")]
        public string RefreshToken { get; set; }

        [Column("refresh_token_expiry")]
        public DateTime? RefreshTokenExpiryTime { get; set; }

        [Column("device_id")]
        [Required]
        public string DeviceID { get; set; }
    }
}