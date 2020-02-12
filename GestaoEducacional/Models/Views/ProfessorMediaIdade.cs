using GestaoEducacional.Models.Tabelas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestaoEducacional.Models.Views
{
    public class ProfessorMediaIdade
    {
        public Professor Professor { get; set; }
        public int ProfessorId { get; }
        public string NomeAluno { get; set; }
        public int Idade { get; set; }
        public int Quantidade { get; }  

        public ProfessorMediaIdade(Professor professor, string nomeAluno, int idade)
        {            
            Professor = professor;
            ProfessorId = professor.Id;
            NomeAluno = nomeAluno;
            Idade = idade;
            Quantidade = 1;

        }
    }
}