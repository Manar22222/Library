using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using DGVPrinterHelper;
using System.Windows.Forms;

namespace test
{
    public partial class Books : Form
    {
        public Books()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=MyLipraryDB;Integrated Security=TrueData Source=.;Initial Catalog=MyLipraryDB;Integrated Security=True;Connect Timeout=30");

        public void Poplate()
        {
            con.Open();
            String query = "Select * from BookTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            bunifuDataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Really Quit?", "Exit", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {

                Application.Exit();

            }
        }


        private void Books_Load(object sender, EventArgs e)
        {
            Poplate();
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {

            if (BookName.Text == "" || Author.Text == "" || Puplisher.Text == "" || Price.Text == "" || Qey.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into BookTbl values('" + BookName.Text + "','" + Author.Text + "','" + Puplisher.Text + "','" + Price.Text + "','" + Qey.Text + "')", con);/*("insert into StudentTbl value("  +Stdid.Text+ ",'"  +StdName.Text+ "','" +StdDep.Text+ "'," +StdSem.SelectedItem.ToString()+ ",'"  +StdPhone.Text+ "')", con);*/
                cmd.ExecuteNonQuery();
                MessageBox.Show("Book Added Successfully");
                con.Close();
                Poplate();
            }
        }

        private void bunifuDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            BookName.Text = bunifuDataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            Author.Text = bunifuDataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            Puplisher.Text = bunifuDataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            Price.Text = bunifuDataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            Qey.Text = bunifuDataGridView1.SelectedRows[0].Cells[4].Value.ToString();

        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            if (BookName.Text == "")
            {
                MessageBox.Show("Enter Book Name");
            }
            else
            {
                con.Open();
                String query = "delete from BookTbl Where BookName ='" + BookName.Text + "';";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Book Successfully deleted");
                con.Close();
                Poplate();
            }
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            if (BookName.Text == "" || Author.Text == "" || Puplisher.Text == "" || Price.Text == "" || Qey.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                con.Open();
                String query = "update  BookTbl Set Author ='" + Author.Text + "',Puplisher='" + Puplisher.Text + "',Price='" + Price.Text + "',Qey='" + Qey.Text + "'Where BookName='" + BookName.Text + "';";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Book Successfully Updated");
                con.Close();
                Poplate();
            }
        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main main = new Main();
            main.Show();
        }

        private void bunifuButton10_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            DGVPrinter printer = new DGVPrinter();
            printer.Title = ($"  تقرير عدد الكتب الموجود  {BookName.Text} ");
            printer.SubTitle = ($"الكمية   {BookName.Text}   السعر   {Qey.Text}  ");
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.Footer = "تقرير عام للكتاب ";
            printer.FooterSpacing = 15;
            printer.ColumnWidth = DGVPrinter.ColumnWidthSetting.DataWidth;
            printer.TableAlignment = DGVPrinter.Alignment.Center;
            printer.PrintNoDisplay(bunifuDataGridView1);
        }
    }
}
