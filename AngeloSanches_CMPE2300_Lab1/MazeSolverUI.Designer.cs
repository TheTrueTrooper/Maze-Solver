namespace AngeloSanches_CMPE2300_Lab1
{
    partial class MazeSolver
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
            this.Bu_Load = new System.Windows.Forms.Button();
            this.Bu_Solve = new System.Windows.Forms.Button();
            this.NumUD_Speed = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.Lb_StepsReturn = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.NumUD_Speed)).BeginInit();
            this.SuspendLayout();
            // 
            // Bu_Load
            // 
            this.Bu_Load.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Bu_Load.Location = new System.Drawing.Point(16, 15);
            this.Bu_Load.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Bu_Load.Name = "Bu_Load";
            this.Bu_Load.Size = new System.Drawing.Size(485, 50);
            this.Bu_Load.TabIndex = 0;
            this.Bu_Load.Text = "Load Maze";
            this.Bu_Load.UseVisualStyleBackColor = true;
            this.Bu_Load.Click += new System.EventHandler(this.Bu_Load_Click);
            // 
            // Bu_Solve
            // 
            this.Bu_Solve.Enabled = false;
            this.Bu_Solve.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Bu_Solve.Location = new System.Drawing.Point(16, 73);
            this.Bu_Solve.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Bu_Solve.Name = "Bu_Solve";
            this.Bu_Solve.Size = new System.Drawing.Size(157, 50);
            this.Bu_Solve.TabIndex = 1;
            this.Bu_Solve.Text = "Solve";
            this.Bu_Solve.UseVisualStyleBackColor = true;
            this.Bu_Solve.Click += new System.EventHandler(this.Bu_Solve_Click);
            // 
            // NumUD_Speed
            // 
            this.NumUD_Speed.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumUD_Speed.Location = new System.Drawing.Point(393, 87);
            this.NumUD_Speed.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.NumUD_Speed.Name = "NumUD_Speed";
            this.NumUD_Speed.Size = new System.Drawing.Size(108, 26);
            this.NumUD_Speed.TabIndex = 3;
            this.NumUD_Speed.ValueChanged += new System.EventHandler(this.NumUD_Speed_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(313, 90);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Speed";
            // 
            // Lb_StepsReturn
            // 
            this.Lb_StepsReturn.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Lb_StepsReturn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lb_StepsReturn.ForeColor = System.Drawing.Color.Red;
            this.Lb_StepsReturn.Location = new System.Drawing.Point(16, 139);
            this.Lb_StepsReturn.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Lb_StepsReturn.Name = "Lb_StepsReturn";
            this.Lb_StepsReturn.Size = new System.Drawing.Size(485, 50);
            this.Lb_StepsReturn.TabIndex = 5;
            this.Lb_StepsReturn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MazeSolver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 212);
            this.Controls.Add(this.Lb_StepsReturn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.NumUD_Speed);
            this.Controls.Add(this.Bu_Solve);
            this.Controls.Add(this.Bu_Load);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MazeSolver";
            this.Text = "Maze Solver";
            ((System.ComponentModel.ISupportInitialize)(this.NumUD_Speed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Bu_Load;
        private System.Windows.Forms.Button Bu_Solve;
        private System.Windows.Forms.NumericUpDown NumUD_Speed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Lb_StepsReturn;
    }
}

