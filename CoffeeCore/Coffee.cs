namespace CoffeeCore
{
    /// <summary>
    /// Class that creates a cup of Jo'. Requires a size to be initialized first.
    /// </summary>
    public class Coffee
    {
        #region Data Members
        private CoffeeSize _size;
        private Sugar _sugar;
        private Creamer _creamer;
        #endregion

        #region Properties
        public CoffeeSize Size => _size;
        public Sugar AddedSugar => _sugar;
        public Creamer AddedCreamer => _creamer;
        #endregion

        /// <summary>
        /// Creates a cup of coffee, with the size preset.
        /// </summary>
        public Coffee(CoffeeSize sizeIn)
        {
            _size = sizeIn;
            _sugar = new Sugar();
            _creamer = new Creamer();
        }

        /// <summary>
        /// Helper function to determine the cost of an order
        /// </summary>
        /// <returns>Cost of order</returns>
        public decimal GetOrderCost()
        {
            decimal cost = Size.Cost + AddedCreamer.GetCost() + AddedSugar.GetCost();
            return cost;
        }

    }
}
