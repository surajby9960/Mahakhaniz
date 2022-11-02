using Mahakhaniz.Model;
using Mahakhaniz.Reposotories.Interfaces;
using System.Data.Common;
using Dapper;

namespace Mahakhaniz.Reposotories
{
    public class PlotRegisterRepository : BaseAsyncRepository, IPlotRegisterRepository
    {
        public PlotRegisterRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public async Task<BaseResponse> GetAllPlot(int pageno, int pagesize, int division, int state, int district, int taluka, string? plotname)
        {
            //make object of baseresponse class to return response
            BaseResponse baseResponse = new BaseResponse();
            //pagination object for hold paginaion data
            PaginationModel paginationModel = new PaginationModel();
            //create plotregister class list for storing multiple record.
            List<PlotRegister> plotRegisters = new List<PlotRegister>();
            var qry = @" 
                            select pr.PlotName,pr.Address,pr.Latitude,pr.Longitude,pr.Village,
                            d.District,
                            pt.PlotType,
                            dv.Division,
                            t.Taluka,
                            sd.SubDivision
                            from tblplotregister pr
                            Left join tbldistrict d on pr.District = d.Id
                            left join tblPlotType pt on pr.PlotType = pt.Id
                            left join tbldivision dv on pr.Division = dv.Id
                            left join tbltaluka t on pr.Taluka = t.Id
                            left join tblstate s on pr.stateid = s.id
                            left join tblSubDivision sd  on t.SubDivisionId = sd.Id
                            left join tblplotassign pa on pr.id = pa.PlotId
                            where(dv.Id = @division or @division = 0) AND
                            (s.id = @state or @state = 0) AND
                            (d.id = @district or @district = 0) AND
                            (pr.PlotName like '%' + @plotname + '%' or @plotname is null)AND
                            (t.Id = @taluka or @taluka = 0) AND
                            (CURRENT_TIMESTAMP between pa.DurationFrom and DurationTo) order by pr.Id desc 
                            offset @val rows fetch next @pagesize rows only;
                            select @pageno as pageno,count(distinct pr.Id) as TotalPages from tblplotregister pr where isdeleted = 0";
            //
            var val = (pageno - 1) * pagesize;
            var par = new { pageno = pageno, pagesize = pagesize, division = division, state = state, district = district, PlotName = plotname, taluka = taluka, val = val };
            using (DbConnection dbConnection = ReaderConnection)
            {
                var res = await dbConnection.QueryMultipleAsync(qry, par);
                var plot = await res.ReadAsync<PlotRegister>();
                plotRegisters = plot.ToList();
                var pagination = await res.ReadAsync<PaginationModel>();
                paginationModel = pagination.FirstOrDefault();
                int last = 0;
                int pagecount = 0;
                last = paginationModel.Totalpages % pagesize;
                pagecount = paginationModel.Totalpages / pagesize;
                paginationModel.Pagecount = paginationModel.Totalpages;
                paginationModel.Totalpages = pagecount;
                if (last > 0)
                {
                    paginationModel.Totalpages = pagecount + 1;
                }
                baseResponse.ResponseData = paginationModel;
                baseResponse.ResponseData1 = plotRegisters;
                return baseResponse;

            }
        }

