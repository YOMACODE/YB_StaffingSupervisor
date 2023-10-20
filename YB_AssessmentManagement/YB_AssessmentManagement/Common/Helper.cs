using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
namespace YB_AssessmentManagement.Common
{
    public partial class ITextEventsMaxHeaderFooter : PdfPageEventHelper
    {
        private readonly IWebHostEnvironment _WebHostEnvironment;
        public ITextEventsMaxHeaderFooter(IWebHostEnvironment webHostEnvironment)
        {
            _WebHostEnvironment = webHostEnvironment;
        }

        // This is the contentbyte object of the writer

        private PdfContentByte cb;
        // we will put the final number of pages in a template
        private PdfTemplate headerTemplate;

        private PdfTemplate footerTemplate;
        // this is the BaseFont we are going to use for the header / footer

        private BaseFont bf = null;
        // This keeps track of the creation time

        private DateTime PrintTime = DateTime.Now;

        #region "Fields"      
        private string _header;
        #endregion

        #region "Properties"
        public string Header
        {
            get { return _header; }
            set { _header = value; }
        }
        #endregion

        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            try
            {
                PrintTime = DateTime.Now;
                bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb = writer.DirectContent;
                headerTemplate = cb.CreateTemplate(100, 100);
                footerTemplate = cb.CreateTemplate(50, 50);
                //handle exception here
            }
            catch (DocumentException)
            {
                throw;
                //handle exception here
            }
            catch (IOException)
            {
                throw;
            }
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);
            string path = _WebHostEnvironment.WebRootPath + "\\Image";
            string imageURL = Path.Combine(path, "Yoma2021logo.jpg");
            iTextSharp.text.Image jpgtableContent = iTextSharp.text.Image.GetInstance(imageURL);
            jpgtableContent.Alignment = Element.ALIGN_LEFT;
            jpgtableContent.ScaleToFit(100f, 30f);


            // Dim p1Header As New Phrase("Sample Header Here", baseFontNormal)

            // Create PdfTable object
            PdfPTable pdfTab = new PdfPTable(3);
            float[] widths = { 2f, 88f, 10f };
            pdfTab.SetWidthPercentage(widths, PageSize.A4);
            pdfTab.SpacingAfter = 10;
            PdfPCell logo = new PdfPCell() { Border = 0, PaddingTop = 5 };
            PdfPCell logo1 = new PdfPCell(jpgtableContent) { Border = 0, PaddingTop = 5 };
            PdfPCell logo2 = new PdfPCell() { Border = 0, PaddingTop = 5 };

            PdfPTable pdfFooter = new PdfPTable(1);
            float[] widthFooter = { 100f };
            pdfFooter.SetWidthPercentage(widthFooter, PageSize.A4);

            PdfPTable pdfTabFooter = new PdfPTable(1);
            BaseColor baseColour = new BaseColor(System.Drawing.Color.FromArgb(33, 110, 183));

