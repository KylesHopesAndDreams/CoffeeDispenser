using System.Linq;

namespace VendorCore
{
    public class AcceptedPayments
    {    
        private static readonly decimal[] _payments = { 0.05m, 0.10m, 0.25m, 1.00m, 2.00m, 5.00m, 10.00m, 20.00m };
        
        /// <summary>
        /// Allowed payments: 
        /// $0.05, $0.10, $0.25, $1.00, $2.00, $5.00, $10.00, $20.00
        /// </summary>
        public static decimal[] Payments => _payments;

        /// <summary>
        /// Checks if the value is an accepted note of currency.
        /// </summary>
        /// <param name="costIn">Cost paid in</param>
        /// <returns>T/F if costIn is a property of AcceptedPayments</returns>
        public static bool IsAcceptableFormOfPayment(decimal costIn)
        {
            return _payments.Contains(costIn);
        }
    }
}
