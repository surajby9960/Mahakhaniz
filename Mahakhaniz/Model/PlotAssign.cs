namespace Mahakhaniz.Model
{
    public class PlotAssign
    {
        public int PlotId { get; set; }
        public string? AuctionOrderNo { get; set; }
        public int? Quantity { get; set; }
        public DateTime DurationFrom { get; set; }
        public DateTime DurationTo { get; set; }
        public string? State { get; set; }
        public string? District { get; set; }
        public string? SchemeType { get; set; }
        public string? RoyaltyType { get; set; }
        public string? PermitType { get; set; }
    }
}
/*pa.AuctionOrderNo,pa.Quantity,pa.DurationFrom,pa.DurationTo,
s.State,
d.District,
gst.SchemeType,
rt.RoyaltyType,
pt.PermitType*/