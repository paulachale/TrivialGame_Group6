namespace ProjectSO
{
    partial class Eliminar
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
            this.contra1_tb = new System.Windows.Forms.TextBox();
            this.usuario1_tb = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Regis_but = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // contra1_tb
            // 
            this.contra1_tb.Location = new System.Drawing.Point(550, 232);
            this.contra1_tb.Name = "contra1_tb";
            this.contra1_tb.PasswordChar = '*';
            this.contra1_tb.Size = new System.Drawing.Size(379, 31);
            this.contra1_tb.TabIndex = 40;
            // 
            // usuario1_tb
            // 
            this.usuario1_tb.Location = new System.Drawing.Point(550, 177);
            this.usuario1_tb.Name = "usuario1_tb";
            this.usuario1_tb.Size = new System.Drawing.Size(379, 31);
            this.usuario1_tb.TabIndex = 39;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(280, 230);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(233, 42);
            this.label2.TabIndex = 38;
            this.label2.Text = "Contraseña:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Yu Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(144, 177);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(379, 48);
            this.label3.TabIndex = 37;
            this.label3.Text = "Nombre de usuario:";
            // 
            // Regis_but
            // 
            this.Regis_but.BackColor = System.Drawing.Color.Black;
            this.Regis_but.BackgroundImage = global::ProjectSO.Properties.Resources.WhatsApp_Image_2022_01_26_at_17_41_45;
            this.Regis_but.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Regis_but.Font = new System.Drawing.Font("Yu Gothic", 16.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Regis_but.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Regis_but.Location = new System.Drawing.Point(417, 313);
            this.Regis_but.Name = "Regis_but";
            this.Regis_but.Size = new System.Drawing.Size(264, 94);
            this.Regis_but.TabIndex = 36;
            this.Regis_but.UseVisualStyleBackColor = false;
            this.Regis_but.Click += new System.EventHandler(this.Regis_but_Click);
            // 
            // Eliminar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ProjectSO.Properties.Resources.WhatsApp_Image_2022_01_07_at_17_121;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1052, 560);
            this.Controls.Add(this.contra1_tb);
            this.Controls.Add(this.usuario1_tb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Regis_but);
            this.Name = "Eliminar";
            this.Text = "Eliminar";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox contra1_tb;
        private System.Windows.Forms.TextBox usuario1_tb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Regis_but;
    }
}