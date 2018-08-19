using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Schedule_Program.lib
{
    /*
     * Class for Generating Tables and PDF 
     */
    public static class FrameWork
    {
        public static void GetTable(PlaceHolder container, List<HtmlInputText> data, bool showdates, int weekdays, int weekindex, List<DateTime> dates, string[] shifttimes, string theme, bool isFullDay)
        {
            Table table = new Table();
            TableHeaderRow hrow = new TableHeaderRow();
            
            TableRow brow = new TableRow();
            
            int start = weekdays * weekindex;
            int end = (start + weekdays) > dates.Count ? (dates.Count - start) + start : (start + weekdays);

            //Shift Days
            for (int i = start; i < end; i++)
            {
                TableHeaderCell th = new TableHeaderCell();
                th.Text = dates.ElementAt(i).ToString("dddd");
                th.ColumnSpan = isFullDay ? 1 : 2;
                th.CssClass = "border-collapse";
                th.Style.Add("text-align", "center");
                hrow.Cells.Add(th);
            }
            table.Rows.Add(hrow);

            //Shift Dates
            if (showdates)
            {
                brow = new TableRow();
                
                for (int i = start; i < end; i++)
                {
                    TableCell td = new TableCell();
                    td.Text = dates.ElementAt(i).ToShortDateString();
                    td.ColumnSpan = isFullDay ? 1 : 2;
                    td.CssClass = "td-size border-collapse";
                    td.Style.Add("text-align", "center");
                    brow.Cells.Add(td);
                }
                table.Rows.Add(brow);
            }

            //Shift Type
            brow = new TableRow();
            
            string[] times = new string[] { "Morning", "Afternoon" };

            for (int i = (isFullDay ? start : start * 2); i < (isFullDay ? end : end * 2); i++)
            {
                TableCell td = new TableCell();
                td.Text = isFullDay ? times[0] : times[i % 2];
                td.ColumnSpan = 1;
                td.CssClass = "td-size border-collapse";
                td.Style.Add("text-align", "center");
                brow.Cells.Add(td);
            }
            table.Rows.Add(brow);

            //Shift Times
            brow = new TableRow();
            
            for (int i = (isFullDay ? start : start * 2); i < (isFullDay ? end : end * 2); i++)
            {
                TableCell td = new TableCell();
                td.Text = isFullDay ? shifttimes[0] : shifttimes[i % 2];
                td.ColumnSpan = 1;
                td.CssClass = "td-xsize border-collapse";
                td.Style.Add("text-align", "center");
                brow.Cells.Add(td);
            }
            table.Rows.Add(brow);

            //Employees
            brow = new TableRow();
            
            for (int i = 0; i < data.Count; i++)
            {
                TableCell td = new TableCell();
                td.Text = data.ElementAt(i).Value;
                td.RowSpan = isFullDay ? 1 : 2;
                td.CssClass = "td-xsize border-collapse";
                td.Style.Add("text-align", "center");
                brow.Cells.Add(td);
            }
            table.Rows.Add(brow);

            brow = new TableRow();
            table.CssClass = "table "+(theme.Equals("0") ? "table-dark " : "thead-light ")+"table-striped table-hover border-collapse";

            container.Controls.Add(table);
        }
        public static void GetPDF(HashMap<int, List<HtmlInputText>> data, string title, bool showdates, int weekdays, List<DateTime> dates, string[] shifttimes, string theme, bool isFullDay)
        {
            Document doc  = new Document();
            doc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(HttpRuntime.AppDomainAppPath + "pdf\\print.pdf", FileMode.Create));
            doc.Open();

            BaseFont mavenbold = BaseFont.CreateFont(HttpRuntime.AppDomainAppPath + "font\\MavenPro-Bold.ttf", BaseFont.CP1252, BaseFont.EMBEDDED);
            BaseFont mavenblack = BaseFont.CreateFont(HttpRuntime.AppDomainAppPath + "font\\MavenPro-Black.ttf", BaseFont.CP1252, BaseFont.EMBEDDED);
            BaseFont mavenregular = BaseFont.CreateFont(HttpRuntime.AppDomainAppPath + "font\\MavenPro-Regular.ttf", BaseFont.CP1252, BaseFont.EMBEDDED);

            iTextSharp.text.Font comtitle = new iTextSharp.text.Font(mavenregular, 22f);
            iTextSharp.text.Font comsubs = new iTextSharp.text.Font(mavenregular, 14f);

            iTextSharp.text.Font headerd = new iTextSharp.text.Font(mavenblack, 12f);
            iTextSharp.text.Font headerl = new iTextSharp.text.Font(mavenbold, 12f);
            iTextSharp.text.Font fdates = new iTextSharp.text.Font(mavenregular, 10f);
            iTextSharp.text.Font ftype = new iTextSharp.text.Font(mavenregular, 8f);
            iTextSharp.text.Font ftimes = new iTextSharp.text.Font(mavenregular, 7f);
            iTextSharp.text.Font femps = new iTextSharp.text.Font(mavenregular, 9f);

            bool isDark = theme.Equals("0") ? true : false;
            
            for (int i = 0; i < data.Size(); i++)
            {
                doc.NewPage();
                int dstart = weekdays * i;
                int dend = (dstart + weekdays) > dates.Count ? (dates.Count - dstart) + dstart - 1 : (dstart + weekdays) - 1;

                string titledate = "Week " + (i + 1) + (showdates ? " : " + dates.ElementAt(dstart).ToString("MMMM dd") +
                                " - " + dates.ElementAt(dend).ToString("MMMM dd") : "");

                Paragraph companyname = new Paragraph("Company X1", comtitle);
                companyname.Alignment = 1;
                Paragraph ntitle = new Paragraph(title, comsubs);
                ntitle.Alignment = 1;
                Paragraph ntitledate = new Paragraph(titledate, comsubs);
                ntitledate.Alignment = 1;
                Paragraph blank = new Paragraph("\n");
                blank.Alignment = 1;

                doc.Add(blank);
                doc.Add(blank);
                doc.Add(blank);
                doc.Add(Chunk.NEWLINE);
                doc.Add(companyname);
                doc.Add(Chunk.NEWLINE);
                doc.Add(ntitle);
                doc.Add(ntitledate);
                doc.Add(blank);


                int mod = dates.Count % weekdays;
                PdfPTable table;

                if (mod > 0 && i == (data.Size() - 1))
                    table = new PdfPTable(isFullDay ? mod : 2 * mod);
                else
                    table = new PdfPTable(isFullDay ? weekdays : 2 * weekdays);

                table.TotalWidth = 750f;
                table.LockedWidth = true;

                int start = weekdays * i;
                int end = (start + weekdays) > dates.Count ? (dates.Count - start) + start : (start + weekdays);
                //Shift Days
                for (int j = start; j < end; j++)
                {
                    headerd.Color = isDark ? BaseColor.WHITE : BaseColor.BLACK;
                    PdfPCell hcell = new PdfPCell(new Phrase(dates.ElementAt(j).ToString("dddd"), isDark ? headerd : headerl));
                    
                    hcell.Colspan = isFullDay ? 1 : 2;
                    hcell.Padding = 10f;
                    hcell.PaddingTop = 15f;
                    hcell.PaddingBottom = 15f;
                    hcell.HorizontalAlignment = 1;
                    hcell.VerticalAlignment = 1;

                    hcell.BorderColor = decode("0x32383e");
                    hcell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
                    hcell.BorderWidthLeft = 0.025f;
                    hcell.BorderWidthRight = 0.025f;
                    hcell.BorderWidthTop = 0.025f;
                    hcell.BorderWidthBottom = 0.025f;

                    hcell.BackgroundColor = isDark ? decode("0x212529") : decode("0xf8f9fa");
                    
                    table.AddCell(hcell);
                }
                //Shift Dates
                if (showdates)
                {
                    for (int j = start; j < end; j++)
                    {
                        fdates.Color = isDark ? BaseColor.WHITE : BaseColor.BLACK;
                        PdfPCell bcell = new PdfPCell(new Phrase(dates.ElementAt(j).ToShortDateString(), fdates));
                        bcell.Colspan = isFullDay ? 1 : 2;
                        bcell.Padding = 10f;
                        bcell.HorizontalAlignment = 1;
                        bcell.VerticalAlignment = 1;

                        bcell.BorderColor = decode("0x32383e");
                        bcell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
                        bcell.BorderWidthLeft = 0.025f;
                        bcell.BorderWidthRight = 0.025f;
                        bcell.BorderWidthTop = 0.025f;
                        bcell.BorderWidthBottom = 0.025f;

                        bcell.BackgroundColor = isDark ? decode("0x343a40") : decode("0xFFFFFF");
                        table.AddCell(bcell);
                    }
                }
                //Shift Type
                string[] times = new string[] { "Morning", "Afternoon" };

                for (int j = (isFullDay ? start : start * 2); j < (isFullDay ? end : end * 2); j++)
                {
                    ftype.Color = isDark ? BaseColor.WHITE : BaseColor.BLACK;
                    PdfPCell bcell = new PdfPCell(new Phrase(isFullDay ? times[0] : times[j % 2], ftype));
                    bcell.Colspan = 1;
                    bcell.Padding = 5f;
                    bcell.PaddingTop = 10f;
                    bcell.PaddingBottom = 10f;
                    bcell.HorizontalAlignment = 1;
                    bcell.VerticalAlignment = 1;

                    bcell.BorderColor = decode("0x32383e");
                    bcell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
                    bcell.BorderWidthLeft = 0.025f;
                    bcell.BorderWidthRight = 0.025f;
                    bcell.BorderWidthTop = 0.025f;
                    bcell.BorderWidthBottom = 0.025f;

                    bcell.BackgroundColor = isDark ? decode("0x212529") : decode("0xf8f9fa");
                    table.AddCell(bcell);
                }
                //Shift Times
                for (int j = (isFullDay ? start : start * 2); j < (isFullDay ? end : end * 2); j++)
                {
                    ftimes.Color = isDark ? BaseColor.WHITE : BaseColor.BLACK;
                    PdfPCell bcell = new PdfPCell(new Phrase(isFullDay ? shifttimes[0] : shifttimes[j % 2], ftimes));
                    bcell.Colspan = 1;
                    bcell.Padding = 3f;
                    bcell.PaddingTop = 10f;
                    bcell.PaddingBottom = 10f;
                    bcell.HorizontalAlignment = 1;
                    bcell.VerticalAlignment = 1;

                    bcell.BorderColor = decode("0x32383e");
                    bcell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
                    bcell.BorderWidthLeft = 0.025f;
                    bcell.BorderWidthRight = 0.025f;
                    bcell.BorderWidthTop = 0.025f;
                    bcell.BorderWidthBottom = 0.025f;

                    bcell.BackgroundColor = isDark ? decode("0x343a40") : decode("0xFFFFFF");
                    table.AddCell(bcell);
                }
                //Employees
                for (int j = 0; j < data.GetValue(i).Count; j++)
                {
                    femps.Color = isDark ? BaseColor.WHITE : BaseColor.BLACK;
                    PdfPCell bcell = new PdfPCell(new Phrase(data.GetValue(i).ElementAt(j).Value, femps));
                    bcell.Colspan = 1;
                    bcell.Padding = 3f;
                    bcell.PaddingTop = 10f;
                    bcell.PaddingBottom = 10f;
                    bcell.HorizontalAlignment = 1;
                    bcell.VerticalAlignment = 1;

                    bcell.BorderColor = decode("0x32383e");
                    bcell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
                    bcell.BorderWidthLeft = 0.025f;
                    bcell.BorderWidthRight = 0.025f;
                    bcell.BorderWidthTop = 0.025f;
                    bcell.BorderWidthBottom = 0.025f;

                    bcell.BackgroundColor = isDark ? decode("0x212529") : decode("0xf8f9fa");
                    table.AddCell(bcell);
                }
                
                doc.Add(table);
            }
            
            doc.Close();

        }
        private static BaseColor decode(string color)
        {
            return new BaseColor(Convert.ToInt32(color.Substring(2, 2), 16),
                Convert.ToInt32(color.Substring(4, 2), 16),
                Convert.ToInt32(color.Substring(6, 2), 16));
        }
    }

}