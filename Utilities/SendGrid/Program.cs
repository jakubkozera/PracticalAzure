using SendGrid;
using SendGrid.Helpers.Mail;

Console.WriteLine("Hello world");

var apiKey = "SG.api...key";

var client = new SendGridClient(apiKey);

var to = new EmailAddress("odbiorca.sendgrid@test.pl", "Odbiorca");
var from = new EmailAddress("nadawca.sendgrid@test.pl", "Nadawca");

var message = MailHelper.CreateSingleTemplateEmail(from, to, "d-34012b901ee64c45b400aeae06bb6d73", new
{
    Title = "Test title",
    Description = "Some description"
});

var response = await client.SendEmailAsync(message);

var responseBody = await response.Body.ReadAsStringAsync();

Console.WriteLine("Email sent");
Console.WriteLine(response.IsSuccessStatusCode);
Console.WriteLine(responseBody);