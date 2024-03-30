using System.ComponentModel.DataAnnotations;

namespace Instagram_Clone.ViewModels
{
    public class UniqueAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid
            (object? value, ValidationContext validationContext)
        {
            
            Context context = validationContext.GetService<Context>();

            string name = value.ToString();
            RegisterViewModel UserFromRequest = validationContext.ObjectInstance as RegisterViewModel;
            
            ApplicationUser UserFromDb = context.Users
                .FirstOrDefault(user => user.UserName == name );
            if (UserFromDb == null)
            {
                //value valid
                return ValidationResult.Success;
            }

            return new ValidationResult("Name Already Found (it Must Be Unique)");
        }
    }
}
