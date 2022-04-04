using CampainModule.Model.CampainModuleModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampainModule.Bll.Services
{
    public interface IProductService
    {
        ReturnModel GetProductInfo(string productCode);
        ReturnModel CreateProduct(ProductsModel products);

    }
}
