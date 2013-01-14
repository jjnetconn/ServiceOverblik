using System;
using System.Globalization;
using System.Xml;
using System.Xml.XPath;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.Rendering;
using System.Diagnostics;
using System.Data;

namespace ServiceOverblik
{
    class ServiceContract
    {
        public ServiceContract()
        {

        }


        /// <summary>
        /// The MigraDoc document that represents the invoice.
        /// </summary>
        Document document;

        /// <summary>
        /// An XML invoice based on a sample created with Microsoft InfoPath.
        /// </summary>
        DataTable dt;
        string path;
        /// <summary>
        /// The root navigator for the XML document.
        /// </summary>


        /// <summary>
        /// The text frame of the MigraDoc document that contains the address.
        /// </summary>
        TextFrame addressFrame;

        /// <summary>
        /// The table of the MigraDoc document that contains the invoice items.
        /// </summary>
        Table table;

        /// <summary>
        /// Initializes a new instance of the class BillFrom and opens the specified XML document.
        /// </summary>
        public ServiceContract(DataTable dtIn, string pathIn)
        {
            dt = dtIn;
            path = pathIn;
        }
        public ServiceContract(string pathIn)
        {
            path = pathIn;
        }

        /// <summary>
        /// Creates the invoice document.
        /// </summary>
        public Document CreateDocument(int serviceNo, string serviceName, string serviceStart, string serviceEnd, double servicePrice, string customer, string street, string postcode, string city)
        {
            // Create a new MigraDoc document
            this.document = new Document();
            this.document.Info.Title = "ServiceKontrakt";
            this.document.Info.Subject = "";
            this.document.Info.Author = "ServiceOverblik - Using Migradoc";

            DefineStyles();

            CreatePage(serviceNo, serviceName, serviceStart, serviceEnd, servicePrice);

            FillContent(customer, street, postcode, city, Properties.Settings.Default.supportPhone, Properties.Settings.Default.supportEmail);

            return this.document;
        }

        /// <summary>
        /// Defines the styles used to format the MigraDoc document.
        /// </summary>
        void DefineStyles()
        {
            // Get the predefined style Normal.
            Style style = this.document.Styles["Normal"];
            // Because all styles are derived from Normal, the next line changes the 
            // font of the whole document. Or, more exactly, it changes the font of
            // all styles and paragraphs that do not redefine the font.
            style.Font.Name = "Verdana";

            style = this.document.Styles[StyleNames.Header];
            style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right);

            style = this.document.Styles[StyleNames.Footer];
            style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Center);

            // Create a new style called Table based on style Normal
            style = this.document.Styles.AddStyle("Table", "Normal");
            style.Font.Name = "Verdana";
            style.Font.Name = "Times New Roman";
            style.Font.Size = 9;

