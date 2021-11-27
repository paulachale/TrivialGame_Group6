namespace ProjectSO
{
    partial class Tablero
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
            this.lanzarDado_btn = new System.Windows.Forms.Button();
            this.picDado = new System.Windows.Forms.PictureBox();
            this.turno_lb = new System.Windows.Forms.Label();
            this.fallo_bt = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picDado)).BeginInit();
            this.SuspendLayout();
            // 
            // lanzarDado_btn
            // 
            this.lanzarDado_btn.Location = new System.Drawing.Point(502, 667);
            this.lanzarDado_btn.Name = "lanzarDado_btn";
            this.lanzarDado_btn.Size = new System.Drawing.Size(166, 50);
            this.lanzarDado_btn.TabIndex = 0;
            this.lanzarDado_btn.Text = "Lanzar dado";
            this.lanzarDado_btn.UseVisualStyleBackColor = true;
            this.lanzarDado_btn.Click += new System.EventHandler(this.lanzarDado_btn_Click);
            // 
            // picDado
            // 
            this.picDado.Location = new System.Drawing.Point(335, 103);
            this.picDado.Name = "picDado";
            this.picDado.Size = new System.Drawing.Size(507, 481);
            this.picDado.TabIndex = 1;
            this.picDado.TabStop = false;
            // 
            // turno_lb
            // 
            this.turno_lb.AutoSize = true;
            this.turno_lb.Location = new System.Drawing.Point(100, 41);
            this.turno_lb.Name = "turno_lb";
            this.turno_lb.Size = new System.Drawing.Size(0, 25);
            this.turno_lb.TabIndex = 2;
            this.turno_lb.Click += new System.EventHandler(this.label1_Click);
            // 
            // fallo_bt
            // 
            this.fallo_bt.Location = new System.Drawing.Point(874, 667);
            this.fallo_bt.Name = "fallo_bt";
            this.fallo_bt.Size = new System.Drawing.Size(232, 50);
            this.fallo_bt.TabIndex = 3;
            this.fallo_bt.Text = "Pregunta fallada";
            this.fallo_bt.UseVisualStyleBackColor = true;
            this.fallo_bt.Click += new System.EventHandler(this.fallo_bt_Click);
            // 
            // Tablero
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 828);
            this.Controls.Add(this.fallo_bt);
            this.Controls.Add(this.turno_lb);
            this.Controls.Add(this.picDado);
            this.Controls.Add(this.lanzarDado_btn);
            this.Name = "Tablero";
            this.Text = "Tablero";
           
            ((System.ComponentModel.ISupportInitialize)(this.picDado)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button lanzarDado_btn;
        private System.Windows.Forms.PictureBox picDado;
        private System.Windows.Forms.Label turno_lb;
        private System.Windows.Forms.Button fallo_bt;
    }
}