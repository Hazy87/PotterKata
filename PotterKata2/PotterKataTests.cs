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
    
    [Fact]
    public void TwoDuplicateBooksCost16Pounds()
    {
        var expected = 16.00;

        _cart.AddBook(PotterBooks.First);
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
        if (!_books.Any()) return 0;

        return _books.Count() * 8.00;
    }

    public void AddBook(PotterBooks book)
    {
        _books.Add(book);
    }
}