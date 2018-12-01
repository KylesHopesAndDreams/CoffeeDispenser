namespace CoffeeCore
{
    /// <summary>
    /// Implements Sugar as an addon to coffee.
    /// </summary>
    public sealed class Sugar : Addon
    {
        #region Data Members
        private ushort _quantity;
        private ushort _maxQuantity;
        private decimal _unitCost;
        #endregion

        #region Properties
        public override ushort Quantity => _quantity;
        public override ushort MaxQuantity => _maxQuantity;
        public override decimal UnitCost => _unitCost;
        #endregion

        public Sugar() : base()
        {
            _quantity = 0;
            _maxQuantity = 3;
            _unitCost = 0.25m;
        }

        /// <summary>
        /// Implements <seealso cref="Addon.AddQuantity(ushort)"/>
        /// </summary>
        public override void AddQuantity(ushort qtyIn = 1)
        {
            _quantity = (_maxQuantity > _quantity + qtyIn) ? (ushort)(_quantity + qtyIn) : _maxQuantity;
        }

        /// <summary>
        /// Implements <seealso cref="Addon.RemoveQuantity(ushort)"/>
        /// </summary>
        public override void RemoveQuantity(ushort qtyIn = 1)
        {
            _quantity = (_quantity - qtyIn >= 0) ? (ushort)(_quantity - qtyIn) : (ushort)0;
        }

    }
}
