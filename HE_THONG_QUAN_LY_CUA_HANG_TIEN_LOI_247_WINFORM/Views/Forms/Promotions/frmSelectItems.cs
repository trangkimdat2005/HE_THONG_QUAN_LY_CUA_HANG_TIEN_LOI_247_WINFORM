using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Views.forms.Promotions
{
    public partial class frmSelectItems : Form
    {
        public List<SelectableItem> SelectedItems { get; private set; } = new List<SelectableItem>();
        private List<SelectableItem> _allItems;
        private HashSet<string> _checkedIds = new HashSet<string>();

        public frmSelectItems(string title, List<SelectableItem> items, List<string> preSelectedIds = null)
        {
            InitializeComponent();
            _allItems = items ?? new List<SelectableItem>();
            
            if (preSelectedIds != null)
                _checkedIds = new HashSet<string>(preSelectedIds);
            
            SetTitle(title);
            LoadItems();
        }

        private void SetTitle(string title)
        {
            this.Text = title;
            lblTitle.Text = title;
        }

        private void LoadItems()
        {
            clbItems.Items.Clear();
            foreach (var item in _allItems)
            {
                bool isChecked = _checkedIds.Contains(item.Id);
                clbItems.Items.Add(item, isChecked);
            }
            UpdateSelectedCount();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SaveCheckedState();
            
            string keyword = txtSearch.Text.Trim().ToLower();
            clbItems.Items.Clear();
            
            var filteredItems = string.IsNullOrEmpty(keyword) 
                ? _allItems 
                : _allItems.Where(x => x.Name.ToLower().Contains(keyword)).ToList();

            foreach (var item in filteredItems)
                clbItems.Items.Add(item, _checkedIds.Contains(item.Id));
            
            UpdateSelectedCount();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clbItems.Items.Count; i++)
            {
                clbItems.SetItemChecked(i, true);
                if (clbItems.Items[i] is SelectableItem item)
                    _checkedIds.Add(item.Id);
            }
            UpdateSelectedCount();
        }

        private void btnDeselectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clbItems.Items.Count; i++)
            {
                clbItems.SetItemChecked(i, false);
                if (clbItems.Items[i] is SelectableItem item)
                    _checkedIds.Remove(item.Id);
            }
            UpdateSelectedCount();
        }

        private void clbItems_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (clbItems.Items[e.Index] is SelectableItem item)
            {
                if (e.NewValue == CheckState.Checked)
                    _checkedIds.Add(item.Id);
                else
                    _checkedIds.Remove(item.Id);
            }
            BeginInvoke(new Action(UpdateSelectedCount));
        }

        private void UpdateSelectedCount() => lblSelected.Text = $"Đã chọn: {_checkedIds.Count}";

        private void SaveCheckedState()
        {
            for (int i = 0; i < clbItems.Items.Count; i++)
            {
                if (clbItems.Items[i] is SelectableItem item)
                {
                    if (clbItems.GetItemChecked(i))
                        _checkedIds.Add(item.Id);
                    else
                        _checkedIds.Remove(item.Id);
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SaveCheckedState();
            SelectedItems = _allItems.Where(item => _checkedIds.Contains(item.Id)).ToList();
        }
    }

    public class SelectableItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public override string ToString() => Name;
    }
}
