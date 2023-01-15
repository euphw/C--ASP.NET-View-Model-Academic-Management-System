using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab6.Models.DataAccess;
using Lab6.Models.ViewModel;
using System.Data;
using System.Security.Cryptography;

namespace Lab6.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly StudentRecordContext _context;

        public EmployeesController(StudentRecordContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            //var employees = _context.Employees.Include(e => e.Roles);
            
            return View(await _context.Employees.Include(e => e.Roles).ToListAsync());
        }

        // GET: Employees/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.Employees == null)
        //    {
        //        return NotFound();
        //    }

        //    var employee = await _context.Employees
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(employee);
        //}

        // GET: Employees/Create
        public IActionResult Create()
        {
            var role =  _context.Roles.ToList();
            List<RoleSelection> roleSelections= new List<RoleSelection>();
            foreach(Role r in role)
            {
                RoleSelection roleSelection=new RoleSelection(r);
                roleSelections.Add(roleSelection);
            }
            EmployeeRoleSelections employeeRoleSelections = new EmployeeRoleSelections();
            employeeRoleSelections.roleSelections = roleSelections;
            return View(employeeRoleSelections);
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeRoleSelections employeeRoleSelections)
        {
            if (ModelState.IsValid)
            {
                foreach(RoleSelection roleSelection in employeeRoleSelections.roleSelections)
                {
                    //if (roleSelection.Selected)
                    //{
                        if (roleSelection.Selected)
                        {
                            Role role = _context.Roles.SingleOrDefault(r => r.Id == roleSelection.role.Id);
                            employeeRoleSelections.employee.Roles.Add(role);
                        }
                    //}
                }
                _context.Add(employeeRoleSelections.employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeRoleSelections);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }
            
           // var employee = await _context.Employees.FindAsync(id);
            Employee employee = await _context.Employees.Include(e => e.Roles).SingleOrDefaultAsync(e => e.Id == id);
            var role = _context.Roles.ToList();
            //List<RoleSelection> roleSelections = new List<RoleSelection>();
            //foreach (Role r in role)
            //{
            //    RoleSelection roleSelection = new RoleSelection(r);
            //    roleSelections.Add(roleSelection);
            //}
            EmployeeRoleSelections employeeRoleSelections = new EmployeeRoleSelections(employee, role);
    
            if (employee == null)
            {
                return NotFound();
            }
            return View(employeeRoleSelections);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeRoleSelections employeeRoleSelections)
        {

            if (!employeeRoleSelections.roleSelections.Any(m => m.Selected)) {
                ModelState.AddModelError("roleSelections", "You must select at least one role!");
            }
               
            if (_context.Employees.Any(e => e.UserName == employeeRoleSelections.employee.UserName && e.Id != employeeRoleSelections.employee.Id))
            {
                ModelState.AddModelError("employee.UserName", "This user name has been used by another employee! ");
            }

            if (ModelState.IsValid)
            {
                Employee employee = await _context.Employees.Include(e => e.Roles).SingleOrDefaultAsync(e => e.Id == employeeRoleSelections.employee.Id);
                employee.Roles.Clear();
                foreach (RoleSelection roleSelection in employeeRoleSelections.roleSelections)
                {
                    //if (roleSelection.Selected)
                    //{
                    if (roleSelection.Selected)
                    {
                        Role role = _context.Roles.SingleOrDefault(r => r.Id == roleSelection.role.Id);
                        employee.Roles.Add(role);
                    }
                    //}
                }
                _context.Update(employee);
                _context.SaveChangesAsync();
                //try
                //{
                //    _context.Update(employee);
                //    await _context.SaveChangesAsync();
                //}
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!EmployeeExists(employee.Id))
                //    {
                //        return NotFound();
                //    }
                //    else
                //    {
                //        throw;
                //    }
                //}
                return RedirectToAction(nameof(Index));
            }
            return View(employeeRoleSelections);
        }

        // GET: Employees/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Employees == null)
        //    {
        //        return NotFound();
        //    }

        //    var employee = await _context.Employees
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(employee);
        //}

        //// POST: Employees/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Employees == null)
        //    {
        //        return Problem("Entity set 'StudentRecordContext.Employees'  is null.");
        //    }
        //    var employee = await _context.Employees.FindAsync(id);
        //    if (employee != null)
        //    {
        //        _context.Employees.Remove(employee);
        //    }
            
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool EmployeeExists(int id)
        {
          return _context.Employees.Any(e => e.Id == id);
        }
    }
}
