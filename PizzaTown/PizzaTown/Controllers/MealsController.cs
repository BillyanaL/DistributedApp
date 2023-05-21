using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaTown.Data;

namespace PizzaTown.Controllers
{
    public class MealsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly AutoMapper.IConfigurationProvider _configuration;

        public MealsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _configuration = mapper.ConfigurationProvider;
        }
        
        public async Task<ActionResult> Index()
        {
            var meals = await _context.Meals.ToListAsync();
            return View(meals);
        }

        // GET: MealsController/Details/5
        public ActionResult Details(Guid id)
        {
            return View();
        }

        // GET: MealsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MealsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MealsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MealsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MealsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MealsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
