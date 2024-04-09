using Domain.Entities;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Utilities.Constants;

/*
 * Created by    : Jahangir
 * Date created  : 30.03.2024
 * Modified by   : 
 * Last modified : 
 * Reviewed by   : 
 * Date Reviewed : 
 */
namespace Web.HttpServices
{
    public class ExpensesHttpService
    {
        private readonly HttpClient httpClient;
        public ExpensesHttpService(HttpClient httpClient)
        {
            var config = new ConfigurationBuilder()
                 .AddJsonFile(GeneralConstants.JsonFileName).Build();

            var baseUri = config.GetValue<string>(GeneralConstants.ExpenseBaseUri);
            var publicKey = config.GetValue<string>(GeneralConstants.PublicKey);

            this.httpClient = httpClient;
            this.httpClient.BaseAddress = new Uri(baseUri);
            this.httpClient.DefaultRequestHeaders.Clear();

            this.httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {publicKey}");
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #region Create
        public async Task<ResponseOutcome<Expense>> CreateExpense(Expense expense)
        {
            ResponseOutcome<Expense> outcome = new ResponseOutcome<Expense>();

            try
            {
                using (HttpResponseMessage response = await httpClient.PostAsJsonAsync(RouteConstants.CreateExpense, expense))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        outcome.ResponseStatus = ResponseStatus.Success;
                        outcome.Entity = await response.Content.ReadAsAsync<Expense>();
                    }
                    else
                    {
                        outcome.Message = JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
                    }
                }
            }
            catch (Exception ex)
            {
                outcome.Message = ex.Message;
            }
            return outcome;
        }
        #endregion

        #region Read
        public async Task<ResponseOutcome<Expense>> ReadExpenses()
        {
            ResponseOutcome<Expense> outcome = new ResponseOutcome<Expense>();

            try
            {
                using (HttpResponseMessage response = await httpClient.GetAsync(RouteConstants.ReadExpenses.ToString()))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        outcome.ResponseStatus = ResponseStatus.Success;
                        outcome.EntityList = JsonConvert.DeserializeObject<List<Expense>>(result) ?? new List<Expense>();
                    }
                    else
                    {
                        outcome.Message = JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
                    }
                }
            }
            catch (Exception ex)
            {
                outcome.Message = ex.Message;
            }

            return outcome;
        }
        public async Task<ResponseOutcome<Expense>> ReadExpenseById(int id)
        {
            ResponseOutcome<Expense> outcome = new ResponseOutcome<Expense>();

            try
            {
                using (HttpResponseMessage response = await httpClient.GetAsync(RouteConstants.ReadExpenseByKey.Replace("{key}", id.ToString())))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        outcome.ResponseStatus = ResponseStatus.Success;
                        outcome.Entity = JsonConvert.DeserializeObject<Expense>(result) ?? new();
                    }
                    else
                    {
                        outcome.Message = JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
                    }
                }
            }
            catch (Exception ex)
            {
                outcome.Message = ex.Message;
            }

            return outcome;
        }
        public async Task<ResponseOutcome<Expense>> ReadExpenseByExpenseCategory(int expensecategoryId)
        {
            ResponseOutcome<Expense> outcome = new ResponseOutcome<Expense>();

            try
            {
                using (HttpResponseMessage response = await httpClient.GetAsync(RouteConstants.ReadExpensesByExpenseCategoryId.Replace("{expensecategoryId}", expensecategoryId.ToString())))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        outcome.ResponseStatus = ResponseStatus.Success;
                        outcome.EntityList = JsonConvert.DeserializeObject<List<Expense>>(result) ?? new List<Expense>();
                    }
                    else
                    {
                        outcome.Message = JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
                    }
                }
            }
            catch (Exception ex)
            {
                outcome.Message = ex.Message;
            }

            return outcome;
        }
        public async Task<ResponseOutcome<ExpenseCategory>> ReadExpenseCategories()
        {
            ResponseOutcome<ExpenseCategory> outcome = new ResponseOutcome<ExpenseCategory>();

            try
            {
                using (HttpResponseMessage response = await httpClient.GetAsync(RouteConstants.ReadExpenseCategories.ToString()))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        outcome.ResponseStatus = ResponseStatus.Success;
                        outcome.EntityList = JsonConvert.DeserializeObject<List<ExpenseCategory>>(result) ?? new List<ExpenseCategory>();
                    }
                    else
                    {
                        outcome.Message = JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
                    }
                }
            }
            catch (Exception ex)
            {
                outcome.Message = ex.Message;
            }

            return outcome;
        }

        public async Task<ResponseOutcome<Expense>> ReadExpenseByKey(int id)
        {
            ResponseOutcome<Expense> outcome = new ResponseOutcome<Expense>();

            try
            {
                using (HttpResponseMessage response = await httpClient.GetAsync(RouteConstants.ReadExpenseByKey.Replace("{key}", id.ToString())))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        outcome.ResponseStatus = ResponseStatus.Success;
                        outcome.Entity = await response.Content.ReadAsAsync<Expense>();
                    }
                    else
                    {
                        outcome.Message = JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
                    }
                }
            }
            catch (Exception ex)
            {
                outcome.Message = ex.Message;
            }

            return outcome;
        }
        #endregion

        #region Update
        public async Task<ResponseOutcome<Expense>> UpdateExpense(Expense expense)
        {
            ResponseOutcome<Expense> outcome = new ResponseOutcome<Expense>();

            try
            {
                using (HttpResponseMessage response = await httpClient.PutAsJsonAsync(RouteConstants.UpdateExpense.Replace("{key}", expense.Oid.ToString()), expense))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        outcome.ResponseStatus = ResponseStatus.Success;
                        outcome.Entity = expense;
                    }
                    else
                    {
                        outcome.Message = JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
                    }
                }
            }
            catch (Exception ex)
            {
                outcome.Message = ex.Message;
            }

            return outcome;
        }
        #endregion
    }
}
