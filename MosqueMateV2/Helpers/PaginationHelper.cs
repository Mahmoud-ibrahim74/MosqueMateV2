namespace MosqueMateV2.Helpers
{
    public class PaginationHelper
    {
        public static int _pageIndex { get; set; } = 1;
        public static int _pageSize { get; set; } = 30;

        public static (int Offset, int Limit) GetPagination()
        {
            // Ensure pageIndex starts at 1
            if (_pageIndex < 1) _pageIndex = 1;

            int offset = (_pageIndex - 1) * _pageSize;
            int limit = _pageSize;
            return (offset, limit);
        }
    }
}
