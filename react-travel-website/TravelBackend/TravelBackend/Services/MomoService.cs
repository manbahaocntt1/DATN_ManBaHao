using System.Text;
using TravelBackend.Models.DTOs;
using Newtonsoft.Json;

namespace TravelBackend.Services
{
    public class MomoService : IMomoService
    {
        private readonly IConfiguration _config;
        public MomoService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<MomoCreateResponseDto> CreateMomoQrPaymentAsync(MomoCreateRequest req)
        {
            // Load credentials from config
            string partnerCode = _config["Momo:PartnerCode"];
            string accessKey = _config["Momo:AccessKey"];
            string secretKey = _config["Momo:SecretKey"];
            string endpoint = _config["Momo:QrEndpoint"]; // Sandbox or production

            var payload = new
            {
                partnerCode,
                accessKey,
                requestId = req.OrderId,
                amount = req.Amount.ToString("0"),
                orderId = req.OrderId,
                orderInfo = req.OrderInfo,
                redirectUrl = req.RedirectUrl,
                ipnUrl = req.IpnUrl,
                requestType = "captureWallet",
                extraData = "",
                lang = "vi"
            };

            // Sign the payload (per Momo docs)
            string rawHash = $"accessKey={accessKey}&amount={payload.amount}&extraData=&ipnUrl={req.IpnUrl}&orderId={req.OrderId}&orderInfo={req.OrderInfo}&partnerCode={partnerCode}&redirectUrl={req.RedirectUrl}&requestId={req.OrderId}&requestType=captureWallet";
            string signature = SignWithHmacSHA256(rawHash, secretKey);

            var requestObj = new
            {
                partnerCode,
                accessKey,
                requestId = req.OrderId,
                amount = payload.amount,
                orderId = req.OrderId,
                orderInfo = req.OrderInfo,
                redirectUrl = req.RedirectUrl,
                ipnUrl = req.IpnUrl,
                requestType = "captureWallet",
                extraData = "",
                lang = "vi",
                signature
            };

            using var http = new HttpClient();
            var httpContent = new StringContent(JsonConvert.SerializeObject(requestObj), Encoding.UTF8, "application/json");
            var response = await http.PostAsync(endpoint, httpContent);
            string responseJson = await response.Content.ReadAsStringAsync();

            dynamic momoRes = JsonConvert.DeserializeObject(responseJson);
            return new MomoCreateResponseDto
            {
                PayUrl = momoRes?.payUrl,
                QrCodeUrl = momoRes?.qrCodeUrl,
                Message = momoRes?.message
                
            };
        }

        private string SignWithHmacSHA256(string data, string key)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA256(Encoding.UTF8.GetBytes(key));
            var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }

}
