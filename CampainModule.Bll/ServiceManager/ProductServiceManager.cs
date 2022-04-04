using CampainModule.Bll.Services;
using CampainModule.Model.CampainModuleModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampainModule.Bll.ServiceManager
{
    public class ProductServiceManager : IProductService
    {
        private DbContextOptions<CampaignModuleDbContext> GetOptions()
        {
            var options = new DbContextOptionsBuilder<CampaignModuleDbContext>();
            //options.UseSqlServer("");

            return options.Options;
        }
        public ReturnModel CreateProduct(ProductsModel products)
        {
            var resultModel = new ReturnModel();
            try
            {
                using (var db = new CampaignModuleDbContext())
                {
                    var model = new Products();
                    model.Id = 2;
                    model.ProductCode = products.ProductCode;
                    model.Price = products.Price;
                    model.Stock = products.Stock;


                    db.Products.Add(model);
                    db.SaveChanges();

                    resultModel.IsSuccess = true;
                    resultModel.Result = "Product created; code " + products.ProductCode + ", price " + products.Price + ", stock " + products.Stock + "";
                }

            }
            catch (Exception ex)
            {
                resultModel.IsSuccess = false;
                resultModel.Result = ex.Message;
            }

            return resultModel;
        }

        public ReturnModel GetProductInfo(string productCode)
        {
            var resultModel = new ReturnModel();
            try
            {
                using (var db = new CampaignModuleDbContext())
                {
                    var product = db.Products.FirstOrDefault(x => x.ProductCode == productCode);

                    resultModel.IsSuccess = true;
                    resultModel.Result = "Product " + product.ProductCode + " info; price " + product.Price + ", stock " + product.Stock + "";
                }

            }
            catch (Exception ex)
            {
                resultModel.IsSuccess = true;
                resultModel.Result = ex.Message;
            }

            return resultModel;
        }
    }
}
