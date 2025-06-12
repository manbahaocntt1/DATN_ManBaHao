using TravelBackend.Entities;
using TravelBackend.Models.DTOs;
using TravelBackend.Repositories;
using TravelBackend.Services;

public class PaymentService : IPaymentService
{
    private readonly ITourBookingRepository _bookingRepo;
    private readonly ITransactionRepository _transRepo;
    private readonly IMomoService _momoService;

    public PaymentService(
        ITourBookingRepository bookingRepo,
        ITransactionRepository transRepo,
        IMomoService momoService)
    {
        _bookingRepo = bookingRepo;
        _transRepo = transRepo;
        _momoService = momoService;
    }

    public async Task<MomoCreateResponseDto> CreateMomoPaymentAsync(CreateMomoPaymentDto dto)
    {
        // 1. Save booking (status: pending)
        var booking = new TourBooking
        {
            UserId = dto.UserId,
            TourId = dto.TourId,
            TourDate = DateTime.Parse(dto.TourDate),
            Adults = dto.Adults,
            Children = dto.Children,
            Note = dto.Note,
            TotalPrice = dto.Amount,
            PaymentMethod = "momo",
            PaymentStatus = "pending",
            CreatedAt = DateTime.UtcNow,
        };
        await _bookingRepo.AddAsync(booking);
        await _bookingRepo.SaveChangesAsync();

        // 2. Generate unique orderId, request Momo payUrl
        string orderId = $"BOOK{booking.BookingId}-{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}";
        var momoRequest = new MomoCreateRequest
        {
            OrderId = orderId,
            Amount = dto.Amount,
            OrderInfo = dto.OrderInfo,
            RedirectUrl = "	http://localhost:3000/payment/success", // Not used for QR, but required
            IpnUrl = "https://xxxxxx.ngrok.io/api/payment/momo-ipn"
        };
        var momoResponse = await _momoService.CreateMomoQrPaymentAsync(momoRequest);

        // 3. Save transaction (status: pending)
        var transaction = new Transaction
        {
            BookingId = booking.BookingId,
            Amount = dto.Amount,
            Currency = "VND",
            PaymentMethod = "momo",
            PaymentStatus = "pending",
            TransactionRef = orderId,
            CreatedAt = DateTime.UtcNow,
            
        };
        await _transRepo.AddAsync(transaction);
        await _transRepo.SaveChangesAsync();

        // 4. Return payUrl for frontend to show QR
        return new MomoCreateResponseDto
        {
            PayUrl = momoResponse.PayUrl,
            QrCodeUrl = momoResponse.QrCodeUrl,
            Message = momoResponse.Message
        };
    }

    public async Task HandleMomoIpnAsync(MomoIpnDto ipnDto)
    {
        // 1. Find transaction by ipnDto.OrderId (TransactionRef) or BookingId
        var transaction = await _transRepo.GetByTransactionRefAsync(ipnDto.OrderId);
        if (transaction == null) return;

        // 2. Update transaction/payment status
        transaction.PaymentStatus = ipnDto.ResultCode == "0" ? "success" : "failed";
        transaction.ResponseCode = ipnDto.ResultCode;
        transaction.ResponseMessage = ipnDto.Message;
        transaction.RawResponse = ipnDto.RawJson;
        transaction.UpdatedAt = DateTime.UtcNow;
        _transRepo.Update(transaction);
        await _transRepo.SaveChangesAsync();

        // 3. Update booking status
        var booking = await _bookingRepo.GetByIdAsync(transaction.BookingId);
        if (booking != null)
        {
            booking.PaymentStatus = transaction.PaymentStatus;
            _bookingRepo.Update(booking);
            await _bookingRepo.SaveChangesAsync();
        }
    }
}
