namespace BL.DTO
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using DAL.Entities;

    using WebApplication.CustomValidation;

    public class CustomerDTO: HasIdBase<int>
    {
        [Required(ErrorMessage = "Please enter first name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter surname")]

        public string Surname { get; set; }

        public string Street { get; set; }

        public string Zip { get; set; }

        public string City { get; set; }

        [Required(ErrorMessage = "Please enter birth date")]
        [Display(Name = "Birth Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [CustomBirthdate(ErrorMessage = "Birth date must be less than or equal to Today's Date")]
        public DateTime Birthdate { get; set; }
    }
}
