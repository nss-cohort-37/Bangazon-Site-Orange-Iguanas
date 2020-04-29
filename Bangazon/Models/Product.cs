using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bangazon.Models
{
    public class Product
    {
        [Key]
        public int ProductId {get;set;}

        [Required]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateCreated {get;set;}

        [Required]
        [StringLength(255)]
        [RegularExpression("^[-_,.A-Za-z0-9 ]*$", ErrorMessage = "The characters '!', '@' '#', '$', '%', '^', '&', '*', '(', and ')' are not allowed")]

        public string Description { get; set; }

        [Required]
        [StringLength(55, ErrorMessage="Please shorten the product title to 55 characters")]
        [RegularExpression("^[-_,.A-Za-z0-9 ]*$", ErrorMessage = "The characters '!', '@' '#', '$', '%', '^', '&', '*', '(', and ')' are not allowed")] 

        public string Title { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Range(typeof(double), "0.01", "10000.00", ErrorMessage = "Price must be be at least $0.01 and not greater than $10,000.00")]
        public double Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string UserId {get; set;}
        [Display(Name = "Location")]

        public string City {get; set;}

        [Display(Name = "Local Delivery Available")]

        public bool LocalDelivery { get; set; }

        public string ImagePath {get; set;}

        public bool Active { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        [Required]
        [Display(Name="Product Category")]
        public int ProductTypeId { get; set; }

        public ProductType ProductType { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }

        public Product ()
        {
            Active = true;
        }
  

    }
}
