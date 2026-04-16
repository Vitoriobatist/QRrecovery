using OpenCvSharp;

namespace AtividadeOpenCV
{
    public class EtapaImagem
    {
        public string Nome { get; set; }
        public Mat Imagem { get; set; }

        public override string ToString()
        {
            return Nome;
        }
    }
}