﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Kundbolaget.EntityFramework.Context;
using Font = iTextSharp.text.Font;

namespace Kundbolaget.PdfGenerator
{
    public class PdfGenerator
    {
        private static StoreContext db;

        public void ExportToPdf(int id)
        {
            db = new StoreContext();

            var product = from od in db.OrderDetails
                          where od.OrderId == id
                          join p in db.ProductsInfoes on od.ProductInfoId equals p.Id
                          select new
                          {
                              od.OrderId,
                              p.Name,
                              od.Amount,
                              od.UnitPrice,
                              od.TotalPrice,
                              Container = p.Container.Name,
                              p.Volume,
                              od.ProductInfoId
                              
                          };

            var company = from od in db.OrderDetails
                          where od.OrderId == id
                          select new
                          {
                              od.OrderId,
                              od.Order.Company.Name,
                              od.Order.Company.DeliveryAddress.Street,
                              od.Order.Company.DeliveryAddress.Number,
                              od.Order.Company.DeliveryAddress.ZipCode
                          };

            Document doc = new Document();
            
            string path = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\_Följesedel_o{id}_c{company.FirstOrDefault().Name}{DateTime.Now.ToShortDateString()}.pdf";

            FileStream fS = File.Create(path);
            //PdfWriter writer = new PdfWriter();
            var pb = new PdfContentByte(PdfWriter.GetInstance(doc, fS));
            //PdfWriter.GetInstance(doc, fS);
            
            
            doc.Open();

            var fs1 = FontFactory.GetFont(FontFactory.HELVETICA, 20);
            var fs2 = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            doc.Add(new Paragraph("Följesedel", fs1) /*{ Alignment = Element.ALIGN_CENTER }*/);
            doc.Add(new Paragraph("\n"));

            foreach (var c in company)
            {
                doc.Add(new Paragraph($"Ordernummer: {c.OrderId} ") { Alignment = Element.ALIGN_LEFT });
                doc.Add(new Paragraph($"Beställare: {c.Name} ") { Alignment = Element.ALIGN_LEFT });
                doc.Add(new Paragraph($"Leveransadress: {c.Street} {c.Number} {c.ZipCode} ") { Alignment = Element.ALIGN_LEFT });
                break;
            }

            
            doc.Add(new Paragraph("\n\n"));

            PdfPTable table = new PdfPTable(4);
            
            table.HorizontalAlignment = 0;
            PdfPCell cell = new PdfPCell(new Phrase("Detaljer"));
            cell.Colspan = 4;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            
            table.AddCell(cell);

            table.AddCell("Artikelnr");
            table.AddCell("Produkt");
            table.AddCell("Antal");
            table.AddCell("Behållare");

            foreach (var item in product)
            {
                table.AddCell($"{item.ProductInfoId}");
                table.AddCell($"{item.Name}");
                table.AddCell($"{item.Amount}");
                table.AddCell($"{item.Container} {item.Volume.Milliliter}ml");
            }

            doc.Add(table);
            doc.Add(new Paragraph("\n\n"));

            

            var table2 = new PdfPTable(2);
            table2.HorizontalAlignment = 0;
            table2.TotalWidth = doc.PageSize.Width - doc.LeftMargin;
            
            


            List<PdfPCell> cells = new List<PdfPCell>();
            cells.Add(new PdfPCell(new Phrase("Kundbolaget AB", fs2)) { Border = 0 });
            cells.Add(new PdfPCell(new Phrase("Innehar F-skattesedel", fs2)) { Border = 0 });
            cells.Add(new PdfPCell(new Phrase("Industrivägen 32", fs2)) { Border = 0 });
            cells.Add(new PdfPCell(new Phrase("Epost: info@kundbolaget.se", fs2)) { Border = 0 });
            cells.Add(new PdfPCell(new Phrase("142 45 Liljeholmen", fs2)) { Border = 0 });
            cells.Add(new PdfPCell(new Phrase("Hemsida: www.kundbolaget.se", fs2)) { Border = 0 });
            cells.Add(new PdfPCell(new Phrase("Sverige", fs2)) { Border = 0 });
            cells.Add(new PdfPCell(new Phrase("Org.nr: 556223-0102", fs2)) { Border = 0 });

            foreach (var pdfCell in cells)
            {
                table2.AddCell(pdfCell);
            }
            //table2.WriteSelectedRows(0, -1, doc.LeftMargin, doc.PageSize.Height - 400, pb.PdfWriter.DirectContent);


            doc.Add(table2);

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