using Microsoft.AspNetCore.Mvc;
using ProcessPayment.DTOs;
using ProcessPayment.Extensions;
using ProcessPayment.Models;
using ProcessPayment.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPayment.Controllers
{
    [Route("api/payment")]
    [ApiController]
    public class PaymentController:ControllerBase
    {
        private IProccessPaymentService _service;
        
        public PaymentController(IProccessPaymentService service)
        {
            this._service = service;
          
        }
        [Route("process")]
        [HttpPost]
        public async Task<IActionResult> ProcessPayment([FromBody]PaymentDTOs dto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetApiResponse());
            }

            bool successResult = _service.MakePayment(dto) == PaymentStates.SUCCESS;

            if(successResult)
            {
                return Ok(PaymentStates.SUCCESS);
            }

            return BadRequest(PaymentStates.FAILED);

          
        }


       
    }
}
