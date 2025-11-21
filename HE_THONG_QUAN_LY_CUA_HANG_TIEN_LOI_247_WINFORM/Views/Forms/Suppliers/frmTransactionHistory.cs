using ClosedXML.Excel;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Suppliers
{
    public partial class frmTransactionHistory : Form
    {
        private readonly IQuanLyServices _services;
        private string _selectedTransactionId;

        public frmTransactionHistory()
        {
            InitializeComponent();
            _services = new QuanLyServices();
            CustomizeInterface();
        }

        private void CustomizeInterface()
        {
            dtpFromDate.Format = DateTimePickerFormat.Custom;
            dtpFromDate.CustomFormat = "dd/MM/yyyy";
            dtpToDate.Format = DateTimePickerFormat.Custom;
            dtpToDate.CustomFormat = "dd/MM/yyyy";

            StyleGrid(dgvTransactions);
            dgvTransactions.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
            dgvTransactions.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvTransactions.DefaultCellStyle.SelectionBackColor = Color.FromArgb(211, 233, 252);
            dgvTransactions.DefaultCellStyle.SelectionForeColor = Color.Black;

            StyleGrid(dgvDetails);
            dgvDetails.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dgvDetails.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvDetails.DefaultCellStyle.SelectionBackColor = Color.FromArgb(236, 240, 241);
            dgvDetails.DefaultCellStyle.SelectionForeColor = Color.Black;
            if (dtpToDate.Value < dtpFromDate.Value)
            {
                dtpToDate.Value = dtpFromDate.Value;
            }
            dtpToDate.MinDate = dtpFromDate.Value;

            dtpFromDate.ValueChanged += (s, e) =>
            {
                dtpToDate.MinDate = DateTimePicker.MinimumDateTime;

                if (dtpFromDate.Value > dtpToDate.Value)
                {
                    dtpToDate.Value = dtpFromDate.Value;
                }

                dtpToDate.MinDate = dtpFromDate.Value;
            };
        }

        private void StyleGrid(DataGridView dgv)
        {
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.FromArgb(230, 230, 230);
            dgv.RowHeadersVisible = false;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersHeight = 40;
            dgv.RowTemplate.Height = 40;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9.75F);
            dgv.DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgv.AllowUserToAddRows = false;
        }

        private void frmTransactionHistory_Load(object sender, EventArgs e)
        {
            try
            {
                SetupDataGridView();
                LoadSupplierComboBox();

                dtpFromDate.Value = DateTime.Now.AddMonths(-12);
                dtpToDate.Value = DateTime.Now;

                LoadTransactions();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView()
        {
            dgvTransactions.AutoGenerateColumns = false;
            dgvDetails.AutoGenerateColumns = false;

            if (dgvTransactions.Columns.Count == 0)
            {
                dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn { Name = "colId", HeaderText = "Mã GD", DataPropertyName = "id", Width = 120 });
                dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn { Name = "colSupplierName", HeaderText = "Nhà cung cấp", DataPropertyName = "TenNCC", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
                dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDate", HeaderText = "Ngày giao dịch", DataPropertyName = "ngayGD", Width = 150 });
                dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTotalAmount", HeaderText = "Tổng tiền", DataPropertyName = "tongTien", Width = 150, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight } });
            }

            if (dgvDetails.Columns.Count == 0)
            {
                dgvDetails.Columns.Add(new DataGridViewTextBoxColumn { Name = "colSTT", HeaderText = "STT", DataPropertyName = "STT", Width = 50 });
                dgvDetails.Columns.Add(new DataGridViewTextBoxColumn { Name = "colProductId", HeaderText = "Mã SP", DataPropertyName = "MaSP", Width = 100 });
                dgvDetails.Columns.Add(new DataGridViewTextBoxColumn { Name = "colProductName", HeaderText = "Tên sản phẩm", DataPropertyName = "TenSanPham", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
                dgvDetails.Columns.Add(new DataGridViewTextBoxColumn { Name = "colUnit", HeaderText = "Đơn vị", DataPropertyName = "DonVi", Width = 100 });
                dgvDetails.Columns.Add(new DataGridViewTextBoxColumn { Name = "colQuantity", HeaderText = "Số lượng", DataPropertyName = "soLuong", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
                dgvDetails.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPrice", HeaderText = "Đơn giá", DataPropertyName = "donGia", Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight } });
                dgvDetails.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTotal", HeaderText = "Thành tiền", DataPropertyName = "ThanhTien", Width = 150, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight } });
            }
        }

        private void LoadSupplierComboBox()
        {
            try
            {
                var suppliers = _services.GetList<NhaCungCap>()
                    .OrderBy(s => s.ten)
                    .Select(s => new { s.id, s.ten })
                    .ToList();

                var allSuppliers = new List<dynamic> { new { id = "", ten = "-- Tất cả nhà cung cấp --" } };
                allSuppliers.AddRange(suppliers);

                cboSupplier.DataSource = allSuppliers;
                cboSupplier.DisplayMember = "ten";
                cboSupplier.ValueMember = "id";
                cboSupplier.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải NCC: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTransactions()
        {
            try
            {
                string supplierId = cboSupplier.SelectedValue?.ToString();
                DateTime fromDate = dtpFromDate.Value.Date;
                DateTime toDate = dtpToDate.Value.Date.AddDays(1).AddSeconds(-1);

                var listPhieuNhap = _services.GetList<PhieuNhap>()
                    .Where(pn => pn.ngayNhap >= fromDate && pn.ngayNhap <= toDate)
                    .ToList();

                var listNCC = _services.GetList<NhaCungCap>();

                var transactions = listPhieuNhap
                    .Join(listNCC, pn => pn.nhaCungCapId, ncc => ncc.id, (pn, ncc) => new
                    {
                        pn.id,
                        pn.nhaCungCapId,
                        TenNCC = ncc.ten,
                        ngayGD = pn.ngayNhap,
                        pn.tongTien
                    })
                    .Where(t => string.IsNullOrEmpty(supplierId) || t.nhaCungCapId == supplierId)
                    .OrderByDescending(t => t.ngayGD)
                    .ToList();

                dgvTransactions.DataSource = transactions;
                lblTotalAmount.Text = $"Tổng giá trị: {transactions.Sum(t => t.tongTien):N0} VNĐ";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải giao dịch: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvTransactions_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTransactions.CurrentRow != null)
            {
                _selectedTransactionId = dgvTransactions.CurrentRow.Cells["colId"].Value?.ToString();
                LoadTransactionDetails(_selectedTransactionId);
            }
            else
            {
                dgvDetails.DataSource = null;
            }
        }

        private void LoadTransactionDetails(string phieuNhapId)
        {
            if (string.IsNullOrEmpty(phieuNhapId))
            {
                dgvDetails.DataSource = null;
                return;
            }

            try
            {
                var details = _services.GetList<ChiTietPhieuNhap>()
                    .Where(ct => ct.phieuNhapId == phieuNhapId)
                    .ToList();

                var listSPDV = _services.GetList<SanPhamDonVi>();
                var listSP = _services.GetList<SanPham>();
                var listDonVi = _services.GetList<DonViDoLuong>();

                var displayList = (from ct in details
                                   join spdv in listSPDV on ct.sanPhamDonViId equals spdv.id
                                   join sp in listSP on spdv.sanPhamId equals sp.id
                                   join dv in listDonVi on spdv.donViId equals dv.id
                                   select new
                                   {
                                       MaSP = sp.id,
                                       TenSanPham = sp.ten,
                                       DonVi = dv.ten,
                                       ct.soLuong,
                                       ct.donGia,
                                       ThanhTien = (decimal?)ct.tongTien ?? (ct.soLuong * ct.donGia) // Null check
                                   })
                                   .Select((x, index) => new
                                   {
                                       STT = index + 1,
                                       x.MaSP,
                                       x.TenSanPham,
                                       x.DonVi,
                                       x.soLuong,
                                       x.donGia,
                                       x.ThanhTien
                                   })
                                   .ToList();

                dgvDetails.DataSource = displayList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải chi tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            LoadTransactions();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            cboSupplier.SelectedIndex = 0;
            dtpFromDate.Value = DateTime.Now.AddMonths(-12);
            dtpToDate.Value = DateTime.Now;
            LoadTransactions();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dgvTransactions.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Excel Workbook|*.xlsx";
                    sfd.FileName = $"LichSuGiaoDich_{DateTime.Now:yyyyMMdd}.xlsx";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        using (var workbook = new XLWorkbook())
                        {
                            var worksheet = workbook.Worksheets.Add("Lịch Sử Giao Dịch");

                            for (int i = 0; i < dgvTransactions.Columns.Count; i++)
                            {
                                worksheet.Cell(1, i + 1).Value = dgvTransactions.Columns[i].HeaderText;
                                worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                                worksheet.Cell(1, i + 1).Style.Fill.BackgroundColor = XLColor.LightBlue;
                            }

                            // Data
                            for (int i = 0; i < dgvTransactions.Rows.Count; i++)
                            {
                                for (int j = 0; j < dgvTransactions.Columns.Count; j++)
                                {
                                    var cell = worksheet.Cell(i + 2, j + 1);
                                    var value = dgvTransactions.Rows[i].Cells[j].Value;

                                    if (value is decimal || value is int)
                                    {
                                        cell.Value = Convert.ToDouble(value); // ClosedXML thích double hơn decimal
                                        cell.Style.NumberFormat.Format = "#,##0";
                                    }
                                    else if (value is DateTime dateValue)
                                    {
                                        cell.Value = dateValue;
                                        cell.Style.DateFormat.Format = "dd/MM/yyyy";
                                    }
                                    else
                                    {
                                        cell.Value = value?.ToString();
                                    }
                                }
                            }

                            worksheet.Columns().AdjustToContents();
                            workbook.SaveAs(sfd.FileName);
                        }
                        MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xuất Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtpFromDate_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}