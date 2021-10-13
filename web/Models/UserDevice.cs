using System;
using System.ComponentModel.DataAnnotations.Schema;
using api.Core.Model;

namespace api.Models
{
    public class UserDevice : BaseModel
    {
        [Column("idx")]
        public int Index { get; set; }
        [Column("user_device_id")]
        public int? UserDeviceID { get; set; }

        [Column("emp_no")]
        public string EmployeeNo { get; set; }

        [Column("comp_name")]
        public string CompanyName { get; set; }

        [Column("emp_name")]
        public string EmployeeName { get; set; }

        [Column("last_login_date")]
        public DateTime? LastLoginDate { get; set; }

        [Column("status")]
        public string Status { get; set; }
    }
}