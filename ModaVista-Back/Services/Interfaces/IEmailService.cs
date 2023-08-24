namespace ModaVista_Back.Services.Interfaces
{
    public interface IEmailService
    {
        void Send(string to, string subject, string html, string from = null);
    }
}
