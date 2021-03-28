using PortfolioSiteExample.Shared.Enums;
using System.Collections.Generic;

namespace PortfolioSiteExample.Shared.Requests
{
    public class AnswerRequest
    {
        public AnswerRequest()
        {
            this.Question = new List<Question>();
        }

        public List<Question> Question { get; set; }
    }
}
