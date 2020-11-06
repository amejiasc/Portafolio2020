using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FERIA.NEGOCIO
{
    public class ServicioCorreo
    {

        MailMessage MyMail;
        SmtpClient SmtpMail;

        string mailFrom = "";
        int mailPort = 0;
        string mailHost = "";
        string mailUser = "";
        string mailPass = "";
        string mailUsaSsl = "";
        string[] mailRecipients;
        public ServicioCorreo() {
            try
            {
                mailFrom = ConfigurationManager.AppSettings.Get("Sender");
                mailPort = int.Parse(ConfigurationManager.AppSettings.Get("Port"));
                mailHost = ConfigurationManager.AppSettings.Get("Host");
                mailUser = ConfigurationManager.AppSettings.Get("User");
                mailPass = ConfigurationManager.AppSettings.Get("Pass");
                mailUsaSsl = ConfigurationManager.AppSettings.Get("UsaSsl");
                mailRecipients = ConfigurationManager.AppSettings.Get("Recipients").Split(';');
            }
            catch (Exception ex) { 
                
            }
        }
        public string Asunto { get; set; }
        public bool Enviar(string bodyText, string to, string[] adjuntos, string codigoQr = null)
        {
            MemoryStream contents = null;
            try
            {
                MyMail = new MailMessage();
                SmtpMail = new SmtpClient();
                MyMail.From = new MailAddress(mailFrom);
                foreach (var item in mailRecipients)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        MyMail.Bcc.Add(item);
                    }
                }
                MyMail.To.Add(to);
                MyMail.Subject = Asunto;
                MyMail.Body = bodyText;
                MyMail.IsBodyHtml = true;
                MyMail.Priority = MailPriority.Normal;
                foreach (var item in adjuntos)
                {
                    MyMail.Attachments.Add(new Attachment(item));

                }
                if (codigoQr != null)
                {
                    var bytes = Convert.FromBase64String(codigoQr);
                    contents = new MemoryStream(bytes);
                    Attachment OAtach = new Attachment(contents, "CodigoQr.png", "image/png");
                    OAtach.ContentId = "CodigoQr";
                    OAtach.ContentDisposition.Inline = true;
                    OAtach.ContentDisposition.DispositionType = System.Net.Mime.DispositionTypeNames.Inline;
                    MyMail.Attachments.Add(OAtach);
                }
                SmtpMail.Port = mailPort;
                SmtpMail.Host = mailHost;
                SmtpMail.Credentials = new System.Net.NetworkCredential(mailUser, mailPass);
                SmtpMail.EnableSsl = mailUsaSsl.Equals("S");
                SmtpMail.Send(MyMail);
                return true;
            }
            catch (Exception ex)
            {
                string strLog = "Ha Ocurrido un error al momento de Enviar el Correo" + Environment.NewLine;
                strLog += "***********" + Environment.NewLine;
                strLog += ex.Message + Environment.NewLine;
                strLog += ex.StackTrace + Environment.NewLine;
                strLog += "***********" + Environment.NewLine;
                return false;
            }
            finally
            {

                SmtpMail.Dispose();
                MyMail.Dispose();
                if (contents != null) contents.Dispose();
            }

        }
        public bool Enviar(string bodyText, string to)
        {
            string[] adjuntos = { };
            return Enviar(bodyText, to, adjuntos);
        }
        public bool Enviar(string bodyText, string to, string codigoQr)
        {
            string[] adjuntos = { };
            return Enviar(bodyText, to, adjuntos, codigoQr);
        }

    }
}
