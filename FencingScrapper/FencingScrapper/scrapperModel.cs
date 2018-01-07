using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FencingScrapper
{
    public class scrapperModel
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SourceUrl { get; set; }
        public string CompanyUrl { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string DetailsPageUrl{get;set;}
        public string Email { get; set; }
        public string Prices { get; set; }
        public string Houses { get; set; }
        public string PagingURL { get; set; }
        public bool IsDetailsPageScrapped { get; set; }
    }
}
