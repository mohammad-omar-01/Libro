using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Libro.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int"
            );

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorID", "Name" },
                values: new object[,]
                {
                    { 1, "Author 1" },
                    { 2, "Author 2" },
                    { 3, "Author 3" }
                }
            );

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookID", "Genre", "PublicationDate", "Title" },
                values: new object[,]
                {
                    {
                        1,
                        "Genre 1",
                        new DateTime(2023, 7, 16, 0, 43, 28, 46, DateTimeKind.Local).AddTicks(9170),
                        "Book 1"
                    },
                    {
                        2,
                        "Genre 2",
                        new DateTime(2023, 7, 9, 0, 43, 28, 46, DateTimeKind.Local).AddTicks(9211),
                        "Book 2"
                    },
                    {
                        3,
                        "Genre 3",
                        new DateTime(2023, 7, 2, 0, 43, 28, 46, DateTimeKind.Local).AddTicks(9214),
                        "Book 3"
                    }
                }
            );

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[]
                {
                    "ID",
                    "DateJoined",
                    "Email",
                    "Name",
                    "Password",
                    "Role",
                    "Username"
                },
                values: new object[,]
                {
                    {
                        1,
                        new DateTime(2023, 6, 23, 0, 43, 28, 46, DateTimeKind.Local).AddTicks(9257),
                        "",
                        "Musab",
                        "password1",
                        "Patron",
                        "user1"
                    },
                    {
                        2,
                        new DateTime(2023, 7, 3, 0, 43, 28, 46, DateTimeKind.Local).AddTicks(9262),
                        "",
                        "Mazen",
                        "password2",
                        "Admin",
                        "user2"
                    }
                }
            );

            migrationBuilder.InsertData(
                table: "BookCopies",
                columns: new[] { "CopyId", "BookId", "IsAvailable" },
                values: new object[,]
                {
                    { 1, 1, false },
                    { 2, 1, true },
                    { 3, 2, false },
                    { 4, 2, true },
                    { 5, 3, false }
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(table: "Authors", keyColumn: "AuthorID", keyValue: 1);

            migrationBuilder.DeleteData(table: "Authors", keyColumn: "AuthorID", keyValue: 2);

            migrationBuilder.DeleteData(table: "Authors", keyColumn: "AuthorID", keyValue: 3);

            migrationBuilder.DeleteData(table: "BookCopies", keyColumn: "CopyId", keyValue: 1);

            migrationBuilder.DeleteData(table: "BookCopies", keyColumn: "CopyId", keyValue: 2);

            migrationBuilder.DeleteData(table: "BookCopies", keyColumn: "CopyId", keyValue: 3);

            migrationBuilder.DeleteData(table: "BookCopies", keyColumn: "CopyId", keyValue: 4);

            migrationBuilder.DeleteData(table: "BookCopies", keyColumn: "CopyId", keyValue: 5);

            migrationBuilder.DeleteData(table: "Users", keyColumn: "ID", keyValue: 1);

            migrationBuilder.DeleteData(table: "Users", keyColumn: "ID", keyValue: 2);

            migrationBuilder.DeleteData(table: "Books", keyColumn: "BookID", keyValue: 1);

            migrationBuilder.DeleteData(table: "Books", keyColumn: "BookID", keyValue: 2);

            migrationBuilder.DeleteData(table: "Books", keyColumn: "BookID", keyValue: 3);

            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)"
            );
        }
    }
}
