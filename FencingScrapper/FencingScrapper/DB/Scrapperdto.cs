using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FencingScrapper.DB
{
   public class Scrapperdto
    {
        public static tblscrapper ConvertToDB(scrapperModel model)
        {
            tblscrapper temp = new tblscrapper();
            temp.Id = model.Id;
            temp.CompanyName = model.CompanyName;
            temp.CompanyUrl = model.CompanyUrl;
            temp.SourceUrl = model.SourceUrl;
            temp.PagingURL = model.PagingURL;
            temp.FirstName = model.FirstName;
            temp.LastName = model.LastName;
            temp.City = model.City;
            temp.State = model.State;
            temp.Address = model.Address;
            temp.Phone = model.Phone;
            temp.Email = model.Email;
            temp.Houses = model.Houses;
            temp.DetailsPageUrl = model.DetailsPageUrl;
            temp.IsDetailsPageScrapped = model.IsDetailsPageScrapped;
            temp.Prices = model.Prices;
            return temp;
        }

        public static scrapperModel ConvertToModel(tblscrapper model)
        {
            scrapperModel temp = new scrapperModel();
            temp.Id = model.Id;
            temp.CompanyName = model.CompanyName;
            temp.CompanyUrl = model.CompanyUrl;
            temp.SourceUrl = model.SourceUrl;
            temp.PagingURL = model.PagingURL;
            temp.FirstName = model.FirstName;
            temp.LastName = model.LastName;
            temp.City = model.City;
            temp.State = model.State;
            temp.Address = model.Address;
            temp.Phone = model.Phone;
            temp.Email = model.Email;
            temp.Houses = model.Houses;
            temp.DetailsPageUrl = model.DetailsPageUrl;
            temp.IsDetailsPageScrapped = model.IsDetailsPageScrapped;
            temp.Prices = model.Prices;
            return temp;
        }

    }
}
