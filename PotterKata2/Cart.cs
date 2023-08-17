namespace PotterKata2;

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
            5 => (5 * SINGLE_BOOK_PRICE) * 0.75,
            4 => (4 * SINGLE_BOOK_PRICE) * 0.80,
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