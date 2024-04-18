using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreWebAPIJWT.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ViillaId",
                table: "VillaNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 4, 18, 14, 46, 43, 327, DateTimeKind.Local).AddTicks(5942));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 4, 18, 14, 46, 43, 327, DateTimeKind.Local).AddTicks(5945));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 4, 18, 14, 46, 43, 327, DateTimeKind.Local).AddTicks(5947));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 4, 18, 14, 46, 43, 327, DateTimeKind.Local).AddTicks(5949));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 4, 18, 14, 46, 43, 327, DateTimeKind.Local).AddTicks(5951));

            migrationBuilder.CreateIndex(
                name: "IX_VillaNumbers_ViillaId",
                table: "VillaNumbers",
                column: "ViillaId");

            migrationBuilder.AddForeignKey(
                name: "FK_VillaNumbers_Villas_ViillaId",
                table: "VillaNumbers",
                column: "ViillaId",
                principalTable: "Villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VillaNumbers_Villas_ViillaId",
                table: "VillaNumbers");

            migrationBuilder.DropIndex(
                name: "IX_VillaNumbers_ViillaId",
                table: "VillaNumbers");

            migrationBuilder.DropColumn(
                name: "ViillaId",
                table: "VillaNumbers");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 4, 17, 14, 34, 10, 223, DateTimeKind.Local).AddTicks(4525));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 4, 17, 14, 34, 10, 223, DateTimeKind.Local).AddTicks(4528));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 4, 17, 14, 34, 10, 223, DateTimeKind.Local).AddTicks(4530));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 4, 17, 14, 34, 10, 223, DateTimeKind.Local).AddTicks(4532));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 4, 17, 14, 34, 10, 223, DateTimeKind.Local).AddTicks(4534));
        }
    }
}

