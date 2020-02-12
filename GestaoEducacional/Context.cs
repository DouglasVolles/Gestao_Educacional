using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GestaoEducacional.Models.Tabelas;

namespace GestaoEducacional
{
    public class Context : DbContext
    {
        public DbSet<Professor> Professores { get; set; }

        public DbSet<Aluno> Alunos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Professor>().HasKey(t => t.Id);

            modelBuilder.Entity<Aluno>().HasKey(t => t.Id);
            modelBuilder.Entity<Aluno>().HasOne(t => t.Professor);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=GestaoEducacional;Trusted_Connection=true;");
        }
    }
}