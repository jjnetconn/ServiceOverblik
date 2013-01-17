using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Text;
using System.Windows.Forms;

namespace ServiceOverblik
{
    public partial class UserAdmin : Form
    {
        ServiceManager mainApp;
        public UserAdmin()
        {
            InitializeComponent();
            mainApp = new ServiceManager();
            fillComboBox();
        }

        private void fillComboBox()
        {
            uaSalesReps.Items.Clear();
            uaSalesReps.Text = "";

            foreach (salesreps srp in mainApp.getSalesReps())
            {
                this.uaSalesReps.Items.Add(srp.init);
            }
        }

        private void getSalesRep(string salesRepInit)
        {
            using (servicebaseEntities sdb = new servicebaseEntities())
            {
                try
                {
                    var query = (from c in sdb.salesreps
                                 where c.init == salesRepInit
                                 select c).FirstOrDefault();

                    this.textBox1.Text = query.name;
                    this.textBox2.Text = query.email;
                    this.textBox3.Text = query.phone;
                    this.textBox4.Text = query.init;

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    sdb.Dispose();
                }
            }
        }

        private void addSalesRep()
        {
            using (servicebaseEntities sdb = new servicebaseEntities())
            {
                try
                {
                    salesreps c = new salesreps();

                    c.init = textBox4.Text;
                    c.name = textBox1.Text;
                    c.email = textBox2.Text;
                    c.phone = textBox3.Text;

                    sdb.salesreps.Add(c);
                    sdb.SaveChanges();

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    sdb.Dispose();
                }
                fillComboBox();
                MessageBox.Show("Bruger oprettet!", "Oprettet", MessageBoxButtons.OK);
                this.Close();
            }
        }

        private void deleteSalesRep(string salesRepInit)
        {
            using (servicebaseEntities sdb = new servicebaseEntities())
            {
                    try
                    {
                        salesreps sro = sdb.salesreps.First(p => p.init == salesRepInit);
                        sdb.salesreps.Remove(sro);
                        sdb.SaveChanges();

                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        sdb.Dispose();
                    }
                }
            fillComboBox();
            MessageBox.Show("Bruger slettet!", "Slettet", MessageBoxButtons.OK);
            this.Close();
        }

        private void clearTextBox()
        {
            this.textBox1.Text = "";
            this.textBox2.Text = "";
            this.textBox3.Text = "";
            this.textBox4.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            getSalesRep(this.uaSalesReps.SelectedItem.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            addSalesRep();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            deleteSalesRep(this.uaSalesReps.SelectedItem.ToString());
        }

    }
}
