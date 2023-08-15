using System.Threading.Channels;

namespace PotterKata2;

public class PotterKataTests
{
    private static Cart _cart;
    
    public PotterKataTests()
    {
        _cart = new Cart();
    }
    
    [Fact]
    public void NoBooksCostZero()
    {
        var expected = 0;

        AssertExpectedCosts(expected);
    }
    
    [Fact]
    public void OneBooksCost8Pounds()
    {
        var expected = 8.00;

        _cart.AddBook(PotterBooks.First);
        
        AssertExpectedCosts(expected);
    }

    private void AssertExpectedCosts(double expected)
    {
        var actual = _cart.GetTotal();
        Assert.Equal(expected, actual);
    }
}

public enum PotterBooks
{
    First
}

public class Cart
{
    private List<PotterBooks> _books = new();
    
    public double GetTotal()
    {
        return (_books.Count() == 1) ? 8.00 : 0;
    }

    public void AddBook(PotterBooks book)
    {
        _books.Add(book);
    }
}