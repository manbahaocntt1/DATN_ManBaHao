using TravelBackend.Models.DTOs;

namespace TravelBackend.Services
{
    public interface IMomoService
    {
        Task<MomoCreateResponseDto> CreateMomoQrPaymentAsync(MomoCreateRequest req);
    }

}
