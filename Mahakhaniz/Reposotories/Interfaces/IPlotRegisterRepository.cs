using Mahakhaniz.Model;

namespace Mahakhaniz.Reposotories.Interfaces
{
    public interface IPlotRegisterRepository
    {
        public Task<BaseResponse> GetAllPlot(int pageno,int pagesize,int division,int state,int district,int taluka,string? plotname );
        public Task<BaseResponse> GetAllPlotassign(int pageno, int pagesize, int division, int state, int district, int permittype, int plottype, string? plotname);
        public Task<BaseResponse> GetAllPlotAndAsign(int pageno, int pagesize, int division, int state, int district, int taluka, string? plotname);
    }
}
/*division 
state int
district int
taluka int
plotname varchar(50)*/