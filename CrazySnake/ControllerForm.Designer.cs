namespace CrazySnake
{
    partial class ControllerForm
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
            this.StartStop_btn = new System.Windows.Forms.Button();
            this.lblScore = new System.Windows.Forms.Label();
            this.lblScoreValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // StartStop_btn
            // 
            this.StartStop_btn.Location = new System.Drawing.Point(23, 362);
            this.StartStop_btn.Name = "StartStop_btn";
            this.StartStop_btn.Size = new System.Drawing.Size(96, 23);
            this.StartStop_btn.TabIndex = 0;
            this.StartStop_btn.Text = "Start/Stop";
            this.StartStop_btn.UseVisualStyleBackColor = true;
            this.StartStop_btn.Click += new System.EventHandler(this.StartStop_btn_Click);
            this.StartStop_btn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StartStop_btn_KeyPress);
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.Location = new System.Drawing.Point(146, 367);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(38, 13);
            this.lblScore.TabIndex = 1;
            this.lblScore.Text = "Score:";
            // 
            // lblScoreValue
            // 
            this.lblScoreValue.AutoSize = true;
            this.lblScoreValue.Location = new System.Drawing.Point(190, 367);
            this.lblScoreValue.Name = "lblScoreValue";
            this.lblScoreValue.Size = new System.Drawing.Size(0, 13);
            this.lblScoreValue.TabIndex = 2;
            // 
            // ControllerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 395);
            this.Controls.Add(this.lblScoreValue);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.StartStop_btn);
            this.Name = "ControllerForm";
            this.Text = "CrazySnake";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartStop_btn;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label lblScoreValue;
    }
}

