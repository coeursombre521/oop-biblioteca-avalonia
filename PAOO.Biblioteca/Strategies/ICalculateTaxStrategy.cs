
namespace PAOO.Biblioteca
{
    public interface ICalculateTaxStrategy
    {
        double CalculateTax(double price, int daysOverdue);
    }
}

