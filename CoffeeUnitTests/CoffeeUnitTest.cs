using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoffeeCore;

namespace CoffeeCore.UnitTests
{
    /// <summary>
    /// Tests the CoffeeCore assembly; Coffee and CoffeeSize are validated
    /// </summary>
    [TestClass]
    public class CoffeeUnitTest
    {
        Coffee _smallCoffee, _mediumCoffee, _largeCoffee;
        CoffeeSize _small, _medium, _large;

        [TestInitialize]
        public void CreateCoffee()
        {
            _small = new SmallSize();
            _medium = new MediumSize(); 
            _large = new LargeSize();

            _smallCoffee = new Coffee(_small);
            _mediumCoffee = new Coffee(_medium);
            _largeCoffee = new Coffee(_large);
        }

        [TestMethod]
        public void AddCreamer()
        {
            AddCreamer(_smallCoffee);
            AddTooMuchCreamer(_mediumCoffee);
        }        

        [TestMethod]
        public void AddSugar()
        {            
            AddSugar(_smallCoffee);            
            AddTooMuchSugar(_mediumCoffee);
        }
        
        [TestMethod]
        public void RemoveCreamer()
        {
            _smallCoffee.AddedCreamer.AddQuantity(1);
            ushort tooLittleCreamer = (ushort)(_smallCoffee.AddedCreamer.MaxQuantity + 1);
            _smallCoffee.AddedCreamer.RemoveQuantity(tooLittleCreamer);
            Assert.IsTrue(_smallCoffee.AddedCreamer.Quantity == 0, "Quantity is: " + _smallCoffee.AddedCreamer.Quantity);
        }

        [TestMethod]
        public void RemoveSugar()
        {
            _smallCoffee.AddedSugar.AddQuantity(1);
            ushort tooLittleSugar = (ushort)(_smallCoffee.AddedSugar.MaxQuantity + 1);
            _smallCoffee.AddedSugar.RemoveQuantity(tooLittleSugar);
            Assert.IsTrue(_smallCoffee.AddedSugar.Quantity == 0);
        }

        [TestMethod]
        public void SizesAreCorrect()
        {
            Assert.IsTrue(_smallCoffee.Size.Description == "Small", "Actual: " + _smallCoffee.Size.Description);
            Assert.IsTrue(_mediumCoffee.Size.Description == "Medium", "Actual: " + _mediumCoffee.Size.Description);
            Assert.IsTrue(_largeCoffee.Size.Description == "Large", "Actual: " + _largeCoffee.Size.Description);
        }

        [TestMethod]
        public void PricesAreCorrect()
        {
            Assert.IsTrue(_smallCoffee.Size.Cost == 1.75m, "smallCoffee CoffeeSize Cost: " + _smallCoffee.Size.Cost);
            Assert.IsTrue(_mediumCoffee.Size.Cost == 2.00m, "mediumCoffee CoffeeSize Cost: " + _mediumCoffee.Size.Cost);
            Assert.IsTrue(_largeCoffee.Size.Cost == 2.25m, "largeCoffee CoffeeSize Cost: " + _largeCoffee.Size.Cost);

            _smallCoffee.AddedCreamer.AddQuantity(2);
            Assert.IsTrue(_smallCoffee.AddedCreamer.GetCost() == 1.00m, "Creamer cost:" + _smallCoffee.AddedCreamer.GetCost());
            _smallCoffee.AddedSugar.AddQuantity(2);
            Assert.IsTrue(_smallCoffee.AddedSugar.GetCost() == 0.50m, "Sugar cost:" + _smallCoffee.AddedCreamer.GetCost());

        }
                
        [TestCleanup()]
        public void Cleanup()
        {
            _smallCoffee = null;
            _mediumCoffee = null;
            _largeCoffee = null;
        }

        private void AddCreamer(Coffee coffee)
        {
            coffee.AddedCreamer.AddQuantity(1);
            Assert.IsTrue(coffee.AddedCreamer.Quantity == 1, "Incorrect, value is " + coffee.AddedCreamer.Quantity);
        }

        private void AddTooMuchCreamer(Coffee coffee)
        {
            ushort tooMuchCreamer = (ushort)(coffee.AddedCreamer.MaxQuantity + 1);
            coffee.AddedCreamer.AddQuantity(tooMuchCreamer);
            Assert.IsTrue(coffee.AddedCreamer.Quantity == coffee.AddedCreamer.MaxQuantity, "Incorrect, value is " + coffee.AddedCreamer.Quantity);
            Assert.IsFalse(coffee.AddedCreamer.Quantity == tooMuchCreamer);
        }

        private void AddSugar(Coffee coffee)
        {
            coffee.AddedSugar.AddQuantity(1);
            Assert.IsTrue(coffee.AddedSugar.Quantity == 1);
        }

        private void AddTooMuchSugar(Coffee coffee)
        {
            ushort tooMuchSugar = (ushort)(coffee.AddedSugar.MaxQuantity + 1);
            coffee.AddedSugar.AddQuantity(tooMuchSugar);
            Assert.IsTrue(coffee.AddedSugar.Quantity == coffee.AddedSugar.MaxQuantity);
            Assert.IsFalse(coffee.AddedSugar.Quantity == tooMuchSugar);
        }
    }
}
