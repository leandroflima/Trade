using System.Collections.Generic;

namespace Trader.Service.Result
{
    public abstract class BaseResult
    {
        protected BaseResult()
        {
            ErrorMessages = new List<string>();
        }

        public bool IsValid {
            get
            {
                return ErrorMessages.Count > 0;
            }
        }

        public List<string> ErrorMessages { get; set; }
    }
}
