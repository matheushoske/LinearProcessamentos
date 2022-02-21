
namespace ConfPDV
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.btnProcessar = new System.Windows.Forms.Button();
            this.rbtDiario = new System.Windows.Forms.RadioButton();
            this.rbtAtual = new System.Windows.Forms.RadioButton();
            this.rbtMargem = new System.Windows.Forms.RadioButton();
            this.cbxFilial = new System.Windows.Forms.ComboBox();
            this.cbxProduto = new System.Windows.Forms.CheckBox();
            this.txtCodProd = new System.Windows.Forms.TextBox();
            this.txtProdDesc = new System.Windows.Forms.TextBox();
            this.dtpDataInicial = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxTodos = new System.Windows.Forms.CheckBox();
            this.cbxNegativa = new System.Windows.Forms.CheckBox();
            this.rbtResumo = new System.Windows.Forms.RadioButton();
            this.dtpDataFinal = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rtxt_log = new System.Windows.Forms.RichTextBox();
            this.pgbProcesso = new System.Windows.Forms.ProgressBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblQuantUpdates = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnProcessar
            // 
            this.btnProcessar.Location = new System.Drawing.Point(134, 379);
            this.btnProcessar.Name = "btnProcessar";
            this.btnProcessar.Size = new System.Drawing.Size(101, 35);
            this.btnProcessar.TabIndex = 1;
            this.btnProcessar.Text = "Processar";
            this.btnProcessar.UseVisualStyleBackColor = true;
            this.btnProcessar.Click += new System.EventHandler(this.btnProcessar_Click);
            // 
            // rbtDiario
            // 
            this.rbtDiario.AutoSize = true;
            this.rbtDiario.Location = new System.Drawing.Point(5, 42);
            this.rbtDiario.Name = "rbtDiario";
            this.rbtDiario.Size = new System.Drawing.Size(314, 19);
            this.rbtDiario.TabIndex = 2;
            this.rbtDiario.TabStop = true;
            this.rbtDiario.Text = "Considerar Pr.Custo e Compra da Data (Fech. Diário)";
            this.rbtDiario.UseVisualStyleBackColor = true;
            // 
            // rbtAtual
            // 
            this.rbtAtual.AutoSize = true;
            this.rbtAtual.Location = new System.Drawing.Point(6, 19);
            this.rbtAtual.Name = "rbtAtual";
            this.rbtAtual.Size = new System.Drawing.Size(309, 19);
            this.rbtAtual.TabIndex = 3;
            this.rbtAtual.TabStop = true;
            this.rbtAtual.Text = "Considerar Pr.Custo e Compra Atuais(Cad.Produtos)";
            this.rbtAtual.UseVisualStyleBackColor = true;
            // 
            // rbtMargem
            // 
            this.rbtMargem.AutoSize = true;
            this.rbtMargem.Location = new System.Drawing.Point(5, 65);
            this.rbtMargem.Name = "rbtMargem";
            this.rbtMargem.Size = new System.Drawing.Size(212, 19);
            this.rbtMargem.TabIndex = 4;
            this.rbtMargem.TabStop = true;
            this.rbtMargem.Text = "Apenas recalcular CMV e Margem";
            this.rbtMargem.UseVisualStyleBackColor = true;
            // 
            // cbxFilial
            // 
            this.cbxFilial.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxFilial.FormattingEnabled = true;
            this.cbxFilial.ItemHeight = 18;
            this.cbxFilial.Location = new System.Drawing.Point(60, 33);
            this.cbxFilial.Name = "cbxFilial";
            this.cbxFilial.Size = new System.Drawing.Size(298, 26);
            this.cbxFilial.TabIndex = 5;
            // 
            // cbxProduto
            // 
            this.cbxProduto.AutoSize = true;
            this.cbxProduto.Location = new System.Drawing.Point(37, 138);
            this.cbxProduto.Name = "cbxProduto";
            this.cbxProduto.Size = new System.Drawing.Size(15, 14);
            this.cbxProduto.TabIndex = 6;
            this.cbxProduto.UseVisualStyleBackColor = true;
            this.cbxProduto.CheckStateChanged += new System.EventHandler(this.cbxProduto_CheckStateChanged);
            // 
            // txtCodProd
            // 
            this.txtCodProd.Enabled = false;
            this.txtCodProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodProd.Location = new System.Drawing.Point(59, 133);
            this.txtCodProd.Name = "txtCodProd";
            this.txtCodProd.Size = new System.Drawing.Size(69, 22);
            this.txtCodProd.TabIndex = 7;
            this.txtCodProd.TextChanged += new System.EventHandler(this.txtCodProd_TextChanged);
            this.txtCodProd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodProd_KeyPress);
            // 
            // txtProdDesc
            // 
            this.txtProdDesc.Enabled = false;
            this.txtProdDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProdDesc.Location = new System.Drawing.Point(134, 133);
            this.txtProdDesc.Name = "txtProdDesc";
            this.txtProdDesc.Size = new System.Drawing.Size(224, 22);
            this.txtProdDesc.TabIndex = 8;
            // 
            // dtpDataInicial
            // 
            this.dtpDataInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDataInicial.Location = new System.Drawing.Point(95, 79);
            this.dtpDataInicial.Name = "dtpDataInicial";
            this.dtpDataInicial.Size = new System.Drawing.Size(92, 21);
            this.dtpDataInicial.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 15);
            this.label1.TabIndex = 20;
            this.label1.Text = "Data inicial";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxTodos);
            this.groupBox1.Controls.Add(this.cbxNegativa);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(32, 298);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(326, 72);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Atualizar Precificação";
            // 
            // cbxTodos
            // 
            this.cbxTodos.AutoSize = true;
            this.cbxTodos.Location = new System.Drawing.Point(6, 32);
            this.cbxTodos.Name = "cbxTodos";
            this.cbxTodos.Size = new System.Drawing.Size(125, 19);
            this.cbxTodos.TabIndex = 1;
            this.cbxTodos.Text = "Período Completo";
            this.cbxTodos.UseVisualStyleBackColor = true;
            this.cbxTodos.CheckedChanged += new System.EventHandler(this.cbxTodos_CheckedChanged);
            // 
            // cbxNegativa
            // 
            this.cbxNegativa.AutoSize = true;
            this.cbxNegativa.Location = new System.Drawing.Point(149, 32);
            this.cbxNegativa.Name = "cbxNegativa";
            this.cbxNegativa.Size = new System.Drawing.Size(166, 19);
            this.cbxNegativa.TabIndex = 0;
            this.cbxNegativa.Text = "Apenas margem negativa";
            this.cbxNegativa.UseVisualStyleBackColor = true;
            this.cbxNegativa.CheckedChanged += new System.EventHandler(this.cbxNegativa_CheckedChanged);
            // 
            // rbtResumo
            // 
            this.rbtResumo.AutoSize = true;
            this.rbtResumo.Location = new System.Drawing.Point(5, 88);
            this.rbtResumo.Name = "rbtResumo";
            this.rbtResumo.Size = new System.Drawing.Size(176, 19);
            this.rbtResumo.TabIndex = 23;
            this.rbtResumo.TabStop = true;
            this.rbtResumo.Text = "Recriar Resumo de Vendas";
            this.rbtResumo.UseVisualStyleBackColor = true;
            this.rbtResumo.CheckedChanged += new System.EventHandler(this.rbtResumo_CheckedChanged);
            // 
            // dtpDataFinal
            // 
            this.dtpDataFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDataFinal.Location = new System.Drawing.Point(266, 79);
            this.dtpDataFinal.Name = "dtpDataFinal";
            this.dtpDataFinal.Size = new System.Drawing.Size(92, 21);
            this.dtpDataFinal.TabIndex = 25;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(201, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 15);
            this.label2.TabIndex = 24;
            this.label2.Text = "Data final";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(21, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 15);
            this.label3.TabIndex = 26;
            this.label3.Text = "Produto Selecionado";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(21, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 15);
            this.label4.TabIndex = 27;
            this.label4.Text = "Filial";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbtAtual);
            this.groupBox2.Controls.Add(this.rbtDiario);
            this.groupBox2.Controls.Add(this.rbtMargem);
            this.groupBox2.Controls.Add(this.rbtResumo);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(32, 172);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(326, 120);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tipo de Processamento";
            // 
            // rtxt_log
            // 
            this.rtxt_log.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxt_log.Location = new System.Drawing.Point(9, 23);
            this.rtxt_log.Name = "rtxt_log";
            this.rtxt_log.Size = new System.Drawing.Size(276, 369);
            this.rtxt_log.TabIndex = 29;
            this.rtxt_log.Text = "";
            // 
            // pgbProcesso
            // 
            this.pgbProcesso.Location = new System.Drawing.Point(31, 422);
            this.pgbProcesso.Name = "pgbProcesso";
            this.pgbProcesso.Size = new System.Drawing.Size(326, 23);
            this.pgbProcesso.TabIndex = 30;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rtxt_log);
            this.groupBox3.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(396, 26);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(291, 398);
            this.groupBox3.TabIndex = 31;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Log de Eventos";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(483, 427);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "Registros atualizados:";
            // 
            // lblQuantUpdates
            // 
            this.lblQuantUpdates.AutoSize = true;
            this.lblQuantUpdates.Location = new System.Drawing.Point(591, 427);
            this.lblQuantUpdates.Name = "lblQuantUpdates";
            this.lblQuantUpdates.Size = new System.Drawing.Size(13, 13);
            this.lblQuantUpdates.TabIndex = 33;
            this.lblQuantUpdates.Text = "0";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 474);
            this.Controls.Add(this.lblQuantUpdates);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.pgbProcesso);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpDataFinal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dtpDataInicial);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtProdDesc);
            this.Controls.Add(this.txtCodProd);
            this.Controls.Add(this.cbxProduto);
            this.Controls.Add(this.cbxFilial);
            this.Controls.Add(this.btnProcessar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recalcular Curva ABC";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnProcessar;
        public static System.Windows.Forms.TextBox tbSetaPraCimaK;
        private System.Windows.Forms.RadioButton rbtDiario;
        private System.Windows.Forms.RadioButton rbtAtual;
        private System.Windows.Forms.RadioButton rbtMargem;
        private System.Windows.Forms.ComboBox cbxFilial;
        private System.Windows.Forms.CheckBox cbxProduto;
        private System.Windows.Forms.TextBox txtCodProd;
        private System.Windows.Forms.TextBox txtProdDesc;
        private System.Windows.Forms.DateTimePicker dtpDataInicial;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbxTodos;
        private System.Windows.Forms.CheckBox cbxNegativa;
        private System.Windows.Forms.RadioButton rbtResumo;
        private System.Windows.Forms.DateTimePicker dtpDataFinal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox rtxt_log;
        private System.Windows.Forms.ProgressBar pgbProcesso;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblQuantUpdates;
    }
}