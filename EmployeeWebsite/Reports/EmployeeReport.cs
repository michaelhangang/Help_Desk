﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;
using System.IO;
using HelpdeskViewModels;
namespace EmployeeWebsite.Reports
{
    public class EmployeeReport
    {
        static Font catFont = new Font(Font.FontFamily.HELVETICA, 20, Font.BOLD);
        static Font subFont = new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD);
        static Font smallFont = new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD);
        static string mappedpath = System.Web.Hosting.HostingEnvironment.MapPath("~/");
        static string IMG = "img/Help_Desk_Logo_Futura.png";
        public void doIt()
        {
            try
            {
                Document document = new Document();
                PdfWriter.GetInstance(document, new FileStream(mappedpath + "PDF/Employee.pdf", FileMode.Create));
                document.Open();
                Paragraph para = new Paragraph();
                Image image1 = Image.GetInstance(mappedpath + IMG);
                image1.SetAbsolutePosition(25f, 740f);             
                image1.ScalePercent(15f);
                para.Add(image1);
                para.Alignment = Element.ALIGN_RIGHT;
                //Lets write a big header
                addEmptyLine(para, 1);
                Paragraph mainHead = new Paragraph(String.Format("{0,8}", "Employees"), catFont);
                mainHead.Alignment = Element.ALIGN_CENTER;
                para.Add(mainHead);
                addEmptyLine(para, 3);

                PdfPTable table = new PdfPTable(3);
                table.WidthPercentage = 80.00F;
                table.AddCell(addCell("Title", "h"));
                table.AddCell(addCell("Firstname", "h"));
                table.AddCell(addCell("Lastname", "h"));
                table.AddCell(addCell(" "));
                table.AddCell(addCell(" "));
                table.AddCell(addCell(" "));
                EmployeeViewModel employee = new EmployeeViewModel();
                List<EmployeeViewModel> employees = employee.GetAll();

                foreach (EmployeeViewModel emp in employees)
                {
                    table.AddCell(addCell(emp.Title));
                    table.AddCell(addCell(emp.Firstname));
                    table.AddCell(addCell(emp.Lastname));
                }

                para.Add(table);
                addEmptyLine(para, 3);
                para.Alignment = Element.ALIGN_CENTER;
                Paragraph footer = new Paragraph("Employee report written on - " + DateTime.Now, smallFont);
                footer.Alignment = Element.ALIGN_CENTER;
                para.Add(footer);
                document.Add(para);
                document.Close();

            }
            catch (Exception ex)
            {
                Trace.WriteLine("Error " + ex.Message);
            }
        }
        private static void addEmptyLine(Paragraph paragraph, int number)
        {
            for (int i = 0; i < number; i++)
            {
                paragraph.Add(new Paragraph(" "));
            }
        }

        private PdfPCell addCell(string data, string celltype = "d")
        {
            PdfPCell cell;

            if (celltype == "h")
            {
                cell = new PdfPCell(new Phrase(data, smallFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
            }
            else
            {
                cell = new PdfPCell(new Phrase(data));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
            }
            return cell;
        }

    }
}