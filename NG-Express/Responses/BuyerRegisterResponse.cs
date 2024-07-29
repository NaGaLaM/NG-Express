using NG_Express.Models;

namespace NG_Express.Responses
{
    public class BuyerRegisterResponse
    {
        public Buyer buyer { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
    }
    public class SellerRegisterResponse
    {
        public Seller Seller { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
    }
}
