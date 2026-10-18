namespace Página_principal
{
    partial class PáginaPrincipal
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
            this.label1 = new System.Windows.Forms.Label();
            this.PlanesDeVuelo = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(271, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(220, 35);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pàgina Principal";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // PlanesDeVuelo
            // 
            this.PlanesDeVuelo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlanesDeVuelo.Location = new System.Drawing.Point(66, 175);
            this.PlanesDeVuelo.Name = "PlanesDeVuelo";
            this.PlanesDeVuelo.Size = new System.Drawing.Size(439, 72);
            this.PlanesDeVuelo.TabIndex = 1;
            this.PlanesDeVuelo.Text = "Flight Plans";
            this.PlanesDeVuelo.UseVisualStyleBackColor = true;
            this.PlanesDeVuelo.Click += new System.EventHandler(this.PlanesDeVuelo_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(66, 269);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(439, 64);
            this.button1.TabIndex = 2;
            this.button1.Text = "Distancia Seguridad y Tiempo de Ciclo";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(66, 358);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(439, 72);
            this.button2.TabIndex = 3;
            this.button2.Text = "Simulación de vuelo";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // PáginaPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1783, 904);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.PlanesDeVuelo);
            this.Controls.Add(this.label1);
            this.Name = "PáginaPrincipal";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button PlanesDeVuelo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

