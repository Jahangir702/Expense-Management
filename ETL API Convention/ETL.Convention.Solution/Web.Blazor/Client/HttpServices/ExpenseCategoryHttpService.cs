using Domain.Entities;
using System.Net.Http.Json;

namespace Web.Blazor.Client.HttpServices
{
    public class ExpenseCategoryHttpService
    {
        private readonly HttpClient httpClient;
        public ExpenseCategoryHttpService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<ExpenseCategory>> GetExpenseCategoriesAsync()
        {
            return await httpClient.GetFromJsonAsync<List<ExpenseCategory>>("expense-api/expensecategories");
        } 

        public async Task<ExpenseCategory>GetcategoryByIdAync(int key)
        {
            return await httpClient.GetFromJsonAsync<ExpenseCategory>($"expense-api/expensecategory/key/{key}");
        }

        public async Task<ExpenseCategory> CreateExpenseCategoryAsync(ExpenseCategory expenseCategory)
        {
            var response = await httpClient.PostAsJsonAsync("expense-api/expensecategory", expenseCategory);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ExpenseCategory>();
        }

        public async Task<ExpenseCategory> UpdateExpenseCategoryAsync(ExpenseCategory expenseCategory)
        {
            var response = await httpClient.PutAsJsonAsync($"expense-api/expensecategory/{expenseCategory.Oid}", expenseCategory);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ExpenseCategory>();
        }

        public async Task DeleteExpenseCategoryAsync(string key)
        {
            var response = await httpClient.DeleteAsync($"expense-api/expensecategory/{key}");
            response.EnsureSuccessStatusCode();
        }
    }
}