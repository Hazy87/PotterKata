namespace PotterKata2;

public class Cart
{
    private const double SINGLE_BOOK_PRICE = 8.00;

    public double GetTotal(List<PotterBooks> books)
    {
        var total = 0.0;
        while (books.Count> 0)
        {
            var numberOfDistinctBooks = GetNumberOfDistinctBooks(books);
            total += numberOfDistinctBooks * GetDiscountAmount(numberOfDistinctBooks) * SINGLE_BOOK_PRICE;
            RemoveOneIssueOfEachBook(books);
        }

        return total;
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

    private void RemoveOneIssueOfEachBook(List<PotterBooks> remainingBooks)
    {
        remainingBooks.GroupBy(x => x).ToList().ForEach(x => remainingBooks.Remove(x.First()));
    }

    private int GetNumberOfDistinctBooks(IEnumerable<PotterBooks> books)
    {
        return books.Distinct().Count();
    }
}