using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
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
    /// Expense entity
    /// </summary>
    public class Expense : BaseModel
    {
        /// <summary>
        /// Primary Key of the Expense table 
        /// </summary>
        [Key]
        public int Oid { get; set; }

        /// <summary>
        /// Name of the Expense
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [StringLength(60)]
        [DataType(DataType.Text)]
        [Display(Name = "Expense Name")]
        public string Description { get; set; }

        /// <summary>
        /// Price of product
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        /// <summary>
        /// Date of Expense
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [Column(TypeName = "smalldatetime")]
        [Display(Name = "Expense Date")]
        public DateTime ExpenseDate { get; set; }

        /// <summary>
        /// Foreign key. Primary key of the table ExpenseCategory .
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        public int ExpenseCategoryId { get; set; }

        /// <summary>
        /// Navigation property.
        /// </summary>
        [ForeignKey("ExpenseCategoryId")]
        [JsonIgnore]
        public virtual ExpenseCategory ExpenseCategories { get; set; }
    }
}