namespace CoffeeCore
{
    public interface IAddon
    {
        ushort Quantity { get; }
        ushort MaxQuantity { get; }
        decimal UnitCost { get; }
        
        /// <summary>
        /// Add a value to the quantity. 
        /// Function will check to make sure quantity does not exceed max, and will not add more if max is filled.
        /// </summary>
        /// <param name="qtyIn">Quantity to add</param>
        void AddQuantity(ushort qtyIn = 1);

        /// <summary>
        /// Removes a value from the quantity. 
        /// Function will check that the quantity is not zero, and will not allow a negative quantity.
        /// </summary>
        /// <param name="qtyIn">Quantity to remove</param>
        void RemoveQuantity(ushort qtyIn = 1);

        /// <summary>
        /// Calculates the total cost of the addon by multiplying the quantity and unit cost.
        /// </summary>
        /// <returns>Total cost as a decimal value</returns>
        decimal GetCost();
    }
}
