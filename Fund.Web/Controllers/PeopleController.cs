using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fund.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.Storage.Internal;

namespace Fund.Web.Controllers
{
    public class PeopleController : Controller
    {
        private readonly FundContext _Context;
        public PeopleController(FundContext Context)
        {
            _Context = Context;
        }
        public IActionResult PersonList()
        {
            try
            {
                var personList = from a in _Context.person
                                 join b in _Context.@group
                                 on a.idgroup equals b.id
                                 into gru
                                 from b in gru.DefaultIfEmpty()
                                 select new Person
                                 {
                                     id = a.id,
                                     name = a.name,
                                     email = a.email,
                                     idgroup = a.idgroup,
                                     grupo = b == null ? "" : b.name

                                 };

                return View(personList);
            }
            catch (Exception ex)
            {
                return View();
            }

        }

        public IActionResult Create(Person p)
        {
            LoadGrupo();
            return View(p);
        }

        [HttpPost]
        public async Task<IActionResult> AddPerson(Person p)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(p.id == 0)
                    {
                        _Context.person.Add(p);
                        await _Context.SaveChangesAsync();                        
                    }
                    else
                    {
                        _Context.Entry(p).State = EntityState.Modified;
                        await _Context.SaveChangesAsync();
                    }
                    return RedirectToAction("PersonList");
                }
                return View(p);
                
            }
            catch (Exception ex)
            {
                return RedirectToAction("PersonList");
                
            }
        }

        private void LoadGrupo()
        {
            try
            {
                List<Group> groupList = new List<Group>();
                groupList = _Context.group.ToList();
                groupList.Insert(0, new Group { id = 0, name = "Seleccione un grupo" });
                ViewBag.GroupList = groupList;
            }
            catch (Exception ex)
            {

               
            }
        }
        

        public async Task<IActionResult> DeletePerson(int id)
        {
            try
            {
                var p =await  _Context.person.FindAsync(id);
                if (p != null)
                {
                    _Context.person.Remove(p);
                    await _Context.SaveChangesAsync();
                }
                
                return RedirectToAction("PersonList");
            }
            catch (Exception)
            {

                return RedirectToAction("PersonList");
            }
        } 
    }

    
}
