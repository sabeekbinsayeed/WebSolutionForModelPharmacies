using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel;
using WebSolutionForModelPharmacies.Models;

namespace WebSolutionForModelPharmacies.Controllers
{
    public class TicketController : BaseController
    {
        Customer customer;
        public TicketController()
        {
            if ((Customer)System.Web.HttpContext.Current.Session["Customer"] != null)
            {
                customer = (Customer)System.Web.HttpContext.Current.Session["Customer"];
            }
            else
            {
                customer = new Customer();
                dRedirect.Redirect("/authentication");
            }
        }
        [Route("ticket/dashboard")]
        public ActionResult TicketDashboard()
        {
            List<Ticket> tickets = Database.getContext().Ticket.Where(c => c.Customer.Id == customer.Id).ToList();

            return View("~/Views/Ticket/Dashboard.cshtml", tickets);
        }

        [Route("ticket/content/{ticketId}")]
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
            return View("~/Views/Ticket/Content.cshtml", tvm);
        }

        [Route("ticket/add")]
        [HttpPost]
        public ActionResult TicketAdd()
        {

            Ticket ticketNew = new Ticket() {
                Title = Request["ticket_title"],
                Customer = customer,
                DateTime = System.DateTime.Now,
            };
            Ticket ticket = Database.getContext().Ticket.Add(ticketNew);
            Database.getContext().SaveChanges();

            TicketContent tck = new TicketContent()
            {
                Content = "Covertation starts at: "+DateTime.Now.ToString(),
                Ticket = ticket,
                DateTime = DateTime.Now
            };
            Database.getContext().TicketContent.Add(tck);
            Database.getContext().SaveChanges();

            List<TicketContent> TicketContents = Database.getContext().TicketContent.Where(c => c.Ticket.Id == ticket.Id).ToList();

            return RedirectToAction("TicketContent", new { ticketId = ticket.Id });
            //return View("~/Views/Ticket/Content.cshtml", TicketContents);
            //dRedirect.Redirect("/ticket/content/"+ ticket.Id);
            //return red

        }

        [Route("ticket/content/add")]
        [HttpPost]
        public ActionResult TicketContentAdd()
        {
            int ticket_id = Convert.ToInt32(Request["ticket_id"]);

            TicketContent ticketTicketContentNew = new TicketContent() {
                Who = customer.Name,
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