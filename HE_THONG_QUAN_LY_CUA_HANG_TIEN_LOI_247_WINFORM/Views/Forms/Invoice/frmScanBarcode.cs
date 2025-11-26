using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Views.forms.Invoice
{
    public partial class frmScanBarcode : Form
    {
        private FilterInfoCollection filterInfoCollection;
        private VideoCaptureDevice videoCaptureDevice;
        public string ScannedCode { get; private set; }
        public frmScanBarcode()
        {
            InitializeComponent();
        }

        private void frmScanBarcode_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(frmChiTietHoaDon_KeyDown);
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            cboCamera.Items.Clear();
            foreach (FilterInfo device in filterInfoCollection)
            {
                cboCamera.Items.Add(device.Name);
            }

            if (cboCamera.Items.Count > 0)
            {
                cboCamera.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Không tìm thấy Camera nào!");
            }

            if (filterInfoCollection.Count > 0)
            {
                videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[0].MonikerString);
                videoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
                videoCaptureDevice.Start();
            }
            else
            {
                MessageBox.Show("Không tìm thấy Camera!");
                this.Close();
            }
        }
        private void frmChiTietHoaDon_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.H)
            {
                btnCancel.PerformClick();
                e.SuppressKeyPress = true;
            }
        }
        private void VideoCaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();

            BarcodeReader reader = new BarcodeReader();
            reader.Options.PossibleFormats = new System.Collections.Generic.List<BarcodeFormat>
    {
        BarcodeFormat.EAN_13,
        BarcodeFormat.CODE_128,
        BarcodeFormat.QR_CODE
    };

            var result = reader.Decode(bitmap);

            if (result != null)
            {
                videoCaptureDevice.NewFrame -= VideoCaptureDevice_NewFrame;

                Invoke(new MethodInvoker(delegate ()
                {
                    ScannedCode = result.Text;

                    SoundPlayer player = new SoundPlayer(Properties.Resources.beep);
                    player.Play();

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }));
            }
            else
            {
                picBox.Image = bitmap;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (videoCaptureDevice != null && videoCaptureDevice.IsRunning)
            {
                videoCaptureDevice.SignalToStop();
                videoCaptureDevice.WaitForStop();
            }
        }

        private void cboCamera_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (videoCaptureDevice != null && videoCaptureDevice.IsRunning)
            {
                videoCaptureDevice.SignalToStop();
                videoCaptureDevice.WaitForStop(); // Chờ tắt hẳn để tránh xung đột
            }

            // Khởi tạo camera mới dựa trên Index vừa chọn
            videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[cboCamera.SelectedIndex].MonikerString);
            videoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
            videoCaptureDevice.Start();
        }
    }
}
