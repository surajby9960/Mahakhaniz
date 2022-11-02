using Mahakhaniz.Model;
using Mahakhaniz.Reposotories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mahakhaniz.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlotRegisterController : ControllerBase
    {
        private readonly IPlotRegisterRepository plotRegisterRepository;
        public PlotRegisterController(IConfiguration configuration,IPlotRegisterRepository plotRegisterRepository)
        {
            this.plotRegisterRepository = plotRegisterRepository;  
        }
        [HttpGet("Get-All-RegisterPlot")]
        public async Task<IActionResult> GetAllPlot(int pageno, int pagesize, int division, int state, int district, int taluka, string? plotname)
        {
            try
            {
                BaseResponse baseResponse = new BaseResponse();
                var data=await plotRegisterRepository.GetAllPlot(pageno,pagesize,division,state, district,taluka,plotname);
                List<PlotRegister> plotRegisterss = (List<PlotRegister>)data.ResponseData1;
                baseResponse.StatusMessage = "All record fetched succesfully";
                baseResponse.ResponseData = plotRegisterss;
                baseResponse.ResponseData1 = data.ResponseData;
                return Ok(baseResponse);
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("Get-All-Assignplot")]
        public async Task<IActionResult> GetAllAssignPlot(int pageno, int pagesize, int division, int state, int district, int permittype, int plottype, string? plotname)
        {
            try
            {
                BaseResponse baseResponse = new BaseResponse();
                var data = await plotRegisterRepository.GetAllPlotassign(pageno,pagesize,division,state,district,permittype,plottype,plotname);
                List<PlotAssign> plotAssigns = (List<PlotAssign>)data.ResponseData1;
                baseResponse.StatusMessage = "All record fetched succesfully";
                baseResponse.ResponseData = plotAssigns;
                baseResponse.ResponseData1 = data.ResponseData;
                return Ok(baseResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("Get-All-AssignplotAndAssign")]
        public async Task<IActionResult> GetAllAssignPlotAndAssign(int pageno, int pagesize, int division, int state, int district,int taluka, string? plotname)
        {
            try
            {
                BaseResponse baseResponse = new BaseResponse();
                var data = await plotRegisterRepository.GetAllPlotAndAsign(pageno, pagesize, division, state, district,taluka, plotname);
                List<PlotRegister1> plotRegister1s = (List<PlotRegister1>)data.ResponseData1;
                baseResponse.StatusMessage = "All record fetched succesfully";
                baseResponse.ResponseData = plotRegister1s;
                baseResponse.ResponseData1 = data.ResponseData;
                return Ok(baseResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
