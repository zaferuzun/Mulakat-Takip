using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mulakat_Takip.Database;
using Mulakat_Takip.Models;

namespace Mulakat_Takip.Controllers
{
    public class AdminPanel : Controller
    {
        public static class GlobalVar
        {
            static string _globalValue;
            public static string UserId
            {
                get
                {
                    return _globalValue;
                }
                set
                {
                    _globalValue = value;
                }
            }
        }
        private readonly DatabaseContext _context;

        public AdminPanel(DatabaseContext context)
        {
            _context = context;
        }
        // GET: AdminPanel
        //int? id
        public async Task<IActionResult> Index()
        {

            var P_Panel = from e in _context.PanelOperations
                          orderby e.PanelDate
                          select e;
            //GlobalVar.UserId = id.ToString();

            return View(P_Panel);
        }
        //public void OrnekPost(int? id)
        //{
        //    var P_panel = (from e in _context.PanelOperations
        //                where e.Panelid == id
        //                select e).ToList();

        //    P_panel[0].PanelStatus = "Onaylandı.";
        //    PanelOperations P_pan = P_panel[0];
        //    _context.Update(P_pan);
        //    _context.SaveChanges();
        //}

        // GET: AdminPanel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            int G_id = Convert.ToInt32(id);

            if (G_id == null)
            {
                return NotFound();
            }

            var P_panelOperations = await _context.PanelOperations
                .FirstOrDefaultAsync(m => m.Panelid == G_id);
            if (P_panelOperations == null)
            {
                return NotFound();
            }

            return View(P_panelOperations);
        }

        //// GET: AdminPanel/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: AdminPanel/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: AdminPanel/Edit/5
        public async Task<IActionResult> Confirmation(int? id)
        {
            int G_id = Convert.ToInt32(id);
            if (G_id == null)
            {
                return NotFound();
            }

            var P_panelOperations = await _context.PanelOperations.FindAsync(G_id);
            if (P_panelOperations == null)
            {
                return NotFound();
            }
            return View(P_panelOperations);
        }

        // POST: PanelOperations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Confirmation(int id, PanelOperations G_panelOperations)
        {
            int G_id = Convert.ToInt32(id);

            if (G_id != G_panelOperations.Panelid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var P_panel = (from e in _context.PanelOperations
                                   where e.Panelid == G_id
                                   select e).ToList();

                    P_panel[0].PanelStatus = G_panelOperations.PanelStatus;
                    P_panel[0].PanelDefinition = G_panelOperations.PanelDefinition;
                    P_panel[0].PanelPostDate = G_panelOperations.PanelPostDate;

                    PanelOperations P_pan = P_panel[0];
                    SendEmail(P_pan);
                    _context.Update(P_pan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                }
                return RedirectToAction(nameof(Index));
            }
            return View(G_panelOperations);
        }

        // GET: AdminPanel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            int G_id = Convert.ToInt32(id);

            if (G_id == null)
            {
                return NotFound();
            }

            var P_panelOperations = await _context.PanelOperations
                .FirstOrDefaultAsync(m => m.Panelid == G_id);
            if (P_panelOperations == null)
            {
                return NotFound();
            }

            return View(P_panelOperations);
        }

        public void SendEmail(PanelOperations G_panelOperations)
        {

            try
            {
                // Credentials
                var credentials = new NetworkCredential("mulakatmail123@gmail.com", "123456789mulakat");
                // Mail message
                //string P_message = "Sayın "+  + G_panelOperations.PanelSurname +",\n"+
                //                   G_panelOperations.PanelDate+" tarihinde oluşturduğunuz talep doğrultusunda "+
                //                   G_panelOperations.PanelPostDate+" tarihinde " + G_panelOperations.PanelStatus+
                //                   " yanıtı verilmiştir."+ "Açıklama "+G_panelOperations.PanelDefinition+
                //                   " İyi Çalışmalar \n"+
                //                   "Admin";
                string P_message = "Sayın " + G_panelOperations.PanelName +" " + G_panelOperations.PanelSurname +",";
                P_message += "<br /><br />"+ G_panelOperations.PanelDate + " tarihinde oluşturduğunuz talep doğrultusunda " ;
                P_message += G_panelOperations.PanelPostDate + " tarihinde " + G_panelOperations.PanelStatus;
                P_message += " yanıtı verilmiştir." + "Açıklama " + G_panelOperations.PanelDefinition;
                P_message += "<br /><br /> İyi Çalışmalar ";
                P_message += "<br /><br /> Admin ";

                string P_toMail = "";
                var mail = new MailMessage()
                {
                    From = new MailAddress("mulakatmail123@gmail.com"),
                    Subject = "Mülakat Bildirim",
                    Body = P_message
                };
                mail.IsBodyHtml = true;
                mail.To.Add(new MailAddress("mulakatmail123@gmail.com"));
                // Smtp client
                var client = new SmtpClient()
                {
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                    Credentials = credentials
                };
                client.Send(mail);
            }
            catch (System.Exception e)
            {
            }
        }




            // POST: PanelOperations/Delete/5
            [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            int G_id = Convert.ToInt32(id);
            var panelOperations = await _context.PanelOperations.FindAsync(G_id);
            _context.PanelOperations.Remove(panelOperations);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
