using Domain.Entities;
using System.Net.Http.Json;

namespace Web.Blazor.Client.HttpServices
{
    public class ExpenseHttpService
    {
        private readonly HttpClient httpClient;

        public ExpenseHttpService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<Expense>> GetExpensesAsync()
        {
            return await httpClient.GetFromJsonAsync<List<Expense>>("expense-api/expenses");
        }

        public async Task<List<ExpenseCategory>> GetExpenseCategoriesAsync()
        {
            return await httpClient.GetFromJsonAsync<List<ExpenseCategory>>("expense-api/expensecategories");
        }

        public async Task<Expense> GetExpenseByIdAsync(int key)
        {
            return await httpClient.GetFromJsonAsync<Expense>($"expense-api/expense/key/{key}");
        }

        public async Task<Expense> CreateExpenseAsync(Expense expense)
        {
            var response = await httpClient.PostAsJsonAsync("expense-api/expense", expense);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Expense>();
        }

        public async Task<Expense> UpdateExpenseAsync(Expense expense)
        {
            var response = await httpClient.PutAsJsonAsync($"expense-api/expense/{expense.Oid}", expense);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Expense>();
        }
        public async Task DeleteExpenseAsync(string key)
        {
            var response = await httpClient.DeleteAsync($"expense-api/expense/{key}");
            response.EnsureSuccessStatusCode();
        }
    }
}