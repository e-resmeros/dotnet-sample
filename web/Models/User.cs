using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Core.Model;

namespace api.Models
{
    [Table("u_usermaster")]
    public class User : BaseModel
    {
        [Key]
        [Column("username")]
        public string UserName { get; set; }

        [Column("password")]
        public string Password {  get; set; }

        [Column("userid")]
        public string UserID { get; set; }

        [Column("usertype")]
        public string Type { get; set; }

        [Column("company")]        
        public string Company { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("designation")]
        public string Designation { get; set; }

        [Column("ccc")]
        public string Section { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("approver1")]
        public string Approver1 { get; set; }

        [Column("approver2")]
        public string Approver2 { get; set; }

        [Column("lastlogin")]
        public DateTime? LastLoginDate { get; set; }

        [Column("mobilelogin")]
        public string MobileLogin { get; set; }
    
        [Column("createddate")]
        public DateTime CreatedDate {get; set; }

        [Column("createdby")]
        public string CreatedBy { get; set; }

        [Column("updateddate")]
        public DateTime UpdatedDate {get; set; }

        [Column("updatedby")]
        public string UpdatedBy { get; set; }
    }
}