using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SbsSW.SwiPlCs; //thu vien ket noi Prolog voi C#
using NUnit.Framework;
using Load_Result;
using System.Windows.Forms;

namespace SwiplCsharp
{
    public partial class Form1 : Form
    {
        Load_Result.LoadResult connect;
        public bool temp_hide= false;
        public Form1()
        {
            connect = new LoadResult();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Environment.SetEnvironmentVariable("SWI_HOME_DIR", @"the_PATH_to_boot64.prc");
            // C:\\Program Files (x86)\\swipl la duong dan toi boot32.prc
            // Neu ban dang su dung W32bit duong dan se la C:\\Program Files\\swipl
            Environment.SetEnvironmentVariable("SWI_HOME_DIR", @"C:\\Program Files (x86)\\swipl");
            Environment.SetEnvironmentVariable("Path", @"C:\\Program Files (x86)\\swipl");
            Environment.SetEnvironmentVariable("Path", @"C:\\Program Files (x86)\\swipl\\bin");
        }

        private void bt_load_Click(object sender, EventArgs e)//mode1
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Prolog file|*.pl";
            op.ShowDialog();
            String FilePath = op.FileName;
            connect.LoadFile(FilePath);
            MessageBox.Show("Load file success !");
            this.bt_query.Enabled = true;
        }

        private void bt_query_Click(object sender, EventArgs e)//mode1
        {
            if (this.txtb_tv.Text != null)
            {
                String s = connect.Result(this.txtb_tv.Text);
                this.txtb_kq.Text = s;
            }
            else
            {
                MessageBox.Show("Please enter query !");
            }
        }

        private void bt_ok_1_Click(object sender, EventArgs e) //button OK prolog 
        {
            this.temp_hide = true;
            if(temp_hide == true)
            {
                groupBox_query.Enabled=true;
            }
        }
        private void button3_Click(object sender, EventArgs e) //button exit
        {
            DialogResult thongbao;
            thongbao = (MessageBox.Show("See you later!", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Information));
            if (thongbao == DialogResult.Yes)
                this.Close();
        }

        private void button2_Click(object sender, EventArgs e) //button next
        {
            if (tabControl1.SelectedTab.Text == "DCG mode")
            {
                tabControl1.SelectedTab = tabPage2;
            }
            else if (tabControl1.SelectedTab.Text == "DCG List mode")
            {
                tabControl1.SelectedTab = tabPage3;
            }
            else if (tabControl1.SelectedTab.Text == "Data")
            {
                tabControl1.SelectedTab = tabPage4;
            }
        }

        private void button1_Click(object sender, EventArgs e) //button back
        {
            if (tabControl1.SelectedTab.Text == "About")
            {
                tabControl1.SelectedTab = tabPage3;
            }
            else if (tabControl1.SelectedTab.Text == "Data")
            {
                tabControl1.SelectedTab = tabPage2;
            }
            else if (tabControl1.SelectedTab.Text == "DCG List mode")
            {
                tabControl1.SelectedTab = tabPage1;
            }
        }


    }
}
