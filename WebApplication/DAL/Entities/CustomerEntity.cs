namespace DAL.Objects
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using DAL.Entities;

    public class CustomerEntity : HasIdBase<int>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        public string Street { get; set; }

        public string Zip { get; set; }

        public string City { get; set; }

        [Column(TypeName = "Date")]
        public DateTime Birthdate { get; set; }
    }
}
