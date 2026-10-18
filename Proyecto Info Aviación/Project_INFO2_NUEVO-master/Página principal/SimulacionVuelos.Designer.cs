namespace Página_principal
{
    partial class SimulacionVuelos
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
            this.BotonAvanzar = new System.Windows.Forms.Button();
            this.panelAviones = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.labelEcuaciones = new System.Windows.Forms.Label();
            this.botonConflicto = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BotonAvanzar
            // 
            this.BotonAvanzar.Location = new System.Drawing.Point(122, 114);
            this.BotonAvanzar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BotonAvanzar.Name = "BotonAvanzar";
            this.BotonAvanzar.Size = new System.Drawing.Size(124, 52);
            this.BotonAvanzar.TabIndex = 1;
            this.BotonAvanzar.Text = "Avanzar ";
            this.BotonAvanzar.UseVisualStyleBackColor = true;
            this.BotonAvanzar.Click += new System.EventHandler(this.BotonAvanzar_Click);
            // 
            // panelAviones
            // 
            this.panelAviones.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panelAviones.Location = new System.Drawing.Point(372, 50);
            this.panelAviones.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelAviones.Name = "panelAviones";
            this.panelAviones.Size = new System.Drawing.Size(958, 834);
            this.panelAviones.TabIndex = 0;
            this.panelAviones.Paint += new System.Windows.Forms.PaintEventHandler(this.panelAviones_Paint);
            this.panelAviones.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelAviones_MouseClick_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(122, 309);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(124, 52);
            this.button1.TabIndex = 2;
            this.button1.Text = "Volver Pantalla Principal";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelEcuaciones
            // 
            this.labelEcuaciones.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelEcuaciones.Location = new System.Drawing.Point(102, 414);
            this.labelEcuaciones.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelEcuaciones.Name = "labelEcuaciones";
            this.labelEcuaciones.Size = new System.Drawing.Size(166, 113);
            this.labelEcuaciones.TabIndex = 3;
            this.labelEcuaciones.Text = "Ecuaciones de las trayectorias";
            // 
            // botonConflicto
            // 
            this.botonConflicto.Location = new System.Drawing.Point(122, 200);
            this.botonConflicto.Name = "botonConflicto";
            this.botonConflicto.Size = new System.Drawing.Size(124, 57);
            this.botonConflicto.TabIndex = 4;
            this.botonConflicto.Text = "Comprobar conflicto";
            this.botonConflicto.UseVisualStyleBackColor = true;
            this.botonConflicto.Click += new System.EventHandler(this.botonConflicto_Click);
            // 
            // SimulacionVuelos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1155, 765);
            this.Controls.Add(this.botonConflicto);
            this.Controls.Add(this.labelEcuaciones);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.BotonAvanzar);
            this.Controls.Add(this.panelAviones);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "SimulacionVuelos";
            this.Text = "SimulacionVuelos";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button BotonAvanzar;
        private System.Windows.Forms.Panel panelAviones;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelEcuaciones;
        private System.Windows.Forms.Button botonConflicto;
    }
}


