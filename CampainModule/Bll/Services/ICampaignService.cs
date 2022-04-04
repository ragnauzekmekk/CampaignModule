using CampainModule.Model.CampainModuleModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampainModule.Bll.Services
{
    public interface ICampaignService
    {
        ReturnModel GetCampaignInfo(string productCode);
        ReturnModel CreateCampaign(CampaignsModel products);

    }
}
