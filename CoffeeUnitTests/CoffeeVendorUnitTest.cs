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
        ICoffeeVendor _vendingMachine;        
        
        [TestInitialize]
        public void CreateVendingMachine()
        {
            _vendingMachine = new CoffeeVendor();
        }       

        [TestMethod]
        public void AddSmallCoffeeNoExtras()
        {
            CoffeeSize small = new SmallSize();
            Coffee order = new Coffee(small);
            _vendingMachine.AddCoffeeOrder(order);
            Assert.IsTrue(_vendingMachine.BalanceDue == small.Cost, "Actual cost: " + _vendingMachine.BalanceDue);
        }

        [TestMethod]
        public void AddLargeCoffeeTwoSugarOneCreamer()
        {
            CoffeeSize large = new LargeSize();            
            Coffee order = new Coffee(large);
            order.AddedCreamer.AddQuantity(1);
            order.AddedSugar.AddQuantity(2);

            _vendingMachine.AddCoffeeOrder(order);
            Assert.IsTrue(_vendingMachine.BalanceDue == (large.Cost + 0.50m + 0.50m), "Actual cost: " + _vendingMachine.BalanceDue);
        }

        [TestMethod]
        public void PayWithATwenty()
        {
            AddLargeCoffeeTwoSugarOneCreamer();
            _vendingMachine.MakePayment(20.00m);
            Assert.IsTrue(_vendingMachine.BalanceDue == (3.25m - 20.00m), "Actual balance: " + _vendingMachine.BalanceDue);
        }

        [TestMethod]
        public void PayWithANickel()
        {
            AddLargeCoffeeTwoSugarOneCreamer();
            _vendingMachine.MakePayment(0.05m);
            Assert.IsTrue(_vendingMachine.BalanceDue == (3.25m - 0.05m), "Actual balance: " + _vendingMachine.BalanceDue);
        }

        [TestMethod]
        public void BuyCoffeeAndGetChange()
        {
            decimal change = 0;
            PayWithATwenty();
            Coffee[] coffeeOrder = _vendingMachine.Dispense(out change);
            Assert.IsNotNull(coffeeOrder);
            Assert.IsTrue(change == 20.00m - 3.25m);
            Assert.IsTrue(_vendingMachine.BalanceDue == 0.00m);
            Assert.IsTrue(_vendingMachine.TenderPaid == 0.00m);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetErrorForNotEnoughPayment()
        {
            decimal change = 0;
            PayWithANickel();
            Coffee[] temp = _vendingMachine.Dispense(out change);
            Assert.Fail("If you see me, the dispense did not fail as it should have.");
        }

        [TestMethod]
        public void ClearOrdersAfterInsertingMoney()
        {
            PayWithATwenty();
            _vendingMachine.ClearOrders();
            Assert.IsTrue(_vendingMachine.BalanceDue == -20.00m, "Actual: " + _vendingMachine.BalanceDue);
        }

        [TestMethod]
        public void CancelTransaction()
        {
            ClearOrdersAfterInsertingMoney();
            decimal change = _vendingMachine.EndTransaction();
            Assert.IsTrue(_vendingMachine.BalanceDue == 0.00m, "Actual: " + _vendingMachine.BalanceDue);
            Assert.IsTrue(change == 20.00m, "Actual: " + change);
            Assert.IsTrue(_vendingMachine.BalanceDue == 0.00m, "Actual: " + _vendingMachine.BalanceDue);
            Assert.IsTrue(_vendingMachine.TenderPaid == 0.00m, "Actual: " + _vendingMachine.TenderPaid);
        }


        [TestCleanup()]
        public void Cleanup() { }
    }
}
