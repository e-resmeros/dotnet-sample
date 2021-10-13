using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Core.Model;

namespace api.Models.DTO
{
    public class UserDeviceDTO : BaseModel
    {
        public int Index { get; set; }
        public int? UserDeviceID { get; set; }

        public string EmployeeNo { get; set; }

        public string CompanyName { get; set; }

        public string EmployeeName { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:ss}")]
        [DataType(DataType.DateTime)]
        public DateTime? LastLoginDate { get; set; }

        public string Status { get; set; }
    }
}