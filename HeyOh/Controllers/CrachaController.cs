using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeyOh.Models;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace HeyOh.Controllers
{
    public class CrachaController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CriarCrachas(Cracha cracha)
        {

            if (cracha.layoutCracha != null && cracha.participantes != null)
            {

                var backgroundCracha = cracha.layoutCracha;
                SalvarImagem(backgroundCracha);

                var caminhoBackgroundCracha = PegarCaminhoImagem(backgroundCracha.FileName);

                //tamanho do arquivo para memória
                MemoryStream workStream = new MemoryStream();

                //nome do pdf a ser criado
                DateTime dTime = DateTime.Now;
                string nomeDocumentoPDF = string.Format("Crachas" + dTime.ToString("yyyyMMddHHmmss"));

                //instância documento
                Document documentoPDF = new Document();
                documentoPDF.SetMargins(0f, 0f, 0f, 0f);

                //cria documento pdf
                string strAttachment = Server.MapPath("~/Content/PDFs/" + nomeDocumentoPDF);
                PdfWriter.GetInstance(documentoPDF, workStream).CloseStream = false;

                //insere imagem e lista de participantes no pdf
                documentoPDF.Open();

                documentoPDF.Add(new Paragraph(cracha.participantes));

                Image backgroundImagemCracha = Image.GetInstance(caminhoBackgroundCracha);
                documentoPDF.Add(backgroundImagemCracha);

                documentoPDF.Close();

                //memória para inserir pdf
                byte[] byteInfo = workStream.ToArray();
                workStream.Write(byteInfo, 0, byteInfo.Length);
                workStream.Position = 0;

                //chama o arquivo pdf para baixar
                return DownloadPDF(workStream, nomeDocumentoPDF);

            }
            else
            {

                ViewBag.Cracha = "Não foi possível criar crachá.Verifique se os campos foram preenchidos.";
                return View("Index");
            }

        }

        /// <summary>
        /// Método usado para salvar o arquivo de imagem vinda do formulário
        /// </summary>
        /// <param name="imagemArquivo"></param>
        public void SalvarImagem(HttpPostedFileBase imagemArquivo)
        {
            var caminhoImagem = PegarCaminhoImagem(imagemArquivo.FileName);
            imagemArquivo.SaveAs(caminhoImagem);
        }

        /// <summary>
        /// Método usado para pegar o caminho do diretório onde está a imagem
        /// </summary>
        /// <param name="nomeImagem"></param>
        /// <returns></returns>
        public string PegarCaminhoImagem(string nomeImagem)
        {
            var caminhoBase = Server.MapPath("~/Content/Images");
            string caminhoImagem = Path.Combine(caminhoBase, Path.GetFileName(nomeImagem));

            return caminhoImagem;
        }

        /// <summary>
        /// Método usado para retornar o arquivo a ser baixado pelo usuário
        /// </summary>
        /// <param name="workStream"></param>
        /// <param name="nomeImagem"></param>
        /// <returns></returns>
        public ActionResult DownloadPDF(MemoryStream workStream, string nomeImagem)
        {
            string contentType = "application/pdf";
            return File(workStream, contentType, nomeImagem+".pdf");

        }
    }
}