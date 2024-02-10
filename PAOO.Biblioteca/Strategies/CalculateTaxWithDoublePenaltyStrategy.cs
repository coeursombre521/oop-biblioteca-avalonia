namespace PAOO.Biblioteca
{
    public class CalculateTaxWithDoublePenaltyStrategy : ICalculateTaxStrategy
    {
        private static CalculateTaxWithDoublePenaltyStrategy? _instance;

        private CalculateTaxWithDoublePenaltyStrategy() {}

        public static CalculateTaxWithDoublePenaltyStrategy GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CalculateTaxWithDoublePenaltyStrategy();
            }
            return _instance;
        }

        public double CalculateTax(double price, int daysOverdue)
        {
            return price * daysOverdue * 2;
        }
    }
}

