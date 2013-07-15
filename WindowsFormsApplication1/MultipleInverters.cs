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
    public partial class MultipleInverters : Form
    {
        private string[] _items;
        public string[] Items
        {
            get { return _items; }
            set { _items = value; }
        }
        private ServiceManager rstate = new ServiceManager();
        
        public MultipleInverters()
        {
            InitializeComponent();
            foreach (prodcat inv in rstate.listInverters())
            {
                mInverterCbx1.Items.Add(inv.cname_prod);
            }
            mInverterCbx1.SelectedIndex = 0;
        }

        private void mInvertersBtn1_Click(object sender, EventArgs e)
        {
            mInvertersLbx1.Items.Add(mInverterCbx1.SelectedItem);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }

        private void mInvertersBtn2_Click(object sender, EventArgs e)
        {
            int listBoxCount = mInvertersLbx1.Items.Count;

            string[] items = new string[listBoxCount];

            for(int i = 0; i < listBoxCount; i++){
                items[i] = mInvertersLbx1.Items[i].ToString();
            }

            Items = items;
            this.DialogResult = DialogResult.OK;
            //this.Close();
        }
    }
}
