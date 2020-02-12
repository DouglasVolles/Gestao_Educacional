using System;
using System.Collections.Generic;
using System.Linq;
using GestaoEducacional.Models.Tabelas;
using GestaoEducacional.Models.Views;
using Microsoft.EntityFrameworkCore;

namespace GestaoEducacional.DAO
{
    public class AlunoDao
    {
        public void Adicionar(Aluno aluno)
        {
            ValidarInclusao(aluno);
            using (var context = new Context())
            {
                context.Alunos.Add(aluno);

                context.SaveChanges();
            }
        }

        public void Atualizar(Aluno aluno)
        {
            using (var context = new Context())
            {
                context.Alunos.Update(aluno);
                context.SaveChanges();
            }

        }
        public IList<Aluno> Lista()
        {
            using (var contexto = new Context())
            {
                return contexto.Alunos.ToList();
            }
        }

        public void Excluir(Aluno aluno)
        {
            using (var context = new Context())
            {
                context.Alunos.Remove(aluno);
                context.SaveChanges();
            }
        }

        public void ExcluirId(int professorId)
        {
            using (var context = new Context())
            {
                var aluno = context.Alunos.FirstOrDefault(p => p.Id == professorId);
                if (aluno != null)
                {
                    context.Alunos.Remove(aluno);
                    context.SaveChanges();
                }
            }
        }

        public static Aluno BuscaPorId(int id)
        {
            using (var contexto = new Context())
            {
                return contexto.Alunos
                    .Where(p => p.Id == id)
                    .FirstOrDefault();
            }
        }

        public static IList<Aluno> BuscaAlunosProfessor(int idProfessor)
        {
            using (var contexto = new Context())
            {
                return contexto.Alunos
                    .Where(p => p.ProfessorId == idProfessor)
                    .ToList();
            }
        }

        public static IList<Aluno> BuscaAlunosMaior16()
        {
            var dataAux = DateTime.Now.Date.AddYears(-16);

            using (var contexto = new Context())
            {
                return contexto.Alunos.Include(a=> a.Professor).Where(a => (a.DataNascimento.Date <= dataAux.Date)).ToList().OrderBy(a => a.DataNascimento).ToList();
            }
        }

        public void ValidarInclusao(Aluno aluno)
        {
            using (var contexto = new Context())
            {
                if (contexto.Alunos.Where(p => p.Nome == aluno.Nome).FirstOrDefault() != null)
                    throw new Exception("Aluno já cadastrado");
            }
        }
    }
}