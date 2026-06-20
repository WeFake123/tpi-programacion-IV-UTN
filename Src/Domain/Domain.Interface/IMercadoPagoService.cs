using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interface
{
    public interface IMercadoPagoService
    {
        Task<string> CreatePreference(Plan plan, Guid userId);

        Task ProcessPayment(string paymentId);

    }
}
