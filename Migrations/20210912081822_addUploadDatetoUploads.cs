using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace FileSharing.Migrations
{
    public partial class addUploadDatetoUploads : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name:"UploadDate",
                table:"Uploads",
                nullable:false,
                defaultValueSql:"getDate()"
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
               name: "UploadDate",
                table: "Uploads");
        }
    }
}
