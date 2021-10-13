using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using api.Config;
using api.Core.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class UserDeviceRepo : RepositoryBase<UserDevice>, IUserDeviceRepo
    {
        public const string Active = "A";
        public const string Inactive = "I";
        public UserDeviceRepo(AppDb Db) : base(Db)
        {
        }

        public async Task<IEnumerable<UserDevice>> GetUserDevices()
        {
            FormattableString baseQuery = $@"SET @row_number = 0; 
                                            SELECT *, (@row_number:=@row_number + 1) as idx FROM
                                                (
                                                    SELECT  d.user_device_id as user_device_id,
                                                        u.userid as emp_no,
                                                        u.name as emp_name,
                                                        c.name as comp_name,
                                                        d.last_login_date as last_login_date,
                                                        u.status as status
                                                    FROM kamogawa_warehouse.u_usermaster u
                                                    LEFT JOIN (
                                                        SELECT *
                                                        FROM kamogawa_warehouse.u_devicemaster
                                                        ORDER BY last_login_date
                                                    ) as d
                                                    ON d.username = u.userid and d.status = 'A'
                                                    LEFT JOIN
                                                        kamogawa_warehouse.u_customermaster c
                                                    ON c.code = u.company
                                                    GROUP BY u.userid
                                                    ORDER BY comp_name, emp_name
                                                ) as x;";

            return await ((AppDb)_db).UserDevices.FromSqlInterpolated(baseQuery).ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}