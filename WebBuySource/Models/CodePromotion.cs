namespace WebBuySource.Models
{
    public class CodePromotion
    {
        public int CodeId { get; set; }
        public Code Code { get; set; }

        public int PromotionId { get; set; }
        public Promotion Promotion { get; set; }

        public ICollection<Code> Codes { get; set; }
    }
}

