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
    public class CampaignServiceManager : ICampaignService
    {
        private DbContextOptions<CampaignModuleDbContext> GetOptions()
        {
            var options = new DbContextOptionsBuilder<CampaignModuleDbContext>();
            options.UseSqlServer("Data Source=DESKTOP-V62LJP0;Initial Catalog=CampaignModule;Integrated Security=true");

            return options.Options;
        }
        public ReturnModel CreateCampaign(CampaignsModel campaigns)
        {

            var resultModel = new ReturnModel();
            try
            {
                using (var db = new CampaignModuleDbContext(GetOptions()))
                {
                    var model = new Campaigns();
                    model.ProduceCode = campaigns.ProduceCode;
                    model.TargetSalesCount = campaigns.TargetSalesCount;
                    model.Duration = campaigns.Duration;
                    model.PriceManipulationLimit = campaigns.PriceManipulationLimit;
                    model.Name = campaigns.Name;


                    db.Campaigns.Add(model);
                    db.SaveChanges();

                    resultModel.IsSuccess = true;
                    resultModel.Result = "Campaign created; name " + campaigns.Name + ", product " + campaigns.ProduceCode + ", duration " + campaigns.Duration + ",limit " + campaigns.PriceManipulationLimit + ", target sales count " + campaigns.TargetSalesCount + "";
                }

            }
            catch (Exception ex)
            {
                resultModel.IsSuccess = false;
                resultModel.Result = ex.Message;
            }

            return resultModel;
        }

        public ReturnModel GetCampaignInfo(string name)
        {
            var resultModel = new ReturnModel();
            try
            {
                using (var db = new CampaignModuleDbContext())
                {
                    var campaigns = db.Campaigns.FirstOrDefault(x => x.Name == name);

                    resultModel.IsSuccess = true;
                    resultModel.Result = "Campaign " + campaigns.Name + " info; Status Active, Target Sales " + campaigns.TargetSalesCount + ",Total Sales 50, Turnover 5000, Average Item Price 100";
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
