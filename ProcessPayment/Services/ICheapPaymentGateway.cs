using ProcessPayment.DTOs;
using ProcessPayment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPayment.Services
{
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


    public class ProcessPayment : IProcessPayment
    {
        private ProcessPaymentDbContext _ctx;
        private IProcessPayment _payment;

        private ProcessPayment instance;

        private object lockArea = new object();
        public ProcessPayment createInstance(IProcessPayment payment)
        {
            lock(lockArea)
            {
                if (instance == null)
                    return new ProcessPayment(payment);
            }
            
            return instance;
        }
        private ProcessPayment(IProcessPayment pay)
        {
            _ctx = new ProcessPaymentDbContext();
            this._payment = pay;
        }
        public bool MakePayment(PaymentDTOs dto)
        {
            return _payment.MakePayment(dto);
        }
    }
}
