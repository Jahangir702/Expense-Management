using Domain.Entities;
using Infrastructure.Contracts;

/*
 * Created by   : Jahangir
 * Date created : 04-02-2024
 * Modified by  : 
 * Last modified: 
 * Reviewed by  :
 * Date reviewed:
 */
namespace Infrastructure.Repositories
{
    public class ExpenseRepository : Repository<Expense>, IExpenseRepository
    {
        public ExpenseRepository(ExpenseDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Expense>> GetExpenseInfoByExpenseCategory(int ExpenseCategoryId)
        {
            try
            {
                var expenseInfos = await QueryAsync(c => c.IsDeleted == false && c.ExpenseCategoryId == ExpenseCategoryId, d => d.ExpenseCategories);

                return expenseInfos.OrderBy(d => d.Description);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Expense> GetExpenseInfoByKey(int key)
        {
            try
            {
                return await FirstOrDefaultAsync(e => e.Oid == key && e.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Expense> GetExpenseInfoByName(string ExpenseInfoName)
        {
            try
            {
                return await FirstOrDefaultAsync(c => c.Description.ToLower().Trim() == ExpenseInfoName.ToLower().Trim() && c.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Expense>> GetExpenseInfos()
        {
            try
            {
                return await LoadListWithChildAsync<Expense>(c => c.IsDeleted == false, d => d.ExpenseCategories);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}