        public async Task<BaseResponse> GetAllPlotAndAsign(int pageno, int pagesize, int division, int state, int district, int taluka, string? plotname)
        {
            BaseResponse baseResponse = new BaseResponse();
            PaginationModel paginationModel = new PaginationModel();
            List<PlotRegister1> plotRegisters1 = new List<PlotRegister1>();
            var qry = @" 
                            select pr.id, pr.PlotName,pr.Address,pr.Latitude,pr.Longitude,pr.Village,
                            d.District,
                            pt.PlotType,
                            dv.Division,
                            t.Taluka,
                            sd.SubDivision
                            from tblplotregister pr
                            Left join tbldistrict d on pr.District = d.Id
                            left join tblPlotType pt on pr.PlotType = pt.Id
                            left join tbldivision dv on pr.Division = dv.Id
                            left join tbltaluka t on pr.Taluka = t.Id
                            left join tblstate s on pr.stateid = s.id
                            left join tblSubDivision sd  on t.SubDivisionId = sd.Id
                            left join tblplotassign pa on pr.id = pa.PlotId
                            where(dv.Id = @division or @division = 0) AND
                            (s.id = @state or @state = 0) AND
                            (d.id = @district or @district = 0) AND
                            (pr.PlotName like '%' + @plotname + '%' or @plotname is null)AND
                            (t.Id = @taluka or @taluka = 0) AND
                            (CURRENT_TIMESTAMP between pa.DurationFrom and DurationTo) order by pr.Id desc 
                            offset @val rows fetch next @pagesize rows only;
                            select @pageno as pageno,count(distinct pr.Id) as TotalPages from tblplotregister pr where isdeleted = 0";
            var qry1 = @" 
                               select pa.PlotId,  pa.AuctionOrderNo,pa.Quantity,pa.DurationFrom,pa.DurationTo,
                                s.State,
                                d.District,
                                gst.SchemeType,
                                rt.RoyaltyType,
                                pt.PermitType,
                                pot.PlotType
                                from tblplotassign pa 
                                left join tblstate s on pa.StateId=s.id
                                left join tbldistrict d on pa.DistrictId=d.Id
                                left join tbldivision dv on pa.DivisionId=dv.Id
                                left join tblGovSchemeType gst on pa.GovSchemeTypeId=gst.id
                                left join tblRoyaltyType rt on pa.RoyaltyTypeId=rt.RoyaltyTypeId
                                left join tblplotregister pr on pa.PlotId=pr.Id
                                left join tblPermitType pt on pa.PermitType=pt.Id
                                left join tblPlotType pot on pr.PlotType=pot.Id
                                where 
                                 (pa.plotId=@plotid)and
                                (CURRENT_TIMESTAMP between pa.DurationFrom and DurationTo)";/* order by pr.Id desc offset (@pageno-1)*@pagesize rows fetch next @pagesize rows only;
                                select @pageno as pageno,count(distinct pr.Id) as TotalPages from tblplotassign pr where isdeleted = 0 and pr.PlotId=@plotid";
            */
            var val = (pageno - 1) * pagesize;
            var par = new { pageno = pageno, pagesize = pagesize, division = division, state = state, district = district, PlotName = plotname, taluka = taluka, val = val };
            using (DbConnection dbConnection = ReaderConnection)
            {
                var res = await dbConnection.QueryMultipleAsync(qry, par);
                var plot = await res.ReadAsync<PlotRegister1>();
                plotRegisters1 = plot.ToList();
                var pagination = await res.ReadAsync<PaginationModel>();
                paginationModel = pagination.FirstOrDefault();
                //close existing connection
                dbConnection.Close();
                //Open new connection 
                using(DbConnection dbConnection2 = ReaderConnection)
                foreach (var plotRegister in plotRegisters1)
                {
                    var ploasssign = await dbConnection2.QueryAsync<PlotAssign>(qry1, new { plotid = plotRegister.Id,pageno,pagesize,val });
                    plotRegister.plotAssigns=ploasssign.ToList();
                }
                
                int last = 0;
                int pagecount = 0;
                last = paginationModel.Totalpages % pagesize;
                pagecount = paginationModel.Totalpages / pagesize;
                paginationModel.Pagecount = paginationModel.Totalpages;
                paginationModel.Totalpages = pagecount;
                if (last > 0)
                {
                    paginationModel.Totalpages = pagecount + 1;
                }
                baseResponse.ResponseData = paginationModel;
                baseResponse.ResponseData1 = plotRegisters1;
                return baseResponse;

            }
        }

            public async Task<BaseResponse> GetAllPlotassign(int pageno, int pagesize, int division, int state, int district, int permittype, int plottype, string? plotname)
            {

            BaseResponse baseResponse = new BaseResponse();
            PaginationModel paginationModel = new PaginationModel();
            List<PlotAssign> plotAssigns = new List<PlotAssign>();
            var qry = @"
                               select  pa.AuctionOrderNo,pa.Quantity,pa.DurationFrom,pa.DurationTo,
                                s.State,
                                d.District,
                                gst.SchemeType,
                                rt.RoyaltyType,
                                pt.PermitType,
                                pot.PlotType
                                from tblplotassign pa 
                                left join tblstate s on pa.StateId=s.id
                                left join tbldistrict d on pa.DistrictId=d.Id
                                left join tbldivision dv on pa.DivisionId=dv.Id
                                left join tblGovSchemeType gst on pa.GovSchemeTypeId=gst.id
                                left join tblRoyaltyType rt on pa.RoyaltyTypeId=rt.RoyaltyTypeId
                                left join tblplotregister pr on pa.PlotId=pr.Id
                                left join tblPermitType pt on pa.PermitType=pt.Id
                                left join tblPlotType pot on pr.PlotType=pot.Id
                                where (dv.Id=@division or @division=0) AND
                                (s.id=@state or @state=0) AND
                                (d.id=@district or @district=0) AND
                                (pr.PlotName like '%'+@plotname+'%' or @plotname is null)AND
                                (pt.Id=@permitype or @permitype=0)AND
                                (pot.id=@plottype or @plottype=0)and
                                (CURRENT_TIMESTAMP between pa.DurationFrom and DurationTo) order by pr.Id desc offset @val rows fetch next @pagesize rows only;
                                select @pageno as pageno,count(distinct pr.Id) as TotalPages from tblplotregister pr where isdeleted = 0";
            var val = (pageno - 1) * pagesize;
            var par = new { pageno = pageno, pagesize = pagesize, division = division, state = state, district = district, PlotName = plotname, permitype = permittype, plottype = plottype, val = val };
            using (DbConnection dbConnection = ReaderConnection)
            {
                var res = await dbConnection.QueryMultipleAsync(qry, par);
                var plot = await res.ReadAsync<PlotAssign>();
                plotAssigns = plot.ToList();
                var pagination = await res.ReadAsync<PaginationModel>();
                paginationModel = pagination.FirstOrDefault();
                int last = 0;
                int pagecount = 0;
                last = paginationModel.Totalpages % pagesize;
                pagecount = paginationModel.Totalpages / pagesize;
                paginationModel.Pagecount = paginationModel.Totalpages;
                paginationModel.Totalpages = pagecount;
                if (last > 0)
                {
                    paginationModel.Totalpages = pagecount + 1;
                }
                baseResponse.ResponseData = paginationModel;
                baseResponse.ResponseData1 = plotAssigns;
                return baseResponse;
            }
        }

    }
}
