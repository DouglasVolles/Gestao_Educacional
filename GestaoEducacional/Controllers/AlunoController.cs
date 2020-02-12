using GestaoEducacional.DAO;
using GestaoEducacional.Models.Tabelas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestaoEducacional.Controllers
{
    public class AlunoController : Controller
    {
        // GET: Aluno
        public ActionResult Form(string professorId = "")
        {
            var professor = ProfessorDao.BuscaPorId(Convert.ToInt32(professorId));
            if ((TempData["idAluno"] != null) && (!string.IsNullOrEmpty(TempData["idAluno"].ToString())))
                ViewBag.Aluno = AlunoDao.BuscaPorId(Convert.ToInt32(TempData["idAluno"].ToString()));
            else
                ViewBag.Aluno = new Aluno(professor);
            
            if (TempData["mensagem"] != null)
                ViewBag.Mensagem = TempData["mensagem"].ToString();
            else
                ViewBag.Mensagem = string.Empty;

            ViewBag.Alunos = AlunoDao.BuscaAlunosProfessor(Convert.ToInt32(professorId));
            ViewBag.Professor = ProfessorDao.BuscaPorId(Convert.ToInt32(professorId));
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AdicionarOuSalvar(Aluno aluno)
        {
            try
            {
                AlunoDao AlunoDao = new AlunoDao();

                if (aluno.Id > 0)
                {
                    AlunoDao.Atualizar(aluno);
                    TempData["mensagem"] = "Registro alterado com sucesso";
                }
                else
                {
                    AlunoDao.Adicionar(aluno);
                    TempData["mensagem"] = "Cadastrado com sucesso";
                }
                return RedirectToAction("Form", "Aluno",new {@professorId = aluno.ProfessorId.ToString() });
            }
            catch (Exception e)
            {
                TempData.Add("mensagem", e.Message);

                return RedirectToAction("Form", "Aluno", new { @professorId = aluno.ProfessorId.ToString() });
            }
        }

        public ActionResult Alterar(string alunoId)
        {
            var aluno = AlunoDao.BuscaPorId(Convert.ToInt32(alunoId));
            try
            {
                TempData["idAluno"] = alunoId;
                return RedirectToAction("Form", "Aluno", new { @professorId = aluno.ProfessorId.ToString() });
            }
            catch (Exception e)
            {
                TempData["mensagem"] = "Erro ao editar o professor" + e.Message;
                return RedirectToAction("Form", "Aluno", new { @professorId = aluno.ProfessorId.ToString() });
            }
        }

        public ActionResult Excluir(string alunoId)
        {
            var aluno = AlunoDao.BuscaPorId(Convert.ToInt32(alunoId));
            try
            {
                var alunoDao = new AlunoDao();
                alunoDao.ExcluirId(Int32.Parse(alunoId));
                return RedirectToAction("Form", "Aluno", new { @professorId = aluno.ProfessorId.ToString() });
            }
            catch (Exception e)
            {
                TempData["mensagem"] = e.Message;
                return RedirectToAction("Form", "Aluno", new { @professorId = aluno.ProfessorId.ToString() });
            }
        }
    }
}