using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel;
using WebSolutionForModelPharmacies.Models;

namespace WebSolutionForModelPharmacies.Controllers.Admin
{
    public class AdminTicketController : AdminBaseController
    {

        [Route("admin/ticket/dashboard")]
        public ActionResult TicketDashboard()
        {
            List<Ticket> tickets = Database.getContext().Ticket.Include("Customer").ToList();

            return View("~/Views/Admin/Ticket/Dashboard.cshtml", tickets);
        }

        [Route("admin/ticket/content/{ticketId}")]
        [HttpGet]
        public ActionResult TicketContent(int ticketId)
        {

            Ticket ticket = Database.getContext().Ticket.SingleOrDefault(c => c.Id == ticketId);
            List<TicketContent> ticketContents = Database.getContext().TicketContent.Where(c => c.Ticket.Id == ticket.Id).ToList();
            TicketViewModel tvm = new TicketViewModel()
            {
                Ticket = ticket,
                TicketContent = ticketContents,
            };
            //TicketContents.ForEach(c => Response.Write(c.Content));
            //return Content("");
            return View("~/Views/Admin/Ticket/TicketContent.cshtml", tvm);
        }



        [Route("admin/ticket/content/add")]
        [HttpPost]
        public ActionResult TicketContentAdd()
        {
            int ticket_id = Convert.ToInt32(Request["ticket_id"]);

            TicketContent ticketTicketContentNew = new TicketContent()
            {
                Who = "Admin",
                Content = Request["ticket_content"],
                DateTime = System.DateTime.Now,
                Ticket = Database.getContext().Ticket.SingleOrDefault(c => c.Id == ticket_id),
            };

            Database.getContext().TicketContent.Add(ticketTicketContentNew);
            Database.getContext().SaveChanges();


            return RedirectToAction("TicketContent", new { ticketId = ticket_id });
            //dRedirect.Redirect("/ticket/content/"+ ticketId);
        }



    }
}