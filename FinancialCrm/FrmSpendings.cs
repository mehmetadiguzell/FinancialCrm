using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using FinancialCrm.Models;

namespace FinancialCrm
{
    public partial class FrmSpendings : Form
    {
        public FrmSpendings()
        {
            InitializeComponent();
        }

        private FinancialCrmDbEntities db = new FinancialCrmDbEntities();
        private int count = 0;
        private List<Spendibgs> spendingList;

        private void FrmSpendings_Load(object sender, EventArgs e)
        {
            var totalspend = db.Spendibgs.Sum(x => x.SpendingAmount);
            lblTotalSpending.Text = totalspend.ToString() + " ₺";

            var highExpense = db.Spendibgs.Max(x => x.SpendingAmount);
            lblHighExpense.Text = highExpense.ToString() + " ₺";

            // Verileri listeye al
            spendingList = db.Spendibgs.ToList();

            // Eğer veri varsa timer'ı başlat
            if (spendingList.Count > 0)
            {
                timer1.Interval = 2000; // 2 saniyede bir güncelleme
                timer1.Start();
            }

            // Grafik verileri
            chart2.Series.Clear();
            var series = chart2.Series.Add("Harcamalar");
            series.ChartType = SeriesChartType.Pie;


            foreach (var item in spendingList)
            {
                series.Points.AddXY(item.SpendingTitle, item.SpendingAmount);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (spendingList == null || spendingList.Count == 0)
                return;

            var currentSpending = spendingList[count % spendingList.Count];

            // Kategori tablosundan ID'ye göre eşleşen kategoriyi getiriyoruz
            var category = db.Categories.Find(currentSpending.CategoryId);

            lblTitleSpending.Text = currentSpending.SpendingTitle;
            lblCategorySpending.Text = category != null ? category.CategoryName : "Bilinmeyen Kategori";
            lblSpendingAmount.Text = currentSpending.SpendingAmount.ToString() + " ₺";

            count++;
        }

        private void btnCategoryForm_Click(object sender, EventArgs e)
        {
            FrmCategories frmCategories = new FrmCategories();
            frmCategories.Show();
            this.Hide();
        }

        private void btnBanksForm_Click(object sender, EventArgs e)
        {
            FrmBanks frmBanks = new FrmBanks();
            frmBanks.Show();
            this.Hide();
        }

        private void btnBillsForm_Click(object sender, EventArgs e)
        {
            FrmBilling frmBilling = new FrmBilling();
            frmBilling.Show();
            this.Hide();
        }

        private void btnBankProccesForm_Click(object sender, EventArgs e)
        {
            FrmBankProcess frmBankProcess = new FrmBankProcess();
            frmBankProcess.Show();
            this.Hide();
        }

        private void btnDashboardForm_Click(object sender, EventArgs e)
        {
            FrmDashboard frmDashboard = new FrmDashboard();
            frmDashboard.Show();
            this.Hide();
        }

        private void btnSettingsForm_Click(object sender, EventArgs e)
        {
            FrmSettings frmSettings = new FrmSettings();
            frmSettings.Show();
            this.Hide();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            FrmLogin frmLogin = new FrmLogin();
            frmLogin.Show();
            this.Hide();
        }
    }
}