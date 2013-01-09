using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.Entity;


namespace ServiceOverblik
{
    class ServiceManager : ExcelLoad
    {
        private servicebaseEntities serviceDB = new servicebaseEntities();

        public ServiceManager()
        {

        }

        public List<customers> searchCustomerByName(string searchFilter)
        {
            List<customers> tmp1 = new List<customers>();
            using (servicebaseEntities sdb = new servicebaseEntities())
            {
                try
                {
                    var query = from c in sdb.customers
                                where c.cname.Contains(searchFilter)
                                select c;
                    tmp1 = query.ToList();
                }
                catch
                {
                }
                finally
                {
                    sdb.Dispose();
                }
                return tmp1;
            }
        }
        public List<customers> searchCustomerByStreet(string searchFilter)
        {
            List<customers> tmp2 = new List<customers>();
            using (servicebaseEntities sdb = new servicebaseEntities())
            {
                try
                {   var query = from c in serviceDB.customers
                             where c.street.Contains(searchFilter)
                                select c;
                     tmp2 = query.ToList();
                }
                catch
                {
                }
                finally
                {
                    sdb.Dispose();
                }
                return tmp2;
            }
        }
        public List<customers> searchCustomerByCity(string searchFilter)
        {
            List<customers> tmp3 = new List<customers>();
            using (servicebaseEntities sdb = new servicebaseEntities())
            {
                try
                {
                    var query = from c in serviceDB.customers
                                where c.city.Contains(searchFilter)
                                select c;
                    tmp3 = query.ToList();
                }
                catch
                {
                }
                finally
                {
                    sdb.Dispose();
                }
                return tmp3;
            }
        }
        public List<customers> searchCustomerByPostcode(string searchFilter)
        {
            List<customers> tmp4 = new List<customers>();
            using (servicebaseEntities sdb = new servicebaseEntities())
            {
                try
                {
                    int tmp = Int32.Parse(searchFilter);
                    var query = from c in serviceDB.customers
                                where c.postcode == tmp
                                select c;
                    tmp4 = query.ToList();
                }
                catch
                {
                }
                finally
                {
                    sdb.Dispose();
                }
                return tmp4;
            }
        }
        public List<customers> searchCustomerByHasService(string searchFilter)
        {
            List<customers> tmp5 = new List<customers>();
            using (servicebaseEntities sdb = new servicebaseEntities())
            {
                try
                {
                    bool tmp = Boolean.Parse(searchFilter);
                    var query = from c in serviceDB.customers
                                where c.hasservice == tmp
                                select c;
                    tmp5 = query.ToList();
                }
                catch
                {
                }
                finally
                {
                    sdb.Dispose();
                }
                return tmp5;
            }
        }
        public List<customers> searchCustomerByServiceDate(string searchFilter)
        {
            List<customers> tmp6 = new List<customers>();
            using (servicebaseEntities sdb = new servicebaseEntities())
            {
                try
                {
                    bool tmp = Boolean.Parse(searchFilter);
                    var query = from c in serviceDB.customers
                                where c.hasservice == tmp
                                select c;
                    tmp6 = query.ToList();
                }
                catch
                {
                }
                finally
                {
                    sdb.Dispose();
                }
            return tmp6;
        }
        }

        //tilføj searchCustomerBySalesRep(string searchFilter)

        public List<servicetypes> getServicetypes()
        {
            List<servicetypes> rtnServicetypes = new List<servicetypes>();

            using (servicebaseEntities stb = new servicebaseEntities())
            {
                try
                {
                    var query = (from s in stb.servicetypes
                                select s);
                    rtnServicetypes = query.ToList();
                }
                catch
                {

                }

                finally
                {
                    stb.Dispose();
                }

                return rtnServicetypes;
            }
        }

        public List<salesreps> getSalesReps()
        {
            List<salesreps> rtnSalesReps = new List<salesreps>();

            using (servicebaseEntities stb = new servicebaseEntities())
            {
                try
                {
                    var query = (from s in stb.salesreps
                                 select s);
                    rtnSalesReps = query.ToList();
                }
                catch
                {

                }

                finally
                {
                    stb.Dispose();
                }

                return rtnSalesReps;
            }
        }

