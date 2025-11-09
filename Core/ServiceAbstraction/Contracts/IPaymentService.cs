using Shared.DTOs.BasketModule;
using System.Globalization;

namespace ServiceAbstraction.Contracts
{
    public interface IPaymentService
    {
        Task<BasketDTO> CreateOrUpdatePaymentIntentAsync(string basketId);
        Task UpdatePaymentStatusAsync(string json, string signatureHeader);
    }
}
