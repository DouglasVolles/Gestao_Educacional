using GestaoEducacional.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestaoEducacional.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Alunos = AlunoDao.BuscaAlunosMaior16();
            ViewBag.Professores = ProfessorDao.BuscaProfessorMedia15_17();

            return View();
        }        
    }
}