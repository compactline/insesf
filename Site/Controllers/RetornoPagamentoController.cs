using Site.Context;
using Site.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Resources;
using Uol.PagSeguro.Service;

namespace Site.Controllers
{
    public class RetornoPagamentoController : Controller
    {

        private SiteDBContext db = new SiteDBContext();
        // GET: RetornoPagamento
        public ActionResult Index()
        {
            return View();
        }



        // GET: RetornoPagamento/Edit/5
        public ActionResult Retorno(string notificationType, string notificationCode)
        {
            AccountCredentials credentials = PagSeguroConfiguration.Credentials(true);


            if (notificationType == "transaction")
            {
                // obtendo o objeto transaction a partir do código de notificação  
                Transaction transaction = NotificationService.CheckTransaction(
                    credentials,
                    notificationCode
                );

                int status = transaction.TransactionStatus;

                if (status == 3) // paga
                {
                    string referencia = transaction.Reference;
                    DateTime data = transaction.LastEventDate;

                    int id = Int32.Parse(referencia.Substring(3));

                    Matricula  mat = db.Matriculas.Find(id);

                    mat.DataPago = data;
                    mat.Situacao = "P"; // pago

                    db.Entry(mat).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            ViewBag.Mensagem = "Processada.";
            return View();
        }

        
    }
}
