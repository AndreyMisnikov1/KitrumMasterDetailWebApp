namespace WebApplication.CustomValidation
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CustomBirthdate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime = Convert.ToDateTime(value);
            return dateTime <= DateTime.Now;
        }
    }
}
