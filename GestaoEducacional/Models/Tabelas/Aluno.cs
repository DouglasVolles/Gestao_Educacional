using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoEducacional.Models.Tabelas
{
    public class Aluno : TabelaBase
    {
        public Professor Professor { get; set; }
        public int ProfessorId { get; set; }

        public string Nome { get; set; }
        [Required]
        public DateTime DataNascimento { get; set; }

        public Aluno()
        {

        }

        public Aluno(Professor professor)
        {
            Professor = professor;
            ProfessorId = professor.Id;
        }
    }
}
