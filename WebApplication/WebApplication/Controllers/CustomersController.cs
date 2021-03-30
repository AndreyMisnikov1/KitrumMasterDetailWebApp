namespace WebApplication.Controllers
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using BL.DTO;
    using BL.Interfaces;

    using DAL;

    using LinqKit;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    using WebApplication.PageHelpers;

    public class CustomersController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly ICustomerService customerService;

        public CustomersController(ILogger<HomeController> logger, ICustomerService customerService)
        {
            this.logger = logger;
            this.customerService = customerService;
        }


        private Expression<Func<CustomerDTO, bool>> BuildExpressionToSearchByFields(string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                return item => true;
            }

            var predicate = PredicateBuilder.Or<CustomerDTO>(
                    customer => customer.Name != null && customer.Name.Contains(searchString),
                    customer => customer.Surname != null && customer.Surname.Contains(searchString))
                .Or(customer => customer.City != null && customer.City.Contains(searchString))
                .Or(customer => customer.Street != null && customer.Street.Contains(searchString)).Or(
                    customer => customer.Zip != null && customer.Zip.Contains(searchString));
            return predicate;
        }


        public async Task<IActionResult> Index(string currentFilter, string searchString, int? pageNumber)
        {
            int pageSize = 5;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            this.ViewData["CurrentFilter"] = searchString;

            //the issue with that method that we cannot pass DTO object to service
            //var items = this.customerService.GetPage(pageNumber ?? 1, pageSize, searchString, out int count); 

            var items = this.customerService.GetPage(pageNumber ?? 1, pageSize, this.BuildExpressionToSearchByFields(searchString), out int count);

            return this.View(PaginatedList<CustomerDTO>.Create(items, count, pageNumber ?? 1, pageSize));
        }

        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Surname,Street,Zip,City,Birthdate,Id")] CustomerDTO customer)
        {
            if (ModelState.IsValid)
            {
                await this.customerService.Insert(customer);
                return RedirectToAction(nameof(this.Index));
            }

            return this.View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var customer = this.customerService.FindById((int)id);
            if (customer == null)
            {
                return this.NotFound();
            }

            return this.View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Surname,Street,Zip,City,Birthdate,Id")] CustomerDTO customer)
        {
            if (id != customer.Id)
            {
                return this.NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await this.customerService.Update(customer);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.customerService.Any(id))
                    {
                        return this.NotFound();
                    }

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return this.View(customer);
        }
    }
}