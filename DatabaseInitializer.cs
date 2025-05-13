using System;
using System.Data;
using Microsoft.Data.Sqlite;
using System.Windows.Forms;

namespace POStest
{
    public class DatabaseInitializer
    {
        private string Data;

        public DatabaseInitializer(string data)
        {
            Data = data;
        }

        public void InitializeDatabase()
        {
            using (var connection = new SqliteConnection(Data))
            {
                connection.Open();
                string createProductsTable = @"
                    CREATE TABLE IF NOT EXISTS Products (
                        Image BLOB,
                        ID INTEGER PRIMARY KEY UNIQUE,
                        Name TEXT NOT NULL,
                        Price REAL NOT NULL,
                        SellingPrice REAL NOT NULL
                    )";
                using (var createProducts = new SqliteCommand(createProductsTable, connection))
                {
                    createProducts.ExecuteNonQuery();
                }

                string createTableSaleHistory = @"
                    CREATE TABLE IF NOT EXISTS Sale_History (
                        Date TEXT PRIMARY KEY,
                        DailyEarnings REAL NOT NULL,
                        TotalEarnings REAL NOT NULL
                    )";
                using (var createSaleHistory = new SqliteCommand(createTableSaleHistory, connection))
                {
                    createSaleHistory.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void RetrieveProductsData(string Data, DataTable dataTable, DataGridView dataGridViewProducts)
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
                            dataTable = new DataTable(); // Initialize dataTable
                            dataTable.Load(reader);

                            // Manually define columns in the desired order
                            dataGridViewProducts.AutoGenerateColumns = false;
                            dataGridViewProducts.Columns.Clear();

                            var imageColumn = new DataGridViewImageColumn
                            {
                                DataPropertyName = "Image",
                                HeaderText = "Image",
                                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                            };
                            dataGridViewProducts.Columns.Add(imageColumn);

                            var idColumn = new DataGridViewTextBoxColumn
                            {
                                DataPropertyName = "ID",
                                HeaderText = "ID",
                                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                            };
                            dataGridViewProducts.Columns.Add(idColumn);

                            var nameColumn = new DataGridViewTextBoxColumn
                            {
                                DataPropertyName = "Name",
                                HeaderText = "Name",
                                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                            };
                            dataGridViewProducts.Columns.Add(nameColumn);

                            var priceColumn = new DataGridViewTextBoxColumn
                            {
                                DataPropertyName = "Price",
                                HeaderText = "Price",
                                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                            };
                            dataGridViewProducts.Columns.Add(priceColumn);

                            var sellingPriceColumn = new DataGridViewTextBoxColumn
                            {
                                DataPropertyName = "SellingPrice",
                                HeaderText = "Selling Price",
                                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                            };
                            dataGridViewProducts.Columns.Add(sellingPriceColumn);

                            // Bind the DataGridView directly to the dataTable
                            dataGridViewProducts.DataSource = dataTable;
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while retrieving products data: {ex.Message}");
            }
        }

        public void RetrieveSaleHistoryData(DataTable dataTable, DataGridView dataGridViewSaleHistory)
        {
            try
            {
                using (var connection = new SqliteConnection(Data))
                {
                    connection.Open();
                    string selectQuery = "SELECT * FROM Sale_History";
                    using (var command = new SqliteCommand(selectQuery, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            dataTable.Load(reader);
                            dataGridViewSaleHistory.DataSource = dataTable;
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while retrieving sale history data: {ex.Message}");
            }
        }
    }
}