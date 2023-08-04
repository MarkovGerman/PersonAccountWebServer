using System.ComponentModel.DataAnnotations;
using PersonalAccountWebServer.Models;

namespace WebApplication2.Validation
{
    public class PersonalAccountAttribute:ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            PersonalAccount? account = value as PersonalAccount;
            return account != null && account.DateStart < account.DateEnd;
        }
    }
}