        public List<paneltypes> getPaneltypes()
        {
            List<paneltypes> rtnPaneltypes = new List<paneltypes>();

            using (servicebaseEntities stb = new servicebaseEntities())
            {
                try
                {
                    var query = (from s in stb.paneltypes
                                 select s);
                    rtnPaneltypes = query.ToList();
                }
                catch
                {

                }

                finally
                {
                    stb.Dispose();
                }

                return rtnPaneltypes;
            }
        }

        public List<prodcat> listInverters()
        {
            int tmp = Int32.Parse(Properties.Settings.Default.InverterProdGrp);
            List<prodcat> rtnInverters = new List<prodcat>();
            using (lagermanEntities ldb = new lagermanEntities())
            {
                try
                {
                    var query = from c in ldb.prodcat
                                where c.group_id == tmp
                                select c;
                    rtnInverters = query.ToList();
                }
                catch
                {

                }
                finally
                {
                    ldb.Dispose();
                }

                return rtnInverters;
            }
        }

        public int customerInverter(List<prodcat> inList, string customerInverter)
        {
            //servicebaseEntities serviceDB = new servicebaseEntities();
            int i = 0;
            foreach (prodcat prod in inList)
            {
                if (prod.cname_prod.Contains(customerInverter))
                { break; }

                i++;
            }

            //serviceDB.Dispose();

            return i;
        }

        public int createServiceContract(object[] serviceData)
        {
            int lastSID = new int();
            int serviceContractID = new int();
            int serviceID = (Int32)serviceData[0];
            servicetypes selectedService = null; 
            
                //Get numbers of servicecontract in DB
                using (servicebaseEntities lc = new servicebaseEntities())
                {
                    var query = (from l in lc.servicecontracts
                                     select l.sid).Count();
                    lastSID = query;

                    var query1 = (from st in lc.servicetypes
                                 where st.tid == serviceID
                                 select st).SingleOrDefault();
                    selectedService = query1;

                //calc enddate on selected service
                DateTime startDate = (DateTime)serviceData[2];
                DateTime endDate = startDate.AddMonths(selectedService.period);
                DateTime timeStamp = DateTime.Now.Date;
                serviceContractID = Convert.ToInt32(DateTime.Now.Year.ToString() + DateTime.Now.DayOfYear.ToString() + lastSID.ToString());
       
                    servicecontracts sc = new servicecontracts();
                    sc.sid = serviceContractID;
                    sc.servicetype = (Int32)serviceData[0];
                    sc.soldby = (String)serviceData[1];
                    sc.startdate = startDate.Date;
                    sc.enddate = endDate.Date;
                    sc.timestamp = timeStamp;
                    lc.servicecontracts.Add(sc);
                    lc.SaveChanges();
                    lc.Dispose();
                }
                
                return serviceContractID;
        }
        
        public bool createCustomer(object[] newData)
        {
            int serviceNo = 9999;
            bool hasService = false;


                //prepare inputdata for ServiceContract creation
                object[] serviceInput = new object[3];
                serviceInput[0] = newData[9];
                serviceInput[1] = newData[13];
                serviceInput[2] = newData[10];

                serviceNo = createServiceContract(serviceInput);

                hasService = true;
            
            using (servicebaseEntities cc = new servicebaseEntities())
            {
                try
                {
                    customers c = new customers();
                    c.cname = newData[0].ToString();
                    c.street = newData[1].ToString();
                    c.city = newData[2].ToString();
                    c.postcode = (int)newData[3];
                    c.email = newData[4].ToString();
                    c.phone = newData[5].ToString();
                    c.panelcount = newData[6].ToString();
                    c.color = newData[7].ToString();
                    c.inverter = newData[8].ToString();
                    c.paneltype = (String)newData[14];
                    c.serviceno = serviceNo;
                    c.hasservice = hasService;

                    cc.customers.Add(c);

                }
                catch
                {

                }
                cc.SaveChanges();
            }
            return true;
        }

