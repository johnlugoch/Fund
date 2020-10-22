using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fund.Web.Models;
using System.Reflection.Metadata.Ecma335;

namespace Fund.Web.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly FundContext _context;

        public PaymentsController(FundContext context)
        {
            _context = context;
        }
        
        
        [HttpPost]
        public async Task<IActionResult> AddPayment(Payment p)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (p.id == 0)
                    {
                        _context.Payment.Add(p);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        _context.Entry(p).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }
                    return RedirectToAction("PaymentList");
                }
                return View(p);

            }
            catch (Exception ex)
            {
                return RedirectToAction("PaymentList");

            }
        }

            public IActionResult PaymentList()
        {
            try
            {
                var payList = from a in _context.Payment
                              join b in _context.person
                              on a.idperson equals b.id
                              into per
                              from b in per.DefaultIfEmpty()
                              select new Payment
                              {
                                  id = a.id,
                                  description = a.description,
                                  valor = a.valor,
                                  fecha = a.fecha,
                                  idperson = a.idperson,
                                  persona = b == null ? "" : b.name

                              };

                return View(payList);
            }
            catch (Exception ex)
            {
                return View();
            }

        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Payment.ToListAsync());
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment
                .FirstOrDefaultAsync(m => m.id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        
        public IActionResult Create(Payment p)
        {
            LoadPersona();
            return View(p);
        }

        
        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,description,valor,fecha,idperson")] Payment payment)
        {
            if (id != payment.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(payment);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment
                .FirstOrDefaultAsync(m => m.id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment = await _context.Payment.FindAsync(id);
            _context.Payment.Remove(payment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
            return _context.Payment.Any(e => e.id == id);
        }

        private void LoadPersona()
        {
            try
            {
                List<Person> personList = new List<Person>();
                personList = _context.person.ToList();
                personList.Insert(0, new Person { id = 0, name = "Seleccione la persona" });
                ViewBag.PersonList = personList;
            }
            catch (Exception ex)
            {


            }
        }
    }
}
