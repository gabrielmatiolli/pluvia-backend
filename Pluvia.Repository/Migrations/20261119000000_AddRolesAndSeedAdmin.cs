using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Pluvia.Repository.Context;

#nullable disable

namespace Pluvia.Repository.Migrations;

[DbContext(typeof(PluviaDbContext))]
[Migration("20261119000000_AddRolesAndSeedAdmin")]
public class AddRolesAndSeedAdmin : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "role",
            table: "users",
            type: "varchar(20)",
            maxLength: 20,
            nullable: false,
            defaultValue: "User");

        migrationBuilder.Sql("""
            INSERT INTO users (name, email, password, role, created_at, updated_at)
            SELECT 'Administrador', 'admin@pluvia.local',
                   '$2y$10$d5iRc4wOIOWupxeaeaBH/O972j07nKL304ALTNldQFJweLe60trOW',
                   'Admin', UTC_TIMESTAMP(), UTC_TIMESTAMP()
            FROM DUAL
            WHERE NOT EXISTS (SELECT 1 FROM users WHERE email = 'admin@pluvia.local');
            """);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("DELETE FROM users WHERE email = 'admin@pluvia.local';");

        migrationBuilder.DropColumn(
            name: "role",
            table: "users");
    }
}
