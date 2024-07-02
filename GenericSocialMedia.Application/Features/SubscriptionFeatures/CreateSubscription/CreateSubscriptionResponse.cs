using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericSocialMedia.Application.Features.SubscriptionFeatures.CreateSubscription
{
    public sealed record CreateSubscriptionResponse
    {
        public string Name { get; set; }
    }
}
