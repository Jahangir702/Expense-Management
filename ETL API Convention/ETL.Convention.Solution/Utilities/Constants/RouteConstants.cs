/*
 * Created by   : Jahangir
 * Date created : 31.03.2024
 * Modified by  : 
 * Last modified: 
 * Reviewed by  :
 * Date reviewed:
 */
namespace Utilities.Constants
{
    /// <summary>
    /// RouteConstants.
    /// </summary>
    public static class RouteConstants
    {
        public const string BaseRoute = "expense-api";

        #region ExpenseCategory
        public const string CreateExpenseCategory = "expensecategory";

        public const string ReadExpenseCategories = "expensecategories";

        public const string ReadExpenseCategoryByKey = "expensecategory/key/{key}";

        public const string UpdateExpenseCategory = "expensecategory/{key}";

        public const string DeleteExpenseCategory = "expensecategory/{key}";
        #endregion

        #region Expense
        public const string CreateExpense = "expense";

        public const string ReadExpenses = "expenses";

        public const string ReadExpensesByExpenseCategoryId = "expenses/expensecategory/{expensecategoryId}";

        public const string ReadExpenseByKey = "expense/key/{key}";

        public const string UpdateExpense = "expense/{key}";

        public const string DeleteExpense = "expense/{key}";
        #endregion
    }
}