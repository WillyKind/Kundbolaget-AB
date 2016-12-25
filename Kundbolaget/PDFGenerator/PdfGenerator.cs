using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using Kundbolaget.EntityFramework.Context;
using Kundbolaget.Models.EntityModels;

namespace Kundbolaget.PDFGenerator
{
    public class PdfGenerator
    {
        private static StoreContext db;

        public void ExportToPdf(int id)
        {
            db = new StoreContext();
      
            var product = from od in db.OrderDetails where od.OrderId == id
                join p in db.ProductsInfoes on od.ProductInfoId equals p.Id
                select new
                {
                    od.OrderId, p.Name, od.Amount, od.UnitPrice, od.TotalPrice, Container = p.Container.Name, p.Volume
                };

            var company = from od in db.OrderDetails
                        where od.OrderId == id
                        select new
                        {
                            od.OrderId, od.Order.Company.Name, od.Order.Company.DeliveryAddress.Street, od.Order.Company.DeliveryAddress.Number,
                            od.Order.Company.DeliveryAddress.ZipCode
                        };

            Document doc = new Document();
            string path = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\_Följesedel_o{id}_c{company.FirstOrDefault().Name}{DateTime.Now.ToShortDateString()}.pdf";
            FileStream fS = File.Create(path);
            PdfWriter.GetInstance(doc, fS);
            //Document doc = new Document();
            //PdfWriter.GetInstance(doc, new FileStream("Följesedel.pdf", FileMode.Create));
            doc.Open();

            var fs20 = FontFactory.GetFont(FontFactory.HELVETICA, 20);
            var fs14 = FontFactory.GetFont(FontFactory.HELVETICA, 14);
            doc.Add(new Paragraph("Följesedel", fs20) { Alignment = Element.ALIGN_CENTER});
            doc.Add(new Paragraph("\n"));

            foreach (var c in company)
            {
                doc.Add(new Paragraph($"Beställare: {c.Name} ", fs14) { Alignment = Element.ALIGN_LEFT });
                doc.Add(new Paragraph($"Ordernummer: {c.OrderId} ", fs14) { Alignment = Element.ALIGN_LEFT });
                doc.Add(new Paragraph($"Leveransadress: {c.Street} {c.Number} {c.ZipCode} ", fs14) { Alignment = Element.ALIGN_LEFT });
                break;
            }

            doc.Add(new Paragraph("\n\n"));
            foreach (var item in product)
            {
                doc.Add(new Paragraph($"\n Produkt: {item.Name}, Antal: {item.Amount}, Behållare: {item.Container} {item.Volume.Milliliter}ml, Pris per kolli: {item.UnitPrice}, Totalpris: {item.TotalPrice}"));
            }
            doc.Close();

            //Chunk glue = new Chunk(new VerticalPositionMark());
            //Paragraph p12 = new Paragraph("Kundbolaget AB", fs16)
            //    {
            //        new Chunk(glue),
            //        $"Beställare: {c.Name}",Chunk.NEWLINE,
            //        new Chunk(glue),
            //        $"Ordernummer: {c.OrderId}"
            //    };
            //doc.Add(p12);

        }
    }
}