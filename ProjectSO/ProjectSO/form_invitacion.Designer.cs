namespace ProjectSO
{
    partial class form_invitacion
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
            this.invitacion_lb = new System.Windows.Forms.Label();
            this.Aceptar_bt = new System.Windows.Forms.Button();
            this.rechazar_bt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // invitacion_lb
            // 
            this.invitacion_lb.AutoSize = true;
            this.invitacion_lb.BackColor = System.Drawing.Color.Transparent;
            this.invitacion_lb.Font = new System.Drawing.Font("Yu Gothic", 10.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.invitacion_lb.Location = new System.Drawing.Point(98, 113);
            this.invitacion_lb.Name = "invitacion_lb";
            this.invitacion_lb.Size = new System.Drawing.Size(0, 38);
            this.invitacion_lb.TabIndex = 0;
            // 
            // Aceptar_bt
            // 
            this.Aceptar_bt.BackgroundImage = global::ProjectSO.Properties.Resources.WhatsApp_Image_2022_01_17_at_11_37_52;
            this.Aceptar_bt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Aceptar_bt.Location = new System.Drawing.Point(441, 296);
            this.Aceptar_bt.Name = "Aceptar_bt";
            this.Aceptar_bt.Size = new System.Drawing.Size(219, 90);
            this.Aceptar_bt.TabIndex = 27;
            this.Aceptar_bt.UseVisualStyleBackColor = true;
            this.Aceptar_bt.Click += new System.EventHandler(this.Aceptar_bt_Click);
            // 
            // rechazar_bt
            // 
            this.rechazar_bt.BackgroundImage = global::ProjectSO.Properties.Resources.WhatsApp_Image_2022_01_17_at_11_37_53;
            this.rechazar_bt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rechazar_bt.Location = new System.Drawing.Point(182, 296);
            this.rechazar_bt.Name = "rechazar_bt";
            this.rechazar_bt.Size = new System.Drawing.Size(218, 90);
            this.rechazar_bt.TabIndex = 26;
            this.rechazar_bt.UseVisualStyleBackColor = true;
            this.rechazar_bt.Click += new System.EventHandler(this.rechazar_bt_Click);
            // 
            // form_invitacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ProjectSO.Properties.Resources.WhatsApp_Image_2022_01_07_at_17_12_26;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(826, 462);
            this.Controls.Add(this.Aceptar_bt);
            this.Controls.Add(this.rechazar_bt);
            this.Controls.Add(this.invitacion_lb);
            this.Name = "form_invitacion";
            this.Text = "form_invitacion";
            this.Load += new System.EventHandler(this.form_invitacion_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label invitacion_lb;
        private System.Windows.Forms.Button rechazar_bt;
        private System.Windows.Forms.Button Aceptar_bt;
    }
}