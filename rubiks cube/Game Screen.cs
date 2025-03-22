using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private Button lockColorButton;
        private Panel colorPanel;
        private Color selectedColor = Color.White;

        public Game_Screen()
        {
            InitializeComponent();

            // Set the size of the form (Width x Height)
            this.Size = new Size(1000, 800); // Adjust the size as needed

            // Optionally, center the form on the screen
            this.StartPosition = FormStartPosition.CenterScreen;
            ConfigureDataGridView();
            ConfigureColorPanel();
            ConfigureLockColorButton();
            ConfigureMoveButtons();
        }

        private void ConfigureDataGridView()
        {
            int cellSize = 50;
            int gridSize = cellSize * 3;
            int gap = 10;

            // Calculate positions based on the form's size
            int startX = (this.ClientSize.Width - (4 * gridSize + 3 * gap)) / 2;
            int startY = (this.ClientSize.Height - (3 * gridSize + 2 * gap)) / 2;

            Point[] positions = new Point[]
            {
                new Point(startX + gridSize + gap, startY),                       // Top face
                new Point(startX + 2 * (gridSize + gap), startY + gridSize + gap), // Right face
                new Point(startX + gridSize + gap, startY + 2 * (gridSize + gap)), // Bottom face
                new Point(startX, startY + gridSize + gap),                       // Left face
                new Point(startX + gridSize + gap, startY + gridSize + gap),       // Front face
                new Point(startX + 3 * (gridSize + gap), startY + gridSize + gap)  // Back face
            };

            // Create DataGridViews with correct positioning
            dataGridView1 = new DataGridView(); // Top face
            ConfigureSingleDataGridView(dataGridView1, Color.White);
            dataGridView1.Location = positions[0];
            Controls.Add(dataGridView1);

            dataGridView2 = new DataGridView(); // Right face
            ConfigureSingleDataGridView(dataGridView2, Color.Red);
            dataGridView2.Location = positions[1];
            Controls.Add(dataGridView2);

            dataGridView3 = new DataGridView(); // Bottom face
            ConfigureSingleDataGridView(dataGridView3, Color.Yellow);
            dataGridView3.Location = positions[2];
            Controls.Add(dataGridView3);

            dataGridView4 = new DataGridView(); // Left face
            ConfigureSingleDataGridView(dataGridView4, Color.Orange);
            dataGridView4.Location = positions[3];
            Controls.Add(dataGridView4);

            dataGridView5 = new DataGridView(); // Front face
            ConfigureSingleDataGridView(dataGridView5, Color.Green);
            dataGridView5.Location = positions[4];
            Controls.Add(dataGridView5);

            dataGridView6 = new DataGridView(); // Back face
            ConfigureSingleDataGridView(dataGridView6, Color.Blue);
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
            Color[] rubiksColors = { Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.White, Color.Navy };

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
                dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = selectedColor;
                dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionBackColor = selectedColor;
            }
        }

        private void ConfigureLockColorButton()
        {
            lockColorButton = new Button();
            lockColorButton.Text = "Lock Colors";
            lockColorButton.Size = new Size(100, 40);

            // Positioning the button at the bottom center
            int buttonX = (this.ClientSize.Width - lockColorButton.Width) / 2;
            int buttonY = this.ClientSize.Height - lockColorButton.Height - 60;
            lockColorButton.Location = new Point(buttonX, buttonY);

            lockColorButton.Click += lockColorButton_Click;
            Controls.Add(lockColorButton);
        }

        private void lockColorButton_Click(object sender, EventArgs e)
        {
            // Toggle the color selection state
            if (lockColorButton.Text == "Lock Colors")
            {
                // Disable color selection
                DisableColorSelection();
                lockColorButton.Text = "Unlock Colors";
            }
            else
            {
                // Enable color selection
                EnableColorSelection();
                lockColorButton.Text = "Lock Colors";
            }
        }

        private void DisableColorSelection()
        {
            // Disable all color buttons in the color panel
            foreach (Control control in colorPanel.Controls)
            {
                if (control is Button)
                {
                    control.Enabled = false; // Disable the button
                }
            }

            // Disable cell click event for all DataGridViews
            dataGridView1.CellClick -= DataGridView_CellClick;
            dataGridView2.CellClick -= DataGridView_CellClick;
            dataGridView3.CellClick -= DataGridView_CellClick;
            dataGridView4.CellClick -= DataGridView_CellClick;
            dataGridView5.CellClick -= DataGridView_CellClick;
            dataGridView6.CellClick -= DataGridView_CellClick;
        }

        private void EnableColorSelection()
        {
            // Enable all color buttons in the color panel
            foreach (Control control in colorPanel.Controls)
            {
                if (control is Button)
                {
                    control.Enabled = true; // Enable the button
                }
            }

            // Re-enable cell click event for all DataGridViews
            dataGridView1.CellClick += DataGridView_CellClick;
            dataGridView2.CellClick += DataGridView_CellClick;
            dataGridView3.CellClick += DataGridView_CellClick;
            dataGridView4.CellClick += DataGridView_CellClick;
            dataGridView5.CellClick += DataGridView_CellClick;
            dataGridView6.CellClick += DataGridView_CellClick;
        }

        private void ConfigureMoveButtons()
        {
            // Define the moves and their positions
            string[] moves = { "F", "R", "U", "B", "L", "D", "F'", "R'", "U'", "B'", "L'", "D'" };
            int startX = this.ClientSize.Width - 120; // Right side of the form
            int startY = 20; // Top of the form
            int buttonWidth = 50;
            int buttonHeight = 30;
            int gap = 10;

            for (int i = 0; i < moves.Length; i++)
            {
                Button moveButton = new Button();
                moveButton.Text = moves[i];
                moveButton.Size = new Size(buttonWidth, buttonHeight);
                moveButton.Location = new Point(startX, startY + i * (buttonHeight + gap));
                moveButton.Click += MoveButton_Click;
                Controls.Add(moveButton);
            }
        }

        private void MoveButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                string move = clickedButton.Text;
                PerformMove(move);
            }
        }

        private void PerformMove(string move)
        {
            // Implement the logic for each move
            switch (move)
            {
                case "F":
                    RotateFrontClockwise();
                    break;
                case "R":
                    RotateRightClockwise();
                    break;
                case "U":
                    RotateUpClockwise();
                    break;
                case "B":
                    RotateBackClockwise();
                    break;
                case "L":
                    RotateLeftClockwise();
                    break;
                case "D":
                    RotateDownClockwise();
                    break;
                case "F'":
                    RotateFrontCounterClockwise();
                    break;
                case "R'":
                    RotateRightCounterClockwise();
                    break;
                case "U'":
                    RotateUpCounterClockwise();
                    break;
                case "B'":
                    RotateBackCounterClockwise();
                    break;
                case "L'":
                    RotateLeftCounterClockwise();
                    break;
                case "D'":
                    RotateDownCounterClockwise();
                    break;
            }
        }


        private void RotateFrontClockwise()
        {
            SwapFrontAdjacentValuesClosckWise();
        }

        private void SwapFrontAdjacentValuesClosckWise()
        {
            Color[] temp = new Color[3];

            for (int i = 0; i < 3; i++)
            {
                temp[i] = dataGridView1.Rows[2].Cells[i].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView1.Rows[2].Cells[i].Style.BackColor = dataGridView4.Rows[i].Cells[2].Style.BackColor; // Correct indexing
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView4.Rows[i].Cells[2].Style.BackColor = dataGridView3.Rows[0].Cells[i].Style.BackColor; // Correct indexing
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView3.Rows[0].Cells[i].Style.BackColor = dataGridView2.Rows[i].Cells[0].Style.BackColor; // Correct indexing
                if(i==0) dataGridView3.DefaultCellStyle.SelectionBackColor = dataGridView2.Rows[i].Cells[0].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView2.Rows[i].Cells[0].Style.BackColor = temp[i]; // Correct indexing
                if(i == 0) dataGridView2.DefaultCellStyle.SelectionBackColor = temp[i];
            }
        }

        private void RotateBackClockwise()
        {
            SwapBackAdjacentValuesClockwise();
        }

        private void SwapBackAdjacentValuesClockwise()
        {
            Color[] temp = new Color[3];

            for (int i = 0; i < 3; i++)
            {
                temp[i] = dataGridView1.Rows[0].Cells[i].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView1.Rows[0].Cells[i].Style.BackColor = dataGridView2.Rows[i].Cells[2].Style.BackColor; // Correct indexing
                if (i == 0) dataGridView1.DefaultCellStyle.SelectionBackColor = dataGridView2.Rows[i].Cells[2].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView2.Rows[i].Cells[2].Style.BackColor = dataGridView3.Rows[2].Cells[i].Style.BackColor; // Correct indexing
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView3.Rows[2].Cells[i].Style.BackColor = dataGridView4.Rows[i].Cells[0].Style.BackColor; // Correct indexing
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView4.Rows[i].Cells[0].Style.BackColor = temp[i]; // Correct indexing
                if (i == 0) dataGridView4.DefaultCellStyle.SelectionBackColor = temp[i];
            }
        }

        private void RotateRightClockwise()
        {
            SwapRightAdjacentValuesClockWise();
        }

        private void SwapRightAdjacentValuesClockWise()
        {
            Color[] temp = new Color[3];

            for (int i = 0; i < 3; i++)
            {
                temp[i] = dataGridView1.Rows[i].Cells[2].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView1.Rows[i].Cells[2].Style.BackColor = dataGridView5.Rows[i].Cells[2].Style.BackColor; // Correct indexing
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView5.Rows[i].Cells[2].Style.BackColor = dataGridView3.Rows[i].Cells[2].Style.BackColor; // Correct indexing
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView3.Rows[i].Cells[2].Style.BackColor = dataGridView6.Rows[i].Cells[2].Style.BackColor; // Correct indexing
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView6.Rows[i].Cells[2].Style.BackColor = temp[i]; // Correct indexing
            }
        }

        private void RotateLeftClockwise()
        {
            SwapLeftAdjacentValuesClockwise();
        }

        private void SwapLeftAdjacentValuesClockwise()
        {
            Color[] temp = new Color[3];

            for (int i = 0; i < 3; i++)
            {
                temp[i] = dataGridView1.Rows[i].Cells[0].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView1.Rows[i].Cells[0].Style.BackColor = dataGridView6.Rows[i].Cells[0].Style.BackColor; // Correct indexing
                if (i == 0) dataGridView1.DefaultCellStyle.SelectionBackColor = dataGridView6.Rows[i].Cells[0].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView6.Rows[i].Cells[0].Style.BackColor = dataGridView3.Rows[i].Cells[0].Style.BackColor; // Correct indexing
                if (i == 0) dataGridView6.DefaultCellStyle.SelectionBackColor = dataGridView3.Rows[i].Cells[0].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView3.Rows[i].Cells[0].Style.BackColor = dataGridView5.Rows[i].Cells[0].Style.BackColor; // Correct indexing
                if (i == 0) dataGridView3.DefaultCellStyle.SelectionBackColor = dataGridView5.Rows[i].Cells[0].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView5.Rows[i].Cells[0].Style.BackColor = temp[i]; // Correct indexing
                if (i == 0) dataGridView5.DefaultCellStyle.SelectionBackColor = temp[i];
            }
        }

        private void RotateUpClockwise()
        {
            SwapUpAdjacentValuesClockWise();
        }

        private void SwapUpAdjacentValuesClockWise()
        {
            Color[] temp = new Color[3];

            for (int i = 0; i < 3; i++)
            {
                temp[i] = dataGridView5.Rows[0].Cells[i].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView5.Rows[0].Cells[i].Style.BackColor = dataGridView2.Rows[0].Cells[i].Style.BackColor; // Correct indexing
                if (i == 0) dataGridView5.DefaultCellStyle.SelectionBackColor = dataGridView2.Rows[0].Cells[i].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView2.Rows[0].Cells[i].Style.BackColor = dataGridView6.Rows[0].Cells[i].Style.BackColor; // Correct indexing
                if (i == 0) dataGridView2.DefaultCellStyle.SelectionBackColor = dataGridView6.Rows[0].Cells[i].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView6.Rows[0].Cells[i].Style.BackColor = dataGridView4.Rows[0].Cells[i].Style.BackColor; // Correct indexing
                if (i == 0) dataGridView6.DefaultCellStyle.SelectionBackColor = dataGridView4.Rows[0].Cells[i].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView4.Rows[0].Cells[i].Style.BackColor = temp[i]; // Correct indexing
                if (i == 0) dataGridView4.DefaultCellStyle.SelectionBackColor = temp[i];
            }
        }

        private void RotateDownClockwise()
        {
            SwapDownAdjacentValuesClockwise();
        }

        private void SwapDownAdjacentValuesClockwise()
        {
            Color[] temp = new Color[3];

            for (int i = 0; i < 3; i++)
            {
                temp[i] = dataGridView5.Rows[2].Cells[i].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView5.Rows[2].Cells[i].Style.BackColor = dataGridView4.Rows[2].Cells[i].Style.BackColor; // Correct indexing
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView4.Rows[2].Cells[i].Style.BackColor = dataGridView6.Rows[2].Cells[i].Style.BackColor; // Correct indexing
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView6.Rows[2].Cells[i].Style.BackColor = dataGridView2.Rows[2].Cells[i].Style.BackColor; // Correct indexing
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView2.Rows[2].Cells[i].Style.BackColor = temp[i]; // Correct indexing
            }
        }

        private void RotateFrontCounterClockwise()
        {
            SwapFrontAdjacentValuesCounterClockwise();
        }

        private void SwapFrontAdjacentValuesCounterClockwise()
        {
            Color[] temp = new Color[3];

            for (int i = 0; i < 3; i++)
            {
                temp[i] = dataGridView1.Rows[2].Cells[i].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView1.Rows[2].Cells[i].Style.BackColor = dataGridView2.Rows[i].Cells[0].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView2.Rows[i].Cells[0].Style.BackColor = dataGridView3.Rows[0].Cells[i].Style.BackColor;
                if (i == 0) dataGridView2.DefaultCellStyle.SelectionBackColor = dataGridView3.Rows[0].Cells[i].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView3.Rows[0].Cells[i].Style.BackColor = dataGridView4.Rows[i].Cells[2].Style.BackColor;
                if (i == 0) dataGridView3.DefaultCellStyle.SelectionBackColor = dataGridView4.Rows[i].Cells[2].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView4.Rows[i].Cells[2].Style.BackColor = temp[i];
            }
        }

        private void RotateBackCounterClockwise()
        {
            SwapBackAdjacentValuesCounterClockwise();
        }

        private void SwapBackAdjacentValuesCounterClockwise()
        {
            Color[] temp = new Color[3];

            for (int i = 0; i < 3; i++)
            {
                temp[i] = dataGridView4.Rows[i].Cells[0].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView4.Rows[i].Cells[0].Style.BackColor = dataGridView3.Rows[2].Cells[i].Style.BackColor;
                if (i == 0) dataGridView4.DefaultCellStyle.SelectionBackColor = dataGridView3.Rows[2].Cells[i].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView3.Rows[2].Cells[i].Style.BackColor = dataGridView2.Rows[i].Cells[2].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView2.Rows[i].Cells[2].Style.BackColor = dataGridView1.Rows[0].Cells[i].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView1.Rows[0].Cells[i].Style.BackColor = temp[i];
                if (i == 0) dataGridView1.DefaultCellStyle.SelectionBackColor = temp[i];
            }
        }

        private void RotateRightCounterClockwise()
        {
            SwapRightAdjacentValuesCounterClockwise();
        }

        private void SwapRightAdjacentValuesCounterClockwise()
        {
            Color[] temp = new Color[3];

            for (int i = 0; i < 3; i++)
            {
                temp[i] = dataGridView1.Rows[i].Cells[2].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView1.Rows[i].Cells[2].Style.BackColor = dataGridView6.Rows[i].Cells[2].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView6.Rows[i].Cells[2].Style.BackColor = dataGridView3.Rows[i].Cells[2].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView3.Rows[i].Cells[2].Style.BackColor = dataGridView5.Rows[i].Cells[2].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView5.Rows[i].Cells[2].Style.BackColor = temp[i];
            }
        }

        private void RotateLeftCounterClockwise()
        {
            SwapLeftAdjacentValuesCounterClockwise();
        }

        private void SwapLeftAdjacentValuesCounterClockwise()
        {
            Color[] temp = new Color[3];
            for (int i = 0; i < 3; i++)
            {
                temp[i] = dataGridView1.Rows[i].Cells[0].Style.BackColor;
            }
            for (int i = 0; i < 3; i++)
            {
                dataGridView1.Rows[i].Cells[0].Style.BackColor = dataGridView5.Rows[i].Cells[0].Style.BackColor;
                if (i == 0) dataGridView1.DefaultCellStyle.SelectionBackColor = dataGridView5.Rows[i].Cells[0].Style.BackColor;
            }
            for (int i = 0; i < 3; i++)
            {
                dataGridView5.Rows[i].Cells[0].Style.BackColor = dataGridView3.Rows[i].Cells[0].Style.BackColor;
                if (i == 0) dataGridView5.DefaultCellStyle.SelectionBackColor = dataGridView3.Rows[i].Cells[0].Style.BackColor;
            }
            for (int i = 0; i < 3; i++)
            {
                dataGridView3.Rows[i].Cells[0].Style.BackColor = dataGridView6.Rows[i].Cells[0].Style.BackColor;
                if (i == 0) dataGridView3.DefaultCellStyle.SelectionBackColor = dataGridView6.Rows[i].Cells[0].Style.BackColor;
            }
            for (int i = 0; i < 3; i++)
            {
                dataGridView6.Rows[i].Cells[0].Style.BackColor = temp[i];
                if (i == 0) dataGridView6.DefaultCellStyle.SelectionBackColor = temp[i];
            }
        }

        private void RotateUpCounterClockwise()
        {
            SwapUpAdjacentValuesCounterClockwise();
        }

        private void SwapUpAdjacentValuesCounterClockwise()
        {
            Color[] temp = new Color[3];

            for (int i = 0; i < 3; i++)
            {
                temp[i] = dataGridView5.Rows[0].Cells[i].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView5.Rows[0].Cells[i].Style.BackColor = dataGridView4.Rows[0].Cells[i].Style.BackColor;
                if (i == 0) dataGridView5.DefaultCellStyle.SelectionBackColor = dataGridView4.Rows[0].Cells[i].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView4.Rows[0].Cells[i].Style.BackColor = dataGridView6.Rows[0].Cells[i].Style.BackColor;
                if (i == 0) dataGridView4.DefaultCellStyle.SelectionBackColor = dataGridView6.Rows[0].Cells[i].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView6.Rows[0].Cells[i].Style.BackColor = dataGridView2.Rows[0].Cells[i].Style.BackColor;
                if (i == 0) dataGridView6.DefaultCellStyle.SelectionBackColor = dataGridView2.Rows[0].Cells[i].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView2.Rows[0].Cells[i].Style.BackColor = temp[i];
                if (i == 0) dataGridView2.DefaultCellStyle.SelectionBackColor = temp[i];
            }
        }

        private void RotateDownCounterClockwise()
        {
            SwapDownAdjacentValuesCounterClockwise();
        }

        private void SwapDownAdjacentValuesCounterClockwise()
        {
            Color[] temp = new Color[3];
            for (int i = 0; i < 3; i++)
            {
                temp[i] = dataGridView5.Rows[2].Cells[i].Style.BackColor;
            }
            for (int i = 0; i < 3; i++)
            {
                dataGridView5.Rows[2].Cells[i].Style.BackColor = dataGridView2.Rows[2].Cells[i].Style.BackColor;
            }
            for (int i = 0; i < 3; i++)
            {
                dataGridView2.Rows[2].Cells[i].Style.BackColor = dataGridView6.Rows[2].Cells[i].Style.BackColor;
            }
            for (int i = 0; i < 3; i++)
            {
                dataGridView6.Rows[2].Cells[i].Style.BackColor = dataGridView4.Rows[2].Cells[i].Style.BackColor;
            }
            for (int i = 0; i < 3; i++)
            {
                dataGridView4.Rows[2].Cells[i].Style.BackColor = temp[i];
            }
        }

    }
}