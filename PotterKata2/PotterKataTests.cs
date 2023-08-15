namespace PotterKata2;

public class PotterKataTests
{
    [Fact]
    public void NoBooksCostZero()
    {
        var expected = 0;

        var cart = new Cart();

        var actual = cart.GetTotal();
        
        Assert.Equal(expected, actual);
    }
}

public class Cart
{
    public int GetTotal()
    {
        return 0;
    }
}