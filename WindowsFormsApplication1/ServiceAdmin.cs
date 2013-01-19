using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ServiceOverblik
{
    public partial class ServiceAdmin : Form
    {
        ServiceManager mainApp;
        String Path;
        
        public ServiceAdmin()
        {
            InitializeComponent();
            mainApp = new ServiceManager();
            fillComboBox();
            Path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

        }

        private void fillComboBox()
        {
            this.comboBox1.DataSource = null;
            this.comboBox1.DisplayMember = "Text";
            this.comboBox1.ValueMember = "Value";
            
            ArrayList items = new ArrayList();
            Queue myQueue = new Queue();

            foreach (servicetypes s in mainApp.getServiceInfo())
                {
                   myQueue.Enqueue( new { Text = s.sname, Value = s.tid });  
                }
                items.AddRange(myQueue);
            this.comboBox1.DataSource = items;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog findLogo = new OpenFileDialog();
            findLogo.InitialDirectory = Path + "\\images\\";
            DialogResult result = findLogo.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox3.Text = findLogo.SafeFileName;
                if (!System.IO.File.Exists(findLogo.InitialDirectory + findLogo.SafeFileName))
                {
                    try
                    {
                        System.IO.File.Copy(findLogo.FileName, findLogo.InitialDirectory + findLogo.SafeFileName);
                    }
                    catch (Exception ex)
                    {
                        mainApp.eventlog.writeError(ex.Message, ex.StackTrace);
                    }
                }
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            servicetypes s = mainApp.getSelectedService((int)this.comboBox1.SelectedValue);
            textBox1.Text = s.sname;
            textBox2.Text = s.price.ToString();
            textBox5.Text = s.period.ToString();
            textBox4.Text = s.startupfee.ToString();
            textBox3.Text = s.servicelogo;
            richTextBox1.Text = s.details;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Validate input
            double price = 0.0;
            double startupfee = 0.0;
            int period = 0;
            string logoPath = "";

            if (Double.TryParse(textBox2.Text, out price)) { }
            if (Int32.TryParse(textBox5.Text, out period)) { }
            if (Double.TryParse(textBox4.Text, out startupfee)) { }
            if (richTextBox1.Text.Equals(null))
            {
                richTextBox1.Text = " ";
            }


            using (servicebaseEntities sdb = new servicebaseEntities())
            {
                try
                {
                    var query = (from c in sdb.servicetypes
                                 where c.tid == (int)this.comboBox1.SelectedValue
                                 select c).FirstOrDefault();
                    
                    query.sname = textBox1.Text;
                    query.servicelogo = logoPath;
                    query.details = richTextBox1.Text;
                    query.price = price;
                    query.period = period;
                    query.startupfee = startupfee;

                    sdb.SaveChanges();

                    MessageBox.Show("Serviceaftale opdateret!", "Opdateret", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    mainApp.eventlog.writeError(ex.Message, ex.StackTrace);
                    MessageBox.Show("Fejl i data", "Fej!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    sdb.Dispose();
                }

                resetInputs();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Validate input
            double price = 0.0;
            double startupfee = 0.0;
            int period = 0;
            string logoPath = "";
            
            if (Double.TryParse(textBox2.Text, out price)) { }
            if (Int32.TryParse(textBox5.Text, out period)) { }
            if (Double.TryParse(textBox4.Text, out startupfee)) { }
            if (richTextBox1.Text.Equals(null))
            {
                richTextBox1.Text = " ";
            }

            logoPath = "images\\" + textBox3.Text;

            using (servicebaseEntities sdb = new servicebaseEntities())
            {
                try
                {
                    servicetypes s = new servicetypes();

                    s.sname = textBox1.Text;
                    s.servicelogo = logoPath;
                    s.details = richTextBox1.Text;
                    s.price = price;
                    s.period = period;
                    s.startupfee = startupfee;

                    sdb.servicetypes.Add(s);
                    sdb.SaveChanges();

                }
                catch (Exception ex)
                {
                    mainApp.eventlog.writeError(ex.Message, ex.StackTrace);
                }
                finally
                {
                    sdb.Dispose();
                }

                MessageBox.Show("Serviceaftale oprettet!", "Oprettet", MessageBoxButtons.OK);
                resetInputs();
            }
        }
        private void resetInputs()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            richTextBox1.Clear();
            fillComboBox();
        }

        private void lukToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (servicebaseEntities sdb = new servicebaseEntities())
            {
                try
                {
                    servicetypes sto = sdb.servicetypes.First(p => p.tid == (int)this.comboBox1.SelectedValue);
                    sdb.servicetypes.Remove(sto);
                    sdb.SaveChanges();
                    MessageBox.Show("Serviceaftale slettet!", "Slettet", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    mainApp.eventlog.writeError(ex.Message, ex.StackTrace);
                }
                finally
                {
                    sdb.Dispose();
                }
            }
            resetInputs();
            
        }
    }
}
