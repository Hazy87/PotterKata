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
    
    [Fact]
    public void TwoBooksCostGet5PercentDiscount()
    {
        var expected = 15.20;

        _cart.AddBook(PotterBooks.First);
        _cart.AddBook(PotterBooks.Second);
        
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
    First,
    Second
}

public class Cart
{
    private Dictionary<PotterBooks, int> _books = new();
    private const double SINGLE_BOOK_PRICE = 8.00;

    public double GetTotal()
    {
        var numberOfDistinctBooks = _books.Distinct().Count();

        if (numberOfDistinctBooks == 2)
        {
            return (2 * SINGLE_BOOK_PRICE) * 0.95;
        }
        
        return _books.Values.Sum() * SINGLE_BOOK_PRICE;
    }

    public void AddBook(PotterBooks book)
    {
        if (_books.ContainsKey(book))
        {
            _books[book] += 1;
        }
        else
        {
            _books.Add(book, 1);
        }
    }
}