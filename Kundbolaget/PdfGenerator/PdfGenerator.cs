using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Kundbolaget.EntityFramework.Context;
using Kundbolaget.EntityFramework.Repositories;
using Font = iTextSharp.text.Font;

namespace Kundbolaget.PdfGenerator
{
    public class PdfGenerator
    {
        private static StoreContext db;

        public void ExportInvoiceToPdf(int id)
        {
            db = new StoreContext();
            var orders = new DbOrderRepository();
            var company = from od in db.OrderDetails
                          where od.OrderId == id
                          select new
                          {
                              od.OrderId,
                              od.Order.Company.Name,
                              od.Order.Company.DeliveryAddress.Street,
                              od.Order.Company.DeliveryAddress.Number,
                              od.Order.Company.DeliveryAddress.ZipCode,
                              od.Order.WishedDeliveryDate,
                              od.Order.CreatedDate,
                              od.Order.CompanyId,
                              od.Order.Company.ContactPerson.FirstName,
                              od.Order.Company.ContactPerson.LastName,
                              
                          };

            var product = from od in db.OrderDetails
                          where od.OrderId == id
                          join p in db.ProductsInfoes on od.ProductInfoId equals p.Id
                          select new
                          {
                              od.OrderId,
                              p.Name,
                              od.Amount,
                              Container = p.Container.Name,
                              p.Volume,
                              od.ProductInfoId,
                              od.UnitPrice,
                              od.TotalPrice,
                          };

            var order = orders.GetEntity(id);
            
            var doc = new Document();
            string path = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\Faktura_o{id}_c{company.FirstOrDefault().Name}{DateTime.Now.ToShortDateString()}.pdf";
            FileStream fS = File.Create(path);
            var pb = new PdfContentByte(PdfWriter.GetInstance(doc, fS));
            //PdfWriter.GetInstance(doc, fS);
            doc.Open();

            var fs1 = FontFactory.GetFont(FontFactory.HELVETICA, 20);
            var hBold = FontFactory.GetFont(FontFactory.HELVETICA_BOLD);
            var fs2 = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            var white = FontFactory.GetFont(FontFactory.HELVETICA, 12,1,color: BaseColor.WHITE);

            doc.Add(new Paragraph("Faktura", fs1));
            doc.Add(new Paragraph("\n"));

            var table1 = new PdfPTable(4) { HorizontalAlignment = 1, WidthPercentage = 100f};
            table1.SetWidths(new float[] { 1f, 1f, 2f, 1f } );
            var nestedTableLeft = new PdfPTable(2);

            var cellsLeft = new List<PdfPCell>
            {
                new PdfPCell(new Phrase("Fakturadatum:")) {Border = 0},
                new PdfPCell(new Phrase(order.Invoice.CreatedDate.ToShortDateString())) {Border = 0},                
                new PdfPCell(new Phrase("Fakturanr:")) {Border = 0},
                new PdfPCell(new Phrase(order.Invoice.Id.ToString())) {Border = 0},
                new PdfPCell(new Phrase("Kundnr:")) {Border = 0},
                new PdfPCell(new Phrase(order.Invoice.CompanyId.ToString())) {Border = 0},             
                new PdfPCell(new Phrase("Förfallodag:")) {Border = 0},
                new PdfPCell(new Phrase(order.Invoice.DueDate.ToShortDateString())) {Border = 0},                       
                new PdfPCell(new Phrase("Betalningsvillkor:")) {Border = 0},
                new PdfPCell(new Phrase("90 dagar")) {Border = 0},
                new PdfPCell(new Phrase("Plusgironr:")) {Border = 0},
                new PdfPCell(new Phrase("1234567-8")) {Border = 0},
                new PdfPCell(new Phrase("Bankgironr:")) {Border = 0},
                new PdfPCell(new Phrase("123-4567")){Border = 0},
                new PdfPCell(new Phrase("Er referens:")) {Border = 0},
                new PdfPCell(new Phrase($"{company.FirstOrDefault().FirstName} {company.FirstOrDefault().LastName}")) {Border = 0}
            };

            foreach (var cell in cellsLeft)
            {
                nestedTableLeft.AddCell(cell);
            }
            PdfPCell nesthousingLeft = new PdfPCell(nestedTableLeft);
            nesthousingLeft.Colspan = 2;
            nesthousingLeft.Border = 0;
            table1.AddCell(nesthousingLeft);

            var nestedTableRight = new PdfPTable(2);
            var cellsRight = new List<PdfPCell>
            {
                new PdfPCell(new Phrase("")) {Border = 0},
                new PdfPCell(new Phrase("Fakturaadress", hBold)) {Border = 0},
                new PdfPCell(new Phrase("")) {Border = 0},
                new PdfPCell(new Phrase($"{company.FirstOrDefault().FirstName} {company.FirstOrDefault().LastName}")) {Border = 0},
                new PdfPCell(new Phrase("")) {Border = 0},
                new PdfPCell(new Phrase(company.FirstOrDefault().Name)) {Border = 0},
                new PdfPCell(new Phrase("")) {Border = 0},
                new PdfPCell(new Phrase($"{company.FirstOrDefault().Street} {company.FirstOrDefault().Number}")) {Border = 0},
                new PdfPCell(new Phrase("")) {Border = 0},
                new PdfPCell(new Phrase(company.FirstOrDefault().ZipCode)) {Border = 0},
            };

            foreach (var cell in cellsRight)
            {
                nestedTableRight.AddCell(cell);
            }
            var nesthousingRight = new PdfPCell(nestedTableRight);
            nesthousingRight.Colspan = 2;
            nesthousingRight.Border = 0;
            table1.AddCell(nesthousingRight);

            doc.Add(table1);

            doc.Add(new Paragraph("\n\n"));
            var table2 = new PdfPTable(5) {HorizontalAlignment = 1, WidthPercentage = 100f};
            table2.SetWidths(new float[] { 0.65f, 2.9f, 0.9f, 0.9f, 0.9f });
            var headerCells = new List<PdfPCell>
            {
                new PdfPCell(new Phrase("Artikelnr", hBold)) {BackgroundColor = BaseColor.LIGHT_GRAY },
                new PdfPCell(new Phrase("Beskrivning", hBold)) { BackgroundColor = BaseColor.LIGHT_GRAY },
                new PdfPCell(new Phrase("Antal Kolli", hBold)) { BackgroundColor = BaseColor.LIGHT_GRAY },
                new PdfPCell(new Phrase("à-pris", hBold)) { BackgroundColor = BaseColor.LIGHT_GRAY },          
                new PdfPCell(new Phrase("Belopp", hBold)) { BackgroundColor = BaseColor.LIGHT_GRAY },
            };
            foreach (var headerCell in headerCells)
            {
                table2.AddCell(headerCell);
            }
            foreach (var item in product)
            {
                table2.AddCell($"{item.ProductInfoId}");
                table2.AddCell($"{item.Name} {item.Container} {item.Volume.Milliliter}ml");
                table2.AddCell($"{item.Amount}");
                table2.AddCell($"{item.UnitPrice}"); 
                table2.AddCell($"{item.UnitPrice * item.Amount}");
            }
            var priceInfoCells = new List<PdfPCell>
            {
                new PdfPCell(new Phrase("FUCKOFF!", white)) {Colspan = 5, Border = 0},
                new PdfPCell(new Phrase("FUCKOFF AGAIN!", white)) {Colspan = 5, Border = 0},
  
                new PdfPCell(new Phrase("")) {Colspan = 2, Border = 0},
                new PdfPCell(new Phrase("Orginalpris:", hBold)) {Colspan = 2, Border = 0},
                new PdfPCell(new Phrase(order.Invoice.OriginalPrice.ToString(),hBold)) {Border = 0},
                
                new PdfPCell(new Phrase("")) {Colspan = 2, Border = 0},
                new PdfPCell(new Phrase("Pris med mängdrabatt:", hBold)) {Colspan = 2, Border = 0},
                new PdfPCell(new Phrase(Math.Round(order.Invoice.PriceWithPalletDiscount, 2).ToString(), hBold)) {Border = 0},
                 
                new PdfPCell(new Phrase("")) {Colspan = 2, Border = 0},
                new PdfPCell(new Phrase("Pris med företagsrabatt:", hBold)) {Colspan = 2, Border = 0},
                new PdfPCell(new Phrase(Math.Round(order.Invoice.PriceWithCompanyDiscount, 2).ToString(), hBold)) {Border = 0},
                 
                new PdfPCell(new Phrase("")) {Colspan = 2, Border = 0},
                new PdfPCell(new Phrase("Total moms:", hBold)) {Colspan = 2, Border = 0},
                new PdfPCell(new Phrase((Math.Round( (order.Invoice.PriceWithCompanyDiscount * 0.25),2)).ToString(), hBold)) {Border = 0},
                 
                new PdfPCell(new Phrase("")) {Colspan = 2, Border = 0},
                new PdfPCell(new Phrase("Öresutjämning:", hBold)) {Colspan = 2, Border = 0},
                new PdfPCell(new Phrase(Math.Round((Math.Round(order.Invoice.PriceWithCompanyDiscount + Math.Round(order.Invoice.PriceWithCompanyDiscount * 0.25,0,MidpointRounding.AwayFromZero))
                - (Math.Round(order.Invoice.PriceWithCompanyDiscount,2)+ Math.Round(order.Invoice.PriceWithCompanyDiscount * 0.25,2))),2).ToString(), hBold)){Border = 0},
                 
                new PdfPCell(new Phrase("")) {Colspan = 2, Border = 0},
                new PdfPCell(new Phrase("Summa att betala SEK:", hBold)) { BorderWidthBottom = 0,BorderWidthLeft = 0,BorderWidthRight = 0, Colspan = 2 },
                new PdfPCell(new Phrase(Math.Round(order.Invoice.PriceWithCompanyDiscount + (order.Invoice.PriceWithCompanyDiscount * 0.25),0,MidpointRounding.AwayFromZero).ToString(), hBold)) { BorderWidthBottom = 0,BorderWidthLeft = 0,BorderWidthRight = 0},
            };

            foreach (var priceCell in priceInfoCells)
            {
                table2.AddCell(priceCell);
            }
            doc.Add(table2);

            Footer(pb,doc,fs2);

            doc.Close();
        }


