using CampainModule.Bll.Services;
using CampainModule.Data;
using CampainModule.Model.CampainModuleModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampainModule.Bll.ServiceManager
{
    public class OrderServiceManager : IOrderService
    {
        private DbContextOptions<CampaignModuleDbContext> GetOptions()
        {
            var options = new DbContextOptionsBuilder<CampaignModuleDbContext>();
            options.UseSqlServer("Data Source=DESKTOP-V62LJP0;Initial Catalog=CampaignModule;Integrated Security=true");

            return options.Options;
        }
        public ReturnModel CreateOrder(OrdersModel orders)
        {
            var resultModel = new ReturnModel();
            try
            {

                using (var db = new CampaignModuleDbContext(GetOptions()))
                {
                    var tran = db.Database.BeginTransaction();
                    try
                    {
                        var model = new Orders();
                        model.ProductCode = orders.ProductCode;
                        model.Quantity = orders.Quantity;


                        db.Orders.Add(model);

                        UpdateStock(db, orders.ProductCode, orders.Quantity);

                        db.SaveChanges();
                        tran.Commit();

                        resultModel.IsSuccess = true;
                        resultModel.Result = "Order created; product " + orders.ProductCode + ", quantity " + orders.Quantity + "";

                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        resultModel.IsSuccess = false;
                        resultModel.Result = ex.Message;

                    }
                }

            }
            catch (Exception ex)
            {
                resultModel.IsSuccess = false;
                resultModel.Result = ex.Message;
            }
            return resultModel;
        }

        private void UpdateStock(CampaignModuleDbContext db, string productCode, int Quantity)
        {
            try
            {
                var model = db.Products.FirstOrDefault(x => x.ProductCode == productCode);
                model.Stock -= Quantity;

                db.Products.Update(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
