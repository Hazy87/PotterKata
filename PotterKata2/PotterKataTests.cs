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
    
    [Fact]
    public void ThreeBooksOneDuplicateCostGet5PercentDiscount()
    {
        var expected = 23.20;

        _cart.AddBook(PotterBooks.First);
        _cart.AddBook(PotterBooks.Second);
        _cart.AddBook(PotterBooks.Second);
        
        AssertExpectedCosts(expected);
    }
    
    [Fact]
    public void FourBooksTwoDuplicateCostGet5PercentDiscount()
    {
        var expected = 30.40;

        _cart.AddBook(PotterBooks.First);
        _cart.AddBook(PotterBooks.Second);
        _cart.AddBook(PotterBooks.First);
        _cart.AddBook(PotterBooks.Second);
        
        AssertExpectedCosts(expected);
    }
    
    [Fact]
    public void ThreeDifferentBooksGets10PercentDiscount()
    {
        var expected = 21.60;

        _cart.AddBook(PotterBooks.First);
        _cart.AddBook(PotterBooks.Second);
        _cart.AddBook(PotterBooks.Third);
        
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
    Second,
    Third
}

public class Cart
{
    private Dictionary<PotterBooks, int> _books = new();
    private const double SINGLE_BOOK_PRICE = 8.00;
    private double _total = 0;
    
    public double GetTotal()
    {
        Dictionary<PotterBooks, int> remainingBooks = _books;
        

        while (GetRemainingBookCount(remainingBooks) > 0)
        {
            var numberOfDistinctBooks = GetNumberOfDistinctBooks(remainingBooks);
            
            _total = CalculateDiscount(_total, numberOfDistinctBooks);

            RemoveOneIssueOfEachBook(remainingBooks);
        }
        

        return _total;
    }

    private static int GetRemainingBookCount(Dictionary<PotterBooks, int> remainingBooks)
    {
        return remainingBooks.Values.Sum();
    }

    private double CalculateDiscount(double total, int numberOfDistinctBooks)
    {
        total += numberOfDistinctBooks switch
        {
            3 => (3 * SINGLE_BOOK_PRICE) * 0.90,
            2 => (2 * SINGLE_BOOK_PRICE) * 0.95,
            _ => SINGLE_BOOK_PRICE
        };

        return total;
    }

    private static void RemoveOneIssueOfEachBook(Dictionary<PotterBooks, int> remainingBooks)
    {
        foreach (var book in remainingBooks)
        {
            var value = book.Value;
            if (value == 1)
            {
                remainingBooks.Remove(book.Key);
            }
            else
            {
                remainingBooks[book.Key] -= 1;
            }
        }
    }

    private int GetNumberOfDistinctBooks(Dictionary<PotterBooks, int> books)
    {
        return books.Distinct().Count();
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