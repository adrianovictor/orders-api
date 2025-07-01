using System.Net.Mail;

namespace OrdersService.Domain.Validators;

public class EmailValidator
{
    public static bool Validate(string email)
    {
        try
        {
            var mailAddress = new MailAddress(email);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}
