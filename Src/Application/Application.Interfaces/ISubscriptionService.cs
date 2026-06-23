using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Application.Interfaces
{
    public interface ISubscriptionService
    {
        Task CheckExpiredSubscriptions();
    }
}
