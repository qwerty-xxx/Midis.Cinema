namespace Cinema.Models.Domain
{
    public class Hall : BaseEntity
    {
        public string Name { get; set; }
        public int Places { get; set; }
        public int ColumnsCount { get; set; }
        public int RowsCount { get; set; }
        
    }
}