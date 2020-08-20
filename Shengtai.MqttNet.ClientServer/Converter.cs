using LiteDB;
using Shengtai.MqttNet.ClientServer.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Shengtai.MqttNet.ClientServer
{
    public partial class frmConverter : Form
    {
        private static readonly Lazy<frmConverter> lazy = new Lazy<frmConverter>(() => new frmConverter());
        public static frmConverter Instance { get { return lazy.Value; } }

        private frmConverter()
        {
            InitializeComponent();
        }

        private void frmConverter_Load(object sender, EventArgs e)
        {
            this.Display();
        }

        private void frmConverter_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            isShown = false;

            Instance.Hide();
        }

        private static bool isShown = false;
        public new void Show()
        {
            base.Show();

            if (!isShown)
                isShown = true;
        }

        private bool IsNotNull(string path)
        {
            var rawAssembly = File.ReadAllBytes(path);
            var assembly = Assembly.Load(rawAssembly);

            foreach (var type in assembly.GetExportedTypes())
                if (type.IsClass && assembly.CreateInstance(type.FullName) is IConvertMessage)
                    return true;

            return false;
        }

        private bool Exists(string id)
        {
            using var db = new LiteDatabase(Settings.Default.ConnectionString);
            var storage = db.GetStorage<string>();

            return storage.Exists(id);
        }

        private void Upload(string path)
        {
            var id = Path.GetFileName(path);

            using var db = new LiteDatabase(Settings.Default.ConnectionString);
            var storage = db.GetStorage<string>();

            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            storage.Upload(id, id, stream);
        }

        private void Display()
        {
            using (var db = new LiteDatabase(Settings.Default.ConnectionString))
            {
                var storage = db.GetStorage<string>();
                var files = storage.FindAll().Select(x => new DllFileInfo
                {
                    Id = x.Id,
                    Length = x.Length,
                    UploadDate = x.UploadDate
                }).ToList();

                this.dgvDll.DataSource = files;
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (this.ofdDll.ShowDialog() == DialogResult.OK)
            {
                if (this.IsNotNull(ofdDll.FileName))
                {
                    if (!this.Exists(this.lblFileName.Text) || MessageBox.Show("檔案已存在，是否覆蓋？", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.Upload(ofdDll.FileName);

                        this.lblFileName.Text = Path.GetFileName(ofdDll.FileName);
                        this.Display();
                    }
                }
                else
                    MessageBox.Show("檔案內容程式設計未符合規範。", "警告訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDll_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.lblFileName.Text = this.dgvDll.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        public void Delete(string id)
        {
            using (var db = new LiteDatabase(Settings.Default.ConnectionString))
            {
                var storage = db.GetStorage<string>();

                storage.Delete(id);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否刪除？", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Delete(this.lblFileName.Text);
                this.lblFileName.Text = string.Empty;
                this.Display();
            }
        }
    }
}
