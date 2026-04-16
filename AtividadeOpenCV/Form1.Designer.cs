namespace AtividadeOpenCV
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.PictureBox pictureBoxAtual;
        private System.Windows.Forms.PictureBox pictureBoxComparacao;
        private System.Windows.Forms.Button btnCarregar;
        private System.Windows.Forms.Button btnDesembaralhar;
        private System.Windows.Forms.Button btnCinza;
        private System.Windows.Forms.Button btnMediana;
        private System.Windows.Forms.Button btnGaussiano;
        private System.Windows.Forms.Button btnAbsDiff;
        private System.Windows.Forms.Button btnThreshold;
        private System.Windows.Forms.Button btnDilatacao;
        private System.Windows.Forms.Button btnErosao;
        private System.Windows.Forms.Button btnComparar;
        private System.Windows.Forms.Button btnVerEtapa;
        private System.Windows.Forms.ListBox listBoxEtapas;
        private System.Windows.Forms.Label lblAtual;

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
            pictureBoxAtual = new PictureBox();
            pictureBoxComparacao = new PictureBox();
            btnCarregar = new Button();
            btnDesembaralhar = new Button();
            btnCinza = new Button();
            btnMediana = new Button();
            btnGaussiano = new Button();
            btnAbsDiff = new Button();
            btnThreshold = new Button();
            btnDilatacao = new Button();
            btnErosao = new Button();
            btnComparar = new Button();
            btnVerEtapa = new Button();
            listBoxEtapas = new ListBox();
            lblAtual = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBoxAtual).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxComparacao).BeginInit();
            SuspendLayout();
            // 
            // pictureBoxAtual
            // 
            pictureBoxAtual.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxAtual.Location = new Point(20, 20);
            pictureBoxAtual.Name = "pictureBoxAtual";
            pictureBoxAtual.Size = new Size(500, 500);
            pictureBoxAtual.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxAtual.TabIndex = 0;
            pictureBoxAtual.TabStop = false;
            // 
            // pictureBoxComparacao
            // 
            pictureBoxComparacao.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxComparacao.Location = new Point(540, 20);
            pictureBoxComparacao.Name = "pictureBoxComparacao";
            pictureBoxComparacao.Size = new Size(500, 500);
            pictureBoxComparacao.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxComparacao.TabIndex = 1;
            pictureBoxComparacao.TabStop = false;
            // 
            // btnCarregar
            // 
            btnCarregar.Location = new Point(20, 550);
            btnCarregar.Name = "btnCarregar";
            btnCarregar.Size = new Size(140, 35);
            btnCarregar.TabIndex = 3;
            btnCarregar.Text = "Carregar";
            btnCarregar.UseVisualStyleBackColor = true;
            btnCarregar.Click += btnCarregar_Click;
            // 
            // btnDesembaralhar
            // 
            btnDesembaralhar.Location = new Point(170, 550);
            btnDesembaralhar.Name = "btnDesembaralhar";
            btnDesembaralhar.Size = new Size(140, 35);
            btnDesembaralhar.TabIndex = 4;
            btnDesembaralhar.Text = "Desembaralhar";
            btnDesembaralhar.UseVisualStyleBackColor = true;
            btnDesembaralhar.Click += btnDesembaralhar_Click;
            // 
            // btnCinza
            // 
            btnCinza.Location = new Point(320, 550);
            btnCinza.Name = "btnCinza";
            btnCinza.Size = new Size(140, 35);
            btnCinza.TabIndex = 5;
            btnCinza.Text = "Cinza";
            btnCinza.UseVisualStyleBackColor = true;
            btnCinza.Click += btnCinza_Click;
            // 
            // btnMediana
            // 
            btnMediana.Location = new Point(470, 550);
            btnMediana.Name = "btnMediana";
            btnMediana.Size = new Size(140, 35);
            btnMediana.TabIndex = 6;
            btnMediana.Text = "Mediana";
            btnMediana.UseVisualStyleBackColor = true;
            btnMediana.Click += btnMediana_Click;
            // 
            // btnGaussiano
            // 
            btnGaussiano.Location = new Point(20, 600);
            btnGaussiano.Name = "btnGaussiano";
            btnGaussiano.Size = new Size(140, 35);
            btnGaussiano.TabIndex = 7;
            btnGaussiano.Text = "Gaussiano";
            btnGaussiano.UseVisualStyleBackColor = true;
            btnGaussiano.Click += btnGaussiano_Click;
            // 
            // btnAbsDiff
            // 
            btnAbsDiff.Location = new Point(170, 600);
            btnAbsDiff.Name = "btnAbsDiff";
            btnAbsDiff.Size = new Size(140, 35);
            btnAbsDiff.TabIndex = 8;
            btnAbsDiff.Text = "AbsDiff";
            btnAbsDiff.UseVisualStyleBackColor = true;
            btnAbsDiff.Click += btnAbsDiff_Click;
            // 
            // btnThreshold
            // 
            btnThreshold.Location = new Point(320, 600);
            btnThreshold.Name = "btnThreshold";
            btnThreshold.Size = new Size(140, 35);
            btnThreshold.TabIndex = 9;
            btnThreshold.Text = "Threshold";
            btnThreshold.UseVisualStyleBackColor = true;
            btnThreshold.Click += btnThreshold_Click;
            // 
            // btnDilatacao
            // 
            btnDilatacao.Location = new Point(470, 600);
            btnDilatacao.Name = "btnDilatacao";
            btnDilatacao.Size = new Size(140, 35);
            btnDilatacao.TabIndex = 10;
            btnDilatacao.Text = "Dilatação";
            btnDilatacao.UseVisualStyleBackColor = true;
            btnDilatacao.Click += btnDilatacao_Click;
            // 
            // btnErosao
            // 
            btnErosao.Location = new Point(20, 650);
            btnErosao.Name = "btnErosao";
            btnErosao.Size = new Size(140, 35);
            btnErosao.TabIndex = 11;
            btnErosao.Text = "Erosão";
            btnErosao.UseVisualStyleBackColor = true;
            btnErosao.Click += btnErosao_Click;
            // 
            // btnComparar
            // 
            btnComparar.Location = new Point(170, 650);
            btnComparar.Name = "btnComparar";
            btnComparar.Size = new Size(140, 35);
            btnComparar.TabIndex = 12;
            btnComparar.Text = "Comparar";
            btnComparar.UseVisualStyleBackColor = true;
            btnComparar.Click += btnComparar_Click;
            // 
            // btnVerEtapa
            // 
            btnVerEtapa.Location = new Point(320, 650);
            btnVerEtapa.Name = "btnVerEtapa";
            btnVerEtapa.Size = new Size(140, 35);
            btnVerEtapa.TabIndex = 13;
            btnVerEtapa.Text = "Ver Etapa";
            btnVerEtapa.UseVisualStyleBackColor = true;
            btnVerEtapa.Click += btnVerEtapa_Click;
            // 
            // listBoxEtapas
            // 
            listBoxEtapas.FormattingEnabled = true;
            listBoxEtapas.ItemHeight = 15;
            listBoxEtapas.Location = new Point(1060, 20);
            listBoxEtapas.Name = "listBoxEtapas";
            listBoxEtapas.Size = new Size(250, 484);
            listBoxEtapas.TabIndex = 2;
            // 
            // lblAtual
            // 
            lblAtual.AutoSize = true;
            lblAtual.Location = new Point(743, 532);
            lblAtual.Name = "lblAtual";
            lblAtual.Size = new Size(80, 15);
            lblAtual.TabIndex = 14;
            lblAtual.Text = "Imagem atual";
            // 
            // Form1
            // 
            ClientSize = new Size(1340, 720);
            Controls.Add(lblAtual);
            Controls.Add(btnVerEtapa);
            Controls.Add(btnComparar);
            Controls.Add(btnErosao);
            Controls.Add(btnDilatacao);
            Controls.Add(btnThreshold);
            Controls.Add(btnAbsDiff);
            Controls.Add(btnGaussiano);
            Controls.Add(btnMediana);
            Controls.Add(btnCinza);
            Controls.Add(btnDesembaralhar);
            Controls.Add(btnCarregar);
            Controls.Add(listBoxEtapas);
            Controls.Add(pictureBoxComparacao);
            Controls.Add(pictureBoxAtual);
            Name = "Form1";
            Text = "Recuperação de QR Code";
            ((System.ComponentModel.ISupportInitialize)pictureBoxAtual).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxComparacao).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}