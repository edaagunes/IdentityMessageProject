using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityMessageProject.DataAccessLayer.Migrations
{
    public partial class mig_add_states_message : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Messages",
                newName: "IsRead");

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Messages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDraft",
                table: "Messages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "IsDraft",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "IsRead",
                table: "Messages",
                newName: "Status");
        }
    }
}
