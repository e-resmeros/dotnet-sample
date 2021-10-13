using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace api.Migrations
{
    public partial class CreateDeviceMasterTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "u_devicemaster",
                columns: table => new
                {
                    user_device_id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    device_id = table.Column<string>(maxLength: 16, nullable: true),
                    status = table.Column<string>(maxLength: 1, nullable: false),
                    last_login_date = table.Column<DateTime>(nullable: false),
                    username = table.Column<string>(maxLength: 20, nullable: false),
                    create_date = table.Column<DateTime>(nullable: true),
                    created_by = table.Column<string>(maxLength: 20, nullable: true),
                    update_date = table.Column<DateTime>(nullable: true),
                    updated_by = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_u_devicemaster", x => x.user_device_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "u_devicemaster");
        }
    }
}
