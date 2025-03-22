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
            ConfigureSingleDataGridView(dataGridView1, Color.Red);
            dataGridView1.Location = positions[0];
            Controls.Add(dataGridView1);

            dataGridView2 = new DataGridView(); // Right face
            ConfigureSingleDataGridView(dataGridView2, Color.Blue);
            dataGridView2.Location = positions[1];
            Controls.Add(dataGridView2);

            dataGridView3 = new DataGridView(); // Bottom face
            ConfigureSingleDataGridView(dataGridView3, Color.Green);
            dataGridView3.Location = positions[2];
            Controls.Add(dataGridView3);

            dataGridView4 = new DataGridView(); // Left face
            ConfigureSingleDataGridView(dataGridView4, Color.Yellow);
            dataGridView4.Location = positions[3];
            Controls.Add(dataGridView4);

            dataGridView5 = new DataGridView(); // Front face
            ConfigureSingleDataGridView(dataGridView5, Color.White);
            dataGridView5.Location = positions[4];
            Controls.Add(dataGridView5);

            dataGridView6 = new DataGridView(); // Back face
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
                UpdateCubeUI();
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
            // Rotate the front face clockwise
            RotateFaceClockwise(4); // Front face is index 4
                                    // Adjust adjacent faces (top, right, bottom, left)
            char[] temp = { cube.Faces[0][2, 0], cube.Faces[0][2, 1], cube.Faces[0][2, 2] };
            cube.Faces[0][2, 0] = cube.Faces[3][2, 2];
            cube.Faces[0][2, 1] = cube.Faces[3][1, 2];
            cube.Faces[0][2, 2] = cube.Faces[3][0, 2];
            cube.Faces[3][0, 2] = cube.Faces[2][0, 0];
            cube.Faces[3][1, 2] = cube.Faces[2][0, 1];
            cube.Faces[3][2, 2] = cube.Faces[2][0, 2];
            cube.Faces[2][0, 0] = cube.Faces[1][2, 0];
            cube.Faces[2][0, 1] = cube.Faces[1][1, 0];
            cube.Faces[2][0, 2] = cube.Faces[1][0, 0];
            cube.Faces[1][0, 0] = temp[0];
            cube.Faces[1][1, 0] = temp[1];
            cube.Faces[1][2, 0] = temp[2];
        }

        private void RotateFrontCounterClockwise()
        {
            // Rotate the front face counter-clockwise
            RotateFaceCounterClockwise(4); // Front face is index 4
                                           // Adjust adjacent faces (top, right, bottom, left)
            char[] temp = { cube.Faces[0][2, 0], cube.Faces[0][2, 1], cube.Faces[0][2, 2] };
            cube.Faces[0][2, 0] = cube.Faces[1][0, 0];
            cube.Faces[0][2, 1] = cube.Faces[1][1, 0];
            cube.Faces[0][2, 2] = cube.Faces[1][2, 0];
            cube.Faces[1][0, 0] = cube.Faces[2][0, 2];
            cube.Faces[1][1, 0] = cube.Faces[2][0, 1];
            cube.Faces[1][2, 0] = cube.Faces[2][0, 0];
            cube.Faces[2][0, 0] = cube.Faces[3][2, 2];
            cube.Faces[2][0, 1] = cube.Faces[3][1, 2];
            cube.Faces[2][0, 2] = cube.Faces[3][0, 2];
            cube.Faces[3][0, 2] = temp[2];
            cube.Faces[3][1, 2] = temp[1];
            cube.Faces[3][2, 2] = temp[0];
        }

        // Implement similar methods for other moves (R, U, B, L, D, R', U', B', L', D')

        private void RotateFaceClockwise(int faceIndex)
        {
            char[,] face = cube.Faces[faceIndex];
            char[,] newFace = new char[3, 3];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    newFace[i, j] = face[2 - j, i];
                }
            }

            cube.Faces[faceIndex] = newFace;
        }

        private void RotateFaceCounterClockwise(int faceIndex)
        {
            char[,] face = cube.Faces[faceIndex];
            char[,] newFace = new char[3, 3];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    newFace[i, j] = face[j, 2 - i];
                }
            }

            cube.Faces[faceIndex] = newFace;
        }

        private void UpdateCubeUI()
        {
            // Update the UI to reflect the cube's state
            UpdateDataGridView(dataGridView1, cube.Faces[0]); // Top face
            UpdateDataGridView(dataGridView2, cube.Faces[1]); // Right face
            UpdateDataGridView(dataGridView3, cube.Faces[2]); // Bottom face
            UpdateDataGridView(dataGridView4, cube.Faces[3]); // Left face
            UpdateDataGridView(dataGridView5, cube.Faces[4]); // Front face
            UpdateDataGridView(dataGridView6, cube.Faces[5]); // Back face
        }

        private void UpdateDataGridView(DataGridView dgv, char[,] face)
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    dgv.Rows[row].Cells[col].Style.BackColor = GetColorFromChar(face[row, col]);
                }
            }
        }

        private Color GetColorFromChar(char colorChar)
        {
            switch (colorChar)
            {
                case 'R': return Color.Red;
                case 'B': return Color.Blue;
                case 'G': return Color.Green;
                case 'Y': return Color.Yellow;
                case 'O': return Color.Orange;
                case 'N': return Color.Navy;
                case 'W': return Color.White;
                default: throw new ArgumentException("Invalid color character");
            }
        }
    }
}