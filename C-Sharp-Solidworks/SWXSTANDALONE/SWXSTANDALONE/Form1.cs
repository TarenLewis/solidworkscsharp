using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SWXSTANDALONE
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SldWorks swApp;

        private async void button1_Click(object sender, EventArgs e)
        {
            
                swApp = await SolidworksSingle.getApplicationAsync();
                Console.WriteLine("Opening SolidWorks...");
        }
    }
}
