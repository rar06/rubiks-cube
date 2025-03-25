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
        private Button scrambleButton;
        private Button resetColorButton;
        private Panel colorPanel;
        private Color selectedColor = Color.White;
        private Random random = new Random();

        public Game_Screen()
        {
            InitializeComponent();

            // Set the size of the form (Width x Height)
            this.Size = new Size(1000, 800); // Adjust the size as needed

            // Optionally, center the form on the screen
            this.StartPosition = FormStartPosition.CenterScreen;
            ConfigureDataGridView();
            ConfigureColorPanel();
            ConfigureResetColorButton();
            ConfigureMoveButtons();
            ConfigureScrambleButton();
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
            for(int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    dgv.Rows[i].Cells[j].Style.BackColor = color;
                    dgv.Rows[i].Cells[j].Style.SelectionBackColor = color;
                }
            }
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
            Color[] rubiksColors = { Color.White, Color.Red, Color.Yellow, Color.Orange, Color.Green, Color.Blue };

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

        private void ConfigureResetColorButton()
        {
            resetColorButton = new Button();
            resetColorButton.Text = "Reset Colors";
            resetColorButton.Size = new Size(100, 40);

            // Positioning the button at the bottom center
            int buttonWidth = resetColorButton.Width;
            int buttonHeight = resetColorButton.Height;
            int spacing = 20; // Space between buttons

            int totalWidth = (buttonWidth * 2) + spacing;
            int startX = (this.ClientSize.Width - totalWidth) / 2;
            int buttonY = this.ClientSize.Height - buttonHeight - 60;

            resetColorButton.Location = new Point(startX + buttonWidth + spacing, buttonY);
            resetColorButton.Click += resetColorButton_Click;
            Controls.Add(resetColorButton);
        }

        private void resetColorButton_Click(object sender, EventArgs e)
        {
            ConfigureSingleDataGridView(dataGridView1, Color.White);

            ConfigureSingleDataGridView(dataGridView2, Color.Red);

            ConfigureSingleDataGridView(dataGridView3, Color.Yellow);

            ConfigureSingleDataGridView(dataGridView4, Color.Orange);

            ConfigureSingleDataGridView(dataGridView5, Color.Green);

            ConfigureSingleDataGridView(dataGridView6, Color.Blue);
        }

        private void ConfigureMoveButtons()
        {
            string[] moves = { "F", "R", "U", "B", "L", "D", "F'", "R'", "U'", "B'", "L'", "D'" };
            int startX = 150; // Left-aligned
            int startY = 20; // Top of the form
            int buttonWidth = 50;
            int buttonHeight = 30;
            int gap = 10;

            for (int i = 0; i < moves.Length; i++)
            {
                Button moveButton = new Button();
                moveButton.Text = moves[i];
                moveButton.Size = new Size(buttonWidth, buttonHeight);

                // Arrange buttons in a row
                moveButton.Location = new Point(startX + i * (buttonWidth + gap), startY);

                moveButton.Click += MoveButton_Click;
                Controls.Add(moveButton);
            }
        }


        private void ConfigureScrambleButton()
        {
            scrambleButton = new Button();
            scrambleButton.Text = "Scramble";
            scrambleButton.Size = new Size(100, 40);

            // Positioning the scramble button to the left of resetColorButton
            int buttonWidth = scrambleButton.Width;
            int buttonHeight = scrambleButton.Height;
            int spacing = 20; // Space between buttons

            int totalWidth = (buttonWidth * 2) + spacing;
            int startX = (this.ClientSize.Width - totalWidth) / 2;
            int buttonY = this.ClientSize.Height - buttonHeight - 60;

            scrambleButton.Location = new Point(startX, buttonY);
            scrambleButton.Click += scrambleButton_Click;
            Controls.Add(scrambleButton);
        }


        private void scrambleButton_Click(object sender, EventArgs e)
        {
            string[] validMoves = { "F", "R", "U", "B", "L", "D", "F'", "R'", "U'", "B'", "L'", "D'" };
            int scrambleLength = random.Next()%20 + 1; // Number of moves in the scramble

            for (int i = 0; i < scrambleLength; i++)
            {
                string move = validMoves[random.Next(validMoves.Length)];
                ApplyMove(move);
            }
        }

        private void ApplyMove(string move)
        {
            // Apply the move to the DataGridViews (simulating a valid rotation)
            switch (move)
            {
                case "F": RotateFrontClockwise(); break;
                case "R": RotateRightClockwise(); break;
                case "U": RotateUpClockwise(); break;
                case "B": RotateBackClockwise(); break;
                case "L": RotateLeftClockwise(); break;
                case "D": RotateDownClockwise(); break;
                case "F'": RotateFrontCounterClockwise(); break;
                case "R'": RotateRightCounterClockwise(); break;
                case "U'": RotateUpCounterClockwise(); break;
                case "B'": RotateBackCounterClockwise(); break;
                case "L'": RotateLeftCounterClockwise(); break;
                case "D'": RotateDownCounterClockwise(); break;
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
                dataGridView1.Rows[2].Cells[i].Style.BackColor = dataGridView4.Rows[i].Cells[2].Style.BackColor;
                dataGridView1.Rows[2].Cells[i].Style.SelectionBackColor = dataGridView4.Rows[i].Cells[2].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView4.Rows[i].Cells[2].Style.BackColor = dataGridView3.Rows[0].Cells[i].Style.BackColor;
                dataGridView4.Rows[i].Cells[2].Style.SelectionBackColor = dataGridView3.Rows[0].Cells[i].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView3.Rows[0].Cells[i].Style.BackColor = dataGridView2.Rows[i].Cells[0].Style.BackColor;
                dataGridView3.Rows[0].Cells[i].Style.SelectionBackColor = dataGridView2.Rows[i].Cells[0].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView2.Rows[i].Cells[0].Style.BackColor = temp[i];
                dataGridView2.Rows[i].Cells[0].Style.SelectionBackColor = temp[i];
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
                dataGridView1.Rows[0].Cells[i].Style.BackColor = dataGridView2.Rows[i].Cells[2].Style.BackColor; 
                dataGridView1.Rows[0].Cells[i].Style.SelectionBackColor = dataGridView2.Rows[i].Cells[2].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView2.Rows[i].Cells[2].Style.BackColor = dataGridView3.Rows[2].Cells[i].Style.BackColor;
                dataGridView2.Rows[i].Cells[2].Style.SelectionBackColor = dataGridView3.Rows[2].Cells[i].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView3.Rows[2].Cells[i].Style.BackColor = dataGridView4.Rows[i].Cells[0].Style.BackColor;
                dataGridView3.Rows[2].Cells[i].Style.SelectionBackColor = dataGridView4.Rows[i].Cells[0].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView4.Rows[i].Cells[0].Style.BackColor = temp[i];
                dataGridView4.Rows[i].Cells[0].Style.SelectionBackColor = temp[i];
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
                dataGridView1.Rows[i].Cells[2].Style.BackColor = dataGridView5.Rows[i].Cells[2].Style.BackColor; 
                dataGridView1.Rows[i].Cells[2].Style.SelectionBackColor = dataGridView5.Rows[i].Cells[2].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView5.Rows[i].Cells[2].Style.BackColor = dataGridView3.Rows[i].Cells[2].Style.BackColor;
                dataGridView5.Rows[i].Cells[2].Style.SelectionBackColor = dataGridView3.Rows[i].Cells[2].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView3.Rows[i].Cells[2].Style.BackColor = dataGridView6.Rows[i].Cells[0].Style.BackColor;
                dataGridView3.Rows[i].Cells[2].Style.SelectionBackColor = dataGridView6.Rows[i].Cells[0].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView6.Rows[i].Cells[0].Style.BackColor = temp[i];
                dataGridView6.Rows[i].Cells[0].Style.SelectionBackColor = temp[i];
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
                dataGridView1.Rows[i].Cells[0].Style.BackColor = dataGridView6.Rows[i].Cells[2].Style.BackColor;
                dataGridView1.Rows[i].Cells[0].Style.SelectionBackColor = dataGridView6.Rows[i].Cells[2].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView6.Rows[i].Cells[2].Style.BackColor = dataGridView3.Rows[i].Cells[0].Style.BackColor;
                dataGridView6.Rows[i].Cells[2].Style.SelectionBackColor = dataGridView3.Rows[i].Cells[0].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView3.Rows[i].Cells[0].Style.BackColor = dataGridView5.Rows[i].Cells[0].Style.BackColor;
                dataGridView3.Rows[i].Cells[0].Style.SelectionBackColor = dataGridView5.Rows[i].Cells[0].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView5.Rows[i].Cells[0].Style.BackColor = temp[i];
                dataGridView5.Rows[i].Cells[0].Style.SelectionBackColor = temp[i];
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
                dataGridView5.Rows[0].Cells[i].Style.BackColor = dataGridView2.Rows[0].Cells[i].Style.BackColor; 
                dataGridView5.Rows[0].Cells[i].Style.SelectionBackColor = dataGridView2.Rows[0].Cells[i].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView2.Rows[0].Cells[i].Style.BackColor = dataGridView6.Rows[0].Cells[i].Style.BackColor;
                dataGridView2.Rows[0].Cells[i].Style.SelectionBackColor = dataGridView6.Rows[0].Cells[i].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView6.Rows[0].Cells[i].Style.BackColor = dataGridView4.Rows[0].Cells[i].Style.BackColor;
                dataGridView6.Rows[0].Cells[i].Style.SelectionBackColor = dataGridView4.Rows[0].Cells[i].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView4.Rows[0].Cells[i].Style.BackColor = temp[i];
                dataGridView4.Rows[0].Cells[i].Style.SelectionBackColor = temp[i];
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
                dataGridView5.Rows[2].Cells[i].Style.BackColor = dataGridView4.Rows[2].Cells[i].Style.BackColor;
                dataGridView5.Rows[2].Cells[i].Style.SelectionBackColor = dataGridView4.Rows[2].Cells[i].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView4.Rows[2].Cells[i].Style.BackColor = dataGridView6.Rows[2].Cells[i].Style.BackColor;
                dataGridView4.Rows[2].Cells[i].Style.SelectionBackColor = dataGridView6.Rows[2].Cells[i].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView6.Rows[2].Cells[i].Style.BackColor = dataGridView2.Rows[2].Cells[i].Style.BackColor;
                dataGridView6.Rows[2].Cells[i].Style.SelectionBackColor = dataGridView2.Rows[2].Cells[i].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView2.Rows[2].Cells[i].Style.BackColor = temp[i];
                dataGridView2.Rows[2].Cells[i].Style.SelectionBackColor = temp[i];
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
                dataGridView1.Rows[2].Cells[i].Style.SelectionBackColor = dataGridView2.Rows[i].Cells[0].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView2.Rows[i].Cells[0].Style.BackColor = dataGridView3.Rows[0].Cells[i].Style.BackColor;
                dataGridView2.Rows[i].Cells[0].Style.SelectionBackColor = dataGridView3.Rows[0].Cells[i].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView3.Rows[0].Cells[i].Style.BackColor = dataGridView4.Rows[i].Cells[2].Style.BackColor;
                dataGridView3.Rows[0].Cells[i].Style.SelectionBackColor = dataGridView4.Rows[i].Cells[2].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView4.Rows[i].Cells[2].Style.BackColor = temp[i];
                dataGridView4.Rows[i].Cells[2].Style.SelectionBackColor = temp[i];
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
                dataGridView4.Rows[i].Cells[0].Style.SelectionBackColor = dataGridView3.Rows[2].Cells[i].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView3.Rows[2].Cells[i].Style.BackColor = dataGridView2.Rows[i].Cells[2].Style.BackColor;
                dataGridView3.Rows[2].Cells[i].Style.SelectionBackColor = dataGridView2.Rows[i].Cells[2].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView2.Rows[i].Cells[2].Style.BackColor = dataGridView1.Rows[0].Cells[i].Style.BackColor;
                dataGridView2.Rows[i].Cells[2].Style.SelectionBackColor = dataGridView1.Rows[0].Cells[i].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView1.Rows[0].Cells[i].Style.BackColor = temp[i];
                dataGridView1.Rows[0].Cells[i].Style.SelectionBackColor = temp[i];
            }
        }

        private void RotateRightCounterClockwise()
        {
            SwapRightAdjacentValuesCounterClockwise();
        }

        private void SwapRightAdjacentValuesCounterClockwise()
        {
            Color[] temp = new Color[3];

            for (int i = 0; i < 3; i--)
            {
                temp[i] = dataGridView6.Rows[i].Cells[0].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView6.Rows[i].Cells[0].Style.BackColor = dataGridView3.Rows[i].Cells[2].Style.BackColor;
                dataGridView6.Rows[i].Cells[0].Style.SelectionBackColor = dataGridView3.Rows[i].Cells[2].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView3.Rows[i].Cells[2].Style.BackColor = dataGridView5.Rows[i].Cells[2].Style.BackColor;
                dataGridView3.Rows[i].Cells[2].Style.SelectionBackColor = dataGridView5.Rows[i].Cells[2].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView5.Rows[i].Cells[2].Style.BackColor = dataGridView1.Rows[i].Cells[2].Style.BackColor;
                dataGridView5.Rows[i].Cells[2].Style.SelectionBackColor = dataGridView1.Rows[i].Cells[2].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView1.Rows[i].Cells[2].Style.BackColor = temp[i];
                dataGridView1.Rows[i].Cells[2].Style.SelectionBackColor = temp[i];
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
                temp[i] = dataGridView5.Rows[i].Cells[0].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView5.Rows[i].Cells[0].Style.BackColor = dataGridView3.Rows[i].Cells[0].Style.BackColor;
                dataGridView5.Rows[i].Cells[0].Style.SelectionBackColor = dataGridView3.Rows[i].Cells[0].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView3.Rows[i].Cells[0].Style.BackColor = dataGridView6.Rows[i].Cells[2].Style.BackColor;
                dataGridView3.Rows[i].Cells[0].Style.SelectionBackColor = dataGridView6.Rows[i].Cells[2].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView6.Rows[i].Cells[2].Style.BackColor = dataGridView1.Rows[i].Cells[0].Style.BackColor;
                dataGridView6.Rows[i].Cells[2].Style.SelectionBackColor = dataGridView1.Rows[i].Cells[0].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView1.Rows[i].Cells[0].Style.BackColor = temp[i];
                dataGridView1.Rows[i].Cells[0].Style.SelectionBackColor = temp[i];
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
                dataGridView5.Rows[0].Cells[i].Style.SelectionBackColor = dataGridView4.Rows[0].Cells[i].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView4.Rows[0].Cells[i].Style.BackColor = dataGridView6.Rows[0].Cells[i].Style.BackColor;
                dataGridView4.Rows[0].Cells[i].Style.SelectionBackColor = dataGridView6.Rows[0].Cells[i].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView6.Rows[0].Cells[i].Style.BackColor = dataGridView2.Rows[0].Cells[i].Style.BackColor;
                dataGridView6.Rows[0].Cells[i].Style.SelectionBackColor = dataGridView2.Rows[0].Cells[i].Style.BackColor;
            }

            for (int i = 0; i < 3; i++)
            {
                dataGridView2.Rows[0].Cells[i].Style.BackColor = temp[i];
                dataGridView2.Rows[0].Cells[i].Style.SelectionBackColor = temp[i];
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
                dataGridView5.Rows[2].Cells[i].Style.SelectionBackColor = dataGridView2.Rows[2].Cells[i].Style.BackColor;
            }
            for (int i = 0; i < 3; i++)
            {
                dataGridView2.Rows[2].Cells[i].Style.BackColor = dataGridView6.Rows[2].Cells[i].Style.BackColor;
                dataGridView2.Rows[2].Cells[i].Style.SelectionBackColor = dataGridView6.Rows[2].Cells[i].Style.BackColor;
            }
            for (int i = 0; i < 3; i++)
            {
                dataGridView6.Rows[2].Cells[i].Style.BackColor = dataGridView4.Rows[2].Cells[i].Style.BackColor;
                dataGridView6.Rows[2].Cells[i].Style.SelectionBackColor = dataGridView4.Rows[2].Cells[i].Style.BackColor;
            }
            for (int i = 0; i < 3; i++)
            {
                dataGridView4.Rows[2].Cells[i].Style.BackColor = temp[i];
                dataGridView4.Rows[2].Cells[i].Style.SelectionBackColor = temp[i];
            }
        }

    }
}