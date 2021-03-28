using PortfolioSiteExample.Shared.Enums;
using System.Collections.Generic;

namespace PortfolioSiteExample.Shared.Responses
{
    public class AnswerResponse
    {
        public AnswerResponse()
        {
            this.Answer = new Dictionary<Question, string>();
        }

        public Dictionary<Question, string> Answer { get; set; }
    }
}
