namespace PotterKata2;

public class Cart
{
    private Dictionary<PotterBooks, int> _books = new();
    private const double SINGLE_BOOK_PRICE = 8.00;
    private double _total = 0;
    
    public double GetTotal()
    {
        var remainingBooks = _books;
        
        while (GetRemainingBookCount(remainingBooks) > 0)
        {
            var numberOfDistinctBooks = GetNumberOfDistinctBooks(remainingBooks);
            
            _total += (numberOfDistinctBooks * GetDiscountAmount(numberOfDistinctBooks)) * SINGLE_BOOK_PRICE;
            RemoveOneIssueOfEachBook(remainingBooks);
        }
        return _total;
    }

    private static int GetRemainingBookCount(Dictionary<PotterBooks, int> remainingBooks)
    {
        return remainingBooks.Values.Sum();
    }

    private double GetDiscountAmount(int numberOfDistinctBooks)
    {
        return numberOfDistinctBooks switch
        {
            5 => 0.75,
            4 => 0.80,
            3 => 0.90,
            2 => 0.95,
            _ => 1
        };
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