namespace PAOO.Biblioteca
{
    public class CalculateTaxStrategy : ICalculateTaxStrategy
    {
        private static CalculateTaxStrategy? _instance;

        private CalculateTaxStrategy() {}

        public static CalculateTaxStrategy GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CalculateTaxStrategy();
            }
            return _instance;
        }

        public double CalculateTax(double price, int daysOverdue)
        {
            return price * daysOverdue;
        }
    }
}

