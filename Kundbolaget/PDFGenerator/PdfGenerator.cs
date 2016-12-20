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
            db = new StoreContext();
            //var query = from c in db.OrderDetails where c.OrderId == id
            //            select c;
            var query = from od in db.OrderDetails where od.OrderId == id
                join p in db.ProductsInfoes on od.ProductInfoId equals p.Id
                select new {od.OrderId, p.Name, od.Amount, od.UnitPrice, od.TotalPrice, Container = p.Container.Name, p.Volume, Comp= od.Order.Company.Name};

            var compinfo = from od in db.OrderDetails
                        where od.OrderId == id
                        //join p in db.ProductsInfoes on od.ProductInfoId equals p.Id
                        select new { od.OrderId, od.Order.Company.Name};


            Document doc = new Document();
            PdfWriter.GetInstance(doc, new FileStream("C:/Users/Shkomi/Documents/test03.pdf", FileMode.Create));
            
            doc.Open();
            var f = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 18);
            Paragraph paragraph = new Paragraph("Följesedel", f) {Alignment = Element.ALIGN_CENTER};

            //doc.Add(new Paragraph("Följesedel", f));
            doc.Add(paragraph);



            foreach (var c in compinfo)
            {
                doc.Add(new Paragraph($"Beställare: {c.Name}, Ordernummer: {c.OrderId}") {Alignment = Element.ALIGN_JUSTIFIED});
                break;
            }
            foreach (var item in query)
            {
                doc.Add(new Paragraph($"Produkt: {item.Name} Antal: {item.Amount} Behållare: {item.Container} {item.Volume.Milliliter}ml Pris per kolli: {item.UnitPrice} Totalpris: {item.TotalPrice}"));
                //doc.Add(new Paragraph($"{item.Comp}"));

            }


            doc.Close();



        }
    }
}