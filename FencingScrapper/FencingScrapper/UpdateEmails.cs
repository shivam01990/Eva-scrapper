using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FencingScrapper
{
   public class UpdateEmails
    {
       public static void UpdateStart()
       {
           List<Comany> lst = DBService.GetCompany();
           foreach (Comany item in lst)
           {
             Comany temp=  GetCompanyEmail(item);
             try
             {
                 DBService.SaveComany(temp);
             }
             catch { }
           }
       }

       public static Comany GetCompanyEmail(Comany _company)
       {
           try
           {
               string outhtml = Helper.OpenIEURL(_company.ComanyURL);
               HtmlDocument doc = new HtmlDocument();
               doc.LoadHtml(outhtml);
               HtmlNode divEmail = doc.DocumentNode.SelectNodes("//div[@rel='contact']//div[@itemprop='email']").FirstOrDefault();
               string Email = divEmail.InnerText.Replace("Email:", "");
               _company.Email = Email;
           }
           catch { }
           return _company;
          
       }
    }
}
