using System.ComponentModel.DataAnnotations;
using Utilities.Constants;

/*
 * Created by   : Jahangir
 * Date created : 30.04.2024
 * Modified by  :
 * Last modified:
 * Reviewed by  :
 * Date reviewed:
 */
namespace Domain.Entities
{
    /// <summary>
    /// ExpenseCategory entity
    /// </summary>
    public class ExpenseCategory : BaseModel
    {
        /// <summary>
        /// Primary Key of the table Province
        /// </summary>
        [Key]
        public int Oid { get; set; }

        /// <summary>
        /// Name of the ExpenseCategory
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [StringLength(100)]
        [DataType(DataType.Text)]
        [Display(Name = "Expense Category Name")]
        public string Description { get; set; }

        /// <summary>
        /// Avilable of the Expencecategory
        /// </summary>
        public bool Available { get; set; }

        /// <summary>
        /// Navigation Property
        /// </summary>
        public virtual IEnumerable<Expense> Expenses { get; set; }
    }
}