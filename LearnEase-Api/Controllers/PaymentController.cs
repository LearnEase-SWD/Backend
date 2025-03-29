using LearnEase.Core.Entities;
using LearnEase.Service.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnEase_Api.Controllers
{
    [AllowAnonymous]
    public class PaymentController : Controller
    {
        private readonly IVnPayService _vnPayService;
        
        public PaymentController(IVnPayService vnPayService)
        {
            _vnPayService = vnPayService;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpPost("Checkout")]
        public async Task<IActionResult> CreatePaymentUrl(PaymentInformation model)
        {
            var url = await _vnPayService.CreatePaymentUrlAsync(model, HttpContext);
            return Ok(url);
        }

        [HttpGet("Result")]
        public async Task<IActionResult> PaymentCallback()
        {
            var response = await _vnPayService.PaymentExecuteAsync(Request.Query);
            return Json(response);
        }
    }
}
