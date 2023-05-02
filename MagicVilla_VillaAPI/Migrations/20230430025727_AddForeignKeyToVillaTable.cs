using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_VillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyToVillaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VillaID",
                table: "VilaaNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2023, 4, 30, 8, 27, 27, 615, DateTimeKind.Local).AddTicks(6098));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2023, 4, 30, 8, 27, 27, 615, DateTimeKind.Local).AddTicks(6121));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2023, 4, 30, 8, 27, 27, 615, DateTimeKind.Local).AddTicks(6124));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreateDate",
                value: new DateTime(2023, 4, 30, 8, 27, 27, 615, DateTimeKind.Local).AddTicks(6126));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreateDate",
                value: new DateTime(2023, 4, 30, 8, 27, 27, 615, DateTimeKind.Local).AddTicks(6129));

            migrationBuilder.CreateIndex(
                name: "IX_VilaaNumbers_VillaID",
                table: "VilaaNumbers",
                column: "VillaID");

            migrationBuilder.AddForeignKey(
                name: "FK_VilaaNumbers_Villas_VillaID",
                table: "VilaaNumbers",
                column: "VillaID",
                principalTable: "Villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VilaaNumbers_Villas_VillaID",
                table: "VilaaNumbers");

            migrationBuilder.DropIndex(
                name: "IX_VilaaNumbers_VillaID",
                table: "VilaaNumbers");

            migrationBuilder.DropColumn(
                name: "VillaID",
                table: "VilaaNumbers");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2023, 4, 30, 7, 31, 30, 281, DateTimeKind.Local).AddTicks(514));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2023, 4, 30, 7, 31, 30, 281, DateTimeKind.Local).AddTicks(552));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2023, 4, 30, 7, 31, 30, 281, DateTimeKind.Local).AddTicks(556));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreateDate",
                value: new DateTime(2023, 4, 30, 7, 31, 30, 281, DateTimeKind.Local).AddTicks(559));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreateDate",
                value: new DateTime(2023, 4, 30, 7, 31, 30, 281, DateTimeKind.Local).AddTicks(563));
        }
    }
}
