using System;
using System.Collections.Generic;
using System.Linq;
using GestaoEducacional.Models.Tabelas;
using GestaoEducacional.Models.Views;
using System.Web;
using Microsoft.EntityFrameworkCore;

namespace GestaoEducacional.DAO
{
    public class ProfessorDao
    {
        public void Adicionar(Professor professor)
        {
            ValidarInclusao(professor);
            using (Context context = new Context())
            {
                context.Professores.Add(professor);

                context.SaveChanges();
            }
        }

        public void Atualizar(Professor professor)
        {
            using (var context = new Context())
            {
                context.Professores.Update(professor);
                context.SaveChanges();
            }

        }
        public static IList<Professor> Lista()
        {
            using (var contexto = new Context())
            {
                return contexto.Professores.ToList();
            }
        }

        public static IList<Professor> BuscaProfessorMedia15_17()
        {
            using (var contexto = new Context())
            {
                var professoresRetorno = new List<Professor>();
                var alunosAux = new List<ProfessorMediaIdade>();
                var alunos = contexto.Alunos.Include(a=> a.Professor).ToList();
                foreach (var aluno in alunos)
                {
                    alunosAux.Add(new ProfessorMediaIdade(aluno.Professor, aluno.Nome, ((DateTime.Now.Date - aluno.DataNascimento.Date).Days / 365)));
                }
                
                var professores = ProfessorDao.Lista();
                foreach (var professor in professores)
                {
                    var alunosProfessor = alunosAux.Where(a => a.ProfessorId == professor.Id);
                    var quantidade = alunosProfessor.Count();
                    if (quantidade > 0)
                    {
                        var soma = alunosProfessor.Sum(a => a.Idade);

                        var mediaIdade = soma / quantidade;

                        if (mediaIdade >= 15 && mediaIdade < 18)
                            professoresRetorno.Add(professor);
                    }
                }

                return professoresRetorno; 
            }
        }


        public void Excluir(Professor professor)
        {
            ValidarExclusao(professor);

            using (var context = new Context())
            {
                context.Professores.Remove(professor);
                context.SaveChanges();
            }
        }

        public void ExcluirId(int professorId)
        {
            using (var context = new Context())
            {
                var professor = context.Professores.FirstOrDefault(p => p.Id == professorId);
                if (professor != null)
                {
                    ValidarExclusao(professor);
                    context.Professores.Remove(professor);
                    context.SaveChanges();
                }
            }
        }

        public static Professor BuscaPorId(Int32 id)
        {
            using (var contexto = new Context())
            {
                return contexto.Professores
                    .Where(p => p.Id == id)
                    .FirstOrDefault();
            }
        }

        public void ValidarInclusao(Professor professor)
        {
            using (var contexto = new Context())
            {
                if (contexto.Professores.Where(p => p.Nome == professor.Nome).FirstOrDefault() != null)
                    throw new Exception("Professor já cadastrado");
            }
        }

        public void ValidarExclusao(Professor professor)
        {
            using (var contexto = new Context())
            {
                if (contexto.Alunos.Where(p => p.ProfessorId == professor.Id).FirstOrDefault() != null)
                    throw new Exception("Não é possível excluir, existem alunos cadastrados para o professor");
            }
        }
    }
}