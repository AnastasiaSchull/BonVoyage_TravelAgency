namespace BonVoyage_TravelAgency.Models
{
    public class PageViewModel
    {
        public int PageNumber { get; }
        public int TotalPages { get; }
        public bool HasPreviousPage => PageNumber > 1;//если PageNumber==1, то это 1я странцица, предыдущей нет
        public bool HasNextPage => PageNumber < TotalPages;//тогда можно идти вперед, на следующую страницу

        public PageViewModel(int count, int pageNumber, int pageSize)//  count- все туры в базе, pageSize -это сколько мы хотим отобразить на одной странице
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);//Ceiling-округление к большему целому
        }
    }
}
