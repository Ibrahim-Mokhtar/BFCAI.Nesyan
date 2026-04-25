using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BFCAI.Nesyan.Infrastructure.Presistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthVerificationCodes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "Relatives",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PasswordResetCode",
                table: "Relatives",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PasswordResetCodeExpires",
                table: "Relatives",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerificationCode",
                table: "Relatives",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "VerificationCodeExpires",
                table: "Relatives",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "Patients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PasswordResetCode",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PasswordResetCodeExpires",
                table: "Patients",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerificationCode",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "VerificationCodeExpires",
                table: "Patients",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "Doctors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PasswordResetCode",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PasswordResetCodeExpires",
                table: "Doctors",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerificationCode",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "VerificationCodeExpires",
                table: "Doctors",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "Caregivers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PasswordResetCode",
                table: "Caregivers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PasswordResetCodeExpires",
                table: "Caregivers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerificationCode",
                table: "Caregivers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "VerificationCodeExpires",
                table: "Caregivers",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "Relatives");

            migrationBuilder.DropColumn(
                name: "PasswordResetCode",
                table: "Relatives");

            migrationBuilder.DropColumn(
                name: "PasswordResetCodeExpires",
                table: "Relatives");

            migrationBuilder.DropColumn(
                name: "VerificationCode",
                table: "Relatives");

            migrationBuilder.DropColumn(
                name: "VerificationCodeExpires",
                table: "Relatives");

            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PasswordResetCode",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PasswordResetCodeExpires",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "VerificationCode",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "VerificationCodeExpires",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "PasswordResetCode",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "PasswordResetCodeExpires",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "VerificationCode",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "VerificationCodeExpires",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "Caregivers");

            migrationBuilder.DropColumn(
                name: "PasswordResetCode",
                table: "Caregivers");

            migrationBuilder.DropColumn(
                name: "PasswordResetCodeExpires",
                table: "Caregivers");

            migrationBuilder.DropColumn(
                name: "VerificationCode",
                table: "Caregivers");

            migrationBuilder.DropColumn(
                name: "VerificationCodeExpires",
                table: "Caregivers");
        }
    }
}
