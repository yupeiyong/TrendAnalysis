namespace TrendAnalysis.Models.DataBase
{
    public class BaseEntity:IEntity<long>
    {
        public long Id { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
