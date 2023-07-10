using Azure;
using Azure.Communication.Email;
using Microsoft.AspNetCore.Mvc;
using SWC_Payment_Website.Models;

namespace SWC_Payment_Website.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View(new ContactModel());
        }

        [HttpPost]
        public async Task<ActionResult> Index(ContactModel model)
        {
            if (!ModelState.IsValid) return View(model);
            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Message) || !IsValidEmail(model.Email)) return View(model);

            string connectionString = "endpoint=https://holidayfindertestcom.communication.azure.com/;accesskey=6wa8h82EL33UiJUezY2wd88Wm44P1LkkeM6wQBxU8mnXKcsqC1kRFvhzjihIF1xmh0wcp04Fzenu6kgcHancmA==";
            EmailClient emailClient = new EmailClient(connectionString);

            var subject = "SWC Customer Message";
            var htmlContent = $"<html><body><h3>Name: </h3><p>{model.Name}</p><h3>Email: </h3><p>{model.Email}</p><h3>Message: </h3><p>{model.Message}</p></body></html>";
            var sender = "customermessage@0b74ef9e-41b1-452d-aed2-540a5c124f1b.azurecomm.net";
            var recipient = "leverslwork@gmail.com";

            try
            {
                EmailSendOperation emailSendOperation = await emailClient.SendAsync(
                    Azure.WaitUntil.Completed,
                    sender,
                    recipient,
                    subject,
                    htmlContent);
                EmailSendResult statusMonitor = emailSendOperation.Value;
            }
            catch (RequestFailedException ex)
            {
                Console.WriteLine($"Email send operation failed with error code: {ex.ErrorCode}, message: {ex.Message}");
            }

            return Redirect("https://swcdynamics.com/");
        }

        bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
    }
}
