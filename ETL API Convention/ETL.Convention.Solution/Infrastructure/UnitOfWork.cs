using Infrastructure.Contracts;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

/*
 * Created by: Jahangir
 * Date created: 29.03.2024
 * Modified by: 
 * Last modified: 
 */
namespace Infrastructure
{
    /// <summary>
    /// Implementation of IUnitOfWork.
    /// </summary>
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        protected readonly ExpenseDbContext context;
        protected readonly IConfiguration configuration;
        private bool _disposed;

        public UnitOfWork(ExpenseDbContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        //EXPENSECATEGORY
        #region ExpenseCategoryRepository
        private IExpenseCategoryRepository expenseCategoryRepository;
        public IExpenseCategoryRepository ExpenseCategoryRepository
        {
            get
            {
                if (expenseCategoryRepository == null)
                    expenseCategoryRepository = new ExpenseCategoryRepository(context);
                return expenseCategoryRepository;
            }
        }
        #endregion

        //EXPENSE
        #region ExpenseRepository
        private IExpenseRepository expenseRepository;
        public IExpenseRepository ExpenseRepository
        {
            get
            {
                if (expenseRepository == null)
                    expenseRepository = new ExpenseRepository(context);
                return expenseRepository;
            }
        }
        #endregion

        public IDbContextTransaction BeginTransaction()
        {
            return context.Database.BeginTransaction();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await context.Database.BeginTransactionAsync();
        }
        protected void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}