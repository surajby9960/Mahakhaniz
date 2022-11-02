namespace Mahakhaniz.Model
{
    public class PlotRegister
    {
        public string? PlotName { get; set; }
        public string? Address { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? Village { get; set; }
        public string? District { get; set; }
        public string? PlotType { get; set; }
        public string? Division { get; set; }
        public string? Taluka { get; set; }
        public string? SubDivision { get; set; }
        //public List<PlotAssign>? plotAssigns { get; set; }
    }
    public class PlotRegister1
    {
        public int Id { get; set; }
        public string? PlotName { get; set; }
        public string? Address { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? Village { get; set; }
        public string? District { get; set; }
        public string? PlotType { get; set; }
        public string? Division { get; set; }
        public string? Taluka { get; set; }
        public string? SubDivision { get; set; }
        public List<PlotAssign>? plotAssigns { get; set; }
    }
}
/*pr.PlotName,pr.Address,pr.Latitude,pr.Longitude,pr.Village,
d.District,
pt.PlotType,
dv.Division,
t.Taluka,
sd.SubDivision*/