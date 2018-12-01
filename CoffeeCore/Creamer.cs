namespace CoffeeCore
{
    /// <summary>
    /// Implements Creamer as an addon to a coffee.
    /// </summary>
    public sealed class Creamer : Addon
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

       
        public Creamer() : base()
        {
            _quantity = 0;
            _maxQuantity = 3;
            _unitCost = 0.50m;
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
            _quantity = (_quantity - qtyIn >= 0) ? (ushort)(_quantity - qtyIn) : (ushort) 0;
        }


    }
}