        public void ExportDeliveryNoteToPdf(int id)
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
                              od.Order.Company.DeliveryAddress.ZipCode,
                              od.Order.WishedDeliveryDate
                          };

            var doc = new Document();
            
            string path = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\_Följesedel_o{id}_c{company.FirstOrDefault().Name}{DateTime.Now.ToShortDateString()}.pdf";

            FileStream fS = File.Create(path);
            var pb = new PdfContentByte(PdfWriter.GetInstance(doc, fS));
            doc.Open();

            var fs1 = FontFactory.GetFont(FontFactory.HELVETICA, 20);
            var fs2 = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            doc.Add(new Paragraph("Följesedel", fs1));
            doc.Add(new Paragraph("\n"));

            foreach (var c in company)
            {
                doc.Add(new Paragraph($"Ordernummer: {c.OrderId} ") { Alignment = Element.ALIGN_LEFT });
                doc.Add(new Paragraph($"Beställare: {c.Name} ") { Alignment = Element.ALIGN_LEFT });
                doc.Add(new Paragraph($"Leveransadress: {c.Street} {c.Number} {c.ZipCode} ") { Alignment = Element.ALIGN_LEFT });
                doc.Add(new Paragraph($"Leveransdatum: {c.WishedDeliveryDate.ToShortDateString()} ") { Alignment = Element.ALIGN_LEFT });
                break;
            }

