using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductSpecificationsParamters
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? Sort { get; set; }
        public string? Search { get; set; }



        private int _pagesize = 5;
        private int _pageIndex = 1;

        public int PageIndex
        {
            get { return _pageIndex = 1; }
            set { _pageIndex  = value; }
        }


        public int Pagesize
        {
            get { return _pagesize = 5; }
            set { _pagesize  = value; }
        }


    }
}
