namespace CoffeeCore
{
    /// <summary>
    /// Implements Sugar as an addon to coffee.
    /// </summary>
    public sealed class Sugar : IAddon
    {
        #region Data Members
        private ushort _quantity;
        private ushort _maxQuantity;
        private decimal _unitCost;
        #endregion

        #region Properties
        public ushort Quantity => _quantity;
        public ushort MaxQuantity => _maxQuantity;
        public decimal UnitCost => _unitCost;
        #endregion

        public Sugar() : base()
        {
            _quantity = 0;
            _maxQuantity = 3;
            _unitCost = 0.25m;
        }

        /// <summary>
        /// Implements <seealso cref="IAddon.AddQuantity(ushort)"/>
        /// </summary>
        public void AddQuantity(ushort qtyIn = 1)
        {
            _quantity = (_maxQuantity > _quantity + qtyIn) ? (ushort)(_quantity + qtyIn) : _maxQuantity;
        }

        /// <summary>
        /// Implements <seealso cref="IAddon.RemoveQuantity(ushort)"/>
        /// </summary>
        public void RemoveQuantity(ushort qtyIn = 1)
        {
            _quantity = (_quantity - qtyIn >= 0) ? (ushort)(_quantity - qtyIn) : (ushort)0;
        }

        /// <summary>
        /// Implements <seealso cref="IAddon.GetCost"/>
        /// </summary>
        /// <returns></returns>
        public decimal GetCost()
        {
            return Quantity * UnitCost;
        }

    }
}
