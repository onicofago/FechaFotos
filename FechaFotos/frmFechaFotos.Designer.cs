namespace FechaFotos
{
    partial class FrmFechaFotos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFechaFotos));
            this.label1 = new System.Windows.Forms.Label();
            this.txtCarpeta = new System.Windows.Forms.TextBox();
            this.btnCarpeta = new System.Windows.Forms.Button();
            this.ProgressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnProcesar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Folder";
            // 
            // txtCarpeta
            // 
            this.txtCarpeta.Location = new System.Drawing.Point(76, 25);
            this.txtCarpeta.Name = "txtCarpeta";
            this.txtCarpeta.Size = new System.Drawing.Size(452, 22);
            this.txtCarpeta.TabIndex = 1;
            // 
            // btnCarpeta
            // 
            this.btnCarpeta.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCarpeta.Location = new System.Drawing.Point(534, 25);
            this.btnCarpeta.Name = "btnCarpeta";
            this.btnCarpeta.Size = new System.Drawing.Size(46, 23);
            this.btnCarpeta.TabIndex = 3;
            this.btnCarpeta.Text = "...";
            this.btnCarpeta.UseVisualStyleBackColor = false;
            this.btnCarpeta.Click += new System.EventHandler(this.btnCarpeta_Click);
            // 
            // ProgressBar1
            // 
            this.ProgressBar1.Location = new System.Drawing.Point(15, 76);
            this.ProgressBar1.Name = "ProgressBar1";
            this.ProgressBar1.Size = new System.Drawing.Size(365, 33);
            this.ProgressBar1.TabIndex = 4;
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnCancelar.Location = new System.Drawing.Point(490, 76);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(90, 33);
            this.btnCancelar.TabIndex = 7;
            this.btnCancelar.Text = "&Cancel";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnProcesar
            // 
            this.btnProcesar.Location = new System.Drawing.Point(394, 76);
            this.btnProcesar.Name = "btnProcesar";
            this.btnProcesar.Size = new System.Drawing.Size(90, 33);
            this.btnProcesar.TabIndex = 6;
            this.btnProcesar.Text = "&Process";
            this.btnProcesar.Click += new System.EventHandler(this.btnProcesar_Click);
            // 
            // FrmFechaFotos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(592, 138);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnProcesar);
            this.Controls.Add(this.ProgressBar1);
            this.Controls.Add(this.btnCarpeta);
            this.Controls.Add(this.txtCarpeta);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmFechaFotos";
            this.Text = "FechaFotos";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCarpeta;
        internal System.Windows.Forms.Button btnCarpeta;
        internal System.Windows.Forms.ProgressBar ProgressBar1;
        internal System.Windows.Forms.Button btnCancelar;
        internal System.Windows.Forms.Button btnProcesar;
    }
}