using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Kundbolaget.EntityFramework.Context;
using Kundbolaget.Models.EntityModels;

namespace Kundbolaget.PDFGenerator
{
    public class PdfGenerator
    {
        private static StoreContext db;
        public void ExportToPdf(int id)
        {
            //db = new OrderDetails();

            db = new StoreContext();
            var query = from c in db.OrderDetails where c.OrderId == id
                        select c;
                         

            Document doc = new Document();
            //PdfWriter.GetInstance(doc, new FileStream("C:/Users/Shkomi/Documents/test.pdf", FileMode.Create));
            string path = "C:/Users/Shkomi/Documents/test5.pdf";
            FileStream fS = File.Create(path);
            PdfWriter.GetInstance(doc, fS);
            doc.Open();

            //doc.Add(new Paragraph("Heelo"));
            foreach (var d in query)
            {

                doc.Add(new Paragraph($"{d.Amount}"));
            }

            doc.Close();



        }
    }
}