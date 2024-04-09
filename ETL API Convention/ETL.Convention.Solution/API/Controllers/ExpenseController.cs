using Domain.Entities;
using Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Utilities.Constants;

namespace API.Controllers
{
    /// <summary>
    /// Expense Controller
    /// </summary>
    [Route(RouteConstants.BaseRoute)]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IUnitOfWork context;
        public ExpenseController(IUnitOfWork context)
        {
            this.context = context;
        }

        /// <summary>
        /// expense-api/expense
        /// </summary>
        /// <param name="expense"></param>
        /// <returns>Http status code: CreateAT.</returns>
        [HttpPost]
        [Route(RouteConstants.CreateExpense)]
        public async Task<IActionResult> CreateExpense(Expense expense)
        {
            try
            {
                expense.DateCreated = DateTime.Now;
                expense.IsDeleted = false;


                context.ExpenseRepository.Add(expense);
                await context.SaveChangesAsync();

                return CreatedAtAction("ReadExpenseByKey", new { key = expense.Oid }, expense);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// expense-api/expenses
        /// </summary>
        /// <returns>Http status code: Ok.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadExpenses)]
        public async Task<IActionResult> ReadExpenses()
        {
            try
            {
                var expenseIndb = await context.ExpenseRepository.GetExpenseInfos();


                return Ok(expenseIndb);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// expense-api/expense/key/{key}
        /// </summary>
        /// <param name="key"> Primary key of the table expenses</param>
        /// <returns>Http status code: Ok.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadExpenseByKey)]
        public async Task<IActionResult> ReadExpenseByKey(int key)
        {
            try
            {
                if (key <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var expenseIndb = await context.ExpenseRepository.GetExpenseInfoByKey(key);

                if (expenseIndb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                return Ok(expenseIndb);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// expense-api/expense/{key}
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expense"></param>
        /// <returns>Http Status code: NoContent</returns>
        [HttpPut]
        [Route(RouteConstants.UpdateExpense)]
        public async Task<IActionResult> UpdateProduct(int key, Expense expense)
        {
            try
            {
                var expenseIndb = await context.ExpenseRepository.GetExpenseInfoByKey(expense.Oid);

                if (key != expense.Oid)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.UnauthorizedAttemptOfRecordUpdateError);
                expenseIndb.DateModified = DateTime.Now;
                expenseIndb.IsDeleted = false;

                context.ExpenseRepository.Update(expense);
                await context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: expense-api/expense/{key}
        /// </summary>
        /// <param name="key">Primary key of the table expenses</param>
        /// <returns>Http status code: Ok.</returns>
        [HttpDelete]
        [Route(RouteConstants.DeleteExpense)]
        public async Task<IActionResult> DeleteExpense(int key)
        {
            try
            {
                if (key <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var expenseIndb = await context.ExpenseRepository.GetExpenseInfoByKey(key);

                if (expenseIndb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                expenseIndb.DateModified = DateTime.Now;
                expenseIndb.IsDeleted = true;

                context.ExpenseRepository.Update(expenseIndb);
                await context.SaveChangesAsync();

                return Ok(expenseIndb);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }
    }
}