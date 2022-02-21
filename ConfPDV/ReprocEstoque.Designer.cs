
namespace ConfPDV
{
    partial class Form4
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form4));
            this.btnProcessar = new System.Windows.Forms.Button();
            this.cbxFilial = new System.Windows.Forms.ComboBox();
            this.lblFilial = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblProcessando = new System.Windows.Forms.Label();
            this.lblQuantUpdates = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxProduto = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rtxt_log = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtProdDesc = new System.Windows.Forms.TextBox();
            this.mtxtData = new System.Windows.Forms.DateTimePicker();
            this.txtCodProd = new System.Windows.Forms.TextBox();
            this.pgbProcesso = new System.Windows.Forms.ProgressBar();
            this.txtApartirDeProd = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxApartirDeProd = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnProcessar
            // 
            this.btnProcessar.Location = new System.Drawing.Point(120, 259);
            this.btnProcessar.Name = "btnProcessar";
            this.btnProcessar.Size = new System.Drawing.Size(97, 31);
            this.btnProcessar.TabIndex = 0;
            this.btnProcessar.Text = "Processar";
            this.btnProcessar.UseVisualStyleBackColor = true;
            this.btnProcessar.Click += new System.EventHandler(this.btnProcessar_Click);
            // 
            // cbxFilial
            // 
            this.cbxFilial.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxFilial.FormattingEnabled = true;
            this.cbxFilial.ItemHeight = 18;
            this.cbxFilial.Location = new System.Drawing.Point(60, 32);
            this.cbxFilial.Name = "cbxFilial";
            this.cbxFilial.Size = new System.Drawing.Size(276, 26);
            this.cbxFilial.TabIndex = 4;
            this.cbxFilial.SelectedIndexChanged += new System.EventHandler(this.cbxFilial_SelectedIndexChanged);
            // 
            // lblFilial
            // 
            this.lblFilial.AutoSize = true;
            this.lblFilial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilial.Location = new System.Drawing.Point(14, 36);
            this.lblFilial.Name = "lblFilial";
            this.lblFilial.Size = new System.Drawing.Size(36, 16);
            this.lblFilial.TabIndex = 5;
            this.lblFilial.Text = "Filial";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Data inicial";
            // 
            // lblProcessando
            // 
            this.lblProcessando.AutoSize = true;
            this.lblProcessando.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcessando.Location = new System.Drawing.Point(6, 305);
            this.lblProcessando.Name = "lblProcessando";
            this.lblProcessando.Size = new System.Drawing.Size(195, 16);
            this.lblProcessando.TabIndex = 8;
            this.lblProcessando.Text = "Reprocessamento de estoque";
            // 
            // lblQuantUpdates
            // 
            this.lblQuantUpdates.AutoSize = true;
            this.lblQuantUpdates.Location = new System.Drawing.Point(324, 337);
            this.lblQuantUpdates.Name = "lblQuantUpdates";
            this.lblQuantUpdates.Size = new System.Drawing.Size(13, 13);
            this.lblQuantUpdates.TabIndex = 9;
            this.lblQuantUpdates.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(217, 336);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Registros atualizados:";
            // 
            // cbxProduto
            // 
            this.cbxProduto.AutoSize = true;
            this.cbxProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxProduto.Location = new System.Drawing.Point(19, 166);
            this.cbxProduto.Name = "cbxProduto";
            this.cbxProduto.Size = new System.Drawing.Size(15, 14);
            this.cbxProduto.TabIndex = 11;
            this.cbxProduto.UseVisualStyleBackColor = true;
            this.cbxProduto.CheckStateChanged += new System.EventHandler(this.cbxProduto_CheckStateChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 16);
            this.label3.TabIndex = 15;
            this.label3.Text = "Produto selecionado";
            // 
            // rtxt_log
            // 
            this.rtxt_log.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxt_log.Location = new System.Drawing.Point(8, 20);
            this.rtxt_log.Name = "rtxt_log";
            this.rtxt_log.Size = new System.Drawing.Size(255, 285);
            this.rtxt_log.TabIndex = 16;
            this.rtxt_log.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rtxt_log);
            this.groupBox1.Controls.Add(this.lblProcessando);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 10.25F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(355, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(269, 321);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Log De Eventos";
            // 
            // txtProdDesc
            // 
            this.txtProdDesc.Enabled = false;
            this.txtProdDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProdDesc.Location = new System.Drawing.Point(117, 161);
            this.txtProdDesc.Name = "txtProdDesc";
            this.txtProdDesc.Size = new System.Drawing.Size(219, 22);
            this.txtProdDesc.TabIndex = 18;
            // 
            // mtxtData
            // 
            this.mtxtData.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxtData.Location = new System.Drawing.Point(95, 83);
            this.mtxtData.Name = "mtxtData";
            this.mtxtData.Size = new System.Drawing.Size(152, 24);
            this.mtxtData.TabIndex = 19;
            // 
            // txtCodProd
            // 
            this.txtCodProd.Enabled = false;
            this.txtCodProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodProd.Location = new System.Drawing.Point(40, 160);
            this.txtCodProd.Name = "txtCodProd";
            this.txtCodProd.Size = new System.Drawing.Size(71, 22);
            this.txtCodProd.TabIndex = 20;
            this.txtCodProd.TextChanged += new System.EventHandler(this.txtCodProd_TextChanged);
            this.txtCodProd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodProd_KeyPress);
            // 
            // pgbProcesso
            // 
            this.pgbProcesso.Location = new System.Drawing.Point(19, 310);
            this.pgbProcesso.Name = "pgbProcesso";
            this.pgbProcesso.Size = new System.Drawing.Size(317, 23);
            this.pgbProcesso.TabIndex = 21;
            // 
            // txtApartirDeProd
            // 
            this.txtApartirDeProd.Enabled = false;
            this.txtApartirDeProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtApartirDeProd.Location = new System.Drawing.Point(40, 224);
            this.txtApartirDeProd.Name = "txtApartirDeProd";
            this.txtApartirDeProd.Size = new System.Drawing.Size(71, 20);
            this.txtApartirDeProd.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 201);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(146, 15);
            this.label4.TabIndex = 23;
            this.label4.Text = "Iniciar a partir de produto:";
            // 
            // cbxApartirDeProd
            // 
            this.cbxApartirDeProd.AutoSize = true;
            this.cbxApartirDeProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxApartirDeProd.Location = new System.Drawing.Point(19, 226);
            this.cbxApartirDeProd.Name = "cbxApartirDeProd";
            this.cbxApartirDeProd.Size = new System.Drawing.Size(15, 14);
            this.cbxApartirDeProd.TabIndex = 22;
            this.cbxApartirDeProd.UseVisualStyleBackColor = true;
            this.cbxApartirDeProd.CheckedChanged += new System.EventHandler(this.cbxApartirDeProd_CheckedChanged);
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(636, 365);
            this.Controls.Add(this.txtApartirDeProd);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbxApartirDeProd);
            this.Controls.Add(this.pgbProcesso);
            this.Controls.Add(this.txtCodProd);
            this.Controls.Add(this.mtxtData);
            this.Controls.Add(this.txtProdDesc);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbxProduto);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblQuantUpdates);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblFilial);
            this.Controls.Add(this.cbxFilial);
            this.Controls.Add(this.btnProcessar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form4";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reprocessamento de estoque";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnProcessar;
        private System.Windows.Forms.ComboBox cbxFilial;
        private System.Windows.Forms.Label lblFilial;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblProcessando;
        private System.Windows.Forms.Label lblQuantUpdates;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbxProduto;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox rtxt_log;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtProdDesc;
        private System.Windows.Forms.DateTimePicker mtxtData;
        private System.Windows.Forms.TextBox txtCodProd;
        private System.Windows.Forms.ProgressBar pgbProcesso;
        private System.Windows.Forms.TextBox txtApartirDeProd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbxApartirDeProd;
    }
}