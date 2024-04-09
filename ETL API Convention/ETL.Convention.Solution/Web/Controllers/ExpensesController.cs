using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Utilities.Constants;
using Web.HttpServices;

/*
 * Created by   : Jahangir
 * Date created : 01.04.2024
 * Modified by  : 
 * Last modified: 
 * Reviewed by  : 
 * Date reviewed:
 */
namespace Web.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly ExpensesHttpService expensesHttpService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private ISession? session => httpContextAccessor.HttpContext?.Session;

        public ExpensesController(ExpensesHttpService expensesHttpService, IHttpContextAccessor httpContextAccessor)
        {
            this.expensesHttpService = expensesHttpService;
            this.httpContextAccessor = httpContextAccessor;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            try
            {
                var outcome = await expensesHttpService.ReadExpenses();

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
        #endregion

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                ViewBag.ExpenseCategories = new SelectList(await ReadExpenseCategories(), FieldConstants.Oid, FieldConstants.ExpenseCategoryName);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Expense expense)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ResponseOutcome<Expense> expenseOutcome = await expensesHttpService.CreateExpense(expense);
                    if (expenseOutcome.ResponseStatus == ResponseStatus.Failed)
                    {
                        ViewBag.ExpenseCategories = new SelectList(await ReadExpenseCategories(), FieldConstants.Oid, FieldConstants.Description);
                        TempData[SessionConstants.Message] = expenseOutcome.Message;
                    }
                    if (expenseOutcome.ResponseStatus == ResponseStatus.Success)
                    {
                        TempData[SessionConstants.Message] = MessageConstants.ExpenseCreatedSuccessfully;
                        return RedirectToAction("Index", new { id = expenseOutcome.Entity.Oid });
                    }
                    ModelState.AddModelError("", expenseOutcome.Message);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(expense);
        }
        #endregion

        #region Details
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                ResponseOutcome<Expense> expenseOutcome = await expensesHttpService.ReadExpenseById(id);

                if (expenseOutcome.ResponseStatus == ResponseStatus.Success)
                    return View(expenseOutcome.Entity);

                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                if (id == null)
                    return BadRequest();

                ResponseOutcome<Expense> expenseOutcome = await expensesHttpService.ReadExpenseById(id);
                //expenseOutcome.Entity.ExpenseCategoryId = expenseOutcome.Entity.ExpenseCategoryId;
                if (expenseOutcome.ResponseStatus == ResponseStatus.Failed)
                    return NotFound();

                ViewBag.ExpenseCategories = new SelectList(await ReadExpenseCategories(), FieldConstants.Oid, FieldConstants.ExpenseCategoryName);

                return View(expenseOutcome.Entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Expense expense)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    expense.DateModified = DateTime.Now;
                    expense.IsDeleted = false;
                    ResponseOutcome<Expense> expenseOutcome = await expensesHttpService.UpdateExpense(expense);

                    if (expenseOutcome.ResponseStatus == ResponseStatus.Failed)
                    {
                        ViewBag.ExpenseCategories = new SelectList(await ReadExpenseCategories(), FieldConstants.Oid, FieldConstants.ExpenseCategoryName);
                        TempData[SessionConstants.Message] = expenseOutcome.Message;
                    }

                    if (expenseOutcome.ResponseStatus == ResponseStatus.Success)
                    {
                        TempData[SessionConstants.Message] = MessageConstants.ExpenseCategoryUpdatedSuccessfully;

                        return RedirectToAction("Index", new { id = expenseOutcome.Entity.Oid });
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(expense);
            
        }
        #endregion

        #region Read Relational Data
        private async Task<List<ExpenseCategory>> ReadExpenseCategories()
        {
            try
            {
                List<ExpenseCategory> expenseCategories = new List<ExpenseCategory>();
                ResponseOutcome<ExpenseCategory> expensecategoryOutcome = await expensesHttpService.ReadExpenseCategories();
                if (expensecategoryOutcome.ResponseStatus == ResponseStatus.Success)
                    expenseCategories = expensecategoryOutcome.EntityList;

                return expenseCategories;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}