        public bool updateCustomer(int userId, object[] newData, bool serviceChanged)
        {
            //int serviceNo = 0;
            //bool hasService = false;

            /*
            if (serviceChanged)
            {
                //prepare inputdata for ServiceContract creation
                object[] serviceInput = new object[3];
                serviceInput[0] = newData[9];
                serviceInput[1] = newData[13];
                serviceInput[2] = newData[10];

                serviceNo = createServiceContract(serviceInput);

                hasService = true;

                //serviceNo++;

            }
            */

            using (servicebaseEntities sdb = new servicebaseEntities())
            {
                try
                {
                    var query = (from c in sdb.customers
                                 where c.uId == userId
                                 select c).FirstOrDefault();

                    query.cname = newData[0].ToString();
                    query.email = newData[4].ToString();
                    query.phone = newData[5].ToString();
                    query.panelcount = newData[6].ToString();
                    query.inverter = newData[8].ToString();
                    //query.serviceno = serviceNo;
                    //query.hasservice = hasService;
                    //query.servicecontracts.servicetype = Int32.Parse(newData[5].ToString()) + 1;
                    //query.servicecontracts.soldby = newData[7].ToString();

                    sdb.SaveChanges();
                }
                catch
                {

                }
                finally
                {
                    sdb.Dispose();
                }

                return true;

            }

        }

        public String loadCustomerHistory(int userId)
        {
            String ServiceBlog = null;

            using (servicebaseEntities sdb = new servicebaseEntities())
            {
                try
                {
                    var query = (from c in sdb.customers
                                 where c.uId == userId
                                 select c).FirstOrDefault();
                    ServiceBlog = query.serviceblog;
                }
                finally
                {
                    sdb.Dispose();
                }
            }
            return ServiceBlog;
        }

        public void addCustomerHistory(string[] newHistory, int userId)
        {
            StringBuilder strHistory = new StringBuilder();
            foreach (string str in newHistory)
            {
                strHistory.Append(str + "%NL");
            }

            using (servicebaseEntities sdb = new servicebaseEntities())
            {
                try
                {
                    var query = (from c in sdb.customers
                                 where c.uId == userId
                                 select c).FirstOrDefault();

                    query.serviceblog = query.serviceblog + strHistory.ToString();

                    sdb.SaveChanges();
                }
                catch
                {

                }
                finally
                {
                    sdb.Dispose();
                }
            }
        }

        public int getServiceContractId(int userId)
        {
            int contractId = 0;
            using (servicebaseEntities sdb = new servicebaseEntities())
            {
                try
                {
                    var query = (from c in sdb.customers
                                 where c.uId == userId
                                 select c.serviceno).FirstOrDefault();

                   contractId =  query.GetValueOrDefault();

                }
                finally
                {
                    sdb.Dispose();
                }
            }
            return contractId;
        }

        public void startServiceCase(int userId)
        {
            using (servicebaseEntities sdb = new servicebaseEntities())
            {
                try
                {
                    var query = (from c in sdb.customers
                                 where c.uId == userId
                                 select c).FirstOrDefault();

                    query.servicecontracts.activeServiceCase = true;

                    sdb.SaveChanges();
                }
                finally
                {
                    sdb.Dispose();
                }
            }
        }

        public void endServiceCase(int userId)
        {
            using (servicebaseEntities sdb = new servicebaseEntities())
            {
                try
                {
                    var query = (from c in sdb.customers
                                 where c.uId == userId
                                 select c).FirstOrDefault();

                    query.servicecontracts.activeServiceCase = false;

                    sdb.SaveChanges();
                }
                finally
                {
                    sdb.Dispose();
                }
            }
        }

        public bool hasActiveServiceCase(int userId)
        {
            bool hasActiveService = false;
            using (servicebaseEntities sdb = new servicebaseEntities())
            {
                try
                {
                    var query = (from c in sdb.customers
                                 where c.uId == userId
                                 select c).FirstOrDefault();
                    hasActiveService = query.servicecontracts.activeServiceCase;
                }
                finally
                {
                    sdb.Dispose();
                }
            }
            return hasActiveService;
        }

        public bool isServiceInvoicePaid(int userId)
        {
            bool isServicePaid = false;
            using (servicebaseEntities sdb = new servicebaseEntities())
            {
                try
                {
                    var query = (from c in sdb.customers
                                 where c.uId == userId
                                 select c).FirstOrDefault();
                    isServicePaid = query.servicecontracts.invoicePaid;
                }
                finally
                {
                    sdb.Dispose();
                }
            }
            return isServicePaid;
        }

        public bool isServiceContractActive(int userId)
        {
            bool isContractActive = false;
            using (servicebaseEntities sdb = new servicebaseEntities())
            {
                try
                {
                    var query = (from c in sdb.customers
                                 where c.uId == userId
                                 select c).FirstOrDefault();
                    isContractActive = query.hasservice;
                }
                finally
                {
                    sdb.Dispose();
                }
            }
            return isContractActive;
        }
    }
}
