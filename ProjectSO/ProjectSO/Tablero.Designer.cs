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
            this.components = new System.ComponentModel.Container();
            this.lanzarDado_btn = new System.Windows.Forms.Button();
            this.picDado = new System.Windows.Forms.PictureBox();
            this.turno_lb = new System.Windows.Forms.Label();
            this.Panel = new System.Windows.Forms.Panel();
            this.resultados_dgv = new System.Windows.Forms.DataGridView();
            this.enviar_btn = new System.Windows.Forms.Button();
            this.ChatTextBox = new System.Windows.Forms.TextBox();
            this.MensajeBox = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picDado)).BeginInit();
            this.Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultados_dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // lanzarDado_btn
            // 
            this.lanzarDado_btn.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lanzarDado_btn.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lanzarDado_btn.Location = new System.Drawing.Point(1267, 618);
            this.lanzarDado_btn.Name = "lanzarDado_btn";
            this.lanzarDado_btn.Size = new System.Drawing.Size(207, 56);
            this.lanzarDado_btn.TabIndex = 0;
            this.lanzarDado_btn.Text = "Lanzar dado";
            this.lanzarDado_btn.UseVisualStyleBackColor = false;
            this.lanzarDado_btn.Click += new System.EventHandler(this.lanzarDado_btn_Click);
            // 
            // picDado
            // 
            this.picDado.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.picDado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picDado.Location = new System.Drawing.Point(1028, 555);
            this.picDado.Name = "picDado";
            this.picDado.Size = new System.Drawing.Size(207, 198);
            this.picDado.TabIndex = 1;
            this.picDado.TabStop = false;
            // 
            // turno_lb
            // 
            this.turno_lb.AutoSize = true;
            this.turno_lb.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.turno_lb.Font = new System.Drawing.Font("Consolas", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.turno_lb.Location = new System.Drawing.Point(388, 628);
            this.turno_lb.Name = "turno_lb";
            this.turno_lb.Size = new System.Drawing.Size(0, 34);
            this.turno_lb.TabIndex = 2;
            this.turno_lb.Click += new System.EventHandler(this.label1_Click);
            // 
            // Panel
            // 
            this.Panel.BackColor = System.Drawing.Color.Transparent;
            this.Panel.BackgroundImage = global::ProjectSO.Properties.Resources.Tablero;
            this.Panel.Controls.Add(this.resultados_dgv);
            this.Panel.Controls.Add(this.turno_lb);
            this.Panel.Controls.Add(this.lanzarDado_btn);
            this.Panel.Controls.Add(this.enviar_btn);
            this.Panel.Controls.Add(this.ChatTextBox);
            this.Panel.Controls.Add(this.picDado);
            this.Panel.Controls.Add(this.MensajeBox);
            this.Panel.Location = new System.Drawing.Point(0, 0);
            this.Panel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Panel.Name = "Panel";
            this.Panel.Size = new System.Drawing.Size(3093, 1328);
            this.Panel.TabIndex = 4;
            // 
            // resultados_dgv
            // 
            this.resultados_dgv.AllowUserToResizeRows = false;
            this.resultados_dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.resultados_dgv.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.resultados_dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.resultados_dgv.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.resultados_dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.resultados_dgv.Location = new System.Drawing.Point(372, 1034);
            this.resultados_dgv.Name = "resultados_dgv";
            this.resultados_dgv.RowHeadersWidth = 82;
            this.resultados_dgv.RowTemplate.Height = 33;
            this.resultados_dgv.Size = new System.Drawing.Size(1000, 397);
            this.resultados_dgv.TabIndex = 8;
            this.resultados_dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.resultados_dgv_CellContentClick);
            // 
            // enviar_btn
            // 
            this.enviar_btn.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.enviar_btn.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.enviar_btn.Location = new System.Drawing.Point(2853, 927);
            this.enviar_btn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.enviar_btn.Name = "enviar_btn";
            this.enviar_btn.Size = new System.Drawing.Size(172, 67);
            this.enviar_btn.TabIndex = 6;
            this.enviar_btn.Text = "Enviar";
            this.enviar_btn.UseVisualStyleBackColor = false;
            this.enviar_btn.Click += new System.EventHandler(this.enviar_btn_Click);
            // 
            // ChatTextBox
            // 
            this.ChatTextBox.Location = new System.Drawing.Point(2394, 300);
            this.ChatTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ChatTextBox.Multiline = true;
            this.ChatTextBox.Name = "ChatTextBox";
            this.ChatTextBox.Size = new System.Drawing.Size(630, 615);
            this.ChatTextBox.TabIndex = 7;
            // 
            // MensajeBox
            // 
            this.MensajeBox.Location = new System.Drawing.Point(2394, 927);
            this.MensajeBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MensajeBox.Name = "MensajeBox";
            this.MensajeBox.Size = new System.Drawing.Size(448, 31);
            this.MensajeBox.TabIndex = 5;
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            // 
            // Tablero
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2884, 1347);
            this.Controls.Add(this.Panel);
            this.Name = "Tablero";
            this.Text = "Tablero";
            this.Load += new System.EventHandler(this.Tablero_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picDado)).EndInit();
            this.Panel.ResumeLayout(false);
            this.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultados_dgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button lanzarDado_btn;
        private System.Windows.Forms.PictureBox picDado;
        private System.Windows.Forms.Label turno_lb;
        private System.Windows.Forms.Panel Panel;
        private System.Windows.Forms.TextBox MensajeBox;
        private System.Windows.Forms.Button enviar_btn;
        private System.Windows.Forms.TextBox ChatTextBox;
        private System.Windows.Forms.DataGridView resultados_dgv;
        private System.Windows.Forms.Timer timer1;
    }
}