using System.Collections.Generic;
using System.Linq;
using System;
using CoffeeCore;

namespace VendorCore
{
    public class CoffeeVendor : ICoffeeVendor
    {
        private IList<Coffee> _coffeeOrders;
        private decimal _balanceDue;
        private decimal _tenderPaid;
        
        /// <summary>
        /// Provides Coffee Orders in transaction
        /// </summary>
        public IList<Coffee> CoffeeOrders => _coffeeOrders;

        /// <summary>
        /// Provides total balance due. If negative, amount is change due.
        /// </summary>
        public decimal BalanceDue => _balanceDue;

        /// <summary>
        /// Provides total payment added
        /// </summary>
        public decimal TenderPaid => _tenderPaid;

        public CoffeeVendor()
        {
            _coffeeOrders = new List<Coffee>();
            _balanceDue = 0.00m;
            _tenderPaid = 0.00m;
        }

        /// <summary>
        /// Implements <seealso cref="ICoffeeVendor.AddCoffeeOrder(Coffee)"/>
        /// </summary>
        public void AddCoffeeOrder(Coffee order)
        {
            _coffeeOrders.Add(order);
            _balanceDue += order.GetOrderCost();
        }

        /// <summary>
        /// Implements <seealso cref="ICoffeeVendor.ClearOrders"/>
        /// </summary>
        public void ClearOrders()
        {
            //Remove each order from the balance
            foreach (Coffee order in _coffeeOrders)
                _balanceDue -= order.GetOrderCost();
            
            //Clear total coffee orders   
            _coffeeOrders.Clear();            
        }

        /// <summary>
        /// Implements <seealso cref="ICoffeeVendor.MakePayment(decimal)"/>
        /// </summary>
        public void MakePayment(decimal paymentIn)
        {
            //Throw error if it is an invalid payment
            if (!AcceptedPayments.IsAcceptableFormOfPayment(paymentIn))
                throw new ArgumentException("Invalid form of payment not accepted.");

            //Otherwise, pay twoards the balance and add the payment to the tender paid total.
            _balanceDue =_balanceDue - paymentIn;
            _tenderPaid += paymentIn;
        }

        /// <summary>
        /// Implements <seealso cref="ICoffeeVendor.TryDispense(out decimal, out Coffee[])"/>
        /// </summary>
        public Coffee[] Dispense(out decimal change)
        {
            //Throw an error if there is still a balance.
            if (_balanceDue > 0)
                throw new InvalidOperationException(String.Format("{0:C} is still owed on the transaction.", _balanceDue));
            
            //Check if paymentDue is negative. If so, get abs value and send change. Otherwise, no change.
            change = (_balanceDue < 0) ? _balanceDue * -1 : 0;

            //Get the dispensed coffee
            Coffee[] dispensedCoffee = _coffeeOrders.ToArray();

            //Once dispensed and change is set, transaction is complete.
            ClearOrders();
            ClearBalances();

            return dispensedCoffee;            
        }

        /// <summary>
        /// Implements <seealso cref="ICoffeeVendor.EndTransaction"/>
        /// </summary>
        public decimal EndTransaction()
        {
            //Clear any current order and reset cost owed.
            ClearOrders();

            //Determine if change is needed and reset values.
            decimal change = (_balanceDue < 0) ? _balanceDue * -1 : 0;
            ClearBalances();

            //Return overpaid balance as change.
            return change;
        }

        /// <summary>
        /// Resets the balance due and tender to zero
        /// </summary>
        private void ClearBalances()
        {
            _balanceDue = 0;
            _tenderPaid = 0;
        }
    }
}
