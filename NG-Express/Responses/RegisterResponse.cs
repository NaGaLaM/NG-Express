using NG_Express.Models;

namespace NG_Express.Responses
{
    public class RegisterResponse
    {
        public Buyer buyer { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
    }
}
