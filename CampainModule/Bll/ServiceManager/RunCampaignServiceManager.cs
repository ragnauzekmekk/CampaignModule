using CampainModule.Bll.Services;
using CampainModule.Data;
using CampainModule.Model.CampainModuleModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CampainModule.Bll.ServiceManager
{
    public class RunCampaignServiceManager : IHostedService, IRunCampaignService
    {
        public static int _time;
        public static string _campaign;
        public static decimal priceControl;
        private static Timer _timer;
        private DbContextOptions<CampaignModuleDbContext> GetOptions()
        {
            var options = new DbContextOptionsBuilder<CampaignModuleDbContext>();
            options.UseSqlServer("Data Source=DESKTOP-V62LJP0;Initial Catalog=CampaignModule;Integrated Security=true");

            return options.Options;
        }
        public ReturnModel IncreaseTime(int time)
        {
            var resultModel = new ReturnModel();

            var count = false;
            var timerCount = 0;

            try
            {
                _timer = new Timer(t =>
                {
                    if (count)
                    {
                        _timer?.Change((_time * 3600000), (_time * 3600000));
                        _timer?.Dispose();
                        _timer = null;

                        _time = 0;


                        using (var db = new CampaignModuleDbContext())
                        {
                            var tran = db.Database.BeginTransaction();
                            var campaign = db.Campaigns.OrderBy(x => x.Id).LastOrDefault();
                            var product = db.Products.FirstOrDefault(x => x.ProductCode == campaign.ProduceCode);

                            product.PriceCampaign = product.Price;
                            db.Products.Update(product);
                            db.SaveChanges();

                            tran.Commit();
                            resultModel.IsSuccess = true;
                            resultModel.Result = Convert.ToDateTime("00:00").AddHours(_time).ToShortTimeString();

                        }


                    }
                    else
                    {
                        using (var db = new CampaignModuleDbContext())
                        {
                            var tran = db.Database.BeginTransaction();
                            try
                            {
                                _time += time;
                                var campaign = db.Campaigns.OrderBy(x => x.Id).LastOrDefault();
                                var product = db.Products.FirstOrDefault(x => x.ProductCode == campaign.ProduceCode);

                                if (_campaign != campaign.Name)
                                {
                                    _time = time;
                                    _campaign = campaign.Name;
                                    priceControl = product.Price - ((product.Price * campaign.PriceManipulationLimit) / 100);
                                }

                                var campaignProportion = _time * (campaign.TargetSalesCount / campaign.PriceManipulationLimit);

                                var priceCampaign = product.Price - campaignProportion;
                                 
                                if (campaign.TargetSalesCount <= product.Stock && priceCampaign >= priceControl)
                                {
                                    product.PriceCampaign = product.Price - campaignProportion;
                                    db.Products.Update(product);
                                    db.SaveChanges();

                                }
                                else
                                {
                                    _timer?.Change((_time * 3600000), (_time * 3600000));
                                    _timer?.Dispose();
                                    _timer = null;
                                    _time = 0;

                                    product.PriceCampaign = product.Price;
                                    db.Products.Update(product);
                                    db.SaveChanges();
                                }

                                tran.Commit();
                                resultModel.IsSuccess = true;
                                resultModel.Result = Convert.ToDateTime("00:00").AddHours(_time).ToShortTimeString();

                            }
                            catch (Exception ex)
                            {
                                tran.Rollback();
                                resultModel.IsSuccess = false;
                                resultModel.Result = ex.Message;
                            }

                        }

                        count = true;
                    }
                }, null, 0, (_time * 3600000));

                resultModel.IsSuccess = true;
                resultModel.Result = Convert.ToDateTime("00:00").AddHours(_time).ToShortTimeString();

            }
            catch (Exception ex)
            {
                resultModel.IsSuccess = false;
                resultModel.Result = ex.Message;
            }
            return resultModel;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
