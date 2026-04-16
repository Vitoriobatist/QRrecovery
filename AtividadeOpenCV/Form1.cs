using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace AtividadeOpenCV
{
    public partial class Form1 : Form
    {
        private Mat imagemOriginal;
        private Mat imagemAtual;
        private Mat imagemAnterior;
        private readonly List<EtapaImagem> etapas = new List<EtapaImagem>();

        public Form1()
        {
            InitializeComponent();
        }

        private void SalvarEtapa(string nome, Mat img)
        {
            if (img == null || img.Empty()) return;

            etapas.Add(new EtapaImagem
            {
                Nome = nome,
                Imagem = img.Clone()
            });

            listBoxEtapas.Items.Add(nome);
        }

        // ESQUERDA = ORIGINAL FIXA
        private void MostrarImagemOriginal(Mat img)
        {
            if (img == null || img.Empty()) return;
            pictureBoxAtual.Image = BitmapConverter.ToBitmap(img);
        }

        // MEIO = IMAGEM PROCESSADA ATUAL
        private void MostrarImagemProcessada(Mat img)
        {
            if (img == null || img.Empty()) return;
            pictureBoxComparacao.Image = BitmapConverter.ToBitmap(img);
        }

        private bool ExecutarPython(string operacao, bool usaAnterior = false)
        {
            if (imagemAtual == null || imagemAtual.Empty())
                return false;

            string pastaTemp = Path.Combine(Application.StartupPath, "temp");
            Directory.CreateDirectory(pastaTemp);

            string entradaPath = Path.Combine(pastaTemp, "entrada.png");
            string saidaPath = Path.Combine(pastaTemp, "saida.png");
            string anteriorPath = Path.Combine(pastaTemp, "anterior.png");
            string scriptPath = Path.Combine(Application.StartupPath, "processar.py");

            if (!File.Exists(scriptPath))
            {
                MessageBox.Show("Arquivo processar.py não encontrado em:\n" + scriptPath);
                return false;
            }

            try
            {
                Cv2.ImWrite(entradaPath, imagemAtual);

                if (usaAnterior)
                {
                    if (imagemAnterior == null || imagemAnterior.Empty())
                    {
                        MessageBox.Show("Não existe imagem anterior para usar no AbsDiff.");
                        return false;
                    }

                    Cv2.ImWrite(anteriorPath, imagemAnterior);
                }

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "python",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    WorkingDirectory = Application.StartupPath
                };

                psi.Arguments = usaAnterior
                    ? $"\"{scriptPath}\" {operacao} \"{entradaPath}\" \"{saidaPath}\" \"{anteriorPath}\""
                    : $"\"{scriptPath}\" {operacao} \"{entradaPath}\" \"{saidaPath}\"";

                using (Process processo = Process.Start(psi))
                {
                    string saida = processo.StandardOutput.ReadToEnd();
                    string erro = processo.StandardError.ReadToEnd();
                    processo.WaitForExit();

                    if (processo.ExitCode != 0)
                    {
                        MessageBox.Show("Erro ao executar Python:\n\n" + erro + "\n" + saida);
                        return false;
                    }
                }

                if (!File.Exists(saidaPath))
                {
                    MessageBox.Show("O Python não gerou a imagem de saída.");
                    return false;
                }

                Mat resultado = Cv2.ImRead(saidaPath, ImreadModes.Unchanged);
                if (resultado == null || resultado.Empty())
                {
                    MessageBox.Show("Não foi possível ler a imagem processada.");
                    return false;
                }

                imagemAtual = resultado;
                MostrarImagemProcessada(imagemAtual);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao chamar o Python:\n\n" + ex.Message);
                return false;
            }
        }

        private bool ExecutarPythonVisual(string operacao, bool usaAnterior = false)
        {
            if (imagemAtual == null || imagemAtual.Empty())
                return false;

            string pastaTemp = Path.Combine(Application.StartupPath, "temp");
            Directory.CreateDirectory(pastaTemp);

            string entradaPath = Path.Combine(pastaTemp, "entrada.png");
            string saidaPath = Path.Combine(pastaTemp, "saida_visual.png");
            string anteriorPath = Path.Combine(pastaTemp, "anterior.png");
            string scriptPath = Path.Combine(Application.StartupPath, "processar.py");

            if (!File.Exists(scriptPath))
            {
                MessageBox.Show("Arquivo processar.py não encontrado em:\n" + scriptPath);
                return false;
            }

            try
            {
                Cv2.ImWrite(entradaPath, imagemAtual);

                if (usaAnterior)
                {
                    if (imagemAnterior == null || imagemAnterior.Empty())
                    {
                        MessageBox.Show("Não existe imagem anterior para usar no AbsDiff.");
                        return false;
                    }

                    Cv2.ImWrite(anteriorPath, imagemAnterior);
                }

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "python",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    WorkingDirectory = Application.StartupPath
                };

                psi.Arguments = usaAnterior
                    ? $"\"{scriptPath}\" {operacao} \"{entradaPath}\" \"{saidaPath}\" \"{anteriorPath}\""
                    : $"\"{scriptPath}\" {operacao} \"{entradaPath}\" \"{saidaPath}\"";

                using (Process processo = Process.Start(psi))
                {
                    string saida = processo.StandardOutput.ReadToEnd();
                    string erro = processo.StandardError.ReadToEnd();
                    processo.WaitForExit();

                    if (processo.ExitCode != 0)
                    {
                        MessageBox.Show("Erro ao executar Python:\n\n" + erro + "\n" + saida);
                        return false;
                    }
                }

                if (!File.Exists(saidaPath))
                {
                    MessageBox.Show("O Python não gerou a imagem visual.");
                    return false;
                }

                Mat resultado = Cv2.ImRead(saidaPath, ImreadModes.Unchanged);
                if (resultado == null || resultado.Empty())
                {
                    MessageBox.Show("Não foi possível ler a imagem visual.");
                    return false;
                }

                // IMPORTANTE: só mostra, não altera imagemAtual
                MostrarImagemProcessada(resultado);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao chamar o Python:\n\n" + ex.Message);
                return false;
            }
        }

        private void btnCarregar_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Imagens|*.png;*.jpg;*.jpeg;*.bmp";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    imagemOriginal = Cv2.ImRead(ofd.FileName, ImreadModes.Color);

                    if (imagemOriginal == null || imagemOriginal.Empty())
                    {
                        MessageBox.Show("Não foi possível carregar a imagem.");
                        return;
                    }

                    imagemAtual = imagemOriginal.Clone();
                    imagemAnterior = null;

                    etapas.Clear();
                    listBoxEtapas.Items.Clear();

                    MostrarImagemOriginal(imagemOriginal);
                    MostrarImagemProcessada(imagemAtual);

                    SalvarEtapa("Original carregada", imagemAtual);
                    lblAtual.Text = "Imagem atual: Original carregada";
                }
            }
        }

        private void btnDesembaralhar_Click(object sender, EventArgs e)
        {
            if (imagemAtual == null) return;

            imagemAnterior = imagemAtual.Clone();

            if (ExecutarPython("desembaralhar"))
            {
                SalvarEtapa("Desembaralhamento", imagemAtual);
                lblAtual.Text = "Imagem atual: Desembaralhamento";
            }
        }

        private void btnCinza_Click(object sender, EventArgs e)
        {
            if (imagemAtual == null) return;

            imagemAnterior = imagemAtual.Clone();

            if (ExecutarPython("cinza"))
            {
                SalvarEtapa("Escala de cinza", imagemAtual);
                lblAtual.Text = "Imagem atual: Escala de cinza";
            }
        }

        private void btnMediana_Click(object sender, EventArgs e)
        {
            if (imagemAtual == null) return;

            imagemAnterior = imagemAtual.Clone();

            if (ExecutarPython("mediana"))
            {
                SalvarEtapa("Filtro mediana", imagemAtual);
                lblAtual.Text = "Imagem atual: Filtro mediana";
            }
        }

        private void btnGaussiano_Click(object sender, EventArgs e)
        {
            if (imagemAtual == null) return;

            imagemAnterior = imagemAtual.Clone();

            if (ExecutarPython("gaussiano"))
            {
                SalvarEtapa("Filtro gaussiano", imagemAtual);
                lblAtual.Text = "Imagem atual: Filtro gaussiano";
            }
        }

        private void btnAbsDiff_Click(object sender, EventArgs e)
        {
            if (imagemAtual == null || imagemAnterior == null) return;

            // Só visualiza, não altera o fluxo principal
            if (ExecutarPythonVisual("absdiff", true))
            {
                Mat absDiff = Cv2.ImRead(Path.Combine(Application.StartupPath, "temp", "saida_visual.png"));
                SalvarEtapa("AbsDiff", absDiff);
                lblAtual.Text = "Imagem atual: Diferença absoluta (visualização)";
            }
        }

        private void btnThreshold_Click(object sender, EventArgs e)
        {
            if (imagemAtual == null) return;

            imagemAnterior = imagemAtual.Clone();

            if (ExecutarPython("threshold"))
            {
                SalvarEtapa("Threshold preto/branco", imagemAtual);
                lblAtual.Text = "Imagem atual: Threshold preto/branco";
            }
        }

        private void btnDilatacao_Click(object sender, EventArgs e)
        {
            if (imagemAtual == null) return;

            imagemAnterior = imagemAtual.Clone();

            if (ExecutarPython("dilatacao"))
            {
                SalvarEtapa("Dilatação", imagemAtual);
                lblAtual.Text = "Imagem atual: Dilatação";
            }
        }

        private void btnErosao_Click(object sender, EventArgs e)
        {
            if (imagemAtual == null) return;

            imagemAnterior = imagemAtual.Clone();

            if (ExecutarPython("erosao"))
            {
                SalvarEtapa("Erosão", imagemAtual);
                lblAtual.Text = "Imagem atual: Erosão";
            }
        }

        private void btnVerEtapa_Click(object sender, EventArgs e)
        {
            if (listBoxEtapas.SelectedIndex < 0) return;
            if (listBoxEtapas.SelectedIndex >= etapas.Count) return;

            EtapaImagem etapa = etapas[listBoxEtapas.SelectedIndex];

            MostrarImagemOriginal(imagemOriginal);

            if (etapa.Nome == "AbsDiff")
            {
                string caminho = Path.Combine(Application.StartupPath, "temp", "saida_visual.png");

                if (File.Exists(caminho))
                {
                    Mat imgAbsDiff = Cv2.ImRead(caminho);
                    MostrarImagemProcessada(imgAbsDiff);
                }
                else
                {
                    MostrarImagemProcessada(etapa.Imagem);
                }
            }
            else
            {
                MostrarImagemProcessada(etapa.Imagem);
            }

            lblAtual.Text = "Visualizando etapa: " + etapa.Nome;
        }

        private void btnComparar_Click(object sender, EventArgs e)
        {
            if (imagemOriginal == null || imagemAtual == null) return;

            MostrarImagemOriginal(imagemOriginal);
            MostrarImagemProcessada(imagemAtual);
            lblAtual.Text = "Comparando imagem original com a imagem final";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}