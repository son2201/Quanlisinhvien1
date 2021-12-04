using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quanlisinhvien
{
    public partial class FormAdd_Insert_delete : Form
    {
        String flag;
        DataTable dataSV;
        int index;
        public FormAdd_Insert_delete()
        {
            InitializeComponent();
        }
        public DataTable createTable() {
            DataTable data = new DataTable();
            data.Columns.Add("Masv");
            data.Columns.Add("HoTen");
            data.Columns.Add("Diem");
            data.Columns.Add("Lop");
            return data;
        }
        public void LockControl() {
            btnInsert.Enabled = true;
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
            btnSave.Enabled = false;
            btnClose.Enabled = false;

            txtMasv.ReadOnly = true;
            txtHoTen.ReadOnly = true;
            txtDiem.ReadOnly = true;
            txtLop.ReadOnly = true;

            btnInsert.Focus();
        }
        public void unlockControl() {
            btnInsert.Enabled = false;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = true;
            btnClose.Enabled = true;

            txtMasv.ReadOnly = false;
            txtHoTen.ReadOnly = false;
            txtDiem.ReadOnly = false;
            txtLop.ReadOnly = false;
            txtMasv.Focus();
        }

        private void FormAdd_Insert_delete_Load(object sender, EventArgs e)
        {
            LockControl();
            dataSV = createTable();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            unlockControl();
            flag = "add";
            txtMasv.Text = "";
            txtHoTen.Text = "";
            txtDiem.Text = "";
            txtLop.Text = "";
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            unlockControl();
            flag = "edit";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (flag == "add")
            {
                if (checkData())
                {
                    dataSV.Rows.Add(txtMasv.Text, txtHoTen.Text, txtDiem.Text, txtLop.Text);
                    dataGridSinhvien.DataSource = dataSV;
                    dataGridSinhvien.RefreshEdit();
                }
            }
            else if (flag == "edit")
            {
                dataSV.Rows[index][0] = txtMasv.Text;
                dataSV.Rows[index][1] = txtHoTen.Text;
                dataSV.Rows[index][2] = txtDiem.Text;
                dataSV.Rows[index][3] = txtLop.Text;
                dataGridSinhvien.DataSource = dataSV;
                dataGridSinhvien.RefreshEdit();
            }
            LockControl();
        }
        public bool checkData() { 
            if(string.IsNullOrWhiteSpace(txtMasv.Text))
            {
                MessageBox.Show("Bạn chưa nhập mã số sinh viên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMasv.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Bạn chưa nhập Họ Tên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtHoTen.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtDiem.Text))
            {
                MessageBox.Show("Bạn chưa nhập Điểm", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiem.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtLop.Text))
            {
                MessageBox.Show("Bạn chưa nhập Lớp", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLop.Focus();
                return false;
            }
            return true;
        }

        private void dataGridSinhvien_SelectionChanged(object sender, EventArgs e)
        {
            index = dataGridSinhvien.CurrentCell.RowIndex;
            DataTable data = (DataTable)dataGridSinhvien.DataSource;
            if(data.Rows.Count>0|| data.Rows != null)
            {
                txtMasv.Text = dataGridSinhvien.Rows[index].Cells[0].Value.ToString();
                txtHoTen.Text= dataGridSinhvien.Rows[index].Cells[1].Value.ToString();
                txtDiem.Text = dataGridSinhvien.Rows[index].Cells[2].Value.ToString();
                txtLop.Text= dataGridSinhvien.Rows[index].Cells[3].Value.ToString();
            }    
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn có muốn xóa sinh viên này ","Cảnh Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                dataSV.Rows.RemoveAt(index);
                dataGridSinhvien.DataSource = dataSV;
                dataGridSinhvien.RefreshEdit();
            }
        }
    }
}
