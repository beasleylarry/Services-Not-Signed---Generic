using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Net.Mail;
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace Services_Not_Signed
{
    class Program
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static string sendEmail(string emailaddress, string supervisoremail, string CCemail, string emailsubject, string emailbody, string emp_id)
        {


            SmtpClient emailToSend = new SmtpClient("mailserver.yourdomain.com");
            MailMessage emailmessage = null;
            string MessageBody = "";
            string[] separators = { ",", "!", "?", ";", ":", " " };
            string[] additionalCC = CCemail.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            MessageBody = "<p> Good " + ((DateTime.Now.ToString("tt") == "AM") ? "Morning" : "Afternoon") + ",</p> " +
                "	<p>Reimbursement reports have indicated that you currently have  service(s) that were previously provided, <br />" +
                "	  that are requiring signature and submission. </p>" +
                "	<p><span style=\"background-color:yellow\"><strong>Please  sign and complete this service(s) immediately.</strong></span></p>" +
                "	<p>If this service(s) isn't signed/submitted by 11:59pm  tonight, we run a high risk of losing valuable reimbursement<br />" +
                "	  for services you've provided.</p>" +
                "	<p><span style=\"background-color:#40e0d0\"><strong>You can  see your own outstanding & incomplete services at any time by:</strong></p>" +
                "	<p><strong>  Going to your employee profile in Credible</strong><br />" +
                "	  <strong>  Clicking on Services List on the left hand side</strong><br />" +
                "	  <strong>  Clicking on the \"incomplete services\" button on the top right corner  of the next screen</strong></span></p>" +
                "	<p>If you have any questions, please consult your direct  supervisor for further instruction.</p>" +
                "	<p style=\"font: 20px Palatino Linotype, Book Antiqua, Palatino, serif;\">Thank  you and have a wonderful " + ((DateTime.Now.ToString("tt") == "AM") ? "day" : "evening") + "! </p>" +
                "	<p>Below you will find a listing of your <a href=\"https://ww2.crediblebh.com/visit/list_visittemps.asp?emp_id=" + emp_id + "\" >services</a>:</p>" +
                            "	<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"905\">" +
                "	  <tr>" +
                "		<td width=\"140\"><p align=\"center\"><strong>Client</strong></p></td>" +
                "		<td width=\"143\"><p align=\"center\"><strong>Service Id</strong></p></td>" +
                "		<td width=\"145\"><p align=\"center\"><strong>Visit Type</strong></p></td>" +
                "		<td width=\"191\"><p align=\"center\"><strong>Program</strong></p></td>" +
                "		<td width=\"76\"><p align=\"center\"><strong>Service Date</strong></p></td>" +
                "		<td width=\"76\"><p align=\"center\"><strong>Service Time</strong></p></td>" +
                "	  </tr>" + emailbody + "</table>" +
                "   <br/><br/><br/>" +
                "   <p>" +
                "    <span style=\"font-family:'Helvetica','sans-serif'; font-size:12.0pt; color:#7F7F7F; \">" +
                "     <em>[This is an automated  message – You are receiving this message because one or more previously  provided services were not signed and completed.]</em>" +
                "    </span>" +
                "   </p>" +
                "<p>&nbsp;</p>";

            try
            {
                emailToSend.UseDefaultCredentials = true;

                if (string.IsNullOrWhiteSpace(emailaddress))
                {
                    emailaddress = ((supervisoremail == null) ? "youremailaddress@yourdomain.com" : supervisoremail);
                    emailsubject += "Employee: " + emp_id + " Has no Email Address" + ((string.IsNullOrWhiteSpace(supervisoremail)) ? " - No Direct Supervisor" : "");
                }

                emailmessage = new MailMessage("no-reply@yourdomain.com", emailaddress, emailsubject + ((string.IsNullOrWhiteSpace(supervisoremail)) ? " - No Direct Supervisor" : ""), MessageBody);

                foreach (string addCC in additionalCC)
                {
                    if (!string.IsNullOrWhiteSpace(addCC)) emailmessage.CC.Add(new MailAddress(addCC));

                }

                if (!string.IsNullOrWhiteSpace(supervisoremail)) emailmessage.CC.Add(new MailAddress(supervisoremail));
                emailmessage.Bcc.Add(new MailAddress("youremailaddress@yourdomain.com"));

                log.Debug(emailmessage.CC.ToString());
                emailmessage.IsBodyHtml = true;
                emailToSend.Send(emailmessage);

            }

            catch (Exception ex)
            {
                log.Error("Error Sending Email: ", ex);
                return "Error Sending Email!" + emailaddress;
                throw ex;

            }

            finally
            {
                if (emailmessage != null)
                {
                    emailmessage.Dispose();

                }
            }

            return "Email Sent Successfully!";

        }



        static void Main(string[] args)
        {

            log.Info("Services Not Signed Running");

            DateTime beginDate = DateTime.Now.AddDays(-1);
            DateTime endDate = DateTime.Now.AddDays(-1);
            DateTime tryDate;
            string CC = "";
            string visittypeparam = "";

            int tryDays;


            if (args.Length > 0)
            {
                if (DateTime.TryParse(args[0], out tryDate))
                {
                    beginDate = tryDate;
                }
                else if (int.TryParse(args[0], out tryDays))
                {
                    beginDate = DateTime.Now.AddDays(tryDays);
                }

                if (args.Length > 1)
                {
                    if (DateTime.TryParse(args[1], out tryDate))
                    {
                        endDate = tryDate;
                    }
                    else if (int.TryParse(args[1], out tryDays))
                    {
                        endDate = DateTime.Now.AddDays(tryDays);
                    }

                }

                foreach (string emailaddress in args)
                {
                    if (emailaddress.Length > 3) { if (emailaddress.Substring(0, 3) == "CC:") CC = emailaddress.Substring(3, emailaddress.Length - 3); }

                }

                foreach (string visittype in args)
                {
                    if (visittype.Length > 10) { if (visittype.Substring(0, 10) == "Visittype:") visittypeparam = visittype.Substring(10, visittype.Length - 10); }

                }

            }

            if (endDate < beginDate) endDate = beginDate;

            log.Debug(beginDate.ToString());
            log.Debug(endDate.ToString());
            log.Debug(CC.ToString());
            log.Debug(visittypeparam.ToString());

            bool tryagain = true;
            int retries = 1;
            while (tryagain)
            {
                try
                {
                    //Declare new Credible Export Service Soap Client
                    CredibleExportService.ExportServiceSoapClient soapclient1 = new CredibleExportService.ExportServiceSoapClient("ExportServiceSoap");

                    using (XmlReader reader = XmlReader.Create(new StringReader(soapclient1.ExportXML("connection_string", "", "", beginDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"), visittypeparam))))
                    {


                        string emailaddress = "";
                        string supervisoremail = "";
                        string emp_id = "";

                        while (reader.Read())
                        {


                            // Only detect start elements.
                            if (reader.IsStartElement())
                            {
                                // Get element name and switch on it.
                                switch (reader.Name)
                                {
                                    case "employeeemail":
                                        // Detect this element.
                                        emailaddress = reader.ReadString();


                                        Console.WriteLine(String.Format("Emailing to...{0}\r\n", emailaddress));
                                        break;

                                    case "supervisoremail":
                                        //Detect if there is a supervisor email in the current XML path
                                        supervisoremail = reader.ReadString();

                                        Console.WriteLine(String.Format("Emailing supervisor...{0}\r\r", supervisoremail));
                                        break;

                                    case "emp_id":
                                        //Detect if there is an employee Id in the current XML path
                                        emp_id = reader.ReadString();

                                        break;

                                    case "email":
                                        // Detect this article element.

                                        Console.WriteLine("Sending Email Contents\r\n" + DateTime.Now.ToString("tt"));
                                        // Search for the attribute name on this current node.
                                        string attribute = reader["name"];
                                        if (attribute != null)
                                        {
                                            Console.WriteLine("  Has attribute name: " + attribute);
                                        }
                                        // Next read will contain text.
                                        if (reader.Read())
                                        {
                                            //Uncomment the below if you wish to send live emails to employees. Otherwise, copy the line below and 
                                            //replace emailaddress & supervisoremail with your own
                                            //string result = sendEmail(emailaddress, supervisoremail, CC, "Services not signed in 24 hours", reader.Value.Trim(), emp_id);
                                            Console.Write(result);

                                            emailaddress = "";
                                            supervisoremail = "";
                                            emp_id = "";
                                        }
                                        break;
                                }
                            }
                        }

                    }

                    break;
                }

                catch (Exception ex)
                {
                    log.Error("Attempt(" + retries + "); Issues with Credible Export Service: ", ex);
                    if (retries++ % 3 != 0) tryagain = true; else throw;

                }
            }
        }


    }
}


