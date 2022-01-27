namespace ProjectSO
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Conectar_but = new System.Windows.Forms.Button();
            this.Desconectar_but = new System.Windows.Forms.Button();
            this.mostrar_lb = new System.Windows.Forms.Label();
            this.conectados_dgv = new System.Windows.Forms.DataGridView();
            this.invitar_bt = new System.Windows.Forms.Button();
            this.Registro_btn = new System.Windows.Forms.Button();
            this.IniciarSesion_btn = new System.Windows.Forms.Button();
            this.consultas_btn = new System.Windows.Forms.Button();
            this.Add_bt = new System.Windows.Forms.Button();
            this.Eliminar_bt = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.conectados_dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // Conectar_but
            // 
            this.Conectar_but.BackgroundImage = global::ProjectSO.Properties.Resources.WhatsApp_Image_2021_12_30_at_13_24_19__1_;
            this.Conectar_but.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Conectar_but.Location = new System.Drawing.Point(214, 505);
            this.Conectar_but.Margin = new System.Windows.Forms.Padding(4);
            this.Conectar_but.Name = "Conectar_but";
            this.Conectar_but.Size = new System.Drawing.Size(281, 103);
            this.Conectar_but.TabIndex = 0;
            this.Conectar_but.UseVisualStyleBackColor = true;
            this.Conectar_but.Click += new System.EventHandler(this.Conectar_but_Click);
            // 
            // Desconectar_but
            // 
            this.Desconectar_but.BackgroundImage = global::ProjectSO.Properties.Resources.WhatsApp_Image_2021_12_30_at_13_24_19;
            this.Desconectar_but.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Desconectar_but.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Desconectar_but.Location = new System.Drawing.Point(462, 612);
            this.Desconectar_but.Margin = new System.Windows.Forms.Padding(4);
            this.Desconectar_but.Name = "Desconectar_but";
            this.Desconectar_but.Size = new System.Drawing.Size(347, 112);
            this.Desconectar_but.TabIndex = 1;
            this.Desconectar_but.UseVisualStyleBackColor = true;
            this.Desconectar_but.Click += new System.EventHandler(this.Desconectar_but_Click);
            // 
            // mostrar_lb
            // 
            this.mostrar_lb.AutoSize = true;
            this.mostrar_lb.Location = new System.Drawing.Point(1346, 106);
            this.mostrar_lb.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.mostrar_lb.Name = "mostrar_lb";
            this.mostrar_lb.Size = new System.Drawing.Size(0, 25);
            this.mostrar_lb.TabIndex = 23;
            // 
            // conectados_dgv
            // 
            this.conectados_dgv.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.conectados_dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.conectados_dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.conectados_dgv.Location = new System.Drawing.Point(1358, 177);
            this.conectados_dgv.Margin = new System.Windows.Forms.Padding(40, 0, 40, 40);
            this.conectados_dgv.Name = "conectados_dgv";
            this.conectados_dgv.RowHeadersWidth = 82;
            this.conectados_dgv.RowTemplate.Height = 33;
            this.conectados_dgv.Size = new System.Drawing.Size(299, 379);
            this.conectados_dgv.TabIndex = 24;
            this.conectados_dgv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.conectados_dgv_CellClick);
            this.conectados_dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.conectados_dgv_CellContentClick);
            // 
            // invitar_bt
            // 
            this.invitar_bt.BackgroundImage = global::ProjectSO.Properties.Resources.WhatsApp_Image_2021_12_30_at_13_24_20__2_;
            this.invitar_bt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.invitar_bt.Location = new System.Drawing.Point(493, -14);
            this.invitar_bt.Margin = new System.Windows.Forms.Padding(4);
            this.invitar_bt.Name = "invitar_bt";
            this.invitar_bt.Size = new System.Drawing.Size(279, 200);
            this.invitar_bt.TabIndex = 25;
            this.invitar_bt.UseVisualStyleBackColor = true;
            this.invitar_bt.Click += new System.EventHandler(this.invitar_bt_Click);
            // 
            // Registro_btn
            // 
            this.Registro_btn.BackgroundImage = global::ProjectSO.Properties.Resources.WhatsApp_Image_2021_12_30_at_13_24_20;
            this.Registro_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Registro_btn.Location = new System.Drawing.Point(219, 148);
            this.Registro_btn.Margin = new System.Windows.Forms.Padding(4);
            this.Registro_btn.Name = "Registro_btn";
            this.Registro_btn.Size = new System.Drawing.Size(269, 83);
            this.Registro_btn.TabIndex = 26;
            this.Registro_btn.UseVisualStyleBackColor = true;
            this.Registro_btn.Click += new System.EventHandler(this.Registro_btn_Click);
            // 
            // IniciarSesion_btn
            // 
            this.IniciarSesion_btn.BackgroundImage = global::ProjectSO.Properties.Resources.WhatsApp_Image_2021_12_30_at_13_241;
            this.IniciarSesion_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.IniciarSesion_btn.Location = new System.Drawing.Point(800, 64);
            this.IniciarSesion_btn.Margin = new System.Windows.Forms.Padding(4);
            this.IniciarSesion_btn.Name = "IniciarSesion_btn";
            this.IniciarSesion_btn.Size = new System.Drawing.Size(225, 183);
            this.IniciarSesion_btn.TabIndex = 27;
            this.IniciarSesion_btn.UseVisualStyleBackColor = true;
            this.IniciarSesion_btn.Click += new System.EventHandler(this.IniciarSesion_btn_Click);
            // 
            // consultas_btn
            // 
            this.consultas_btn.BackgroundImage = global::ProjectSO.Properties.Resources.WhatsApp_Image_2022_01_08_at_12_38_32;
            this.consultas_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.consultas_btn.Location = new System.Drawing.Point(1126, 673);
            this.consultas_btn.Margin = new System.Windows.Forms.Padding(4);
            this.consultas_btn.Name = "consultas_btn";
            this.consultas_btn.Size = new System.Drawing.Size(194, 69);
            this.consultas_btn.TabIndex = 28;
            this.consultas_btn.UseVisualStyleBackColor = true;
            this.consultas_btn.Click += new System.EventHandler(this.consultas_btn_Click);
            // 
            // Add_bt
            // 
            this.Add_bt.BackgroundImage = global::ProjectSO.Properties.Resources.WhatsApp_Image_2022_01_17_at_11_37_52__1_;
            this.Add_bt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Add_bt.Location = new System.Drawing.Point(1126, 584);
            this.Add_bt.Margin = new System.Windows.Forms.Padding(4);
            this.Add_bt.Name = "Add_bt";
            this.Add_bt.Size = new System.Drawing.Size(194, 69);
            this.Add_bt.TabIndex = 29;
            this.Add_bt.UseVisualStyleBackColor = true;
            this.Add_bt.Click += new System.EventHandler(this.Add_bt_Click);
            // 
            // Eliminar_bt
            // 
            this.Eliminar_bt.BackgroundImage = global::ProjectSO.Properties.Resources.WhatsApp_Image_2022_01_26_at_17_41_45;
            this.Eliminar_bt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Eliminar_bt.Location = new System.Drawing.Point(1126, 64);
            this.Eliminar_bt.Margin = new System.Windows.Forms.Padding(4);
            this.Eliminar_bt.Name = "Eliminar_bt";
            this.Eliminar_bt.Size = new System.Drawing.Size(194, 69);
            this.Eliminar_bt.TabIndex = 30;
            this.Eliminar_bt.UseVisualStyleBackColor = true;
            this.Eliminar_bt.Click += new System.EventHandler(this.Eliminar_bt_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
           
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImage = global::ProjectSO.Properties.Resources.WhatsApp_Image_2021_12_30_at_14_22_55;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1737, 823);
            this.Controls.Add(this.Eliminar_bt);
            this.Controls.Add(this.Add_bt);
            this.Controls.Add(this.consultas_btn);
            this.Controls.Add(this.IniciarSesion_btn);
            this.Controls.Add(this.Registro_btn);
            this.Controls.Add(this.invitar_bt);
            this.Controls.Add(this.conectados_dgv);
            this.Controls.Add(this.mostrar_lb);
            this.Controls.Add(this.Desconectar_but);
            this.Controls.Add(this.Conectar_but);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.conectados_dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Conectar_but;
        private System.Windows.Forms.Button Desconectar_but;
        private System.Windows.Forms.Label mostrar_lb;
        private System.Windows.Forms.DataGridView conectados_dgv;
        private System.Windows.Forms.Button invitar_bt;
        private System.Windows.Forms.Button Registro_btn;
        private System.Windows.Forms.Button IniciarSesion_btn;
        private System.Windows.Forms.Button consultas_btn;
        private System.Windows.Forms.Button Add_bt;
        private System.Windows.Forms.Button Eliminar_bt;
        private System.Windows.Forms.Timer timer1;
    }
}