            // Create a new style called Reference based on style Normal
            style = this.document.Styles.AddStyle("Reference", "Normal");
            style.ParagraphFormat.SpaceBefore = "5mm";
            style.ParagraphFormat.SpaceAfter = "5mm";
            style.ParagraphFormat.TabStops.AddTabStop("16cm", TabAlignment.Right);
        }

        /// <summary>
        /// Creates the static parts of the invoice.
        /// </summary>

        void CreatePage(int serviceNo, string serviceName, string serviceStart, string serviceEnd, double servicePrice)
        {
            // Each MigraDoc document needs at least one section.
            Section section = this.document.AddSection();

            // Put a logo in the header
            Image image = section.AddImage(path);


            image.Top = ShapePosition.Top;
            image.Left = ShapePosition.Left;
            image.WrapFormat.Style = WrapStyle.Through;

            // Create footer
            Paragraph paragraph = section.Footers.Primary.AddParagraph();
            paragraph.AddText("Solcelle Specialisten A/S - Nordre Strandvej 119E - DK-3150 Hellebæk");
            paragraph.Format.Font.Size = 9;
            paragraph.Format.Alignment = ParagraphAlignment.Center;

            // Create the text frame for the address
            this.addressFrame = section.AddTextFrame();
            this.addressFrame.Height = "3.0cm";
            this.addressFrame.Width = "7.0cm";
            this.addressFrame.Left = ShapePosition.Left;
            this.addressFrame.RelativeHorizontal = RelativeHorizontal.Margin;
            this.addressFrame.Top = "7.0cm";
            this.addressFrame.RelativeVertical = RelativeVertical.Page;

            // Put sender in address frame
            paragraph = this.addressFrame.AddParagraph("Servicekontrakt nr.:" + serviceNo.ToString());
            paragraph.Format.Font.Name = "Times New Roman";
            paragraph.Format.Font.Size = 7;
            paragraph.Format.SpaceAfter = 3;

            // Add the print date field
            paragraph = section.AddParagraph();
            paragraph.Format.SpaceBefore = "8cm";
            paragraph.Style = "Reference";
            paragraph.AddFormattedText("Servicekontrakt:", TextFormat.Bold);
            paragraph.AddTab();
            paragraph.AddText("Dato, ");
            paragraph.AddDateField("dd.MM.yyyy");
            
            paragraph = section.AddParagraph();
            paragraph.Format.SpaceBefore = "1cm";
            paragraph.Style = "Reference";
            paragraph.AddFormattedText("Aftale: " + serviceName, TextFormat.Bold);
            paragraph.AddLineBreak();
            paragraph.AddFormattedText("Aftale start: " + serviceStart, TextFormat.NotBold);
            paragraph.AddLineBreak();
            paragraph.AddFormattedText("Aftale ophør: " + serviceEnd, TextFormat.NotBold);
            paragraph.AddLineBreak();
            paragraph.AddFormattedText("Pris: " + servicePrice + " DKr, inkl. Moms", TextFormat.Underline);
            paragraph.AddLineBreak();
            /*
            // Create the item table
            this.table = section.AddTable();
            this.table.Style = "Table";
            this.table.Borders.Color = TableBorder;
            this.table.Borders.Width = 0.25;
            this.table.Borders.Left.Width = 0.5;
            this.table.Borders.Right.Width = 0.5;
            this.table.Rows.LeftIndent = 0;

            // Before you can add a row, you must define the columns
            Column column;
            foreach (DataColumn col in dt.Columns)
            {
                column = this.table.AddColumn(Unit.FromCentimeter(3));
                column.Format.Alignment = ParagraphAlignment.Center;
                column.Width = 130;
            }

            // Create the header of the table
            Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;
            row.Shading.Color = TableBlue;


            for (int i = 0; i < dt.Columns.Count; i++)
            {
                row.Cells[i].AddParagraph(dt.Columns[i].ColumnName);
                row.Cells[i].Format.Font.Bold = false;
                row.Cells[i].Format.Alignment = ParagraphAlignment.Left;
                row.Cells[i].VerticalAlignment = VerticalAlignment.Bottom;
            }

            this.table.SetEdge(0, 0, dt.Columns.Count, 1, Edge.Box, BorderStyle.Single, 0.75, Color.Empty);
            */

        }
        /// <summary>
        /// Creates the dynamic parts of the invoice.
        /// </summary>
        void FillContent(string customer, string street, string postcode, string city, string servicePhone, string serviceEmail)
        {
            // Fill address in address text frame

            Paragraph paragraph = this.addressFrame.AddParagraph();
            paragraph.AddText(customer);
            paragraph.AddLineBreak();
            paragraph.AddText(street);
            paragraph.AddLineBreak();
            paragraph.AddText("DK-" + postcode + " " + city);

            //Row row1;
            /*for (int i = 0; i < dt.Rows.Count; i++)
            {
                row1 = this.table.AddRow();


                row1.TopPadding = 1.5;


                for (int j = 0; j < dt.Columns.Count; j++)
                {

                    row1.Cells[j].Shading.Color = TableGray;
                    row1.Cells[j].VerticalAlignment = VerticalAlignment.Center;

                    row1.Cells[j].Format.Alignment = ParagraphAlignment.Left;
                    row1.Cells[j].Format.FirstLineIndent = 1;



                    row1.Cells[j].AddParagraph(dt.Rows[i][j].ToString());


                    this.table.SetEdge(0, this.table.Rows.Count - 2, dt.Columns.Count, 1, Edge.Box, BorderStyle.Single, 0.75);
                }
             
            }*/


            // Add the notes paragraph
            paragraph = this.document.LastSection.AddParagraph();
            paragraph.Format.SpaceBefore = "2cm";
            paragraph.Format.Borders.Width = 0.75;
            paragraph.Format.Borders.Distance = 3;
            paragraph.Format.Borders.Color = TableBorder;
            paragraph.Format.Shading.Color = TableGray;

            paragraph.AddText("Ved fejl kontakt vores service afdeling på flg.:");
            paragraph.AddText("Telefon: " + servicePhone);
            paragraph.AddText("Email: " + serviceEmail);


        }

        // Some pre-defined colors
#if true
        // RGB colors
        readonly static Color TableBorder = new Color(81, 125, 192);
        readonly static Color TableBlue = new Color(235, 240, 249);
        readonly static Color TableGray = new Color(242, 242, 242);
#else
        // CMYK colors
        readonly static Color tableBorder = Color.FromCmyk(100, 50, 0, 30);
        readonly static Color tableBlue = Color.FromCmyk(0, 80, 50, 30);
        readonly static Color tableGray = Color.FromCmyk(30, 0, 0, 0, 100);
#endif
    }
}
