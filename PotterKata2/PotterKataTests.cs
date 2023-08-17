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
    
    [Fact]
    public void FourBooksOneDuplicateBookGets10PercentDiscount()
    {
        var expected = 29.60;

        _cart.AddBook(PotterBooks.First);
        _cart.AddBook(PotterBooks.Second);
        _cart.AddBook(PotterBooks.Third);
        _cart.AddBook(PotterBooks.Second);
        
        AssertExpectedCosts(expected);
    }
    
    [Fact]
    public void FourBookGets20PercentDiscount()
    {
        var expected = 25.60;

        _cart.AddBook(PotterBooks.First);
        _cart.AddBook(PotterBooks.Second);
        _cart.AddBook(PotterBooks.Third);
        _cart.AddBook(PotterBooks.Fourth);
        
        AssertExpectedCosts(expected);
    }

    [Fact]
    public void FiveBooksWithTwoDuplicateGets10PercentDiscountOn3BooksAnd5PercentOn2Books()
    {
        var expected = 21.60 + 15.20;

        _cart.AddBook(PotterBooks.First);
        _cart.AddBook(PotterBooks.Second);
        _cart.AddBook(PotterBooks.Third);
        _cart.AddBook(PotterBooks.Third);
        _cart.AddBook(PotterBooks.Second);
        
        AssertExpectedCosts(expected);
    }
    
    [Fact]
    public void FiveBooksOneDuplicatedReceives20PercentDiscount()
    {
        var expectedCost = 33.60;
        
        _cart.AddBook(PotterBooks.First);
        _cart.AddBook(PotterBooks.Second);
        _cart.AddBook(PotterBooks.Third);
        _cart.AddBook(PotterBooks.Third);
        _cart.AddBook(PotterBooks.Fourth);
            
        AssertExpectedCosts(expectedCost);
    }
    
    [Fact]
    public void SixBooksTwoDuplicatedReceives20PercentDiscountOnFourBooksAnd5PercentOnTwoBooks()
    {
        var expectedCost = 40.80;
        
        _cart.AddBook(PotterBooks.First);
        _cart.AddBook(PotterBooks.Second);
        _cart.AddBook(PotterBooks.Third);
        _cart.AddBook(PotterBooks.Fourth);
        _cart.AddBook(PotterBooks.Third);
        _cart.AddBook(PotterBooks.Fourth);
            
        AssertExpectedCosts(expectedCost);
    }
    
    [Fact]
    public void FiveTimesDifferentBooksReceives25PercentDiscount()
    {
            
        var expectedCost = 30.00;
            
        _cart.AddBook(PotterBooks.First);
        _cart.AddBook(PotterBooks.Second);
        _cart.AddBook(PotterBooks.Third);
        _cart.AddBook(PotterBooks.Fourth);
        _cart.AddBook(PotterBooks.Fifth);
            
        AssertExpectedCosts(expectedCost);
    }
        
    [Fact]
    public void ManyBooksCostCorrectPrice()
    {

        double expectedCost = 30.00 + 25.60 + 15.20 + 8.00;
        
        _cart.AddBook(PotterBooks.First);
        _cart.AddBook(PotterBooks.First);
        _cart.AddBook(PotterBooks.First);
        _cart.AddBook(PotterBooks.Second);
        _cart.AddBook(PotterBooks.Second);
        _cart.AddBook(PotterBooks.Third);
        _cart.AddBook(PotterBooks.Third);
        _cart.AddBook(PotterBooks.Third);
        _cart.AddBook(PotterBooks.Third);
        _cart.AddBook(PotterBooks.Fourth);
        _cart.AddBook(PotterBooks.Fourth);
        _cart.AddBook(PotterBooks.Fifth);
            
        AssertExpectedCosts(expectedCost);
    }
    
    private void AssertExpectedCosts(double expected)
    {
        var actual = _cart.GetTotal();
        Assert.Equal(expected, actual);
    }
}