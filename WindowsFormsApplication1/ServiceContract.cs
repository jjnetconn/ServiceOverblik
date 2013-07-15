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
using System.Data.Entity;
using System.Linq;

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
        //DataTable dt;
        string path;
        /// <summary>
        /// The root navigator for the XML document.
        /// </summary>

        /// <summary>
        /// The text frame of the MigraDoc document that contains the address.
        /// </summary>
        TextFrame addressFrame;
        TextFrame contentFrame;
        TextFrame serviceDescFrame;
        //TextFrame endFrame;
        /// <summary>
        /// The table of the MigraDoc document that contains the invoice items.
        /// </summary>
        //Table table;

        

        /// <summary>
        /// Initializes a new instance of the class BillFrom and opens the specified XML document.
        /// </summary>
        public ServiceContract(string pathIn)
        {
            path = pathIn;
        }

        /// <summary>
        /// Creates the invoice document.
        /// </summary>
        
        public Document CreateDocument(int selUserId, int serviceNo)
        {
            // Create a new MigraDoc document
            this.document = new Document();
            this.document.Info.Title = "ServiceKontrakt";
            this.document.Info.Subject = "";
            this.document.Info.Author = "ServiceOverblik - Using Migradoc";

            DefineStyles();

            CreatePage(selUserId, serviceNo);

            FillContent(selUserId, serviceNo);

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

            // Create a new style called TextBox based on style Normal 
            style = this.document.Styles.AddStyle("TextBox", "Normal");
            style.ParagraphFormat.Alignment = ParagraphAlignment.Justify;
            style.ParagraphFormat.Borders.Width = 2.5;
            style.ParagraphFormat.Borders.Distance = "3pt";
            style.ParagraphFormat.Shading.Color = Colors.SkyBlue; 
        }

        /// <summary>
        /// Creates the static parts of the invoice.
        /// </summary>

        void CreatePage(int selUserId, int serviceNo)
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
            paragraph.Format.Font.Size = 10;
            paragraph.Format.SpaceAfter = 3;

            // Add the print date field
            paragraph = section.AddParagraph();
            paragraph.Format.SpaceBefore = "7.5cm";
            paragraph.Style = "Reference";
            paragraph.AddFormattedText("Servicekontrakt:", TextFormat.Bold);
            paragraph.AddTab();
            paragraph.AddText("Dato, ");
            paragraph.AddDateField("dd.MM.yyyy");
            
            this.contentFrame = section.AddTextFrame();
            //this.contentFrame.Height = "19.0cm";
            this.contentFrame.Width = "19.0cm";
            this.contentFrame.Left = ShapePosition.Left;
            this.contentFrame.RelativeHorizontal = RelativeHorizontal.Margin;
            this.contentFrame.Top = "10.0cm";
            this.contentFrame.RelativeVertical = RelativeVertical.Page;

            // Put sender in address frame
            paragraph = this.contentFrame.AddParagraph();
            paragraph.Format.Font.Name = "Times New Roman";
            paragraph.Format.Font.Size = 7;
            paragraph.Format.SpaceAfter = 3;

            // Add the print date field
            paragraph = section.AddParagraph();
            paragraph.Format.SpaceBefore = "1cm";

            this.serviceDescFrame = section.AddTextFrame();
            //this.serviceDescFrame.Height = "3.0cm";
            this.serviceDescFrame.Width = "19.0cm";
            this.serviceDescFrame.Left = ShapePosition.Left;
            this.serviceDescFrame.RelativeHorizontal = RelativeHorizontal.Margin;
            this.serviceDescFrame.Top = "16.0cm";
            this.serviceDescFrame.RelativeVertical = RelativeVertical.Page;
            
            // Put sender in address frame
            paragraph = this.serviceDescFrame.AddParagraph();
            paragraph.Format.Font.Name = "Times New Roman";
            paragraph.Format.Font.Size = 7;
            paragraph.Format.SpaceAfter = 3;

            // Add the print date field
            paragraph = section.AddParagraph();
            paragraph.Format.SpaceBefore = "1cm";

        }
        /// <summary>
        /// Creates the dynamic parts of the invoice.
        /// </summary>
        void FillContent(int selUserId, int serviceNo)
        {
            // Fill address in address text frame
            ServiceManager runstate = new ServiceManager();

            using (servicebaseEntities sdb = new servicebaseEntities())
            {
                try
                {
                    var query = (from c in sdb.customers
                                 where c.uId == selUserId
                                 select c).FirstOrDefault();
                    
                    var query2 = (from c in sdb.servicecontracts
                                  where c.sid == query.serviceno
                                  select c).FirstOrDefault();
                    
                    var query3 = (from c in sdb.servicetypes
                                  where c.tid == query2.servicetype
                                  select c).FirstOrDefault();

                    Paragraph paragraph = this.addressFrame.AddParagraph();
                    paragraph.AddText(query.cname);
                    paragraph.AddLineBreak();
                    paragraph.AddText(query.street);
                    paragraph.AddLineBreak();
                    paragraph.AddText("DK-" + query.postcode + " " + query.city);
                    paragraph.AddLineBreak();
                    paragraph.AddLineBreak(); 
                    paragraph.AddLineBreak();
                    
                    Paragraph paragraph2 = this.contentFrame.AddParagraph();
                    paragraph2.Format.SpaceBefore = "0.5cm";
                    paragraph2.AddFormattedText("Aftale: " + query3.sname, TextFormat.Bold);
                    paragraph2.AddLineBreak();
                    paragraph2.AddLineBreak();
                    paragraph2.AddFormattedText("Aftale start: " + query2.startdate.ToLongDateString(), TextFormat.NotBold);
                    paragraph2.AddLineBreak();
                    paragraph2.AddFormattedText("Aftale ophør: " + (query2.startdate.AddMonths(query3.period)).ToLongDateString(), TextFormat.NotBold);
                    paragraph2.AddLineBreak();
                    paragraph2.AddLineBreak();
                    paragraph2.AddFormattedText("Pris: " + runstate.calcServicePrice((int)query2.servicetype, runstate.getServiceInfo((int)query2.servicetype), (double)query.kwp).ToString("##,###.00") + " DKr, inkl. Moms", TextFormat.Underline);
                    paragraph2.AddLineBreak();
                    paragraph2.AddLineBreak();
                    paragraph2.AddLineBreak();
                    paragraph2.AddFormattedText("Inverter: " + query.inverter, TextFormat.NotBold);
                    paragraph2.AddLineBreak();
                    paragraph2.AddFormattedText("Paneler: " + query.paneltype, TextFormat.NotBold);
                    paragraph2.AddLineBreak();
                    paragraph2.AddFormattedText("Antal: " + query.panelcount.ToString() + " Paneler", TextFormat.NotBold);
                    paragraph2.AddLineBreak();
                    paragraph2.AddFormattedText("Installeret effekt: " + query.kwp.ToString() + " kWp", TextFormat.NotBold);
                    
                    Paragraph paragraph3 = this.serviceDescFrame.AddParagraph();
                    paragraph3.Format.SpaceBefore = "0.5cm";
                    paragraph3.AddImage(@query3.servicelogo);
                    paragraph3.AddLineBreak();
                    paragraph3.AddFormattedText(query3.sname, TextFormat.Bold);
                    paragraph3.AddLineBreak();
                    paragraph2.AddLineBreak();
                    paragraph3.AddFormattedText(query3.details, TextFormat.NotBold);
                    paragraph3.AddLineBreak();
                    paragraph3.AddLineBreak();
                    paragraph3.AddLineBreak();
                    paragraph3.AddText("Ved fejl kontakt vores service afdeling på flg.:");
                    paragraph3.AddLineBreak();
                    paragraph3.AddText("Telefon: " + Properties.Settings.Default.supportPhone);
                    paragraph3.AddLineBreak();
                    paragraph3.AddText("Email: " + Properties.Settings.Default.supportEmail);
                    
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

                    /*
                    // Add the notes paragraph
                    Paragraph paragraph4 = this.document.LastSection.AddParagraph();
                    paragraph4.Format.SpaceBefore = "2cm";
                    paragraph4.Format.Borders.Width = 0.75;
                    paragraph4.Format.Borders.Distance = 3;
                    paragraph4.Format.Borders.Color = TableBorder;
                    paragraph4.Format.Shading.Color = TableGray;

                    paragraph4.AddText("Ved fejl kontakt vores service afdeling på flg.:");
                    paragraph4.AddLineBreak();
                    paragraph4.AddText("Telefon: " + Properties.Settings.Default.supportPhone);
                    paragraph4.AddLineBreak();
                    paragraph4.AddText("Email: " + Properties.Settings.Default.supportEmail);
                     * */
                }
                finally
                {
                    sdb.Dispose();
                }
            }
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
