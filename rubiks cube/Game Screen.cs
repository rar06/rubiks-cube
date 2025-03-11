using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace rubiks_cube
{
    public partial class Game_Screen : Form
    {
        private DataGridView dataGridView1;
        private DataGridView dataGridView2; // Second cube
        private DataGridView dataGridView3;
        private DataGridView dataGridView4;
        private DataGridView dataGridView5;
        private DataGridView dataGridView6;
        private Panel colorPanel;
        private Color selectedColor = Color.White;

        public Game_Screen()
        {
            InitializeComponent();

            // Set the size of the form (Width x Height)
            this.Size = new Size(800, 600); // Adjust the size as needed

            // Optionally, center the form on the screen
            this.StartPosition = FormStartPosition.CenterScreen;
            ConfigureDataGridView();
            ConfigureColorPanel();
        }

        private void ConfigureDataGridView()
        {
            int cellSize = 50;
            int gridSize = cellSize * 3;
            int gap = 10;

            // Calculate positions based on the form's size
            int startX = (this.ClientSize.Width - (2 * gridSize + gap)) / 2;
            int startY = (this.ClientSize.Height - (3 * gridSize + 2 * gap)) / 2;

            Point[] positions = new Point[]
            {
        new Point(startX, startY),                       // First row, first column
        new Point(startX + gridSize + gap, startY),      // First row, second column
        new Point(startX, startY + gridSize + gap),      // Second row, first column
        new Point(startX + gridSize + gap, startY + gridSize + gap), // Second row, second column
        new Point(startX, startY + 2 * (gridSize + gap)),    // Third row, first column
        new Point(startX + gridSize + gap, startY + 2 * (gridSize + gap)) // Third row, second column
            };

            // Create DataGridViews with correct positioning
            dataGridView1 = new DataGridView();
            ConfigureSingleDataGridView(dataGridView1, Color.Red);
            dataGridView1.Location = positions[0];
            Controls.Add(dataGridView1);

            dataGridView2 = new DataGridView();
            ConfigureSingleDataGridView(dataGridView2, Color.Blue);
            dataGridView2.Location = positions[1];
            Controls.Add(dataGridView2);

            dataGridView3 = new DataGridView();
            ConfigureSingleDataGridView(dataGridView3, Color.Green);
            dataGridView3.Location = positions[2];
            Controls.Add(dataGridView3);

            dataGridView4 = new DataGridView();
            ConfigureSingleDataGridView(dataGridView4, Color.Yellow);
            dataGridView4.Location = positions[3];
            Controls.Add(dataGridView4);

            dataGridView5 = new DataGridView();
            ConfigureSingleDataGridView(dataGridView5, Color.Orange);
            dataGridView5.Location = positions[4];
            Controls.Add(dataGridView5);

            dataGridView6 = new DataGridView();
            ConfigureSingleDataGridView(dataGridView6, Color.Navy);
            dataGridView6.Location = positions[5];
            Controls.Add(dataGridView6);
        }

        private void ConfigureSingleDataGridView(DataGridView dgv, Color color)
        {
            int cellSize = 50;

            // Set columns and rows
            dgv.ColumnCount = 3;
            dgv.AllowUserToAddRows = false;
            dgv.RowHeadersVisible = false;
            dgv.ColumnHeadersVisible = false;

            // Set size and appearance
            dgv.RowTemplate.Height = cellSize;
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                column.Width = cellSize;
            }

            dgv.ReadOnly = true;
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
            //  dgv.DefaultCellStyle.BackColor = color;
            //dgv.DefaultCellStyle.ForeColor = color;
            // dgv.DefaultCellStyle.ForeColor = Color.Black;
            dgv.DefaultCellStyle.SelectionBackColor = color;


            dgv.AutoSize = false;
            dgv.ScrollBars = ScrollBars.None;
            dgv.Size = new Size(cellSize * 3, cellSize * 3);
            dgv.Dock = DockStyle.None;
            dgv.Anchor = AnchorStyles.None;

            // Manually add rows
            for (int i = 0; i < 3; i++)
            {
                dgv.Rows.Add();
            }

            // Set random Rubik's Cube colors for each grid
            SetRubiksCubeColors(dgv, color);

            dgv.CellClick += DataGridView_CellClick;
        }

        private void SetRubiksCubeColors(DataGridView dgv, Color color)
        {
            // Example: Set colors differently for each cube
            dgv.Rows[0].Cells[0].Style.BackColor = color;
            dgv.Rows[0].Cells[1].Style.BackColor = color;
            dgv.Rows[0].Cells[2].Style.BackColor = color;

            dgv.Rows[1].Cells[0].Style.BackColor = color;
            dgv.Rows[1].Cells[1].Style.BackColor = color;
            dgv.Rows[1].Cells[2].Style.BackColor = color;

            dgv.Rows[2].Cells[0].Style.BackColor = color;
            dgv.Rows[2].Cells[1].Style.BackColor = color;
            dgv.Rows[2].Cells[2].Style.BackColor = color;
        }

        private void ConfigureColorPanel()
        {
            colorPanel = new Panel();

            // Set the size of the color panel
            colorPanel.Size = new Size(300, 50); // Width x Height

            // Position the panel at the bottom of the form
            int panelX = (this.ClientSize.Width - colorPanel.Width) / 2; // Center horizontally
            int panelY = this.ClientSize.Height - colorPanel.Height; // Place at the bottom
            colorPanel.Location = new Point(panelX, panelY);

            // Add the panel to the form
            Controls.Add(colorPanel);

            // Define colors for Rubik's Cube
            Color[] rubiksColors = { Color.White, Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange };

            for (int i = 0; i < rubiksColors.Length; i++)
            {
                Button colorButton = new Button();
                colorButton.BackColor = rubiksColors[i];
                colorButton.Size = new Size(40, 40);
                colorButton.Location = new Point(i * 45, 5); // Position buttons within the panel
                colorButton.Click += ColorButton_Click;
                colorPanel.Controls.Add(colorButton);
            }
        }

        private void ColorButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                selectedColor = clickedButton.BackColor;
            }
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv != null && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                /*
                MessageBox.Show($"DataGridView: {dgv}\n" +
                $"Row: {e.RowIndex}, Column: {e.ColumnIndex}\n" +
                $"Current Color: {dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor}\n" +
                $"Selected Color: {selectedColor}");
                */
                dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = selectedColor;
                dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionBackColor = selectedColor;
            }
        }
    }
}

