using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Profissional.Data;
using Profissional.Models;
using Profissional.Models.Domain;

namespace Profissional.Controllers
{
    public class ProfissionaisController : Controller
    {
        private readonly ProfissionalDbContext context;

        public ProfissionaisController(ProfissionalDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var list = await context.Employees.ToListAsync();
            return View(list);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProfissionalViewModel request)
        {
            var newPro = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Tel = request.Tel,
                RG = request.RG,
                Adress = request.Adress,
                Salary = request.Salary,
            };

            await context.Employees.AddAsync(newPro);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var employee = await context.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee != null)
            {
                var viewModel = new UpdateProfissionalViewModel()
                {
                    Id = Guid.NewGuid(),
                    Name = employee.Name,
                    Tel = employee.Tel,
                    RG = employee.RG,
                    Adress = employee.Adress,
                    Salary = employee.Salary,
                };

                return await Task.Run(() => View("View", viewModel));
            }

            

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateProfissionalViewModel model)
        {
            var employee = await context.Employees.FindAsync(model.Id);

            if (employee != null)
            {
                employee.Name = model.Name;
                employee.Tel = model.Tel;
                employee.RG = model.RG;
                employee.Adress = model.Adress;
                employee.Salary = model.Salary;

                await context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateProfissionalViewModel model)
        {
            var employee = context.Employees.Find(model.Id);

            if (employee != null)
            {
                context.Employees.Remove(employee);
                await context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
