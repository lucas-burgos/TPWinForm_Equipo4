namespace Presentacion1
{
    partial class FrmVerArticulo
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pbxImagen = new System.Windows.Forms.PictureBox();
            this.btnAnterior = new System.Windows.Forms.Button();
            this.btnSiguiente = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.txtDetalles = new System.Windows.Forms.TextBox();
            this.tmrCarrusel = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagen)).BeginInit();
            this.SuspendLayout();
            
            // pbxImagen
            this.pbxImagen.AccessibleDescription = "Visualización de la imagen del producto";
            this.pbxImagen.AccessibleName = "Imagen del Artículo";
            this.pbxImagen.Location = new System.Drawing.Point(20, 20);
            this.pbxImagen.Name = "pbxImagen";
            this.pbxImagen.Size = new System.Drawing.Size(250, 250);
            this.pbxImagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxImagen.TabIndex = 0;
            this.pbxImagen.TabStop = false;
            
            // btnAnterior
            this.btnAnterior.AccessibleDescription = "Muestra la imagen anterior del artículo";
            this.btnAnterior.AccessibleName = "Imagen Anterior";
            this.btnAnterior.Location = new System.Drawing.Point(20, 280);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new System.Drawing.Size(120, 30);
            this.btnAnterior.TabIndex = 1;
            this.btnAnterior.Text = "< Anterior";
            this.btnAnterior.UseVisualStyleBackColor = true;
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click);
            
            // btnSiguiente
            this.btnSiguiente.AccessibleDescription = "Muestra la siguiente imagen del artículo";
            this.btnSiguiente.AccessibleName = "Imagen Siguiente";
            this.btnSiguiente.Location = new System.Drawing.Point(150, 280);
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.Size = new System.Drawing.Size(120, 30);
            this.btnSiguiente.TabIndex = 2;
            this.btnSiguiente.Text = "Siguiente >";
            this.btnSiguiente.UseVisualStyleBackColor = true;
            this.btnSiguiente.Click += new System.EventHandler(this.btnSiguiente_Click);
            
            // txtDetalles
            this.txtDetalles.AccessibleDescription = "Muestra toda la información del producto";
            this.txtDetalles.AccessibleName = "Detalles del Artículo";
            this.txtDetalles.Location = new System.Drawing.Point(290, 20);
            this.txtDetalles.Multiline = true;
            this.txtDetalles.Name = "txtDetalles";
            this.txtDetalles.ReadOnly = true;
            this.txtDetalles.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDetalles.Size = new System.Drawing.Size(280, 250);
            this.txtDetalles.TabIndex = 3;
            this.txtDetalles.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular);
            
            // tmrCarrusel
            this.tmrCarrusel.Tick += new System.EventHandler(this.tmrCarrusel_Tick);
            
            // btnCerrar
            this.btnCerrar.AccessibleDescription = "Cierra la ventana de detalles";
            this.btnCerrar.AccessibleName = "Cerrar Detalles";
            this.btnCerrar.Location = new System.Drawing.Point(470, 280);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(100, 30);
            this.btnCerrar.TabIndex = 4;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            
            // FrmVerArticulo
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 330);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.txtDetalles);
            this.Controls.Add(this.btnSiguiente);
            this.Controls.Add(this.btnAnterior);
            this.Controls.Add(this.pbxImagen);
            this.Name = "FrmVerArticulo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Detalle del Artículo";
            this.Load += new System.EventHandler(this.FrmVerArticulo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.PictureBox pbxImagen;
        private System.Windows.Forms.Button btnAnterior;
        private System.Windows.Forms.Button btnSiguiente;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.TextBox txtDetalles;
        private System.Windows.Forms.Timer tmrCarrusel;
    }
}