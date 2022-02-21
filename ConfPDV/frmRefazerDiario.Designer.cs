
namespace LinearProcessamentos
{
    partial class frmRefazerDiario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRefazerDiario));
            this.label4 = new System.Windows.Forms.Label();
            this.dtpDataFinal = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpDataInicial = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxFilial = new System.Windows.Forms.ComboBox();
            this.btnProcessar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtProdDesc = new System.Windows.Forms.TextBox();
            this.txtCodProd = new System.Windows.Forms.TextBox();
            this.cbxProduto = new System.Windows.Forms.CheckBox();
            this.cbxApartirDeProd = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtProdutoInicio = new System.Windows.Forms.TextBox();
            this.lblProgress = new System.Windows.Forms.Label();
            this.pgbProgress = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(63, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 15);
            this.label4.TabIndex = 33;
            this.label4.Text = "Filial";
            // 
            // dtpDataFinal
            // 
            this.dtpDataFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDataFinal.Location = new System.Drawing.Point(308, 90);
            this.dtpDataFinal.Name = "dtpDataFinal";
            this.dtpDataFinal.Size = new System.Drawing.Size(92, 21);
            this.dtpDataFinal.TabIndex = 32;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(243, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 15);
            this.label2.TabIndex = 31;
            this.label2.Text = "Data final";
            // 
            // dtpDataInicial
            // 
            this.dtpDataInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDataInicial.Location = new System.Drawing.Point(137, 90);
            this.dtpDataInicial.Name = "dtpDataInicial";
            this.dtpDataInicial.Size = new System.Drawing.Size(92, 21);
            this.dtpDataInicial.TabIndex = 30;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(63, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 15);
            this.label1.TabIndex = 29;
            this.label1.Text = "Data inicial";
            // 
            // cbxFilial
            // 
            this.cbxFilial.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxFilial.FormattingEnabled = true;
            this.cbxFilial.ItemHeight = 18;
            this.cbxFilial.Location = new System.Drawing.Point(102, 44);
            this.cbxFilial.Name = "cbxFilial";
            this.cbxFilial.Size = new System.Drawing.Size(298, 26);
            this.cbxFilial.TabIndex = 28;
            // 
            // btnProcessar
            // 
            this.btnProcessar.Location = new System.Drawing.Point(176, 216);
            this.btnProcessar.Name = "btnProcessar";
            this.btnProcessar.Size = new System.Drawing.Size(101, 35);
            this.btnProcessar.TabIndex = 34;
            this.btnProcessar.Text = "Processar";
            this.btnProcessar.UseVisualStyleBackColor = true;
            this.btnProcessar.Click += new System.EventHandler(this.btnProcessar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(63, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 15);
            this.label3.TabIndex = 38;
            this.label3.Text = "Produto Selecionado";
            // 
            // txtProdDesc
            // 
            this.txtProdDesc.Enabled = false;
            this.txtProdDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProdDesc.Location = new System.Drawing.Point(176, 147);
            this.txtProdDesc.Name = "txtProdDesc";
            this.txtProdDesc.Size = new System.Drawing.Size(224, 22);
            this.txtProdDesc.TabIndex = 37;
            // 
            // txtCodProd
            // 
            this.txtCodProd.Enabled = false;
            this.txtCodProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodProd.Location = new System.Drawing.Point(101, 147);
            this.txtCodProd.Name = "txtCodProd";
            this.txtCodProd.Size = new System.Drawing.Size(69, 22);
            this.txtCodProd.TabIndex = 36;
            this.txtCodProd.TextChanged += new System.EventHandler(this.txtCodProd_TextChanged);
            // 
            // cbxProduto
            // 
            this.cbxProduto.AutoSize = true;
            this.cbxProduto.Location = new System.Drawing.Point(79, 152);
            this.cbxProduto.Name = "cbxProduto";
            this.cbxProduto.Size = new System.Drawing.Size(15, 14);
            this.cbxProduto.TabIndex = 35;
            this.cbxProduto.UseVisualStyleBackColor = true;
            this.cbxProduto.CheckedChanged += new System.EventHandler(this.cbxProduto_CheckedChanged);
            // 
            // cbxApartirDeProd
            // 
            this.cbxApartirDeProd.AutoSize = true;
            this.cbxApartirDeProd.Location = new System.Drawing.Point(29, 227);
            this.cbxApartirDeProd.Name = "cbxApartirDeProd";
            this.cbxApartirDeProd.Size = new System.Drawing.Size(15, 14);
            this.cbxApartirDeProd.TabIndex = 39;
            this.cbxApartirDeProd.UseVisualStyleBackColor = true;
            this.cbxApartirDeProd.CheckedChanged += new System.EventHandler(this.cbxApartirDeProd_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 200);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(144, 15);
            this.label5.TabIndex = 40;
            this.label5.Text = "Iniciar a partir de Produto";
            // 
            // txtProdutoInicio
            // 
            this.txtProdutoInicio.Enabled = false;
            this.txtProdutoInicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProdutoInicio.Location = new System.Drawing.Point(50, 222);
            this.txtProdutoInicio.Name = "txtProdutoInicio";
            this.txtProdutoInicio.Size = new System.Drawing.Size(69, 22);
            this.txtProdutoInicio.TabIndex = 41;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(160, 279);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(10, 13);
            this.lblProgress.TabIndex = 42;
            this.lblProgress.Text = "-";
            // 
            // pgbProgress
            // 
            this.pgbProgress.Location = new System.Drawing.Point(113, 253);
            this.pgbProgress.Name = "pgbProgress";
            this.pgbProgress.Size = new System.Drawing.Size(228, 23);
            this.pgbProgress.TabIndex = 43;
            // 
            // Form5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 299);
            this.Controls.Add(this.pgbProgress);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.txtProdutoInicio);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbxApartirDeProd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtProdDesc);
            this.Controls.Add(this.txtCodProd);
            this.Controls.Add(this.cbxProduto);
            this.Controls.Add(this.btnProcessar);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpDataFinal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpDataInicial);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxFilial);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form5";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Refazer Diário";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpDataFinal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpDataInicial;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxFilial;
        private System.Windows.Forms.Button btnProcessar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtProdDesc;
        private System.Windows.Forms.TextBox txtCodProd;
        private System.Windows.Forms.CheckBox cbxProduto;
        private System.Windows.Forms.CheckBox cbxApartirDeProd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtProdutoInicio;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.ProgressBar pgbProgress;
    }
}