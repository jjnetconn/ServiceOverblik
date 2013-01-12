using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;

namespace ServiceOverblik
{
    class EmailSender
    {
        public bool sendToInvoice(string customerName, double servicePrice, int startFee, int serviceNo, string serviceType, int servicePeriod, string salesrep)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient(Properties.Settings.Default.smptServer);

                mail.From = new MailAddress(Properties.Settings.Default.fraEmail);
                mail.To.Add(Properties.Settings.Default.fakturaEmail);
                if (Properties.Settings.Default.isMailTest)
                {
                    mail.Subject = "Test Mail! - Serviceaftale til fakturering";
                }
                else
                {
                    mail.Subject = "Serviceaftale til fakturering";
                }

                StringBuilder bodyTxt = new StringBuilder();
                bodyTxt.AppendFormat("Hej, {0} har valgt at tegne serviceaftale.\n", customerName);
                bodyTxt.AppendLine("");
                bodyTxt.AppendFormat("I den forbindelse skal kunden faktureres {0} DKr inkl. moms\n", (servicePrice + startFee)*Properties.Settings.Default.salesTax);
                bodyTxt.AppendLine("");
                bodyTxt.AppendFormat("Betalingen skal ske med reference nr.: {0}\n", serviceNo);
                bodyTxt.AppendLine("");
                bodyTxt.AppendFormat("Aftalen gælder for serviceaftalen: {0} med en løbetid på {1} måender\n", serviceType, servicePeriod);
                bodyTxt.AppendLine("");
                bodyTxt.AppendFormat("Dette er en autogeneret mail, aftalen af solgt af {0}", salesrep);

                mail.Body = bodyTxt.ToString();
                smtpServer.Port = Properties.Settings.Default.smtpPort;
                smtpServer.Credentials = new System.Net.NetworkCredential(Properties.Settings.Default.smptUser, Properties.Settings.Default.smtpPass);
                smtpServer.EnableSsl = Properties.Settings.Default.smtpSSL;
                smtpServer.Send(mail);
                MessageBox.Show("Sendt til fakturering", "Mail sendt", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            Cursor.Current = Cursors.Default;

            return true;
        }

        public bool sendToCustomer(string customerEmail, int serviceNo, string customerName, string salesRep, string salesRepName, string salesRepPhone)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient(Properties.Settings.Default.smptServer);

                mail.From = new MailAddress(salesRep + "@solcellespecialisten.dk");
                mail.To.Add(customerEmail);
                if (Properties.Settings.Default.isMailTest)
                {
                    mail.Subject = "Test Mail! - Solcellespecialisten - Serviceaftale";
                }
                else
                {
                    mail.Subject = "Solcellespecialisten - Serviceaftale";
                }

                StringBuilder bodyTxt = new StringBuilder();
                bodyTxt.AppendFormat("Hej, {0},\n", customerName);
                bodyTxt.AppendLine("");
                bodyTxt.AppendFormat("Vedhæftet denne mail er:");
                bodyTxt.AppendLine("");
                bodyTxt.AppendLine(" -Dit service certifikat\n -Betingelser vedr. serviceaftale\n");
                bodyTxt.AppendLine("");
                bodyTxt.AppendLine("For at melde fejl på dit anlæg, kan vores serviceafdeling kontaktes på: +45 2043 9925");
                bodyTxt.AppendLine("eller via e-mail på: support@solcellespecialisten.dk");
                bodyTxt.AppendLine("");
                bodyTxt.AppendLine("Venlig hilsen.");
                bodyTxt.AppendLine("Solcellespecialisten A/S");
                bodyTxt.AppendFormat("{0}", salesRepName);

                mail.Body = bodyTxt.ToString();
                StringBuilder serviceContract = new StringBuilder();
                serviceContract.AppendFormat("serviceKontrakt_{0}.pdf", serviceNo);

                //System.Net.Mail.Attachment attachment1 = new System.Net.Mail.Attachment(@"tmp\" + serviceContract.ToString()});
                System.Net.Mail.Attachment attachment2 = new System.Net.Mail.Attachment(@"PDF\betingelser_vedr_serviceaftale.pdf");
                
                mail.Attachments.Add(attachment2);
                mail.Attachments.Add(attachment2);

                smtpServer.Port = Properties.Settings.Default.smtpPort;
                smtpServer.Credentials = new System.Net.NetworkCredential(Properties.Settings.Default.smptUser, Properties.Settings.Default.smtpPass);
                smtpServer.EnableSsl = Properties.Settings.Default.smtpSSL;
                smtpServer.Send(mail);
                MessageBox.Show("Serviceaftale sendt til kunden", "Mail sendt", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            Cursor.Current = Cursors.Default;

            return true;
        }
    }
}
