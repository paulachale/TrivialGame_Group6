namespace ProjectSO
{
    partial class Registrarse
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
            this.mail_tb = new System.Windows.Forms.TextBox();
            this.contra1_tb = new System.Windows.Forms.TextBox();
            this.usuario1_tb = new System.Windows.Forms.TextBox();
            this.Mail_lb = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Regis_but = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mail_tb
            // 
            this.mail_tb.Location = new System.Drawing.Point(525, 383);
            this.mail_tb.Name = "mail_tb";
            this.mail_tb.Size = new System.Drawing.Size(379, 31);
            this.mail_tb.TabIndex = 29;
            // 
            // contra1_tb
            // 
            this.contra1_tb.Location = new System.Drawing.Point(525, 328);
            this.contra1_tb.Name = "contra1_tb";
            this.contra1_tb.Size = new System.Drawing.Size(379, 31);
            this.contra1_tb.TabIndex = 28;
            // 
            // usuario1_tb
            // 
            this.usuario1_tb.Location = new System.Drawing.Point(525, 273);
            this.usuario1_tb.Name = "usuario1_tb";
            this.usuario1_tb.Size = new System.Drawing.Size(379, 31);
            this.usuario1_tb.TabIndex = 27;
            // 
            // Mail_lb
            // 
            this.Mail_lb.AutoSize = true;
            this.Mail_lb.BackColor = System.Drawing.Color.Transparent;
            this.Mail_lb.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Mail_lb.Location = new System.Drawing.Point(376, 383);
            this.Mail_lb.Name = "Mail_lb";
            this.Mail_lb.Size = new System.Drawing.Size(101, 42);
            this.Mail_lb.TabIndex = 26;
            this.Mail_lb.Text = "Mail:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(255, 327);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(233, 42);
            this.label2.TabIndex = 25;
            this.label2.Text = "Contraseña:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Yu Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(118, 273);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(379, 48);
            this.label3.TabIndex = 24;
            this.label3.Text = "Nombre de usuario:";
            // 
            // Regis_but
            // 
            this.Regis_but.BackColor = System.Drawing.Color.Black;
            this.Regis_but.BackgroundImage = global::ProjectSO.Properties.Resources.WhatsApp_Image_2022_01_07_at_17_25_29;
            this.Regis_but.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Regis_but.Font = new System.Drawing.Font("Yu Gothic", 16.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Regis_but.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Regis_but.Location = new System.Drawing.Point(588, 444);
            this.Regis_but.Name = "Regis_but";
            this.Regis_but.Size = new System.Drawing.Size(240, 94);
            this.Regis_but.TabIndex = 22;
            this.Regis_but.UseVisualStyleBackColor = false;
            this.Regis_but.Click += new System.EventHandler(this.Regis_but_Click);
            // 
            // Registrarse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::ProjectSO.Properties.Resources.WhatsApp_Image_2022_01_07_at_17_12_26;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1240, 781);
            this.Controls.Add(this.mail_tb);
            this.Controls.Add(this.contra1_tb);
            this.Controls.Add(this.usuario1_tb);
            this.Controls.Add(this.Mail_lb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Regis_but);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Registrarse";
            this.Text = "Registrarse";
            this.Load += new System.EventHandler(this.Registrarse_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox mail_tb;
        private System.Windows.Forms.TextBox contra1_tb;
        private System.Windows.Forms.TextBox usuario1_tb;
        private System.Windows.Forms.Label Mail_lb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Regis_but;
    }
}