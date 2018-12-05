using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendorCore;
using CoffeeCore;

namespace VendorCore.UnitTests
{
    /// <summary>
    // Tests the VendorCore assembly; CoffeeVendor and AcceptablePayments are tested here
    /// </summary>
    [TestClass]
    public class CoffeeVendorUnitTest
    {
        private const decimal TwentyDollars = 20.00m;
        private const decimal FiveCents = 0.05m;
        private const decimal MedCoffee_3C1S_Cost = 2.00m + 1.5m + 0.25m;
        private const decimal TenDollars = 10.00m;
        ICoffeeVendor _vendingMachine;        
        
        [TestInitialize]
        public void CreateVendingMachine()
        {
            _vendingMachine = new CoffeeVendor();
        }       

        [TestMethod]
        public void AddSmallCoffeeNoExtras()
        {
            ICoffeeSize small = new SmallSize();
            Coffee order = new Coffee(small);
            _vendingMachine.AddCoffeeOrder(order);
            Assert.AreEqual(_vendingMachine.BalanceDue, small.Cost, "Actual cost: " + _vendingMachine.BalanceDue);
        }

        [TestMethod]
        public void AddLargeCoffeeTwoSugarOneCreamer()
        {
            decimal expectedCost = 2.25m + 0.50m + 0.50m;
            ICoffeeSize large = new LargeSize();
            Coffee order = new Coffee(large);
            order.AddedCreamer.AddQuantity(1);
            order.AddedSugar.AddQuantity(2);

            _vendingMachine.AddCoffeeOrder(order);
            Assert.AreEqual(_vendingMachine.BalanceDue, expectedCost, "Actual cost: " + _vendingMachine.BalanceDue);
        }

        [TestMethod]
        public void PayWithATwenty()
        {
            ICoffeeSize medium = new MediumSize();
            Coffee order = new Coffee(medium);
            order.AddedCreamer.AddQuantity(3);
            order.AddedSugar.AddQuantity(1);

            _vendingMachine.AddCoffeeOrder(order);
            _vendingMachine.MakePayment(TwentyDollars);
            Assert.AreEqual(_vendingMachine.BalanceDue, MedCoffee_3C1S_Cost - TwentyDollars, "Actual balance: " + _vendingMachine.BalanceDue);
        }

        [TestMethod]
        public void PayWithANickel()
        {
            ICoffeeSize medium = new MediumSize();
            Coffee order = new Coffee(medium);
            order.AddedCreamer.AddQuantity(3);
            order.AddedSugar.AddQuantity(1);

            _vendingMachine.AddCoffeeOrder(order);
            _vendingMachine.MakePayment(FiveCents);
            Assert.AreEqual(_vendingMachine.BalanceDue, MedCoffee_3C1S_Cost - FiveCents, "Actual balance: " + _vendingMachine.BalanceDue);
        }

        [TestMethod]
        public void BuyCoffeeAndGetChange()
        {
            decimal change = 0;
            ICoffeeSize medium = new MediumSize();
            Coffee order = new Coffee(medium);
            order.AddedCreamer.AddQuantity(3);
            order.AddedSugar.AddQuantity(1);

            _vendingMachine.AddCoffeeOrder(order);
            _vendingMachine.MakePayment(TwentyDollars);
            Coffee[] coffeeOrder = _vendingMachine.Dispense(out change);
            Assert.IsNotNull(coffeeOrder);
            Assert.AreEqual(change, TwentyDollars - MedCoffee_3C1S_Cost, "Change value did not match expected" );
            Assert.AreEqual(expected: _vendingMachine.BalanceDue, actual: 0.00m, message: "Balance due value did not match expected");
            Assert.AreEqual(expected: _vendingMachine.TenderPaid, actual: 0.00m, message: "Tender paid value did not match expected");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetErrorForNotEnoughPayment()
        {
            decimal change = 0;
            ICoffeeSize medium = new MediumSize();
            Coffee order = new Coffee(medium);
            _vendingMachine.AddCoffeeOrder(order);
            _vendingMachine.MakePayment(FiveCents);
            Coffee[] temp = _vendingMachine.Dispense(out change);

            Assert.Fail("If you see me, the dispense did not fail as it should have.");
        }

        [TestMethod]
        public void ClearOrdersAfterInsertingMoney()
        {
            ICoffeeSize large = new LargeSize();
            Coffee order = new Coffee(large);
            order.AddedCreamer.AddQuantity(3);
            order.AddedSugar.AddQuantity(3);

            _vendingMachine.AddCoffeeOrder(order);
            _vendingMachine.MakePayment(TwentyDollars);
            _vendingMachine.MakePayment(TwentyDollars);
            _vendingMachine.ClearOrders();
            Assert.AreEqual(expected: _vendingMachine.BalanceDue, actual: -40.00m, message: "Actual balance: " + _vendingMachine.BalanceDue);
            Assert.IsNotNull(value: _vendingMachine.CoffeeOrders);
            Assert.AreEqual(expected: _vendingMachine.CoffeeOrders.Count, actual: 0, message: "Actual orders: " + _vendingMachine.CoffeeOrders);
        }

        [TestMethod]
        public void CancelTransaction()
        {
            ICoffeeSize large = new LargeSize();
            Coffee order = new Coffee(large);
            order.AddedCreamer.AddQuantity(3);
            order.AddedSugar.AddQuantity(3);

            _vendingMachine.AddCoffeeOrder(order);
            _vendingMachine.MakePayment(paymentIn: TenDollars);
            decimal change = _vendingMachine.EndTransaction();
            Assert.AreEqual(change, actual: TenDollars, message: "Actual change: " + change);
            Assert.AreEqual(expected: _vendingMachine.BalanceDue, actual: 0.00m, message: "Actual balance: " + _vendingMachine.BalanceDue);
            Assert.AreEqual(expected: _vendingMachine.TenderPaid, actual: 0.00m, message: "Actual tender: " + _vendingMachine.TenderPaid);
        }


        [TestCleanup()]
        public void Cleanup() { }
    }
}
