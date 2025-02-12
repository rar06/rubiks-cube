namespace rubiks_cube
{
    partial class MainFrame
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.B1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.B2 = new System.Windows.Forms.Button();
            this.B3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // B1
            // 
            this.B1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.B1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.B1.ForeColor = System.Drawing.Color.DarkRed;
            this.B1.Location = new System.Drawing.Point(307, 125);
            this.B1.Name = "B1";
            this.B1.Size = new System.Drawing.Size(205, 71);
            this.B1.TabIndex = 1;
            this.B1.Text = "Rubiks Cube Solver";
            this.B1.UseVisualStyleBackColor = false;
            this.B1.Click += new System.EventHandler(this.LoadSolver);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(238, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(293, 46);
            this.label1.TabIndex = 3;
            this.label1.Text = "Cube Odyssey";
            // 
            // B2
            // 
            this.B2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.B2.ForeColor = System.Drawing.Color.DarkRed;
            this.B2.Location = new System.Drawing.Point(307, 211);
            this.B2.Name = "B2";
            this.B2.Size = new System.Drawing.Size(205, 74);
            this.B2.TabIndex = 4;
            this.B2.Text = "Cube Play";
            this.B2.UseVisualStyleBackColor = true;
            this.B2.Click += new System.EventHandler(this.LoadCubePlay);
            // 
            // B3
            // 
            this.B3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.B3.ForeColor = System.Drawing.Color.DarkRed;
            this.B3.Location = new System.Drawing.Point(307, 303);
            this.B3.Name = "B3";
            this.B3.Size = new System.Drawing.Size(205, 58);
            this.B3.TabIndex = 5;
            this.B3.Text = "Helper Cube";
            this.B3.UseVisualStyleBackColor = true;
            this.B3.Click += new System.EventHandler(this.LoadHelper);
            // 
            // MainFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::rubiks_cube.Properties.Resources.unnamed__1_;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.B3);
            this.Controls.Add(this.B2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.B1);
            this.Name = "MainFrame";
            this.Text = "Game menu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button B1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button B2;
        private System.Windows.Forms.Button B3;
    }
}

