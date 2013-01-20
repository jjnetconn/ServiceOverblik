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
        public bool sendToInvoice(int serviceID, string customerName, double servicePrice, int startFee, int serviceNo, string serviceType, int servicePeriod, string salesrep, string cAddress, int cPostNo, string cCity, string cPhone, string cMail)
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
                bodyTxt.AppendLine("Faktura info:");
                bodyTxt.AppendFormat("Navn: {0}\n", customerName);
                bodyTxt.AppendFormat("Adresse: {0}\nPost nr.: {1}\nBy: {2}\n", cAddress, cPostNo, cCity);
                bodyTxt.AppendFormat("Telefon: {0}\n", cPhone);
                bodyTxt.AppendFormat("E-Mail: {0}\n", cMail);
                bodyTxt.AppendLine("");
                bodyTxt.AppendFormat("I den forbindelse skal kunden faktureres {0} DKr inkl. moms\n", servicePrice);
                bodyTxt.AppendLine("");
                bodyTxt.AppendFormat("Kundes serviceaftale har nr.: {0}\n", serviceNo);
                bodyTxt.AppendLine("");
                bodyTxt.AppendFormat("Aftalen gælder for serviceaftalen: {0} med en løbetid på {1} måneder\n", serviceType, servicePeriod);
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

        public bool sendToCustomer(string customerEmail, int serviceNo, string customerName, string salesRep, string salesRepName, string salesRepPhone, string fileName)
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
                bodyTxt.AppendFormat("Hej {0},\n", customerName);
                bodyTxt.AppendLine("");
                bodyTxt.AppendFormat("Vedhæftet denne mail er:");
                bodyTxt.AppendLine("");
                bodyTxt.AppendLine(" - Din servicekontrakt\n - Betingelser vedr. serviceaftale\n");
                bodyTxt.AppendLine("");
                bodyTxt.AppendLine("Faktura for serviceaftalen sendes automatisk inden for få dage.");
                bodyTxt.AppendLine("Bemærk at indbetaling skal mærkes med faktura nr. som reference.");
                bodyTxt.AppendLine("");
                bodyTxt.AppendLine("For at melde fejl på dit anlæg, kan vores serviceafdeling kontaktes på: +45 2043 9925\n");
                bodyTxt.AppendLine("eller via e-mail på: support@solcellespecialisten.dk");
                bodyTxt.AppendLine("");
                bodyTxt.AppendLine("Venlig hilsen.");
                bodyTxt.AppendLine("Solcellespecialisten A/S");
                bodyTxt.AppendFormat("{0}", salesRepName);
                bodyTxt.AppendLine("");
                bodyTxt.AppendFormat("Tlf.: +45 {0}", salesRepPhone);
                bodyTxt.AppendLine("");
                bodyTxt.AppendFormat("Email: " + salesRep + "@solcellespecialisten.dk");
                mail.Body = bodyTxt.ToString();

                System.Net.Mail.Attachment attachment1 = new System.Net.Mail.Attachment(@fileName);
                System.Net.Mail.Attachment attachment2 = new System.Net.Mail.Attachment(@"PDF\betingelser_vedr_serviceaftale.pdf");
                
                mail.Attachments.Add(attachment1);
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

        public bool sendToSupport(string cName, string cAddress, string cPostNo, string cCity, string cPhone, string cMail, string serviceName, string salesRep)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient(Properties.Settings.Default.smptServer);

                mail.From = new MailAddress(salesRep + "@solcellespecialisten.dk");
                mail.To.Add(Properties.Settings.Default.supportEmail);
                if (Properties.Settings.Default.isMailTest)
                {
                    mail.Subject = "Test Mail! - Supportsag startet";
                }
                else
                {
                    mail.Subject = "Supportsag startet";
                }

                StringBuilder bodyTxt = new StringBuilder();
                bodyTxt.AppendLine("Hej,");
                bodyTxt.AppendLine("");
                bodyTxt.AppendLine("Der er startet en service sag på flg. kunde:");
                bodyTxt.AppendLine("");
                bodyTxt.AppendFormat("Navn: {0}\n", cName);
                bodyTxt.AppendFormat("Adresse: {0}\nPost nr.: {1}\nBy: {2}\n", cAddress, cPostNo, cCity);
                bodyTxt.AppendFormat("Telefon: {0}\n", cPhone);
                bodyTxt.AppendFormat("E-Mail: {0}\n", cMail);
                bodyTxt.AppendLine("");
                bodyTxt.AppendLine("Service aftale:");
                bodyTxt.AppendFormat("{0}\n", serviceName);
                bodyTxt.AppendLine("");
                bodyTxt.AppendLine("Dette er en autogeneret mail.");

                mail.Body = bodyTxt.ToString();
               
                smtpServer.Port = Properties.Settings.Default.smtpPort;
                smtpServer.Credentials = new System.Net.NetworkCredential(Properties.Settings.Default.smptUser, Properties.Settings.Default.smtpPass);
                smtpServer.EnableSsl = Properties.Settings.Default.smtpSSL;
                smtpServer.Send(mail);
                MessageBox.Show("Supportsag oprettet", "Mail sendt", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            Cursor.Current = Cursors.Default;

            return true;
        }

        public bool sendToSalesRep(List<customers> expiring)
        {
            foreach (customers c in expiring)
            {
                ServiceManager mainApp = new ServiceManager();
                servicecontracts currentService = mainApp.getServiceContract((int)c.serviceno);
                try
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient smtpServer = new SmtpClient(Properties.Settings.Default.smptServer);

                    mail.From = new MailAddress("AutoMail@solcellespecialisten.dk");
                    mail.To.Add(currentService.soldby + "@solcellespecialisten.dk");
                    if (Properties.Settings.Default.isMailTest)
                    {
                        mail.Subject = "Test Mail! - Serviceaftale udløber snart!";
                    }
                    else
                    {
                        mail.Subject = "Serviceaftale udløber snart!";
                    }

                    StringBuilder bodyTxt = new StringBuilder();
                    bodyTxt.AppendFormat("Hej,\n");
                    bodyTxt.AppendLine("");
                    bodyTxt.AppendLine("Flg. kundes serviceaftaler udløber om 2 måneder");
                    bodyTxt.AppendLine("");
                    bodyTxt.AppendLine("Kunde info:");
                    bodyTxt.AppendFormat("Navn: {0}\n", c.cname);
                    bodyTxt.AppendFormat("Adresse: {0}\nPost nr.: {1}\nBy: {2}\n", c.street, c.postcode, c.city);
                    bodyTxt.AppendFormat("Telefon: {0}\n", c.phone);
                    bodyTxt.AppendFormat("E-Mail: {0}\n", c.email);
                    bodyTxt.AppendLine("");
                    bodyTxt.AppendFormat("Kundes serviceaftale har nr.: {0}\n", c.serviceno);
                    bodyTxt.AppendLine("");
                    bodyTxt.AppendFormat("Dette er en autogeneret mail");

                    mail.Body = bodyTxt.ToString();
                    smtpServer.Port = Properties.Settings.Default.smtpPort;
                    smtpServer.Credentials = new System.Net.NetworkCredential(Properties.Settings.Default.smptUser, Properties.Settings.Default.smtpPass);
                    smtpServer.EnableSsl = Properties.Settings.Default.smtpSSL;
                    smtpServer.Send(mail);
                    mainApp.setExpireMailSendt((int)c.serviceno);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return true;
        }
    }
}
