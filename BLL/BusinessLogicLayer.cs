using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class BusinessLogicLayer//establishing a 3-tier architecture
    {
        DataAccessLayer dal = new DataAccessLayer();

        public int InsertProduct(Product products)
        {
            return dal.InsertProduct(products);
        }

    }
}
