using System;
using System.Data;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            lbTemp.Text = "";
        }

        bool isClickedCalc = false;  // xác nhận đã nhấn phép tính +, - , *, /
        bool calculated = false;     // xác nhận đã nhấn dấu bằng "="

        private void btnNum_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            // Xóa text của lbTemp sau khi tính toán xong
            if (calculated)
            {
                lbTemp.Text = "";
                calculated = false;
            }

            if (lbResult.Text == "0" || isClickedCalc)
            {
                lbResult.Text = button.Text;
                isClickedCalc = false;
            }
            else
            {
                lbResult.Text = lbResult.Text + button.Text;
            }
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (calculated)
            {
                lbTemp.Text = "";
                calculated = false;
            }

            if (button.Text != "=")
            {
                lbTemp.Text += lbResult.Text + " " + button.Text + " ";
                isClickedCalc = true;
            }
            else 
            {
                lbTemp.Text += lbResult.Text;

                // Chuyển biểu thức từ string sang biểu thức tính toán được
                DataTable dt = new DataTable();
                var v = dt.Compute(lbTemp.Text, "");
                lbTemp.Text += " " + button.Text;
                lbResult.Text = v.ToString();

                isClickedCalc = true;
                calculated = true;
            }
        }

        private void btnNegate_Click(object sender, EventArgs e)
        {
            if (calculated)
            {
                lbTemp.Text = "";
                calculated = false;
            }

            double temp = Convert.ToDouble(lbResult.Text);
            temp = -temp;
            lbResult.Text = temp + "";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lbTemp.Text = "";
            lbResult.Text = "0";
        }

        private void btnBackspace_Click(object sender, EventArgs e)
        {
            // Chỉ xóa khi chưa tính toán (chưa nhấn dấu "=")
            if (!calculated)   
            {
                // Cắt bỏ phần tử cuối, giữ lại phần phía trước
                int length = lbResult.Text.Length;
                lbResult.Text = lbResult.Text.Substring(0, length - 1);
            }
            // xóa phần lưu biểu thức khi đã tính toán, giữ nguyên kq hiển thị
            else
            {
                lbTemp.Text = "";
            }

            // Khi xóa hết số, gán giá trị hiển thị lại bằng 0
            if (lbResult.Text == "")
            {
                lbResult.Text = "0";
            }
        }
    }
}