            doc.Add(new Paragraph("\n\n"));

            var table = new PdfPTable(4) {HorizontalAlignment = 1, WidthPercentage = 100f};
            var cell = new PdfPCell(new Phrase("Detaljer"))
            {
                Colspan = 4,
                HorizontalAlignment = 1 //0=Left, 1=Centre, 2=Right
            }; 
            table.AddCell(cell);
            table.AddCell("Artikelnr");
            table.AddCell("Produkt");
            table.AddCell("Behållare");
            table.AddCell("Antal");
            
            foreach (var item in product)
            {
                table.AddCell($"{item.ProductInfoId}");
                table.AddCell($"{item.Name}");
                table.AddCell($"{item.Container} {item.Volume.Milliliter}ml");
                table.AddCell($"{item.Amount}");
            }
            doc.Add(table);

            Footer(pb, doc, fs2);

            doc.Close();
        }

        public void Footer(PdfContentByte pb, Document doc, Font fs2)
        {
            var table4 = new PdfPTable(2)
            {
                HorizontalAlignment = 1,
                WidthPercentage = 100f,
                TotalWidth = doc.PageSize.Width - doc.LeftMargin
            };
            var bottomCells = new List<PdfPCell>
            {
                new PdfPCell(new Phrase("Kundbolaget AB", fs2)) {Border = 0},
                new PdfPCell(new Phrase("Innehar F-skattesedel", fs2)) {Border = 0},
                new PdfPCell(new Phrase("Industrivägen 32", fs2)) {Border = 0},
                new PdfPCell(new Phrase("Epost: info@kundbolaget.se", fs2)) {Border = 0},
                new PdfPCell(new Phrase("142 45 Liljeholmen", fs2)) {Border = 0},
                new PdfPCell(new Phrase("Hemsida: www.kundbolaget.se", fs2)) {Border = 0},
                new PdfPCell(new Phrase("Sverige", fs2)) {Border = 0},
                new PdfPCell(new Phrase("Org.nr: 556223-0102", fs2)) {Border = 0}
            };
            foreach (var pdfCell in bottomCells)
            {
                table4.AddCell(pdfCell);
            }
            //Adds to table instead of: doc.Add(table2);
            table4.WriteSelectedRows(0, -1, doc.LeftMargin, doc.BottomMargin + 60, pb.PdfWriter.DirectContent);
        }
    }
}