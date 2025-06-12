using TravelBackend.Models.DTOs;

namespace TravelBackend.Services
{
    public interface IPaymentService
    {
        Task<MomoCreateResponseDto> CreateMomoPaymentAsync(CreateMomoPaymentDto dto);
        Task HandleMomoIpnAsync(MomoIpnDto ipnDto);
    }


}
