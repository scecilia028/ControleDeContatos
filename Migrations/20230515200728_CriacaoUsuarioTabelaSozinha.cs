using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ControleDeContatos.Migrations
{
    public partial class CriacaoUsuarioTabelaSozinha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
              name: "Usuario",
              columns: table => new
              {
                  Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                      .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                  Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                  Login = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                  Email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                  Perfil = table.Column<int>(type: "NUMBER(10)", nullable: false),
                  Senha = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                  DataCadastro = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                  DataAtualizacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_Usuario", x => x.Id);
              });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "Usuario");
        }
    }
}
