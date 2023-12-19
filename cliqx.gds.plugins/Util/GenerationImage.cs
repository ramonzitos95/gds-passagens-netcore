using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using cliqx.gds.contract.GdsModels;

namespace cliqx.gds.plugins.Util
{
    public class GenerationImage
    {
        public static string montaLayout(IEnumerable<cliqx.gds.contract.GdsModels.Seat> layout, string imgUrl)
        {
            var imageUrl = imgUrl;
            string retorno = "";
            try
            {
                // CARREGA A IMAGEM


                Bitmap imagemBitmap = GetImageByName(imageUrl);
                Graphics graphicsImage = Graphics.FromImage(imagemBitmap);

                // CRIA A FONTE DO NUMERO DA POLTRONA E DESCRICAO
                int posicao = 0;
                int coluna = 0;


                foreach (var poltrona in layout)
                {
                    // PEGA O NUMERO DA POLTRONA
                    string numeroPoltrona = string.Format("{0:00}", poltrona.Number);

                    // VERIFICA SE POLTRONA É 410, SE SIM PULA PARA A PROXIMA INTERAÇÃO DO FOREACH

                    if (numeroPoltrona == "410") continue;

                    // CONVERTE O  VALOR DE X E Y PARA INTEIRO
                    int linhaPoltrona = Int32.Parse(poltrona.Layout.Y.ToString());
                    int colunaPoltrona = Int32.Parse(poltrona.Layout.X.ToString());

                    // REGRA QUE VERIFICA SE O NUMERO DA COLUNA DA POLTRONA MUDAR, ELE ADD +1 NO INDEX
                    if (coluna != colunaPoltrona)
                    {
                        posicao++;
                        coluna = colunaPoltrona;
                    }


                    // ADICIONA COR DA POLTRONA E COR DO NUMERO DA POLTRONA

                    var corPoltrona = poltrona.Vacant == true ? Brushes.MediumSeaGreen : Brushes.LightGray;
                    var corNumeroPoltrona = poltrona.Vacant == true ? Brushes.White : Brushes.Black;

                    // CONFIGURA FONTE PADRÃO

                    Font numeroPoltronaFonte = new Font("Arial", 18, FontStyle.Regular);

                    // VERIFICA SE O NUMERO DA POLTRONA É VALIDAO. CASO NÃO SEJA ALTERA O TIPO E TAMANHO DA FONTE
                    bool isPoltronaNumeroValido = ValidaNumero(poltrona.Number);
                    if (!isPoltronaNumeroValido)
                    {
                        corPoltrona = Brushes.Transparent;
                        numeroPoltronaFonte = new Font("Arial", 12, FontStyle.Regular);
                    }

                    // FAZ O CONTROLE DA LINHA PELO MENOR E MAIOR TAMANHO
                    int linhaMinimo = 37;
                    int linhaMaximo = 170;
                    int y = linhaMaximo - (linhaMinimo * linhaPoltrona);

                    // FAZ O CONTROLE DA COLUNA PELO MENOR TAMANHO QUE PODE TER
                    int colunaMinimo = 50;
                    int colunaMaximo = 650;
                    int x = colunaMinimo + (39 * posicao);

                    // CONFIGURA O RETANGULO QUE FICA ATRÁS DO FUNDO

                    Rectangle desenhoPoltrona = new Rectangle()
                    {
                        Width = 34,           // LARGURA DO RETANGULO
                        Height = 34,           // ALTURA DO RETANGULO
                        X = (x - 2),        // COLUNA QUE RETANGULO COMEÇA
                        Y = (y - 2)         // LINHA QUE O RETANGULO COMECA
                    };

                    graphicsImage.FillRectangle(corPoltrona, desenhoPoltrona);
                    graphicsImage.DrawString(numeroPoltrona, numeroPoltronaFonte, corNumeroPoltrona, x, y);
                }


                using (MemoryStream m = new MemoryStream())
                {
                    imagemBitmap.Save(m, System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] imageBytes = m.ToArray();

                    retorno = Convert.ToBase64String(imageBytes);
                }

                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro ao montar layout :{0}", ex.StackTrace));
                throw new Exception(string.Format("Error :{0}", ex.Message));
            }
        }

        public static bool ValidaNumero(string numero)
        {
            try
            {
                Int64.Parse(numero);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static Bitmap BytesToBitmap(byte[] byteArray)
        {

            try
            {
                using (MemoryStream ms = new MemoryStream(byteArray))
                {
                    Bitmap img = (Bitmap)Image.FromStream(ms);
                    return img;
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine($"BytesToBitmap >> {e.Message}");
                return null;
            }



        }
        private static Bitmap GetImageByName(string imageUrl)
        {

            byte[] image = new WebClient().DownloadData(imageUrl);
            var imageBitmap = BytesToBitmap(image);
            return imageBitmap;

        }

        
    }
}
