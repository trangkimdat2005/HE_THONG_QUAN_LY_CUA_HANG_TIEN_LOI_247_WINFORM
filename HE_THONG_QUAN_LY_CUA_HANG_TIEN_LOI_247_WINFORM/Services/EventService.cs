using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Services
{
    public enum FormMode
    {
        Add,
        Edit
    }

    internal class EventService
    {
        public static void TextBox_KhongNhapSo_KeyPress(object sender, KeyPressEventArgs e) // Thêm 'static'
        {
            if (char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public static void TextBox_KhongNhapChu_KeyPress(object sender, KeyPressEventArgs e) // Thêm 'static'
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public static void TextBox_Email_Validating(object sender, CancelEventArgs e) // Thêm 'static'
        {
            System.Windows.Forms.TextBox txt = sender as System.Windows.Forms.TextBox;
            if (txt == null || string.IsNullOrWhiteSpace(txt.Text))
            {
                return;
            }
            txt.Text = txt.Text.ToLower();
            string email = txt.Text;
            try
            {
                MailAddress m = new MailAddress(email);
            }
            catch (FormatException)
            {
                MessageBox.Show("Định dạng email không hợp lệ. Vui lòng nhập lại.",
                                "Lỗi định dạng",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        public static void TextBox_KhongNhapKyTuDacBiet_KeyPress(object sender, KeyPressEventArgs e) // Thêm 'static'
        {
            if (char.IsControl(e.KeyChar))
            {
                return;
            }
            if (!Regex.IsMatch(e.KeyChar.ToString(), "^[a-zA-Z0-9]$"))
            {
                e.Handled = true;
            }
        }
        public static bool KiemTraRong(params (TextBox textBox, string message)[] controls)
        {
            foreach (var item in controls)
            {
                var txt = item.textBox;
                var msg = item.message;

                if (txt == null)
                    continue;

                if (string.IsNullOrWhiteSpace(txt.Text))
                {
                    MessageBox.Show(msg, "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt.Focus();
                    return false;
                }
            }
            return true; 
        }
        public static void TextBox_SoDienThoai_Validating(object sender, CancelEventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt == null) return;

            string phone = txt.Text.Trim();

            if (string.IsNullOrWhiteSpace(phone))
                return;

            string pattern = @"^0\d{9,10}$";

            if (!Regex.IsMatch(phone, pattern))
            {
                MessageBox.Show(
                    "Số điện thoại không hợp lệ!\nVí dụ đúng: 0987654321 (10–11 chữ số, bắt đầu bằng 0)",
                    "Lỗi định dạng",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                e.Cancel = true;
                txt.Focus();
            }
        }
        public static void TextBox_SoTien_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = sender as TextBox;

            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsDigit(e.KeyChar))
                return;

            if (e.KeyChar == '.')
            {
                if (txt.Text.Contains("."))
                {
                    e.Handled = true;
                    return;
                }

                if (txt.Text.Length == 0)
                {
                    e.Handled = true;
                    return;
                }

                return;
            }

            e.Handled = true;
        }
        public static void SetupDateTimePicker_AgeRange(DateTimePicker dtp, int minAge, int maxAge)
        {
            DateTime today = DateTime.Today;

            dtp.MaxDate = today.AddYears(-minAge);
            dtp.MinDate = today.AddYears(-maxAge);

            if (dtp.Value > dtp.MaxDate)
                dtp.Value = dtp.MaxDate;

            if (dtp.Value < dtp.MinDate)
                dtp.Value = dtp.MinDate;
        }
        public static void SetupDatePicker_ByCondition(
            DateTimePicker dtp,
            DateTime conditionDate,
            string op)
                {
                    DateTime value = dtp.Value;

                    switch (op)
                    {
                        case ">=":
                            dtp.MinDate = conditionDate;
                            if (value < conditionDate)
                                dtp.Value = conditionDate;
                            break;

                        case ">":
                            dtp.MinDate = conditionDate.AddDays(1);
                            if (value <= conditionDate)
                                dtp.Value = conditionDate.AddDays(1);
                            break;

                        case "<=":
                            dtp.MaxDate = conditionDate;
                            if (value > conditionDate)
                                dtp.Value = conditionDate;
                            break;

                        case "<":
                            dtp.MaxDate = conditionDate.AddDays(-1);
                            if (value >= conditionDate)
                                dtp.Value = conditionDate.AddDays(-1);
                            break;

                        case "=":
                            dtp.MinDate = conditionDate;
                            dtp.MaxDate = conditionDate;
                            dtp.Value = conditionDate;
                            break;

                        default:
                            throw new Exception("Toán tử không hợp lệ! Hỗ trợ: >=, >, <=, <, =");
                    }
                }
    }
}
