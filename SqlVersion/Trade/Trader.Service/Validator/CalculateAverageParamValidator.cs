using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trader.Service.Param;

namespace Trader.Service.Validator
{
    public class CalculateAverageParamValidator : AbstractValidator<CalculateAverageParam>
    {
        public CalculateAverageParamValidator()
        {
            RuleFor(a => a.InitialDate).GreaterThan(new DateTime(1900,1,1)).WithMessage("Data inicial deve ser maior que 1/1/1900");

            RuleFor(a => a.FinalDate).GreaterThan(new DateTime(1900, 1, 1)).WithMessage("Data inicial deve ser maior que 1/1/1900");
        }
    }
}
