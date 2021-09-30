using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mijalski.Imagegram.Server.Migrations
{
    public partial class AddAccountName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Caption",
                table: "DbPosts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "DbPosts",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "DbComments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "DbComments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "DbAccounts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_DbComments_AccountId",
                table: "DbComments",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_DbComments_DbAccounts_AccountId",
                table: "DbComments",
                column: "AccountId",
                principalTable: "DbAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DbComments_DbAccounts_AccountId",
                table: "DbComments");

            migrationBuilder.DropIndex(
                name: "IX_DbComments_AccountId",
                table: "DbComments");

            migrationBuilder.DropColumn(
                name: "Caption",
                table: "DbPosts");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "DbPosts");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "DbComments");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "DbComments");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "DbAccounts");
        }
    }
}
