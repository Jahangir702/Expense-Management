using Domain.Entities;
using Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Utilities.Constants;

namespace API.Controllers
{
    /// <summary>
    /// Category controller.
    /// </summary>
    [Route(RouteConstants.BaseRoute)]
    [ApiController]
    public class ExpenseCategoryController : ControllerBase
    {
        private readonly IUnitOfWork context;
        public ExpenseCategoryController(IUnitOfWork context)
        {
            this.context = context;
        }

        /// <summary>
        /// Checks whether the product name is duplicate or not.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(RouteConstants.CreateExpenseCategory)]
        public async Task<IActionResult> CreateExpenseCategory(ExpenseCategory expenseCategory)
        {
            try
            {
                if (await IsExpenseCategoryDuplicate(expenseCategory) == true)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.DuplicateError);

                expenseCategory.DateCreated = DateTime.Now;
                expenseCategory.IsDeleted = false;

                context.ExpenseCategoryRepository.Add(expenseCategory);
                await context.SaveChangesAsync();

                return CreatedAtAction("ReadExpenseCategoryByKey", new { key = expenseCategory.Oid }, expenseCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// checks whether the expensecategory name is duplicate or not.
        /// </summary>
        /// <param name="expensecategory"></param>
        /// <returns></returns>
        private async Task<bool> IsExpenseCategoryDuplicate(ExpenseCategory expenseCategory)
        {
            try
            {
                var expensecategoryInDb = await context.ExpenseCategoryRepository.GetExpenseCategoryByName(expenseCategory.Description);

                if (expensecategoryInDb != null)
                    if (expensecategoryInDb.Oid != expenseCategory.Oid)
                        return true;
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// URL: expense-api/expensecategories
        /// </summary>
        /// <returns> Http status code: Ok.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadExpenseCategories)]
        public async Task<IActionResult> ReadExpenseCategories()
        {
            try
            {
                var expensecategoryInDb = await context.ExpenseCategoryRepository.GetExpenseCategories();
                return Ok(expensecategoryInDb);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// expense-api/expensecategory/key/{key}
        /// </summary>
        /// <param name="key"> Primary key of the table expensecategories</param>
        /// <returns> Http status code: Ok.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadExpenseCategoryByKey)]
        public async Task<IActionResult> ReadExpenseCategoryByKey(int key)
        {
            try
            {
                if (key <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var expensecategoryInDb = await context.ExpenseCategoryRepository.GetExpenseCategoryInfoByKey(key);

                if (expensecategoryInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                return Ok(expensecategoryInDb);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// expense-api/expensecategory/{key}
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Http status code: Ok.</returns>
        [HttpDelete]
        [Route(RouteConstants.DeleteExpenseCategory)]
        public async Task<IActionResult> DeleteExpenseCategory(int key)
        {
            try
            {
                if (key <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);
                
                var expensecategoryInDb = await context.ExpenseCategoryRepository.GetExpenseCategoryInfoByKey(key);

                if (expensecategoryInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                expensecategoryInDb.DateModified = DateTime.Now;
                expensecategoryInDb.IsDeleted = true;

                context.ExpenseCategoryRepository.Update(expensecategoryInDb);
                await context.SaveChangesAsync();

                return Ok(expensecategoryInDb);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// expense-api/expensecategory/{key}
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expensecategory"></param>
        /// <returns> Http status code: NoContent</returns>
        [HttpPut]
        [Route(RouteConstants.UpdateExpenseCategory)]
        public async Task<IActionResult> UpdateExpenseCategory(int key, ExpenseCategory expenseCategory)
        {
            try
            {
                if (key != expenseCategory.Oid)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.UnauthorizedAttemptOfRecordUpdateError);

                if (await IsExpenseCategoryDuplicate(expenseCategory) == true)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.DuplicateError);

                expenseCategory.DateModified = DateTime.Now;
                expenseCategory.IsDeleted = false;

                context.ExpenseCategoryRepository.Update(expenseCategory);
                await context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }
    }
}