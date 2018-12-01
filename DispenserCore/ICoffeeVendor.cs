using System.Collections.Generic;
using CoffeeCore;

namespace VendorCore
{
    public interface ICoffeeVendor
    {
        /// <summary>
        /// Gets amount user has paid
        /// </summary>
        decimal TenderPaid { get; }

        /// <summary>
        /// Gets total transaction balance
        /// </summary>
        decimal BalanceDue { get; }

        /// <summary>
        /// Retrieves the list of current coffee orders. Is Read-Only
        /// </summary>
        IList<Coffee> CoffeeOrders { get; }


        /// <summary>
        /// Adds a coffee order to the total transaction
        /// </summary>
        /// <param name="order">Coffee to add to the transaction</param>
        void AddCoffeeOrder(Coffee coffeeIn);

        /// <summary>
        /// Clears the orders in the transaction, and resets the balance due.
        /// </summary>
        void ClearOrders();

        /// <summary>
        /// Validates that the payment in is a valid form of payment, and proceeds to deduct from the payment due.
        /// </summary>
        /// <param name="paymentIn">Money in to pay twoards the due</param>
        /// <exception cref="ArgumentException">Thrown if payment is not of AcceptedPayments values.</exception>
        void MakePayment(decimal paymentIn);

        /// <summary>
        /// Attempts to dispense the coffee if the balance is paid.
        /// </summary>
        /// <param name="change">Negative balances are paid back as change</param>
        /// <returns>Coffee orders to be dispensed</returns>
        /// <exception cref="InvalidOperationException">Thrown if payment is still due</exception>
        Coffee[] Dispense(out decimal change);

        /// <summary>
        /// Ends the transaction, clearing the order 
        /// </summary>
        /// <returns>Any change remaining</returns>
        decimal EndTransaction();
    }
}
