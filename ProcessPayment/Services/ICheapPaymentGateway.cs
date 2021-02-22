using ProcessPayment.DTOs;
using ProcessPayment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPayment.Services
{
    public interface IProccessPaymentService
    {
        public string MakePayment(PaymentDTOs dto);
        
    }




    public class ProcessPaymentService : IProccessPaymentService
    {
        private ProcessPayment service;
        
        ICheapPaymentGateway cheapPaymentGateway;
        IPremiumPaymentGateway premiumPaymentGateway;
        IExpensivePaymentGateway expensivePaymentGateway;
        public ProcessPaymentService(ICheapPaymentGateway cheapPaymentGateway, IPremiumPaymentGateway premiumPaymentGateway,
            IExpensivePaymentGateway expensivePaymentGateway)
        {
            service = new ProcessPayment();
            this.expensivePaymentGateway = expensivePaymentGateway;
            this.cheapPaymentGateway = cheapPaymentGateway;
            this.premiumPaymentGateway = premiumPaymentGateway;
        }
        private bool isExpensivePaymentAvailable()
        {
            // this is just a simple and assumed logic to determine if Expensive payment gateway is available
            var random = new Random();
            var next = random.Next(1, 99);

            return next % 2 == 0;
        }
        public string MakePayment(PaymentDTOs dto)
        {
            if (dto.Amount < 20)
            {
                var result = service.MakePayment(cheapPaymentGateway, dto);
                if (result)
                {
                    return PaymentStates.SUCCESS;
                }

                return PaymentStates.FAILED;
            }

            if (dto.Amount >= 21 && dto.Amount <= 500)
            {
                bool isExpensivePaymentAvaiable = isExpensivePaymentAvailable();

                if (isExpensivePaymentAvaiable)
                {
                    bool result = service.MakePayment(expensivePaymentGateway, dto);
                    if (result)
                    {
                        return PaymentStates.SUCCESS;
                    }

                    result = service.MakePayment(cheapPaymentGateway, dto);
                    if (result)
                    {
                        return PaymentStates.SUCCESS;
                    }

                    return PaymentStates.FAILED;
                }
            }

            if (dto.Amount > 500)
            {
                var result = service.MakePayment(premiumPaymentGateway, dto);

                if(result)
                {
                    return PaymentStates.SUCCESS;
                }

                return PaymentStates.FAILED;
            }

            return PaymentStates.FAILED;
        }
    }
    public interface ICheapPaymentGateway : IProcessPayment
    {
    }

    public interface IPremiumPaymentGateway : IProcessPayment { }

    public class CheapPaymentGateway:ICheapPaymentGateway
    {
        private ProcessPaymentDbContext _db;

        public CheapPaymentGateway()
        {
            _db = new ProcessPaymentDbContext();
        }
        public bool MakePayment(PaymentDTOs dto)
        {
            var requestModel = (Payment)dto;
            _db.payments.Add(requestModel);

            bool status = _db.SaveChanges() > 0;

            if (status)
            {
                _db.paymentState.Add(new PaymentState()
                {
                    state = PaymentStates.SUCCESS,
                    paymentId = requestModel.PaymentId
                });

                return _db.SaveChanges() > 0;
            }

            return false;
        }
    }

    public class PremiumPaymentGateway : IPremiumPaymentGateway
    {
        private ProcessPaymentDbContext _db;

        public PremiumPaymentGateway()
        {
            _db = new ProcessPaymentDbContext();
        }
        public bool MakePayment(PaymentDTOs dto)
        {
            var requestModel = (Payment)dto;
            _db.payments.Add(requestModel);

            bool status = _db.SaveChanges() > 0;

            if (status)
            {
                _db.paymentState.Add(new PaymentState()
                {
                    state = PaymentStates.SUCCESS,
                    paymentId = requestModel.PaymentId
                });

                return _db.SaveChanges() > 0;
            }

            return false;
        }
    }
    public interface IExpensivePaymentGateway:IProcessPayment
    {

    }

    public class ExpensivePaymentGatewaye : IExpensivePaymentGateway
    {
        private ProcessPaymentDbContext _db;

        public ExpensivePaymentGatewaye()
        {
            _db = new ProcessPaymentDbContext();
        }
        public bool MakePayment(PaymentDTOs dto)
        {
            var requestModel = (Payment)dto;
            _db.payments.Add(requestModel);

            bool status =  _db.SaveChanges() > 0;

            if(status)
            {
                _db.paymentState.Add(new PaymentState()
                {
                    state = PaymentStates.SUCCESS,
                    paymentId = requestModel.PaymentId
                });

                return _db.SaveChanges() > 0;
            }

            return false;
        }
    }

    public interface IProcessPayment
    {
        public bool MakePayment(PaymentDTOs dto);
    }


    public class ProcessPayment
    {
       
        public bool MakePayment(IProcessPayment _payment, PaymentDTOs dto)
        {
            return _payment.MakePayment(dto);
        }
    }
}
