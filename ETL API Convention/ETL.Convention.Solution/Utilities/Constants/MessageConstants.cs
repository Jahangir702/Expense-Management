/*
 * Created by   : Jahangir
 * Date created : 30.03.2024
 * Modified by  : 
 * Last modified: 
 * Reviewed by  : 
 * Reviewed Date:
 */
namespace Utilities.Constants
{
    /// <summary>
    /// Error message constants.
    /// </summary>
    public static class MessageConstants
    {
        //Common
        public const string RequiredFieldError = "Required";

        public const string DuplicateError = "Duplicate data found";

        public const string GenericError = "Something went wrong! Please try after sometime. If you are experiencing similar problem frequently, please report it to helpdesk.";

        public const string InvalidParameterError = "Invalid parameter(s)!";

        public const string NoMatchFoundError = "No match found!";

        public const string UnauthorizedAttemptOfRecordUpdateError = "Unauthorized attempt of updating record!";

        //ExpenseCategory
        public const string ExpenseCategoryCreatedSuccessfully = "ExpenseCategory Saved Successfully.";

        public const string ExpenseCategoryUpdatedSuccessfully = "ExpenseCategory Updated Successfully.";

        //Expense
        public const string ExpenseCreatedSuccessfully = "Expense Saved Successfully.";

        public const string ExpenseUpdatedSuccessfully = "Expense Updated Successfully.";
    }
}