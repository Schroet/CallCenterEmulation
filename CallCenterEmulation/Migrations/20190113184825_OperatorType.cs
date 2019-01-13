using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CallCenterEmulation.Migrations
{
    public partial class OperatorType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Operators");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Operators");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Operators",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Operators",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmployeeStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Operators_StatusId",
                table: "Operators",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Operators_TypeId",
                table: "Operators",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Operators_EmployeeStatus_StatusId",
                table: "Operators",
                column: "StatusId",
                principalTable: "EmployeeStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Operators_EmployeeType_TypeId",
                table: "Operators",
                column: "TypeId",
                principalTable: "EmployeeType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operators_EmployeeStatus_StatusId",
                table: "Operators");

            migrationBuilder.DropForeignKey(
                name: "FK_Operators_EmployeeType_TypeId",
                table: "Operators");

            migrationBuilder.DropTable(
                name: "EmployeeStatus");

            migrationBuilder.DropTable(
                name: "EmployeeType");

            migrationBuilder.DropIndex(
                name: "IX_Operators_StatusId",
                table: "Operators");

            migrationBuilder.DropIndex(
                name: "IX_Operators_TypeId",
                table: "Operators");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Operators");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Operators");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Operators",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Operators",
                nullable: false,
                defaultValue: 0);
        }
    }
}
