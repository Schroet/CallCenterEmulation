using Microsoft.EntityFrameworkCore.Migrations;

namespace CallCenterEmulation.Migrations
{
    public partial class TypeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operators_EmployeeStatus_StatusId",
                table: "Operators");

            migrationBuilder.DropForeignKey(
                name: "FK_Operators_EmployeeType_TypeId",
                table: "Operators");

            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "Operators",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "Operators",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Operators_EmployeeStatus_StatusId",
                table: "Operators",
                column: "StatusId",
                principalTable: "EmployeeStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Operators_EmployeeType_TypeId",
                table: "Operators",
                column: "TypeId",
                principalTable: "EmployeeType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.InsertData(
            table: "Operators",
            columns: new[] { "Id", "Name", "StatusId", "TypeId" },
            values: new object[] { 1, "John", "2", "1" });

            migrationBuilder.InsertData(
            table: "Operators",
            columns: new[] { "Id", "Name", "StatusId", "TypeId" },
            values: new object[] { 2, "Sam", "2", "1" });

            migrationBuilder.InsertData(
            table: "Operators",
            columns: new[] { "Id", "Name", "StatusId", "TypeId" },
            values: new object[] { 3, "Ars", "2", "2" });

            migrationBuilder.InsertData(
            table: "Operators",
            columns: new[] { "Id", "Name", "StatusId", "TypeId" },
            values: new object[] { 4, "Zen", "2", "3" });

            migrationBuilder.InsertData(
            table: "EmployeeType",
            columns: new[] { "Id", "Name"},
            values: new object[] { 1, "Operator",});

            migrationBuilder.InsertData(
            table: "EmployeeType",
            columns: new[] { "Id", "Name" },
            values: new object[] { 2, "Manager", });

            migrationBuilder.InsertData(
            table: "EmployeeType",
            columns: new[] { "Id", "Name" },
            values: new object[] { 3, "Senior Manager", });

            migrationBuilder.InsertData(
            table: "EmployeeStatus",
            columns: new[] { "Id", "Name" },
            values: new object[] { 1, "Busy", });

            migrationBuilder.InsertData(
            table: "EmployeeStatus",
            columns: new[] { "Id", "Name" },
            values: new object[] { 2, "Free", });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operators_EmployeeStatus_StatusId",
                table: "Operators");

            migrationBuilder.DropForeignKey(
                name: "FK_Operators_EmployeeType_TypeId",
                table: "Operators");

            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "Operators",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "Operators",
                nullable: true,
                oldClrType: typeof(int));

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
    }
}
