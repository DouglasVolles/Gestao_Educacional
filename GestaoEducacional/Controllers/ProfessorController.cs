using GestaoEducacional.DAO;
using GestaoEducacional.Models.Tabelas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestaoEducacional.Controllers
{
    public class ProfessorController : Controller
    {
        public ActionResult Form()
        {
            if ((TempData["idProfessor"] != null) && (!string.IsNullOrEmpty(TempData["idProfessor"].ToString())))
                ViewBag.Professor = ProfessorDao.BuscaPorId(Convert.ToInt32(TempData["idProfessor"].ToString()));
            else
                ViewBag.Professor = new Professor();
             if (TempData["mensagem"] != null)
                ViewBag.Mensagem = TempData["mensagem"].ToString();
            else
                ViewBag.Mensagem = string.Empty;

            ViewBag.Professores = ProfessorDao.Lista();

            return View();

        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AdicionarOuSalvar(Professor professor)
        {
            try
            {
                ProfessorDao professorDao = new ProfessorDao();

                if (professor.Id > 0)
                {
                    professorDao.Atualizar(professor);
                    TempData["mensagem"] = "Registro Alterado com sucesso";
                }
                else
                {
                    professorDao.Adicionar(professor);
                    TempData["mensagem"] = "Cadastrado com sucesso";
                }
                return RedirectToAction("Form", "Professor");
            }
            catch (Exception e)
            {
                TempData.Add("mensagem", e.Message);

                return RedirectToAction("Form");
            }
        }

        public ActionResult Alterar(string professorId)
        {
            try
            {
                TempData["idProfessor"] = professorId;
                return RedirectToAction("Form");
            }
            catch (Exception e)
            {
                TempData["mensagem"] = "Erro ao editar o professor" + e.Message;
                return RedirectToAction("Form");
            }
        }

        public ActionResult Excluir(string professorId)
        {
            try
            {
                var professorDao = new ProfessorDao();
                professorDao.ExcluirId(Int32.Parse(professorId));
                return RedirectToAction("Form");
            }
            catch (Exception e)
            {
                TempData["mensagem"] = e.Message;
                return RedirectToAction("Form");
            }
        }

    }
}