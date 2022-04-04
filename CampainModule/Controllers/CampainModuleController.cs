using CampainModule.Bll.Services;
using CampainModule.Model.CampainModuleModel;
using Microsoft.AspNetCore.Mvc;

namespace CampainModule.Controllers
{
    [ApiController]
    [Route("api/1.0/[controller]")]
    public class CampainModuleController : ControllerBase
    {
        private readonly ILogger<CampainModuleController> _logger;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly ICampaignService _campaignService;
        private readonly IRunCampaignService _runCampaignService;

        public CampainModuleController(ILogger<CampainModuleController> logger, IProductService productService, IOrderService orderService, ICampaignService campaignService, IRunCampaignService runCampaignService)
        {
            _logger = logger;
            _productService = productService;
            _orderService = orderService;
            _campaignService = campaignService;
            _runCampaignService = runCampaignService;

        }

        [HttpGet("GetProductInfo/{productCode}")]
        public IActionResult Get_Product_Info(string productCode)
        {
            var result = _productService.GetProductInfo(productCode);
            return Ok(result);
        }

        [HttpGet("GetCampaignInfo/{name}")]
        public IActionResult Get_Campaign_Info(string name)
        {
            var result = _campaignService.GetCampaignInfo(name);
            return Ok(result);
        }

        [HttpPost("createProduct")]
        public IActionResult Create_Product(ProductsModel products)
        {
            var result = _productService.CreateProduct(products);
            return Ok(result);
        }

        [HttpPost("createOrder")]
        public IActionResult Create_Order(OrdersModel orders)
        {
            var result = _orderService.CreateOrder(orders);
            return Ok(result);
        }

        [HttpPost("createCampaign")]
        public IActionResult Create_Campaign(CampaignsModel campaigns)
        {
            var result = _campaignService.CreateCampaign(campaigns);
            return Ok(result);
        }

        [HttpPost("increaseTime/{time}")]
        public IActionResult Increase_Time(int time)
        {
            var result = _runCampaignService.IncreaseTime(time);
            return Ok(result);
        }
    }
}