namespace Página_principal
{
    partial class ParametrosTiempoDistancia
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
            this.DistanciaSeguridad = new System.Windows.Forms.TextBox();
            this.BotonGuardarParametros = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.VolverMenuPrincipal = new System.Windows.Forms.Button();
            this.TiempoCiclo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // DistanciaSeguridad
            // 
            this.DistanciaSeguridad.Location = new System.Drawing.Point(410, 217);
            this.DistanciaSeguridad.Name = "DistanciaSeguridad";
            this.DistanciaSeguridad.Size = new System.Drawing.Size(195, 22);
            this.DistanciaSeguridad.TabIndex = 0;
            // 
            // BotonGuardarParametros
            // 
            this.BotonGuardarParametros.Location = new System.Drawing.Point(410, 290);
            this.BotonGuardarParametros.Name = "BotonGuardarParametros";
            this.BotonGuardarParametros.Size = new System.Drawing.Size(195, 66);
            this.BotonGuardarParametros.TabIndex = 2;
            this.BotonGuardarParametros.Text = "Guardar Parámetros";
            this.BotonGuardarParametros.UseVisualStyleBackColor = true;
            this.BotonGuardarParametros.Click += new System.EventHandler(this.BotonGuardarParametros_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(240, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Tiempo de Ciclo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(220, 220);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Distancia de Seguridad";
            // 
            // VolverMenuPrincipal
            // 
            this.VolverMenuPrincipal.Location = new System.Drawing.Point(223, 290);
            this.VolverMenuPrincipal.Name = "VolverMenuPrincipal";
            this.VolverMenuPrincipal.Size = new System.Drawing.Size(138, 66);
            this.VolverMenuPrincipal.TabIndex = 5;
            this.VolverMenuPrincipal.Text = "Volver";
            this.VolverMenuPrincipal.UseVisualStyleBackColor = true;
            this.VolverMenuPrincipal.Click += new System.EventHandler(this.VolverMenuPrincipal_Click);
            // 
            // TiempoCiclo
            // 
            this.TiempoCiclo.Location = new System.Drawing.Point(410, 129);
            this.TiempoCiclo.Name = "TiempoCiclo";
            this.TiempoCiclo.Size = new System.Drawing.Size(195, 22);
            this.TiempoCiclo.TabIndex = 6;
            // 
            // ParametrosTiempoDistancia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.TiempoCiclo);
            this.Controls.Add(this.VolverMenuPrincipal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BotonGuardarParametros);
            this.Controls.Add(this.DistanciaSeguridad);
            this.Name = "ParametrosTiempoDistancia";
            this.Text = "Guardar Paràmetres";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox DistanciaSeguridad;
        private System.Windows.Forms.Button BotonGuardarParametros;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button VolverMenuPrincipal;
        private System.Windows.Forms.TextBox TiempoCiclo;
    }
}