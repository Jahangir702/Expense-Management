using Domain.Entities;

namespace Infrastructure.Contracts
{
    public interface IExpenseRepository : IRepository<Expense>
    {
        /// <summary>
        /// The method is used to get a ExpenseInfo by ExpenseInfo name.
        /// </summary>
        /// <param name="ExpenseInfoName">Name of a ExpenseInfo.</param>
        /// <returns>Returns a Expense if the Expense name is matched.</returns>
        public Task<Expense> GetExpenseInfoByName(string ExpenseInfoName);

        /// <summary>
        /// The method is used to get a ExpenseInfo by key.
        /// </summary>
        /// <param name="key">Primary key of the table ExpenseInfos.</param>
        /// <returns>Returns a ExpenseInfo if the key is matched.</returns>
        public Task<Expense> GetExpenseInfoByKey(int key);

        /// <summary>
        /// The method is used to get the ExpenseInfo byShelfId.
        /// </summary>
        /// <param name="ShelfId">PovinceID of the table ExpenseInfos.</param>
        /// <returns>Returns a ExpenseInfo if theShelfId is matched.</returns>
        public Task<IEnumerable<Expense>> GetExpenseInfoByExpenseCategory(int ExpenseCategoryId);

        /// <summary>
        /// The method is used to get the list of ExpenseCategoryInfos.
        /// </summary>
        /// <returns>Returns a list of all ExpenseCategoryInfos.</returns>
        public Task<IEnumerable<Expense>> GetExpenseInfos();
    }
}