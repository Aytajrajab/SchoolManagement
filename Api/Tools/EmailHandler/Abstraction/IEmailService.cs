using System.Threading.Tasks;

namespace Api.Tools.EmailHandler.Abstraction
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
