﻿using Microsoft.EntityFrameworkCore;
using ControleDeContatos.Models;

namespace ControleDeContatos.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext>options) : base(options)
        {   
        }
        public DbSet<ContatoModel> Contato { get; set; }

        public DbSet<UsuarioModel> Usuario { get; set; }
    }
}
