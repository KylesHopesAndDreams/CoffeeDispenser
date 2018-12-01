namespace CoffeeCore
{
    /**
     * These classes provide sizes of coffee available.
     * All classes implement a description and a cost.
     * These values are set in the constructors. 
     * **/

    public abstract class CoffeeSize
    {
        public abstract string Description { get; }
        public abstract decimal Cost { get; }
    }
        
    public sealed class SmallSize : CoffeeSize
    {
        private string _description;
        private decimal _cost;

        public override string Description => _description;
        public override decimal Cost => _cost;

        public SmallSize() : base()
        {
            _description = "Small";
            _cost = 1.75m;
        }
    }

    public sealed class MediumSize : CoffeeSize
    {
        private string _description;
        private decimal _cost;

        public override string Description => _description;
        public override decimal Cost => _cost;

        public MediumSize() : base()
        {
            _description = "Medium";
            _cost = 2.00m;
        }
    }

    public sealed class LargeSize : CoffeeSize
    {
        private string _description;
        private decimal _cost;

        public override string Description => _description;
        public override decimal Cost => _cost;

        public LargeSize() : base()
        {
            _description = "Large";
            _cost = 2.25m;
        }

    }
}
