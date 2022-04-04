using CampainModule.Model.CampainModuleModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampainModule.Bll.Services
{
    public interface IOrderService
    {
        OrdersModel CreateOrder(OrdersModel orders);
    }
}
