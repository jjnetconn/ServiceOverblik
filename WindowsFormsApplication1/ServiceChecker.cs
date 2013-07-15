using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Threading;

namespace ServiceOverblik
{
    class ServiceChecker
    {
        ServiceManager mainApp;
        EmailSender sendMail;
        Thread workerThread;
        

        public ServiceChecker()
        {
            mainApp = new ServiceManager();
            sendMail = new EmailSender();
            workerThread = new Thread(CheckServices);
            workerThread.Start();
            /*while (!workerThread.IsAlive)
            {

            }*/
        }

        public void CheckServices()
        {
            if (DateTime.Now.Day >= 1 && DateTime.Now.Day < 5)
            {
                SalesReportGenerator srp = new SalesReportGenerator();
            }
            List<customers> expiring = new List<customers>(servicesNearExpire());
            callSendMail(expiring);
        }

        private List<customers> servicesNearExpire()
        {
            List<customers> tmp1 = new List<customers>();
            DateTime expireLimit = DateTime.Now.AddMonths(2);
            using (servicebaseEntities sdb = new servicebaseEntities())
            {
                try
                {
                    var query = from c in sdb.customers
                                where c.servicecontracts.enddate.Month == expireLimit.Month && c.servicecontracts.enddate.Year == expireLimit.Year && c.servicecontracts.renewmailsendt == false
                                select c;
                    tmp1 = query.ToList();
                }
                catch(Exception ex)
                {
                    mainApp.eventlog.writeError(ex.Message, ex.StackTrace);
                }
                finally
                {
                    sdb.Dispose();
                }
                return tmp1;
            }
        }

        private void callSendMail(List<customers> expiring)
        {
            Console.WriteLine("DEBUG POINT");
            sendMail.sendToSalesRep(expiring);
        }
    }
}
