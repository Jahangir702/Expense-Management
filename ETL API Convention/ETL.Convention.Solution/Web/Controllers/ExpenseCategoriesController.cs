using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Utilities.Constants;
using Web.HttpServices;

/*
 * Created by   : Jahangir
 * Date created : 27-03-2024
 * Modified by  : 
 * Last modified: 
 * Reviewed by  :
 * Date reviewed:
 */
namespace Web.Controllers
{
    public class ExpenseCategoriesController : Controller
    {
        private readonly ExpenseCategoriesHttpService expenseCategoriesHttpService;
        private readonly IHttpContextAccessor httpContextAccessor;

        private ISession? session => httpContextAccessor.HttpContext?.Session;

        public ExpenseCategoriesController(ExpenseCategoriesHttpService expenseCategoriesHttpService, IHttpContextAccessor httpContextAccessor)
        {
            this.expenseCategoriesHttpService = expenseCategoriesHttpService;
            this.httpContextAccessor = httpContextAccessor;
        }
        #region Index
        public async Task<IActionResult> Index()
        {
            try
            {
                var outcome = await expenseCategoriesHttpService.ReadExpenseCategories();

                if (outcome.ResponseStatus == ResponseStatus.Success)
                {
                    return View(outcome.EntityList);
                }
                else
                {
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //public async Task<IActionResult> Index()
        //{
        //    ResponseOutcome<ExpenseCategory> expensesOutcome = await expenseCategoriesHttpService.ReadExpenseCategories();

        //    if (expensesOutcome.ResponseStatus == ResponseStatus.Success)
        //        return View(expensesOutcome.EntityList);

        //    return View(expensesOutcome);
        //}
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExpenseCategory expenseCategory)
        {
            ResponseOutcome<ExpenseCategory> expensecategoryOutcome = await expenseCategoriesHttpService.CreateExpenseCategory(expenseCategory);

            if (expensecategoryOutcome.ResponseStatus == ResponseStatus.Failed)
            {
                TempData[SessionConstants.Message] = expensecategoryOutcome.Message;
            }

            if (expensecategoryOutcome.ResponseStatus == ResponseStatus.Success)
            {
                TempData[SessionConstants.Message] = MessageConstants.ExpenseCategoryCreatedSuccessfully;

                return RedirectToAction("Details", new { id = expensecategoryOutcome.Entity.Oid });
            }

            return View();
        }
        #endregion

        #region Details
        public async Task<IActionResult> Details(int id)
        {
            ResponseOutcome<ExpenseCategory> expensecategoryOutcome = await expenseCategoriesHttpService.ReadExpenseCategoryById(id);
            if (expensecategoryOutcome.ResponseStatus == ResponseStatus.Success)
                return View(expensecategoryOutcome.Entity);
            else
                return NotFound();
        }
        #endregion

        #region Edit
        public async Task<IActionResult> Edit(int id)
        {
            ResponseOutcome<ExpenseCategory> expensecategoryOutcome = await expenseCategoriesHttpService.ReadExpenseCategoryById(id);
            if (expensecategoryOutcome.ResponseStatus == ResponseStatus.Success)
                return View(expensecategoryOutcome.Entity);
            else
                return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ExpenseCategory expenseCategory)
        {
            ResponseOutcome<ExpenseCategory> expensecategoryOutcome = await expenseCategoriesHttpService.UpdateExpenseCategory(expenseCategory);

            if (expensecategoryOutcome.ResponseStatus == ResponseStatus.Failed)
            {
                TempData[SessionConstants.Message] = expensecategoryOutcome.Message;
            }

            if (expensecategoryOutcome.ResponseStatus == ResponseStatus.Success)
            {
                TempData[SessionConstants.Message] = MessageConstants.ExpenseCategoryUpdatedSuccessfully;

                return RedirectToAction("Details", new { id = expensecategoryOutcome.Entity.Oid });
            }

            return View();
        }
        #endregion
    }
}