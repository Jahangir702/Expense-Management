using Domain.Entities;

namespace Infrastructure.Contracts
{
    /// <summary>
    /// Implementation of IExpenseCategoryRepository interface.
    /// </summary>
    public interface IExpenseCategoryRepository : IRepository<ExpenseCategory>
    {
        /// <summary>
        /// The method is used to get a ExpenseCategoryInfo by key.
        /// </summary>
        /// <param name="key">Primary key of the table ExpenseCategoryInfo.</param>
        /// <returns>Returns a ExpenseCategoryInfo if the key is matched.</returns>
        public Task<ExpenseCategory> GetExpenseCategoryInfoByKey(int key);

        /// <summary>
        /// The method is used to get a ExpenseCategory by ExpenseCategory name.
        /// </summary>
        /// <param name="ExpenseCategoryName">Name of a ExpenseCategory.</param>
        /// <returns>Returns a ExpenseCategory if the ExpenseCategory name is matched.</returns>
        public Task<ExpenseCategory> GetExpenseCategoryByName(string ExpenseCategoryName);

        /// <summary>
        /// Returns all ExpenseCategory.
        /// </summary>
        /// <returns>List of ExpenseCategory object.</returns>
        public Task<IEnumerable<ExpenseCategory>> GetExpenseCategories();
    }
}