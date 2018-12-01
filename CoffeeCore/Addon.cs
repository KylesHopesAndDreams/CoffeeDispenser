namespace CoffeeCore
{
    public abstract class Addon
    {
        public Addon()
        { }

        public abstract ushort Quantity { get; }
        public abstract ushort MaxQuantity { get; }
        public abstract decimal UnitCost { get; }
        
        /// <summary>
        /// Add a value to the quantity. 
        /// Function will check to make sure quantity does not exceed max, and will not add more if max is filled.
        /// </summary>
        /// <param name="qtyIn">Quantity to add</param>
        public abstract void AddQuantity(ushort qtyIn = 1);

        /// <summary>
        /// Removes a value from the quantity. 
        /// Function will check that the quantity is not zero, and will not allow a negative quantity.
        /// </summary>
        /// <param name="qtyIn">Quantity to remove</param>
        public abstract void RemoveQuantity(ushort qtyIn = 1);

        /// <summary>
        /// Calculates the total cost of the addon by multiplying the quantity and unit cost.
        /// </summary>
        /// <returns>Total cost as a decimal value</returns>
        public decimal GetCost()
        {
            return Quantity * UnitCost;
        }
    }
}
