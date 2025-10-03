
namespace Ordering.Domain.ValueObjects
{
    public record Payment
    {
        public string? CardName { get; } = default!;
        public string CardNumber { get; } = default!;
        public string Expiration { get; } = default!;
        public string CVV { get; } = default!;
        public int PaymetnMethod { get; } = default!;

        protected Payment() { }
        private Payment(string? cardName, string cardNumber, string expiration, string cvv, int paymetnMethod)
        {
            CardName = cardName;
            CardNumber = cardNumber;
            Expiration = expiration;
            CVV = cvv;
            PaymetnMethod = paymetnMethod;
        }
        public static Payment Of(string? cardName, string cardNumber, string expiration, string cvv, int paymetnMethod)
        {
            ArgumentException.ThrowIfNullOrEmpty(cardNumber);
            ArgumentException.ThrowIfNullOrEmpty(expiration);
            ArgumentException.ThrowIfNullOrEmpty(cvv);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(cvv.Length, 3);
            return new Payment(cardName, cardNumber, expiration, cvv, paymetnMethod);
        }
    }
}
