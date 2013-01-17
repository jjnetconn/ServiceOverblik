using System;
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

        public ServiceAdmin()
        {
            InitializeComponent();
            mainApp = new ServiceManager();
            fillComboBox();
        }

        private void fillComboBox()
        {
            comboBox1.Items.Clear();
            comboBox1.Text = "";

            foreach (salesreps srp in mainApp.getSalesReps())
            {
                this.comboBox1.Items.Add(srp.init);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}
