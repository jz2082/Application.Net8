namespace StockApp.Net6.Utilitis
{
    public interface IAbstractFactory<T>
    {
        T Create();
    }
}