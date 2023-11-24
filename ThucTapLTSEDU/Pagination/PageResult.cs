namespace ThucTapLTSEDU.Pagination
{
    public class PageResult<T>
    {
        public Pagintation pagintation { get; set; }
        public IEnumerable<T> Data { get; set; }

        public PageResult(Pagintation pagintation, IEnumerable<T> data)
        {
            this.pagintation = pagintation;
            Data = data;
        }
        public static IEnumerable<T> toPageResult(Pagintation pagintation, IEnumerable<T> data)
        {
            pagintation.PageNumber = pagintation.PageNumber < 1 ? 1 : pagintation.PageNumber;
            data = pagintation.PageSize == 0 ? data : data.Skip(pagintation.PageSize * (pagintation.PageNumber - 1)).Take(pagintation.PageSize).AsQueryable();
            return data;
        }
    }
}
