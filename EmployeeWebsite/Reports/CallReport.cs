﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;
using System.IO;
using HelpdeskViewModels;
using HelpdeskDAL;
namespace EmployeeWebsite.Reports
{
    public class CallReport
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
                PdfWriter.GetInstance(document, new FileStream(mappedpath + "PDF/Call.pdf", FileMode.Create));
                document.Open();
                Paragraph para = new Paragraph();
                Image image1 = Image.GetInstance(mappedpath + IMG);
                image1.SetAbsolutePosition(25f, 740f);
                image1.ScalePercent(15f);
                para.Add(image1);
                para.Alignment = Element.ALIGN_RIGHT;
                //Lets write a big header
                addEmptyLine(para, 1);
                Paragraph mainHead = new Paragraph(String.Format("{0,8}", "Calls"), catFont);
                mainHead.Alignment = Element.ALIGN_CENTER;
           
                para.Add(mainHead);
                addEmptyLine(para, 3);

                PdfPTable table = new PdfPTable(6);
                table.WidthPercentage = 100.00F;
                table.AddCell(addCell("Opened", "h"));
                table.AddCell(addCell("Lastname", "h"));
                table.AddCell(addCell("Tech", "h"));
                table.AddCell(addCell("Problem", "h"));
                table.AddCell(addCell("Status", "h"));
                table.AddCell(addCell("Closed", "h"));
                table.AddCell(addCell(" "));
                table.AddCell(addCell(" "));
                table.AddCell(addCell(" "));
                table.AddCell(addCell(" "));
                table.AddCell(addCell(" "));
                table.AddCell(addCell(" "));

                CallViewModel call = new CallViewModel();
                List<CallViewModel> calls = call.GetAll();

                foreach (CallViewModel cal in calls)
                {
                    EmployeeModel emp = new EmployeeModel();
                    Employee employee = emp.GetById(cal.TechId);
                    table.AddCell(addCell(cal.DateOpened.ToString("MM/dd/yyyy")));
                    table.AddCell(addCell(cal.EmployeeName));
                    table.AddCell(addCell(employee.LastName));
                    table.AddCell(addCell(cal.ProblemDescription));
                    table.AddCell(addCell(cal.OpenStatus?"Open":"Closed"));
                    table.AddCell(addCell((cal.DateClosed==null)?"":((DateTime) cal.DateClosed).ToString("MM-dd-yyyy")));

                }

                para.Add(table);
                addEmptyLine(para, 3);
                para.Alignment = Element.ALIGN_CENTER;
                Paragraph footer = new Paragraph("Call report written on - " + DateTime.Now, smallFont);
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