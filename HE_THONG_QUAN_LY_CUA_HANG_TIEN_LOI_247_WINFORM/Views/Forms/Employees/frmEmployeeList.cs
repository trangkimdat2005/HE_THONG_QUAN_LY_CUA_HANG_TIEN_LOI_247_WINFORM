using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Employees
{
    public partial class frmEmployeeList : Form
    {
        private readonly IQuanLyServices quanLyServices;
        public frmEmployeeList()
        {
            InitializeComponent();
            quanLyServices=new QuanLyServices();
            DisplayCustomers(quanLyServices.GetList<NhanVien>());
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var f = new frmEmployees(Services.FormMode.Add))
            {
                var result = f.ShowDialog();

                if (result == DialogResult.OK)
                {
                    DisplayCustomers(quanLyServices.GetList<NhanVien>());
                }
            }

        }
        private void DisplayCustomers(List<NhanVien> xs)
        {
            dgvEmployees.Rows.Clear();

            foreach (var x in xs)
            {
                dgvEmployees.Rows.Add(
                    x.id,
                    x.hoTen,
                    x.gioiTinh?"Nam":"Nữ",
                    x.soDienThoai,
                    x.email,
                    x.chucVu,
                    x.trangThai
                );
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string id = dgvEmployees.CurrentRow.Cells["colId"].Value.ToString();

            var f = new frmEmployees(FormMode.Edit, id);
            if (f.ShowDialog() == DialogResult.OK)
                DisplayCustomers(quanLyServices.GetList<NhanVien>());

        }

        private void pnlTop_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
