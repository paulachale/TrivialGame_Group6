namespace ProjectSO
{
    partial class Consultas
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
            this.consulta_but = new System.Windows.Forms.Button();
            this.tuspartidas_rb = new System.Windows.Forms.RadioButton();
            this.mail_rb = new System.Windows.Forms.RadioButton();
            this.top_rb = new System.Windows.Forms.RadioButton();
            this.nombre_tb = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // consulta_but
            // 
            this.consulta_but.BackgroundImage = global::ProjectSO.Properties.Resources.dbahj;
            this.consulta_but.Location = new System.Drawing.Point(166, 358);
            this.consulta_but.Name = "consulta_but";
            this.consulta_but.Size = new System.Drawing.Size(264, 88);
            this.consulta_but.TabIndex = 19;
            this.consulta_but.UseVisualStyleBackColor = true;
            this.consulta_but.Click += new System.EventHandler(this.consulta_but_Click);
            // 
            // tuspartidas_rb
            // 
            this.tuspartidas_rb.AutoSize = true;
            this.tuspartidas_rb.BackColor = System.Drawing.Color.Transparent;
            this.tuspartidas_rb.Font = new System.Drawing.Font("Yu Gothic Medium", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tuspartidas_rb.Location = new System.Drawing.Point(168, 266);
            this.tuspartidas_rb.Name = "tuspartidas_rb";
            this.tuspartidas_rb.Size = new System.Drawing.Size(716, 40);
            this.tuspartidas_rb.TabIndex = 14;
            this.tuspartidas_rb.TabStop = true;
            this.tuspartidas_rb.Text = "Consulta el número de partidas que has ganado.";
            this.tuspartidas_rb.UseVisualStyleBackColor = false;
            // 
            // mail_rb
            // 
            this.mail_rb.AutoSize = true;
            this.mail_rb.BackColor = System.Drawing.Color.Transparent;
            this.mail_rb.Font = new System.Drawing.Font("Yu Gothic Medium", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mail_rb.Location = new System.Drawing.Point(166, 197);
            this.mail_rb.Name = "mail_rb";
            this.mail_rb.Size = new System.Drawing.Size(518, 40);
            this.mail_rb.TabIndex = 13;
            this.mail_rb.TabStop = true;
            this.mail_rb.Text = "Consulta el email de este usuario:";
            this.mail_rb.UseVisualStyleBackColor = false;
            // 
            // top_rb
            // 
            this.top_rb.AutoSize = true;
            this.top_rb.BackColor = System.Drawing.Color.Transparent;
            this.top_rb.Font = new System.Drawing.Font("Yu Gothic Medium", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.top_rb.Location = new System.Drawing.Point(168, 134);
            this.top_rb.Name = "top_rb";
            this.top_rb.Size = new System.Drawing.Size(945, 40);
            this.top_rb.TabIndex = 12;
            this.top_rb.TabStop = true;
            this.top_rb.Text = "Consulta el nombre del jugador@ que haya ganado más partidas.";
            this.top_rb.UseVisualStyleBackColor = false;
            this.top_rb.CheckedChanged += new System.EventHandler(this.top_rb_CheckedChanged);
            // 
            // nombre_tb
            // 
            this.nombre_tb.Location = new System.Drawing.Point(696, 203);
            this.nombre_tb.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nombre_tb.Name = "nombre_tb";
            this.nombre_tb.Size = new System.Drawing.Size(205, 31);
            this.nombre_tb.TabIndex = 20;
            // 
            // Consultas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ProjectSO.Properties.Resources.WhatsApp_Image_2022_01_07_at_17_12_26;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1200, 703);
            this.Controls.Add(this.nombre_tb);
            this.Controls.Add(this.consulta_but);
            this.Controls.Add(this.tuspartidas_rb);
            this.Controls.Add(this.mail_rb);
            this.Controls.Add(this.top_rb);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Consultas";
            this.Text = "Consultas";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button consulta_but;
        private System.Windows.Forms.RadioButton tuspartidas_rb;
        private System.Windows.Forms.RadioButton mail_rb;
        private System.Windows.Forms.RadioButton top_rb;
        private System.Windows.Forms.TextBox nombre_tb;
    }
}