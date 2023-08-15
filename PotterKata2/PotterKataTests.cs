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
    private double _total = 0;
    
    public double GetTotal()
    {
        var numberOfDistinctBooks = GetNumberOfDistinctBooks();

        _total += numberOfDistinctBooks switch
        {
            2 => (2 * SINGLE_BOOK_PRICE) * 0.95,
            _ => GetNumberOfBooks() * SINGLE_BOOK_PRICE
        };

        return _total;
    }

    private int GetNumberOfBooks()
    {
        return _books.Values.Sum();
    }

    private int GetNumberOfDistinctBooks()
    {
        return _books.Distinct().Count();
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