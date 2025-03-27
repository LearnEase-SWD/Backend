using LearnEase.Core.Entities;
using LearnEase.Core.Models.reponse;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnEase.Service.IServices
{
    public interface IVnPayService
    {
        Task<string> CreatePaymentUrlAsync(PaymentInformation model, HttpContext context);
        Task<PaymentResponse> PaymentExecuteAsync(IQueryCollection collections);
    }
}
