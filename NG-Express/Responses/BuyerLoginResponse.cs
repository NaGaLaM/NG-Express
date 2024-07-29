using NG_Express.Models;
namespace NG_Express.Responses
{
    public class BuyerLoginResponse
    {
        public Buyer buyer { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
    }
    public class SellerLoginResponse 
    {
        public Seller Seller { get; set; }
        public string Message { get; set; } 
        public int Status { get; set; }
    }

}
