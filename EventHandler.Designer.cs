using System.Windows.Forms;

namespace POStest
{
    partial class EventHandler
    {
        private System.ComponentModel.IContainer components = null;
        private Button btnAdd;
        private Button btnDelete; // Declare the Delete button
        private TextBox txtSearch; // Declare the Search TextBox
        private TabControl tabControl;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private DataGridView dataGridViewProducts;
        private DataGridView dataGridViewSaleHistory;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button(); // Initialize the Delete button
            this.txtSearch = new System.Windows.Forms.TextBox(); // Initialize the Search TextBox
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridViewProducts = new System.Windows.Forms.DataGridView();
            this.dataGridViewSaleHistory = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSaleHistory)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            //
            // btnAdd
            //
            this.btnAdd.Location = new System.Drawing.Point(100, 300);
            this.btnAdd.Name = "Add Button";
            this.btnAdd.Size = new System.Drawing.Size(100, 50);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add Entry";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.Add_Click);
            //
            // btnDelete
            //
            this.btnDelete.Location = new System.Drawing.Point(100, 360); // Adjust the location as needed
            this.btnDelete.Name = "Delete Button";
            this.btnDelete.Size = new System.Drawing.Size(100, 50);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "Delete Entry";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.Delete_Click);
            //
            // txtSearch
            //
            this.txtSearch.Location = new System.Drawing.Point(250, 20); // Adjust the location as needed
            this.txtSearch.Name = "Search TextBox";
            this.txtSearch.Size = new System.Drawing.Size(200, 22);
            this.txtSearch.TabIndex = 3;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            //
            // dataGridViewProducts
            //
            this.dataGridViewProducts.Location = new System.Drawing.Point(250, 60); // Adjust the location as needed
            this.dataGridViewProducts.Name = "dataGridViewProducts";
            this.dataGridViewProducts.Size = new System.Drawing.Size(900, 600); // Increase the size of the DataGridView
            this.dataGridViewProducts.TabIndex = 4;
            this.dataGridViewProducts.ReadOnly = false; // Ensure the DataGridView is editable
            this.dataGridViewProducts.AutoGenerateColumns = false;
            this.dataGridViewProducts.Columns.Add(new DataGridViewImageColumn { HeaderText = "Image", DataPropertyName = "Image" });
            this.dataGridViewProducts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "ID" });
            this.dataGridViewProducts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Name", DataPropertyName = "Name" });
            this.dataGridViewProducts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Price", DataPropertyName = "Price" });
            this.dataGridViewProducts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Selling Price", DataPropertyName = "Selling Price" });
            //
            // dataGridViewSaleHistory
            //
            this.dataGridViewSaleHistory.Location = new System.Drawing.Point(250, 50);
            this.dataGridViewSaleHistory.Name = "dataGridViewSaleHistory";
            this.dataGridViewSaleHistory.Size = new System.Drawing.Size(900, 600); // Increase the size of the DataGridView
            this.dataGridViewSaleHistory.TabIndex = 5;
            this.dataGridViewSaleHistory.ReadOnly = true; // Ensure the DataGridView is read-only
            this.dataGridViewSaleHistory.AutoGenerateColumns = false;
            this.dataGridViewSaleHistory.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "ID" });
            this.dataGridViewSaleHistory.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Date", DataPropertyName = "Date" });
            this.dataGridViewSaleHistory.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Daily Earnings", DataPropertyName = "Daily Earnings" });
            this.dataGridViewSaleHistory.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Total Earnings", DataPropertyName = "Total Earnings" });
            //
            // tabControl
            //
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Location = new System.Drawing.Point(12, 31);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1200, 700); // Increase the size of the tabControl
            this.tabControl.TabIndex = 6;
            //
            // tabPage1
            //
            this.tabPage1.Controls.Add(this.btnAdd);
            this.tabPage1.Controls.Add(this.btnDelete); // Add the Delete button to the tabPage1
            this.tabPage1.Controls.Add(this.txtSearch); // Add the Search TextBox to the tabPage1
            this.tabPage1.Controls.Add(this.dataGridViewProducts);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1192, 671); // Increase the size of the tabPage1
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Products";
            this.tabPage1.UseVisualStyleBackColor = true;
            //
            // tabPage2
            //
            this.tabPage2.Controls.Add(this.dataGridViewSaleHistory);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1192, 671); // Increase the size of the tabPage2
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Sale History";
            this.tabPage2.UseVisualStyleBackColor = true;
            //
            // EventHandler
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1224, 743); // Increase the size of the Form
            this.Controls.Add(this.tabControl);
            this.Name = "EventHandler";
            this.Text = "POS Test";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSaleHistory)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout(); // Ensure the controls are properly laid out
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}