            var linkFont = FontFactory.GetFont(FontFactory.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.WHITE);
            var boldFont = FontFactory.GetFont(FontFactory.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.WHITE);
            pdfTabFooter.AddCell(new PdfPCell(new Phrase("YOMA Business Solutions Private Limited ", boldFont)) { BackgroundColor = baseColour, Border = 0, PaddingTop = 5, HorizontalAlignment = Element.ALIGN_CENTER });
            pdfTabFooter.AddCell(new PdfPCell(new Phrase("Regd & Corporate Office: 3RD FLOOR, PLOT NO. 48, SECTOR 44, Gurugram,Gurugram, Haryana, 122002,", linkFont)) { BackgroundColor = baseColour, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
            pdfTabFooter.AddCell(new PdfPCell(new Phrase("CIN : U74999HR2016PTC064921", linkFont)) { BackgroundColor = baseColour, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
            pdfTabFooter.AddCell(new PdfPCell(new Phrase("For Business Queries:1800 102 1345, Web: - http://www.yomamultinational.com ", linkFont)) { BackgroundColor = baseColour, PaddingBottom = 65, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
            pdfFooter.AddCell(new PdfPCell(new Phrase("", boldFont)) { BackgroundColor = baseColour, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });

            pdfFooter.AddCell(new PdfPCell(pdfTabFooter) { BackgroundColor = baseColour, Border = 0 });

            //add all three cells into PdfTable

            pdfTab.AddCell(logo);
            pdfTab.AddCell(logo1);
            pdfTab.AddCell(logo2);

            iTextSharp.text.Rectangle page = document.PageSize;
            pdfFooter.TotalWidth = page.Width;
            pdfTab.TotalWidth = page.Width;

            pdfTab.WriteSelectedRows(0, -1, 0, document.PageSize.Height, writer.DirectContent);
            pdfFooter.WriteSelectedRows(0, -1, 0, 60, writer.DirectContent);//73

            base.OnEndPage(writer, document);
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);
            if (document.PageNumber > 1)
            {
                footerTemplate.BeginText();
                footerTemplate.SetFontAndSize(bf, 10);
                footerTemplate.SetTextMatrix(0, 0);
                footerTemplate.ShowText((writer.PageNumber - 2).ToString());
                footerTemplate.EndText();
            }
        }
    }

    #region Header/Footer For POST GST Invoice

    public partial class ITextEventsMaxHeaderFooterForPostGSTInvoice : PdfPageEventHelper
    {
        private readonly IWebHostEnvironment _WebHostEnvironment;
        public ITextEventsMaxHeaderFooterForPostGSTInvoice(IWebHostEnvironment webHostEnvironment)
        {
            _WebHostEnvironment = webHostEnvironment;
        }
        string BranchAddress = string.Empty;
        string GSTNumber = string.Empty;
        string CreditFooterInvoice = "";

        string showFooter = "";
        string showHeader = "";

        public ITextEventsMaxHeaderFooterForPostGSTInvoice(IWebHostEnvironment webHostEnvironment, string YomaBranchAddress, string YomaGSTNumber)
        {
            _WebHostEnvironment = webHostEnvironment;
            BranchAddress = YomaBranchAddress;
            GSTNumber = YomaGSTNumber;
        }
        public ITextEventsMaxHeaderFooterForPostGSTInvoice(IWebHostEnvironment webHostEnvironment, string YomaBranchAddress, string YomaGSTNumber, string CreditStatus)
        {
            _WebHostEnvironment = webHostEnvironment;
            BranchAddress = YomaBranchAddress;
            GSTNumber = YomaGSTNumber;
            CreditFooterInvoice = CreditStatus;
        }
        public ITextEventsMaxHeaderFooterForPostGSTInvoice(IWebHostEnvironment webHostEnvironment, string YomaBranchAddress, string YomaGSTNumber, string WithFooter, string NoHeader)
        {
            _WebHostEnvironment = webHostEnvironment;
            BranchAddress = YomaBranchAddress;
            GSTNumber = YomaGSTNumber;
            showFooter = WithFooter;
            showHeader = NoHeader;
        }
        // This is the contentbyte object of the writer

        private PdfContentByte cb;
        // we will put the final number of pages in a template
        private PdfTemplate headerTemplate;

        private PdfTemplate footerTemplate;
        // this is the BaseFont we are going to use for the header / footer

        private BaseFont bf = null;
        // This keeps track of the creation time

        private DateTime PrintTime = DateTime.Now;

        #region "Fields"
        private string _header;
        #endregion

        #region "Properties"
        public string Header
        {
            get { return _header; }
            set { _header = value; }
        }
        #endregion

        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            try
            {
                PrintTime = DateTime.Now;
                bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb = writer.DirectContent;
                headerTemplate = cb.CreateTemplate(100, 100);
                footerTemplate = cb.CreateTemplate(50, 50);
                //handle exception here

            }
            catch (DocumentException de)
            {
                //handle exception here
            }
            catch (System.IO.IOException ioe)
            {
            }
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);
            string path = _WebHostEnvironment.WebRootPath + "\\Image";
            string imageURL = Path.Combine(path, "Yoma2021logo.jpg");
            Image jpgtableContent = Image.GetInstance(imageURL);
            jpgtableContent.Alignment = Element.ALIGN_LEFT;
            jpgtableContent.ScaleToFit(150f, 65f);

            //Dim p1Header As New Phrase("Sample Header Here", baseFontNormal)

            //Create PdfTable object
            PdfPTable pdfTab = new PdfPTable(3);
            float[] widths = { 2f, 88f, 10f };
            pdfTab.SetWidthPercentage(widths, PageSize.A4);
            PdfPCell logo = new PdfPCell() { Border = 0 };
            PdfPCell logo1 = new PdfPCell(jpgtableContent) { Border = 0 };
            PdfPCell logo2 = new PdfPCell() { Border = 0 };

            PdfPTable pdfFooter = new PdfPTable(3);
            float[] widthFooter = { 5f, 90f, 5f };
            pdfFooter.SetWidthPercentage(widthFooter, PageSize.A4);

            PdfPTable pdfTabFooter = new PdfPTable(1);
            //float[] widthFooter = { 10f, 80f, 10f };
            //pdfTabFooter.SetWidthPercentage(widthFooter, PageSize.A4);

            var linkFont = FontFactory.GetFont(FontFactory.HELVETICA, 7, Font.NORMAL, BaseColor.BLACK);
            var boldFont = FontFactory.GetFont(FontFactory.HELVETICA, 7, Font.BOLD, BaseColor.BLACK);
            if (CreditFooterInvoice == "")
            {
                if (showHeader == "CreditNote")
                {
                    pdfTabFooter.AddCell(new PdfPCell(new Phrase("This is a computer generated credit note. ", boldFont)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, PaddingTop = 20 });
                }
                else if (showHeader == "DebitNote")
                {
                    pdfTabFooter.AddCell(new PdfPCell(new Phrase("This is a computer generated debit note. ", boldFont)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, PaddingTop = 20 });
                }
                else
                {
                    pdfTabFooter.AddCell(new PdfPCell(new Phrase("This is a computer generated invoice. ", boldFont)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, PaddingTop = 20 });
                }
            }
            else if (CreditFooterInvoice == "DebitNote")
            {

                pdfTabFooter.AddCell(new PdfPCell(new Phrase("This is a computer generated debit note. ", boldFont)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, PaddingTop = 20 });
            }
            else
            {
                if (CreditFooterInvoice != "OtherAmount")
                {
                    pdfTabFooter.AddCell(new PdfPCell(new Phrase("This is a computer generated credit note. ", boldFont)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, PaddingTop = 20 });
                }
                else if (showHeader == "DebitNote")
                {
                    pdfTabFooter.AddCell(new PdfPCell(new Phrase("This is a computer generated debit note. ", boldFont)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, PaddingTop = 20 });
                }
                else
                {
                    pdfTabFooter.AddCell(new PdfPCell(new Phrase("This is a computer generated invoice. ", boldFont)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, PaddingTop = 20 });
                }
            }

            if (showFooter == "")
            {
                pdfTabFooter.AddCell(new PdfPCell(new Phrase("", boldFont)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, FixedHeight = 20 });
                pdfTabFooter.AddCell(new PdfPCell(new Phrase("YOMA Business Solutions Private Limited ", boldFont)) { BackgroundColor = BaseColor.RED, Border = 0, PaddingTop = 2, HorizontalAlignment = Element.ALIGN_CENTER });
                //pdfTabFooter.AddCell(new PdfPCell(new Phrase("Corporate Office: A - 203, IRIS Tech Park, Sohna Road, Sector - 48, Gurgaon - 122018, Haryana (India), CIN : U74999HR2016PTC064921 ", linkFont)) { BackgroundColor = BaseColor.RED, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                //pdfTabFooter.AddCell(new PdfPCell(new Phrase("Corporate Office: Unit No. 403 - 405,4th Floor,Vipul Trade Centre,Commercial Complex, Sector - 48, Gurgaon - 122018, Haryana (India), CIN : U74999HR2016PTC064921 ", linkFont)) { BackgroundColor = BaseColor.RED, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                pdfTabFooter.AddCell(new PdfPCell(new Phrase("Corporate Office: 3RD FLOOR, PLOT NO. 48, SECTOR 44, Gurugram,Gurugram, Haryana, 122002, CIN : U74999HR2016PTC064921 ", linkFont)) { BackgroundColor = BaseColor.RED, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                //pdfTabFooter.AddCell(new PdfPCell(new Phrase("Head Office: B 605, Oxford Chambers, Saki Vihar Road, Andheri East, Mumbai - 400072, Ph: +91 22 61307307 ", linkFont)) { BackgroundColor = BaseColor.RED, Border = 0, Bottom = 1, HorizontalAlignment = Element.ALIGN_CENTER });
                pdfTabFooter.AddCell(new PdfPCell(new Phrase("Regional Office: YOMA Business Solutions Private Limited Office No. 317, 3rd Floor,Bhaveshwar Arcade Annex,Opposite Shreyas Cinema, L.B.S MargGhatkopar (W) Mumbai -400086", linkFont)) { BackgroundColor = BaseColor.RED, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                //pdfTabFooter.AddCell(new PdfPCell(new Phrase("Regd. Office: 502 Tower - 4, Sagavi Appat., Plot. 85, Sector - 55, Gurgaon - 122011, Haryana (India) ", linkFont)) { BackgroundColor = BaseColor.RED, Border = 0, Bottom = 1, HorizontalAlignment = Element.ALIGN_CENTER });
                pdfTabFooter.AddCell(new PdfPCell(new Phrase("Branch Office: " + BranchAddress + "," + GSTNumber + "", linkFont)) { BackgroundColor = BaseColor.RED, Border = 0, Bottom = 1, HorizontalAlignment = Element.ALIGN_CENTER });
                pdfTabFooter.AddCell(new PdfPCell(new Phrase("For Associates (Toll Free): +91-8448188503, For Business Queries: +91 01133147000, Web: - http://www.yomamultinational.com ", linkFont)) { BackgroundColor = BaseColor.RED, Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
            }
            else
            {
                pdfTabFooter.AddCell(new PdfPCell(new Phrase("", boldFont)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER, FixedHeight = 20 });
                pdfTabFooter.AddCell(new PdfPCell(new Phrase("YOMA Business Solutions Private Limited ", boldFont)) { Border = 0, PaddingTop = 2, HorizontalAlignment = Element.ALIGN_CENTER });
                //pdfTabFooter.AddCell(new PdfPCell(new Phrase("Corporate Office: A - 203, IRIS Tech Park, Sohna Road, Sector - 48, Gurgaon - 122018, Haryana (India), CIN : U74999HR2016PTC064921 ", linkFont)) {  Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                pdfTabFooter.AddCell(new PdfPCell(new Phrase("Corporate Office: 3RD FLOOR, PLOT NO. 48, SECTOR 44, Gurugram,Gurugram, Haryana, 122002, CIN : U74999HR2016PTC064921 ", linkFont)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                //pdfTabFooter.AddCell(new PdfPCell(new Phrase("Head Office: B 605, Oxford Chambers, Saki Vihar Road, Andheri East, Mumbai - 400072, Ph: +91 22 61307307 ", linkFont)) { Border = 0, Bottom = 1, HorizontalAlignment = Element.ALIGN_CENTER });
                pdfTabFooter.AddCell(new PdfPCell(new Phrase("Regional Office: YOMA Business Solutions Private Limited Office No. 317, 3rd Floor,Bhaveshwar Arcade Annex,Opposite Shreyas Cinema, L.B.S MargGhatkopar (W) Mumbai -400086", linkFont)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                //pdfTabFooter.AddCell(new PdfPCell(new Phrase("Regd. Office: 502 Tower - 4, Sagavi Appat., Plot. 85, Sector - 55, Gurgaon - 122011, Haryana (India) ", linkFont)) { Border = 0, Bottom = 1, HorizontalAlignment = Element.ALIGN_CENTER });
                pdfTabFooter.AddCell(new PdfPCell(new Phrase("Branch Office: " + BranchAddress + "," + GSTNumber + "", linkFont)) { Border = 0, Bottom = 1, HorizontalAlignment = Element.ALIGN_CENTER });
                pdfTabFooter.AddCell(new PdfPCell(new Phrase("For Associates (Toll Free): +91-8448188503, For Business Queries: +91 01133147000, Web: - http://www.yomamultinational.com ", linkFont)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
            }
            pdfFooter.AddCell(new PdfPCell(new Phrase("", boldFont)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
            pdfFooter.AddCell(new PdfPCell(pdfTabFooter) { Border = 0 });
            pdfFooter.AddCell(new PdfPCell(new Phrase("", boldFont)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });

            //add all three cells into PdfTable
            if (showFooter == "")
            {
                pdfTab.AddCell(logo);
                pdfTab.AddCell(logo1);
                pdfTab.AddCell(logo2);
            }
            iTextSharp.text.Rectangle page = document.PageSize;
            pdfFooter.TotalWidth = page.Width;
            pdfTab.TotalWidth = page.Width;

            pdfTab.WriteSelectedRows(0, -1, 0, document.PageSize.Height, writer.DirectContent);
            pdfFooter.WriteSelectedRows(0, -1, 0, 128, writer.DirectContent);//128

            base.OnEndPage(writer, document);
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);
            if (document.PageNumber > 1)
            {
                footerTemplate.BeginText();
                footerTemplate.SetFontAndSize(bf, 10);
                footerTemplate.SetTextMatrix(0, 0);
                footerTemplate.ShowText((writer.PageNumber - 2).ToString());
                footerTemplate.EndText();
            }
        }
    }

    #endregion
}
