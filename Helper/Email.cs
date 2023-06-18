using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace ControleDeContatos.Helper
{
    public class Email : IEmail
    {
        private readonly IConfiguration _configuration;//O Configuration consegue usar as propriedadedes inseridas no Appsettings.json 

        public Email(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        public bool Enviar(string email, string assunto, string mensagem)
        {
            //recupera do appsettings
            try
            {
                string host = _configuration.GetValue<string>("SMTP:Host"); //SMTP é a chave principal do node, depois vem host
                int porta = _configuration.GetValue<int>("SMTP:Porta");
                string senha = _configuration.GetValue<string>("SMTP:Senha");
                string username = _configuration.GetValue<string>("SMTP:UserName");
                string nome = _configuration.GetValue<string>("SMTP:Nome");

                MailMessage mail = new MailMessage(){
                    From = new MailAddress(username, nome)
                };
                mail.To.Add(email);
                mail.Subject = assunto;
                mail.Body = mensagem;
                mail.IsBodyHtml = true; //permite html no email
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(host, porta)) 
                {
                    smtp.Credentials = new NetworkCredential(username, senha);
                    smtp.EnableSsl = true; //envio de email seguro
                    smtp.Send(mail);
                    return true;
                }
            }
            catch (System.Exception ex)
            {
                //gravar log de erro
                return false;
            }
        }
    }
}
