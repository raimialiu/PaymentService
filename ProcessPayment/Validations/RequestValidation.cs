using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPayment.Validations
{
    public class RequestValidation
    {
    }

    public class MustNotBeInPast : Attribute, IModelValidator
    {
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            var objectToValidate = context?.Model;

            var isValid = DateTime.TryParse(objectToValidate.ToString(), out var result);

            if(isValid)
            {
                bool isPast = DateTime.Now > result;

                if(isPast)
                {
                    return new List<ModelValidationResult>()
                       {
                            new ModelValidationResult("", "invalid datetime passed")
                        };
                }

                return Enumerable.Empty<ModelValidationResult>();
            }

            return new List<ModelValidationResult>()
            {
                new ModelValidationResult("", "invalid datetime passed")
            };
        }
    }
}
