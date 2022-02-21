
namespace ConfPDV
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.processamentosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cadastrosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.estoqueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refazerDiárioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recalcularHistDeMovimentaçãoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vendasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recalcularMargemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importaçõesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importaçãoPLUToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip2
            // 
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.processamentosToolStripMenuItem,
            this.importaçõesToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(620, 24);
            this.menuStrip2.TabIndex = 3;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // processamentosToolStripMenuItem
            // 
            this.processamentosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cadastrosToolStripMenuItem,
            this.estoqueToolStripMenuItem,
            this.vendasToolStripMenuItem});
            this.processamentosToolStripMenuItem.Name = "processamentosToolStripMenuItem";
            this.processamentosToolStripMenuItem.Size = new System.Drawing.Size(105, 20);
            this.processamentosToolStripMenuItem.Text = "Processamentos";
            // 
            // cadastrosToolStripMenuItem
            // 
            this.cadastrosToolStripMenuItem.Name = "cadastrosToolStripMenuItem";
            this.cadastrosToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.cadastrosToolStripMenuItem.Text = "Cadastros";
            // 
            // estoqueToolStripMenuItem
            // 
            this.estoqueToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refazerDiárioToolStripMenuItem,
            this.recalcularHistDeMovimentaçãoToolStripMenuItem});
            this.estoqueToolStripMenuItem.Name = "estoqueToolStripMenuItem";
            this.estoqueToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.estoqueToolStripMenuItem.Text = "Estoque";
            // 
            // refazerDiárioToolStripMenuItem
            // 
            this.refazerDiárioToolStripMenuItem.Name = "refazerDiárioToolStripMenuItem";
            this.refazerDiárioToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.refazerDiárioToolStripMenuItem.Text = "Refazer Diário";
            this.refazerDiárioToolStripMenuItem.Click += new System.EventHandler(this.refazerDiárioToolStripMenuItem_Click);
            // 
            // recalcularHistDeMovimentaçãoToolStripMenuItem
            // 
            this.recalcularHistDeMovimentaçãoToolStripMenuItem.Name = "recalcularHistDeMovimentaçãoToolStripMenuItem";
            this.recalcularHistDeMovimentaçãoToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.recalcularHistDeMovimentaçãoToolStripMenuItem.Text = "Recalcular Hist. de Movimentação";
            this.recalcularHistDeMovimentaçãoToolStripMenuItem.Click += new System.EventHandler(this.recalcularHistDeMovimentaçãoToolStripMenuItem_Click);
            // 
            // vendasToolStripMenuItem
            // 
            this.vendasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.recalcularMargemToolStripMenuItem});
            this.vendasToolStripMenuItem.Name = "vendasToolStripMenuItem";
            this.vendasToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.vendasToolStripMenuItem.Text = "Vendas";
            // 
            // recalcularMargemToolStripMenuItem
            // 
            this.recalcularMargemToolStripMenuItem.Name = "recalcularMargemToolStripMenuItem";
            this.recalcularMargemToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.recalcularMargemToolStripMenuItem.Text = "Recalcular Curva ABC";
            this.recalcularMargemToolStripMenuItem.Click += new System.EventHandler(this.recalcularMargemToolStripMenuItem_Click);
            // 
            // importaçõesToolStripMenuItem
            // 
            this.importaçõesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importaçãoPLUToolStripMenuItem});
            this.importaçõesToolStripMenuItem.Name = "importaçõesToolStripMenuItem";
            this.importaçõesToolStripMenuItem.Size = new System.Drawing.Size(85, 20);
            this.importaçõesToolStripMenuItem.Text = "Importações";
            // 
            // importaçãoPLUToolStripMenuItem
            // 
            this.importaçãoPLUToolStripMenuItem.Name = "importaçãoPLUToolStripMenuItem";
            this.importaçãoPLUToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.importaçãoPLUToolStripMenuItem.Text = "Importação de PLU";
            this.importaçãoPLUToolStripMenuItem.Click += new System.EventHandler(this.importaçãoPLUToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 309);
            this.Controls.Add(this.menuStrip2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Linear Processamentos";
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem processamentosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cadastrosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem estoqueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refazerDiárioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recalcularHistDeMovimentaçãoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vendasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importaçõesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importaçãoPLUToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recalcularMargemToolStripMenuItem;
    }
}

