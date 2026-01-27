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

        // control flags to avoid re-entrancy and safe shutdown
        private volatile bool _isDetected = false;
        private volatile bool _isClosing = false;
        private volatile bool _isDecoding = false;
        private readonly object _imageLock = new object();

        // timing controls to throttle work
        private long _lastDecodeTicks = 0;
        private int _decodeIntervalMs = 150; // decode at most ~6-7 times/sec
        private long _lastUiUpdateTicks = 0;
        private int _uiUpdateIntervalMs = 80; // update picturebox ~12 times/sec

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
                StartCamera(0);
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

        private void StartCamera(int index)
        {
            StopCamera();

            if (index < 0 || index >= filterInfoCollection.Count) return;

            videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[index].MonikerString);

            // Try to pick a lower resolution to reduce processing and latency (choose closest <=640 width)
            try
            {
                var caps = videoCaptureDevice.VideoCapabilities;
                if (caps != null && caps.Length > 0)
                {
                    var chosen = caps.OrderBy(c => Math.Abs(c.FrameSize.Width - 640)).FirstOrDefault();
                    if (chosen != null)
                    {
                        videoCaptureDevice.VideoResolution = chosen;
                    }
                }
            }
            catch { }

            videoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
            videoCaptureDevice.Start();
        }

        private void StopCamera()
        {
            try
            {
                if (videoCaptureDevice != null)
                {
                    if (videoCaptureDevice.IsRunning)
                    {
                        videoCaptureDevice.NewFrame -= VideoCaptureDevice_NewFrame;
                        videoCaptureDevice.SignalToStop();
                        videoCaptureDevice.WaitForStop();
                    }
                    videoCaptureDevice = null;
                }
            }
            catch { }
        }

        private void VideoCaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (_isDetected || _isClosing) // short-circuit if already done or closing
            {
                return;
            }

            Bitmap frame = null;
            try
            {
                // Quick clone of the frame for immediate use
                frame = (Bitmap)eventArgs.Frame.Clone();

                var nowTicks = DateTime.UtcNow.Ticks;
                var elapsedDecodeMs = (nowTicks - _lastDecodeTicks) / TimeSpan.TicksPerMillisecond;
                var elapsedUiMs = (nowTicks - _lastUiUpdateTicks) / TimeSpan.TicksPerMillisecond;

                // Schedule background decode if allowed and not already decoding
                if (!_isDecoding && elapsedDecodeMs >= _decodeIntervalMs)
                {
                    _isDecoding = true;
                    _lastDecodeTicks = nowTicks;

                    // Pass a smaller clone to background task to reduce work
                    Bitmap decodeBmp = null;
                    try
                    {
                        // scale down for faster decode (keep aspect ratio)
                        int targetW = Math.Max(160, frame.Width / 2);
                        int targetH = Math.Max(120, frame.Height / 2);
                        decodeBmp = new Bitmap(frame, new Size(targetW, targetH));
                    }
                    catch
                    {
                        // fallback to original
                        decodeBmp = (Bitmap)frame.Clone();
                    }

                    Task.Run(() =>
                    {
                        try
                        {
                            var reader = new BarcodeReader()
                            {
                                AutoRotate = true,
                                TryInverted = true
                            };
                            reader.Options.PossibleFormats = new System.Collections.Generic.List<BarcodeFormat>
                            {
                                BarcodeFormat.EAN_13,
                                BarcodeFormat.CODE_128,
                                BarcodeFormat.QR_CODE
                            };

                            var result = reader.Decode(decodeBmp);
                            if (result != null && !_isDetected && !_isClosing)
                            {
                                _isDetected = true;
                                // invoke on UI thread
                                this.BeginInvoke(new Action(() =>
                                {
                                    try
                                    {
                                        ScannedCode = result.Text;
                                        try { new SoundPlayer(Properties.Resources.beep).Play(); } catch { }

                                        StopCamera();
                                        this.DialogResult = DialogResult.OK;
                                        this.Close();
                                    }
                                    catch { }
                                }));
                            }
                        }
                        catch { }
                        finally
                        {
                            try { decodeBmp?.Dispose(); } catch { }
                            _isDecoding = false;
                        }
                    });
                }

                // Update picture box image at a lower rate to avoid UI thread congestion
                if (elapsedUiMs >= _uiUpdateIntervalMs && !this.IsDisposed && !this.Disposing)
                {
                    _lastUiUpdateTicks = nowTicks;
                    this.BeginInvoke(new Action(() =>
                    {
                        if (_isDetected || _isClosing)
                        {
                            try { frame.Dispose(); } catch { }
                            return;
                        }

                        lock (_imageLock)
                        {
                            try
                            {
                                var old = picBox.Image;
                                // show a resized preview to reduce memory/cost if needed
                                Bitmap preview = null;
                                try
                                {
                                    int pw = Math.Min(640, frame.Width);
                                    int ph = (int)((decimal)frame.Height * pw / frame.Width);
                                    preview = new Bitmap(frame, new Size(pw, ph));
                                }
                                catch
                                {
                                    preview = (Bitmap)frame.Clone();
                                }

                                picBox.Image = preview;
                                if (old != null)
                                {
                                    try { old.Dispose(); } catch { }
                                }
                            }
                            catch { }
                            finally
                            {
                                try { frame.Dispose(); } catch { }
                            }
                        }
                    }));
                }
                else
                {
                    // Not updating UI now, release frame
                    try { frame.Dispose(); } catch { }
                }
            }
            catch
            {
                try { frame?.Dispose(); } catch { }
                _isDecoding = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _isClosing = true;
            StopCamera();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void cboCamera_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCamera.SelectedIndex >= 0)
            {
                StartCamera(cboCamera.SelectedIndex);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _isClosing = true;
            StopCamera();
            base.OnFormClosing(e);
        }
    }
}
