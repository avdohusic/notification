using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationQueues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    From = table.Column<string>(maxLength: 200, nullable: false),
                    FromName = table.Column<string>(maxLength: 200, nullable: false),
                    To = table.Column<string>(maxLength: 1000, nullable: false),
                    ToName = table.Column<string>(maxLength: 1000, nullable: true),
                    ReplyTo = table.Column<string>(maxLength: 200, nullable: false),
                    ReplyToName = table.Column<string>(maxLength: 200, nullable: true),
                    Subject = table.Column<string>(maxLength: 300, nullable: true),
                    Body = table.Column<string>(nullable: true),
                    Attachment = table.Column<string>(nullable: true),
                    CreatedTimestamp = table.Column<DateTime>(nullable: false),
                    FirstAttemptedTimestamp = table.Column<DateTime>(nullable: true),
                    LastAttemptedTimestamp = table.Column<DateTime>(nullable: true),
                    SentTimestamp = table.Column<DateTime>(nullable: true),
                    ExpiredTimestamp = table.Column<DateTime>(nullable: true),
                    FailCount = table.Column<short>(nullable: false),
                    Priority = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationQueues", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationQueues");
        }
    }
}
