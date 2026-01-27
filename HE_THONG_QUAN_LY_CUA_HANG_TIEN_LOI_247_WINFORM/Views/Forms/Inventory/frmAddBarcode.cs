using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Controllers;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Views.forms.Inventory
{
    public partial class frmAddBarcode : Form
    {
        private readonly BarcodeController _barcodeController;
        private string _barcodeId;
        private bool _isEditMode = false;

        public frmAddBarcode()
        {
            InitializeComponent();
            _barcodeController = new BarcodeController();
        }

        public frmAddBarcode(string barcodeId) : this()
        {
            _barcodeId = barcodeId;
            _isEditMode = true;
        }

        private void frmAddBarcode_Load(object sender, EventArgs e)
        {
            try
            {
                LoadProductUnits();

                if (_isEditMode && !string.IsNullOrEmpty(_barcodeId))
                {
                    LoadBarcodeData();
                    this.Text = "Cập nhật Barcode";
                    btnSave.Text = "Cập nhật";
                }
                else
                {
                    txtBarcodeId.Text = _barcodeController.GenerateNewBarcodeId();
                    this.Text = "Thêm Barcode mới";
                    btnSave.Text = "Thêm mới";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải form: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProductUnits()
        {
            try
            {
                var productUnits = _barcodeController.GetAllProductUnits();
                cboProductUnit.DataSource = productUnits;
                cboProductUnit.DisplayMember = "SanPham.ten";
                cboProductUnit.ValueMember = "id";
                cboProductUnit.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách sản phẩm: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadBarcodeData()
        {
            try
            {
                var barcode = _barcodeController.GetBarcodeById(_barcodeId);
                if (barcode != null)
                {
                    txtBarcodeId.Text = barcode.id;
                    cboProductUnit.SelectedValue = barcode.sanPhamDonViId;
                    cboCodeType.SelectedItem = barcode.loaiMa;
                    txtBarcodeCode.Text = barcode.maCode;
                    txtImagePath.Text = barcode.duongDan;

                    GenerateBarcodeImage(barcode.maCode, barcode.loaiMa);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu barcode: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBarcodeCode.Text))
            {
                MessageBox.Show("Vui lòng nhập mã barcode!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBarcodeCode.Focus();
                return;
            }

            if (cboCodeType.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn loại mã!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboCodeType.Focus();
                return;
            }

            GenerateBarcodeImage(txtBarcodeCode.Text, cboCodeType.SelectedItem.ToString());
        }

        private void GenerateBarcodeImage(string code, string codeType)
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
                else
                {
                    writer.Format = BarcodeFormat.EAN_13;
                }

                writer.Options = new EncodingOptions
                {
                    Width = 300,
                    Height = 150,
                    Margin = 10
                };

                var bitmap = writer.Write(code);
                picBarcode.Image = bitmap;
                picBarcode.SizeMode = PictureBoxSizeMode.Zoom;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tạo barcode: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                var barcode = new MaDinhDanhSanPham
                {
                    id = txtBarcodeId.Text.Trim(),
                    sanPhamDonViId = cboProductUnit.SelectedValue.ToString(),
                    loaiMa = cboCodeType.SelectedItem.ToString(),
                    maCode = txtBarcodeCode.Text.Trim(),
                    duongDan = txtImagePath.Text.Trim(),
                    isDelete = false
                };

                if (_isEditMode)
                {
                    var (success, message) = _barcodeController.UpdateBarcode(barcode);
                    if (success)
                    {
                        MessageBox.Show(message, "Thành công", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(message, "Lỗi", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    var (success, message, _) = _barcodeController.AddBarcode(barcode);
                    if (success)
                    {
                        MessageBox.Show(message, "Thành công", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(message, "Lỗi", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lưu barcode: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (cboProductUnit.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboProductUnit.Focus();
                return false;
            }

            if (cboCodeType.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn loại mã!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboCodeType.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtBarcodeCode.Text))
            {
                MessageBox.Show("Vui lòng nhập mã barcode!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBarcodeCode.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtImagePath.Text))
            {
                MessageBox.Show("Vui lòng nhập đường dẫn lưu ảnh!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtImagePath.Focus();
                return false;
            }

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _barcodeController?.Dispose();
            base.OnFormClosing(e);
        }
    }
}
