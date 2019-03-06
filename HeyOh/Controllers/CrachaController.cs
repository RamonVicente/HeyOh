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
        public ActionResult CriarCrachas(Cracha cracha){

            //salva imagem num diretório
            if (cracha.layoutCracha != null){

                //instancia o background do crachá e cria um path
                var layoutCracha = cracha.layoutCracha;
                var uploadPath = Server.MapPath("~/Content/Images");
                string caminhoArquivo = Path.Combine(uploadPath, Path.GetFileName(layoutCracha.FileName));

                //salva a imagem no diretório
                layoutCracha.SaveAs(caminhoArquivo);
                
                MemoryStream workStream = new MemoryStream();

                DateTime dTime = DateTime.Now;
                string strPDFFileName = string.Format("Crachas" + dTime.ToString("yyyyMMddHHmmss"));

                Document documentoPDF = new Document(PageSize.A6);
                documentoPDF.SetMargins(0f, 0f, 0f, 0f);

                //file will created in this path  
                string strAttachment = Server.MapPath("~/Content/PDFs/" + strPDFFileName);
                PdfWriter.GetInstance(documentoPDF, workStream).CloseStream = false;

                documentoPDF.Open();

                documentoPDF.Add(new Paragraph(cracha.participantes));

                Image gif = Image.GetInstance(caminhoArquivo);
                documentoPDF.Add(gif);

                documentoPDF.Close();

                byte[] byteInfo = workStream.ToArray();
                workStream.Write(byteInfo, 0, byteInfo.Length);
                workStream.Position = 0;

                return DownloadPDF(workStream, strPDFFileName);

            } else {

                return ViewBag.Cracha = "Não foi possível criar crachá. Verifique se os campos foram preenchidos.";
            }

        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult retornaDados(Cracha cracha)
        {
            string teste = cracha.participantes;
            ViewBag.Cracha = teste;
            return View("Index");
        }

        //baixa do pdf
        public ActionResult DownloadPDF(MemoryStream workStream, string nomeImagem)
        {
            string contentType = "application/pdf";
            return File(workStream, contentType, nomeImagem+".pdf");

        }
    }
}