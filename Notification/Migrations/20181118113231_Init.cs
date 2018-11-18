using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Notification.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationQueues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    From = table.Column<string>(maxLength: 200, nullable: false),
                    To = table.Column<string>(maxLength: 150, nullable: false),
                    Subject = table.Column<string>(maxLength: 300, nullable: true),
                    Body = table.Column<string>(nullable: true),
                    CreatedTimestamp = table.Column<DateTime>(nullable: false),
                    FirstAttemptedTimestamp = table.Column<DateTime>(nullable: true),
                    LastAttemptedTimestamp = table.Column<DateTime>(nullable: true),
                    SentTimestamp = table.Column<DateTime>(nullable: true),
                    ExpiredTimestamp = table.Column<DateTime>(nullable: true),
                    FailCount = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationQueues", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "NotificationQueues",
                columns: new[] { "Id", "Body", "CreatedTimestamp", "ExpiredTimestamp", "FailCount", "FirstAttemptedTimestamp", "From", "LastAttemptedTimestamp", "SentTimestamp", "Subject", "To" },
                values: new object[] { 1, "This is content of the <b>Email</b> message", new DateTime(2018, 11, 18, 12, 32, 30, 917, DateTimeKind.Local), null, (short)0, null, "notification.test@com", null, null, "Test email notification", "avdohusic@gmail.com" });

            migrationBuilder.InsertData(
                table: "NotificationQueues",
                columns: new[] { "Id", "Body", "CreatedTimestamp", "ExpiredTimestamp", "FailCount", "FirstAttemptedTimestamp", "From", "LastAttemptedTimestamp", "SentTimestamp", "Subject", "To" },
                values: new object[] { 2, "This is content of the <b>Email</b> message", new DateTime(2018, 11, 18, 12, 32, 30, 919, DateTimeKind.Local), null, (short)0, null, "notification.test@com", null, null, "Test email notification", "avdo.husic@gmail.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationQueues");
        }
    }
}
