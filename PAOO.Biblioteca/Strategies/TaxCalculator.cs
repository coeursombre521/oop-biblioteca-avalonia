namespace PAOO.Biblioteca
{
    public class TaxCalculator
    {
        private ICalculateTaxStrategy _strategy;
        public double BasePrice { get; set; }

        public TaxCalculator(ICalculateTaxStrategy strategy, double basePrice)
        {
            _strategy = strategy;
            BasePrice = basePrice;
        }

        public void SetStrategy(ICalculateTaxStrategy strategy)
        {
            _strategy = strategy;
        }

        public double CalculateTax(int daysOverdue) => _strategy.CalculateTax(BasePrice, daysOverdue);
    }
}

