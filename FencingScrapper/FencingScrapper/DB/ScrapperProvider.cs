using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FencingScrapper.DB
{
    public class ScrapperProvider
    {
        public List<scrapperModel> GetScrapperModel(string sourceUrl)
        {
            List<scrapperModel> modeldata = new List<scrapperModel>();
            using (EvaScrapperEntities db = new DB.EvaScrapperEntities())
            {
                modeldata = (from s in db.tblscrappers
                             where s.SourceUrl == sourceUrl
                             select new scrapperModel
                             {
                                 Id = s.Id,
                                 CompanyName = s.CompanyName,
                                 CompanyUrl = s.CompanyUrl,
                                 SourceUrl = s.SourceUrl,
                                 FirstName = s.FirstName,
                                 LastName = s.LastName,
                                 City = s.City,
                                 State = s.State,
                                 Address = s.Address,
                                 Phone = s.Phone,
                                 Email = s.Email,
                                 Houses = s.Houses,
                                 DetailsPageUrl = s.DetailsPageUrl,
                                 IsDetailsPageScrapped = s.IsDetailsPageScrapped,
                                 Prices = s.Prices
                             }).ToList();
            }
            return modeldata;
        }


        public int SaveScrapper(tblscrapper model)
        {
            int Id = 0;
            using (EvaScrapperEntities db = new EvaScrapperEntities())
            {
                if (model.Id > 0)
                {
                    tblscrapper temp = db.tblscrappers.Where(u => u.Id == model.Id).FirstOrDefault();

                    if (temp != null)
                    {

                        temp.CompanyName = model.CompanyName;
                        temp.CompanyUrl = model.CompanyUrl;
                        temp.SourceUrl = model.SourceUrl;
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
                        db.Entry(temp).State = EntityState.Modified;
                    }
                }
                else
                {
                    db.tblscrappers.Add(model);
                }

                int x = db.SaveChanges();
                if (x > 0)
                {
                    Id = model.Id;
                }
            }

            return Id;
        }

    }
}
