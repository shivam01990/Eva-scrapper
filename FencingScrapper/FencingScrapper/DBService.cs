using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FencingScrapper
{
    public class DBService
    {
        public static int SaveComany(Comany ud)
        {
            int ComanyID = 0;
            using (DBEntities db = new DBEntities())
            {
                if (ud.ComanyID > 0)
                {
                    Comany temp = db.Comanies.Where(u => u.ComanyID == ud.ComanyID).FirstOrDefault();

                    if (temp != null)
                    {
                        temp.ComanyID = ud.ComanyID;
                        temp.CompanyName = ud.CompanyName;
                        temp.State = ud.State;
                        temp.Email = ud.Email;
                        temp.ComanyURL = ud.ComanyURL;
                        temp.UpdatedOn = DateTime.Now;
                    }
                }
                else
                {
                    db.Comanies.Add(ud);
                }

                int x = db.SaveChanges();
                if (x > 0)
                {
                    ComanyID = ud.ComanyID;
                }
            }

            return ComanyID;
        }

        public static List<Comany> GetCompany()
        {
            var TagIds = new string[] { "Alabama", "Alaska", "Arizona", "Arkansas", "California", "Colorado", "Connecticut", "Delaware", "District of Columbia", "Florida"
            ,"Georgia","Hawaii","Idaho","Illinois"};
            using (DBEntities db = new DBEntities())
            {
                //return (from c in db.Comanies
                //        where !TagIds.Contains(c.State)
                //        select c).ToList();

                return (from c in db.Comanies where c.ComanyID > 5500 select c).ToList();
            }
        }

        public static List<Comany> GetAllCompany()
        {
           
            using (DBEntities db = new DBEntities())
            {  
                return db.Comanies.ToList();
            }
        }
    }
}
