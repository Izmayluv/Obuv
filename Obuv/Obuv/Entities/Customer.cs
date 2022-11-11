namespace Obuv.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Customer")]
    public partial class Customer
    {
        [Key]
        public int userID { get; set; }

        [Required]
        [StringLength(100)]
        public string userSurname { get; set; }

        [Required]
        [StringLength(100)]
        public string userName { get; set; }

        [Required]
        [StringLength(100)]
        public string userPatronymic { get; set; }

        [Required]
        public string userLogin { get; set; }

        [Required]
        public string userPassword { get; set; }

        public int userRole { get; set; }

        public virtual StaffRole StaffRole { get; set; }
    }
}
