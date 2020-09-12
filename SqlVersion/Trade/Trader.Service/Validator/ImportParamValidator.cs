using FluentValidation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trader.Service.Param;

namespace Trader.Service.Validator
{
    public class ImportParamValidator : AbstractValidator<ImportParam>
    {
        public ImportParamValidator()
        {
            RuleFor(a => a.BasePath).NotEmpty().NotNull();

            RuleFor(a => a.FileName).NotEmpty().NotNull();

            RuleFor(a => a).Custom((a, context) => {

                var path = Path.Combine(a.BasePath, a.FileName);

                if (!File.Exists(path))
                {
                    context.AddFailure($"Arquivo({path}) não encontrado!");
                }
            });
        }
    }
}
