using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace POStest
{
    public partial class EventHandler : Form
    {
        private string Data = "Data Source=data.db";
        private DataTable dataTable; // Declare dataTable as a class-level variable
        private DatabaseInitializer databaseInitializer;

        public EventHandler()
        {
            InitializeComponent();
            dataTable = new DataTable(); // Initialize dataTable in the constructor
            databaseInitializer = new DatabaseInitializer(Data);
            databaseInitializer.InitializeDatabase();
            databaseInitializer.RetrieveProductsData(Data, dataTable, dataGridViewProducts);
            databaseInitializer.RetrieveSaleHistoryData(dataTable, dataGridViewSaleHistory);
            dataGridViewProducts.CellValueChanged += dataGridViewProducts_CellValueChanged;
            dataGridViewProducts.CellClick += dataGridViewProducts_ImageClick; // Add CellClick event handler
            txtSearch.TextChanged += txtSearch_TextChanged;
        }

        private void Add_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqliteConnection(Data))
                {
                    
                    //
                    string searchValue = txtSearch.Text.Trim();
                    MessageBox.Show(searchValue);
                    //


                    connection.Open();
                    string insertQuery = @"
                        INSERT INTO Products (Image, ID, Name, Price, SellingPrice) VALUES (@Image, @ID, @Name,  @Price, @SellingPrice)";
                    using (var command = new SqliteCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Image", DBNull.Value);
                        command.Parameters.AddWithValue("@ID", DBNull.Value); // ID is auto-incremented
                        command.Parameters.AddWithValue("@Name", "Unnamed Product"); // Default value for Name
                        command.Parameters.AddWithValue("@Price", 0.0); // Default value for Price
                        command.Parameters.AddWithValue("@SellingPrice", 0.0); // Default value for SellingPrice
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                // Refresh the data displayed in the DataGridView
                databaseInitializer.RetrieveProductsData(Data, dataTable, dataGridViewProducts);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while adding the product: {ex.Message}");
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (dataGridViewProducts.SelectedRows.Count > 0)
            {
                try
                {
                    using (var connection = new SqliteConnection(Data))
                    {
                        connection.Open();
                        string selectQuery = "SELECT * FROM Products";
                        using (var command = new SqliteCommand(selectQuery, connection))
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                var dataTable = new DataTable();
                                dataTable.Load(reader);
                                string currentID = dataTable.Rows[dataGridViewProducts.SelectedRows[0].Index]["ID"]?.ToString() ?? string.Empty;
                                MessageBox.Show($"Deleting product with ID: {currentID}");
                                string deleteQuery = $"DELETE FROM Products WHERE ID = {currentID}";
                                using (var delete = new SqliteCommand(deleteQuery, connection))
                                {
                                    delete.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                    // Refresh the data displayed in the DataGridView
                    databaseInitializer.RetrieveProductsData(Data, dataTable, dataGridViewProducts);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while deleting the product: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }
        }

        private void dataGridViewProducts_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            try {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    using (var connection = new SqliteConnection(Data))
                    {
                        connection.Open();
                        string selectQuery = "SELECT * FROM Products";
                        using (var command = new SqliteCommand(selectQuery, connection))
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                var dataTable = new DataTable();
                                dataTable.Load(reader);
                                string ColName = dataTable.Columns[e.ColumnIndex].ColumnName;
                                string currentID = dataTable.Rows[e.RowIndex]["ID"]?.ToString() ?? string.Empty;
                                //
                                string originalValue = dataTable.Rows[e.RowIndex][ColName]?.ToString() ?? string.Empty;
                                //
                                string updatedvalue = dataGridViewProducts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString() ?? string.Empty;
                                
                                if (ColName == "SellingPrice") {
                                    using (var retrievePrice = new SqliteCommand($"SELECT Price FROM Products WHERE ID = {currentID}", connection))
                                    {
                                        double price = Convert.ToDouble(retrievePrice.ExecuteScalar());
                                        double SellingPrice = Convert.ToDouble(updatedvalue);

                                        if (SellingPrice < price)
                                        {
                                            MessageBox.Show($"Selling Price cannot be less than the Price.");
                                            using (var update = new SqliteCommand($"UPDATE Products SET SellingPrice = @value WHERE ID = {currentID}", connection))
                                            {
                                                update.Parameters.AddWithValue("@value", originalValue);
                                                update.ExecuteNonQuery();
                                            }
                                        }
                                        else {
                                            string updateQuery = $"UPDATE Products SET {ColName} = @value WHERE ID = {currentID}";

                                            using (var update = new SqliteCommand(updateQuery, connection))
                                            {
                                                update.Parameters.AddWithValue("@value", updatedvalue);
                                                update.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    string updateQuery = $"UPDATE Products SET {ColName} = @value WHERE ID = {currentID}";

                                    using (var update = new SqliteCommand(updateQuery, connection))
                                    {
                                        update.Parameters.AddWithValue("@value", updatedvalue);
                                        update.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                        connection.Close();
                    }
                    // Call RetrieveProductsData() to refresh the data displayed in the DataGridView
                    databaseInitializer.RetrieveProductsData(Data, dataTable, dataGridViewProducts);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating the product: {ex.Message}");
            }    
        }

        private void dataGridViewProducts_ImageClick(object? sender, DataGridViewCellEventArgs e)
        {
            try {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    using (var connection = new SqliteConnection(Data))
                    {
                        connection.Open();
                        string selectQuery = "SELECT * FROM Products";
                        using (var command = new SqliteCommand(selectQuery, connection))
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                var dataTable = new DataTable();
                                dataTable.Load(reader);
                                string ColName = dataTable.Columns[e.ColumnIndex].ColumnName;
                                string currentValue = dataGridViewProducts.CurrentCell?.Value?.ToString() ?? string.Empty;
                                string currentID = dataTable.Rows[e.RowIndex]["ID"]?.ToString() ?? string.Empty;

                                byte[]? updatedImageData = null;
                                string updateQuery = $"UPDATE Products SET {ColName} = @Image WHERE ID = {currentID}";

                                if (ColName == "Image")
                                {
                                    using (OpenFileDialog openFileDialog = new OpenFileDialog())
                                    {
                                        openFileDialog.Filter = "Image files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*";
                                        openFileDialog.FilterIndex = 1;
                                        openFileDialog.RestoreDirectory = true;
                                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                                        {
                                            string selectedFile = openFileDialog.FileName;

                                            using (var ms = new System.IO.MemoryStream())
                                            {
                                                Image.FromFile(selectedFile).Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                                                updatedImageData = ms.ToArray();
                                            }
                                        }
                                    }

                                    using (var update = new SqliteCommand(updateQuery, connection))
                                    {
                                        update.Parameters.AddWithValue("@Image", updatedImageData);
                                        update.ExecuteNonQuery();
                                        
                                       // Call RetrieveProductsData() to refresh the data displayed in the DataGridView
                                        databaseInitializer.RetrieveProductsData(Data, dataTable, dataGridViewProducts);
                                    }
                                }
                            }
                        }
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while retrieving the product: {ex.Message}");
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string searchValue = txtSearch.Text.Trim();

                if (!string.IsNullOrEmpty(searchValue))
                {
                    // Example: Filter rows in the DataGridView based on the search value
                    foreach (DataGridViewRow row in dataGridViewProducts.Rows)
                    {
                        MessageBox.Show($"An error occurred while filtering: {row}");
                        if (row.Cells["Name"].Value != null &&
                            row.Cells["Name"].Value.ToString().IndexOf(searchValue, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            row.Visible = true; // Show rows that match the search
                        }
                        else
                        {
                            row.Visible = false; // Hide rows that don't match the search
                        }
                    }
                }
                else
                {
                    // If the search box is empty, show all rows
                    foreach (DataGridViewRow row in dataGridViewProducts.Rows)
                    {
                        row.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while filtering: {ex.Message}");
            }
        }
    }
}