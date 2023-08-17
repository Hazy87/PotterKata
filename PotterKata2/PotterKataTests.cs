using System.Threading.Channels;

namespace PotterKata2;

public class PotterKataTests
{
    
    [Fact]
    public void NoBooksCostZero()
    {
        var expected = 0;

        AssertExpectedCosts(expected, new List<PotterBooks>());
    }
    
    [Fact]
    public void OneBooksCost8Pounds()
    {
        var expected = 8.00;
        var cart = new List<PotterBooks> { PotterBooks.First };

        AssertExpectedCosts(expected, cart);
    }
    
    [Fact]
    public void TwoDuplicateBooksCost16Pounds()
    {
        var expected = 16.00;
        var cart = new List<PotterBooks>
        {
            PotterBooks.First,
            PotterBooks.First
        };

        AssertExpectedCosts(expected, cart);
    }
    
    [Fact]
    public void TwoBooksCostGet5PercentDiscount()
    {
        var expected = 15.20;
        var cart = new List<PotterBooks>
        {
            PotterBooks.First,
            PotterBooks.Second
        };

        AssertExpectedCosts(expected, cart);
    }
    
    [Fact]
    public void ThreeBooksOneDuplicateCostGet5PercentDiscount()
    {
        var expected = 23.20;
        var cart = new List<PotterBooks>
        {
            PotterBooks.First,
            PotterBooks.Second,
            PotterBooks.Second
        };

        AssertExpectedCosts(expected, cart);
    }
    
    [Fact]
    public void FourBooksTwoDuplicateCostGet5PercentDiscount()
    {
        var expected = 30.40;
        var cart = new List<PotterBooks>
        {
            PotterBooks.First,
            PotterBooks.Second,
            PotterBooks.First,
            PotterBooks.Second
        };

        AssertExpectedCosts(expected, cart);
    }
    
    [Fact]
    public void ThreeDifferentBooksGets10PercentDiscount()
    {
        var expected = 21.60;
        var cart = new List<PotterBooks>
        {
            PotterBooks.First,
            PotterBooks.Second,
            PotterBooks.Third
        };

        AssertExpectedCosts(expected, cart);
    }
    
    [Fact]
    public void FourBooksOneDuplicateBookGets10PercentDiscount()
    {
        var expected = 29.60;
        var cart = new List<PotterBooks>
        {
            PotterBooks.First,
            PotterBooks.Second,
            PotterBooks.Third,
            PotterBooks.Second
        };

        AssertExpectedCosts(expected, cart);
    }
    
    [Fact]
    public void FourBookGets20PercentDiscount()
    {
        var expected = 25.60;
        var cart = new List<PotterBooks>
        {
            PotterBooks.First,
            PotterBooks.Second,
            PotterBooks.Third,
            PotterBooks.Fourth
        };

        AssertExpectedCosts(expected, cart);
    }

    [Fact]
    public void FiveBooksWithTwoDuplicateGets10PercentDiscountOn3BooksAnd5PercentOn2Books()
    {
        var expected = 21.60 + 15.20;
        var cart = new List<PotterBooks>
        {
            PotterBooks.First,
            PotterBooks.Second,
            PotterBooks.Third,
            PotterBooks.Third,
            PotterBooks.Second
        };

        AssertExpectedCosts(expected, cart);
    }
    
    [Fact]
    public void FiveBooksOneDuplicatedReceives20PercentDiscount()
    {
        var expectedCost = 33.60;
        var cart = new List<PotterBooks>
        {
            PotterBooks.First,
            PotterBooks.Second,
            PotterBooks.Third,
            PotterBooks.Third,
            PotterBooks.Fourth
        };

        AssertExpectedCosts(expectedCost, cart);
    }
    
    [Fact]
    public void SixBooksTwoDuplicatedReceives20PercentDiscountOnFourBooksAnd5PercentOnTwoBooks()
    {
        var expectedCost = 40.80;
        var cart = new List<PotterBooks>
        {
            PotterBooks.First,
            PotterBooks.Second,
            PotterBooks.Third,
            PotterBooks.Fourth,
            PotterBooks.Third,
            PotterBooks.Fourth
        };

        AssertExpectedCosts(expectedCost, cart);
    }
    
    [Fact]
    public void FiveTimesDifferentBooksReceives25PercentDiscount()
    {
            
        var expectedCost = 30.00;
        var cart = new List<PotterBooks>
        {
            PotterBooks.First,
            PotterBooks.Second,
            PotterBooks.Third,
            PotterBooks.Fourth,
            PotterBooks.Fifth
        };

        AssertExpectedCosts(expectedCost, cart);
    }
        
    [Fact]
    public void ManyBooksCostCorrectPrice()
    {

        double expectedCost = 30.00 + 25.60 + 15.20 + 8.00;
        var cart = new List<PotterBooks>
        {
            PotterBooks.First,
            PotterBooks.First,
            PotterBooks.First,
            PotterBooks.Second,
            PotterBooks.Second,
            PotterBooks.Third,
            PotterBooks.Third,
            PotterBooks.Third,
            PotterBooks.Third,
            PotterBooks.Fourth,
            PotterBooks.Fourth,
            PotterBooks.Fifth
        };

        AssertExpectedCosts(expectedCost, cart);
    }
    
    private void AssertExpectedCosts(double expected, List<PotterBooks> books)
    {
        var actual = new Cart().GetTotal(books);
        Assert.Equal(expected, actual);
    }
}