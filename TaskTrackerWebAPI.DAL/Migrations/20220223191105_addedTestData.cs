using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskTrackerWebAPI.DAL.Migrations
{
    public partial class addedTestData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "CompletionDate", "CreatedDate", "Name", "Priority", "StartDate", "Status" },
                values: new object[] { 1, null, new DateTime(2022, 2, 24, 1, 11, 3, 843, DateTimeKind.Local).AddTicks(3491), "Project777", 2, new DateTime(2022, 2, 24, 1, 11, 3, 841, DateTimeKind.Local).AddTicks(8482), 1 });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "CompletionDate", "CreatedDate", "Name", "Priority", "StartDate", "Status" },
                values: new object[] { 2, null, new DateTime(2021, 11, 24, 1, 11, 3, 843, DateTimeKind.Local).AddTicks(5155), "Project9000", 3, new DateTime(2021, 11, 24, 1, 11, 3, 843, DateTimeKind.Local).AddTicks(5022), 1 });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "CreatedDate", "Description", "Name", "Priority", "ProjectId", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 2, 24, 1, 11, 3, 845, DateTimeKind.Local).AddTicks(1445), "desc1", "Task1", 3, 1, 1 },
                    { 2, new DateTime(2022, 2, 24, 1, 11, 3, 845, DateTimeKind.Local).AddTicks(2636), "desc2", "Task2", 4, 1, 0 },
                    { 3, new DateTime(2021, 12, 24, 1, 11, 3, 845, DateTimeKind.Local).AddTicks(2684), "desc1", "Task1", 2, 2, 2 },
                    { 4, new DateTime(2022, 1, 24, 1, 11, 3, 845, DateTimeKind.Local).AddTicks(2690), "desc2", "Task2", 4, 2, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
