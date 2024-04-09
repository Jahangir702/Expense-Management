using Microsoft.EntityFrameworkCore.Storage;

/*
 * Created by   : Jahangir
 * Date created : 02.04.2024
 * Modified by  : 
 * Last modified: 
 * Reviewed by  :
 * Date reviewed:
 */
namespace Infrastructure.Contracts
{
    /// <summary>
    /// IUnitOfWork.
    /// </summary>
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        IDbContextTransaction BeginTransaction();
        Task<IDbContextTransaction> BeginTransactionAsync();
        IExpenseCategoryRepository ExpenseCategoryRepository { get; }
        IExpenseRepository ExpenseRepository { get; }
    }
}