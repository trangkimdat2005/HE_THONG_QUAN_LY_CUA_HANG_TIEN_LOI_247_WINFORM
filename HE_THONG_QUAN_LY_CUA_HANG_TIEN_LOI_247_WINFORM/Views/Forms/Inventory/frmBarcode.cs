using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Controllers;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;
using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Views.forms.Inventory
{
    public partial class frmBarcode : Form
    {
        private readonly BarcodeController _barcodeController;
        private string _selectedBarcodeId;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        private const int EM_SETCUEBANNER = 0x1501;

        private void SetPlaceholder(TextBox txt, string text)
        {
            if (txt != null)
            {
                SendMessage(txt.Handle, EM_SETCUEBANNER, 0, text);
            }
        }

        public frmBarcode()
        {
            InitializeComponent();
            _barcodeController = new BarcodeController();

            SetPlaceholder(txtSearch, "Nhập mã hoặc tên sản phẩm để tìm...");

            if (txtSearch != null)
            {
                txtSearch.TextChanged += (s, e) => { btnSearch_Click(null, null); };
            }

            if (dgvBarcodes != null) 
                this.dgvBarcodes.SelectionChanged += dgvBarcodes_SelectionChanged;
            
            if (txtSearch != null) 
                txtSearch.KeyPress += (s, e) => { if (e.KeyChar == 13) { btnSearch_Click(s, e); e.Handled = true; } };

            this.VisibleChanged += (s, e) => {
                if (this.Visible) LoadBarcodes();
            };
        }

        private void frmBarcode_Load(object sender, EventArgs e)
        {
            try
            {
                if (dgvBarcodes != null) dgvBarcodes.AutoGenerateColumns = false;
                LoadBarcodes();
            }
            catch (Exception ex) 
            { 
                MessageBox.Show($"Lỗi tải form: {ex.Message}"); 
            }
        }

        private void LoadBarcodes()
        {
            try
            {
                var barcodes = _barcodeController.GetAllBarcodes();
                dgvBarcodes.DataSource = barcodes;

                foreach (DataGridViewColumn col in dgvBarcodes.Columns)
                {
                    if (col.HeaderText.ToLower().Contains("mã định danh")) 
                        col.DataPropertyName = "Id";
                    else if (col.HeaderText.ToLower().Contains("sản phẩm")) 
                        col.DataPropertyName = "TenSanPham";
                    else if (col.HeaderText.ToLower().Contains("loại")) 
                        col.DataPropertyName = "LoaiMa";
                    else if (col.HeaderText.ToLower().Contains("mã code") || col.HeaderText.ToLower().Contains("barcode")) 
                        col.DataPropertyName = "MaCode";
                    else if (col.HeaderText.ToLower().Contains("trạng thái")) 
                        col.DataPropertyName = "TrangThai";
                }

                if (barcodes is IList list) 
                    lblStatus.Text = $"Tổng số: {list.Count} barcode";
            }
            catch (Exception ex) 
            { 
                MessageBox.Show($"Lỗi hiển thị: {ex.Message}"); 
            }
        }

        private string GetCurrentId()
        {
            if (dgvBarcodes.CurrentRow == null) return null;

            var dataItem = dgvBarcodes.CurrentRow.DataBoundItem;
            if (dataItem != null)
            {
                var prop = dataItem.GetType().GetProperty("Id");
                if (prop != null) return prop.GetValue(dataItem)?.ToString();
            }

            if (dgvBarcodes.ColumnCount > 0 && dgvBarcodes.CurrentRow.Cells[0].Value != null)
                return dgvBarcodes.CurrentRow.Cells[0].Value.ToString();

            return null;
        }

        private void dgvBarcodes_SelectionChanged(object sender, EventArgs e)
        {
            string id = GetCurrentId();
            if (!string.IsNullOrEmpty(id))
            {
                _selectedBarcodeId = id;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                using (var frm = new frmAddBarcode())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        LoadBarcodes();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi mở form thêm barcode: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string id = GetCurrentId();
            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("Vui lòng chọn barcode!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var frm = new frmAddBarcode(id))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        LoadBarcodes();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi mở form sửa barcode: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string id = GetCurrentId();
            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("Vui lòng chọn barcode!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa barcode này?", "Xác nhận", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var (success, message) = _barcodeController.DeleteBarcode(id);
                if (success)
                {
                    MessageBox.Show(message, "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadBarcodes();
                }
                else 
                {
                    MessageBox.Show(message, "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var list = _barcodeController.SearchBarcodes(txtSearch.Text.Trim());
            dgvBarcodes.DataSource = list;
            if (list is IList l) 
                lblStatus.Text = $"Tìm thấy: {l.Count}";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadBarcodes();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string id = GetCurrentId();
            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("Vui lòng chọn barcode để in!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var barcode = _barcodeController.GetBarcodeById(id);
                if (barcode == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin barcode!", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                PrintBarcode(barcode);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi in barcode: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintBarcode(MaDinhDanhSanPham barcode)
        {
            try
            {
                PrintDocument printDocument = new PrintDocument();
                printDocument.PrintPage += (sender, e) => PrintBarcodePage(sender, e, barcode);

                PrintDialog printDialog = new PrintDialog();
                printDialog.Document = printDocument;

                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    printDocument.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi in: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintBarcodePage(object sender, PrintPageEventArgs e, MaDinhDanhSanPham barcode)
        {
            try
            {
                Graphics graphics = e.Graphics;
                Font titleFont = new Font("Segoe UI", 16, FontStyle.Bold);
                Font normalFont = new Font("Segoe UI", 10);
                Font codeFont = new Font("Consolas", 12, FontStyle.Bold);
                
                Brush blackBrush = Brushes.Black;
                
                float yPos = 50;
                float leftMargin = 100;
                
                // Tiêu đề
                graphics.DrawString("BARCODE LABEL", titleFont, blackBrush, leftMargin, yPos);
                yPos += 40;
                
                // Thông tin sản phẩm
                var productUnits = _barcodeController.GetAllProductUnits();
                var productUnit = productUnits.Find(p => p.id == barcode.sanPhamDonViId);
                
                if (productUnit != null && productUnit.SanPham != null)
                {
                    graphics.DrawString($"Sản phẩm: {productUnit.SanPham.ten}", normalFont, blackBrush, leftMargin, yPos);
                    yPos += 25;
                    
                    if (productUnit.DonViDoLuong != null)
                    {
                        graphics.DrawString($"Đơn vị: {productUnit.DonViDoLuong.ten}", normalFont, blackBrush, leftMargin, yPos);
                        yPos += 25;
                    }
                }
                
                graphics.DrawString($"Loại mã: {barcode.loaiMa}", normalFont, blackBrush, leftMargin, yPos);
                yPos += 25;
                
                graphics.DrawString($"Mã code: {barcode.maCode}", codeFont, blackBrush, leftMargin, yPos);
                yPos += 40;
                
                // Tạo và vẽ barcode
                Bitmap barcodeImage = GenerateBarcodeImage(barcode.maCode, barcode.loaiMa);
                if (barcodeImage != null)
                {
                    int imageWidth = 400;
                    int imageHeight = 200;
                    graphics.DrawImage(barcodeImage, leftMargin, yPos, imageWidth, imageHeight);
                    yPos += imageHeight + 20;
                }
                
                // Ngày in
                graphics.DrawString($"Ngày in: {DateTime.Now:dd/MM/yyyy HH:mm}", normalFont, blackBrush, leftMargin, yPos);
                
                e.HasMorePages = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo trang in: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Bitmap GenerateBarcodeImage(string code, string codeType)
        {
            try
            {
                BarcodeWriter writer = new BarcodeWriter();
                
                if (codeType == "QR Code")
                {
                    writer.Format = BarcodeFormat.QR_CODE;
                }
                else if (codeType == "Barcode")
                {
                    writer.Format = BarcodeFormat.CODE_128;
                }
                else if (codeType == "EAN-13")
                {
                    writer.Format = BarcodeFormat.EAN_13;
                }
                else
                {
                    writer.Format = BarcodeFormat.CODE_128;
                }

                writer.Options = new EncodingOptions
                {
                    Width = 400,
                    Height = 200,
                    Margin = 10
                };

                return writer.Write(code);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tạo barcode image: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _barcodeController?.Dispose();
            base.OnFormClosing(e);
        }
    }
}
