namespace PearAdmin.Abp.CommonDto
{
    /// <summary>
    /// 数量Dto
    /// </summary>
    public class CountDto
    {
        public CountDto(int count)
        {
            Count = count;
        }

        public int Count { get; set; }
    }
}
