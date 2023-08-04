using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication2.Validation;

namespace PersonalAccountWebServer.Models
{
    public class PersonalAccount:IValidatableObject
    {
        public int Id { get; set; }
        public string NumberPA { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateStart { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateEnd { get; set; }
        public string Address { get; set; }
        public decimal Area { get; set; }

        [NotMapped]
        public bool ExistResident { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            if (DateStart > DateEnd)
                errors.Add(new ValidationResult("Дата старта позже дата окончания", new List<string>() { $"{DateStart}" }));
            if (string.IsNullOrWhiteSpace(NumberPA))
                errors.Add(new ValidationResult("Номер ЛС не заполнен", new List<string>() { $"{DateStart}" }));

            return errors;
        }
    }
}