namespace PizzaTown.Models
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; set; }

        public int TotalPages { get; set; }

        public PaginatedList(IEnumerable<T> entities, int pageIndex,
            int count, int pageSize)
        {
            this.PageIndex = pageIndex;
            this.TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(entities);
        }

        public bool PreviousPage => this.PageIndex > 1;

        public bool NextPage => this.PageIndex < this.TotalPages;

        public static PaginatedList<T> Create(IEnumerable<T> elements, int pageIndex,
            int pageSize)
        {
            var elementsList = elements.ToList();
            var count = elementsList.Count;
            var entities = elementsList
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToList();

            return new PaginatedList<T>
                (entities, pageIndex, count, pageSize);
        }
    }
}