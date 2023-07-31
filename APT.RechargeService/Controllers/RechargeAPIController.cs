using APT.RechargeService.BusinessLayer;
using APT.RechargeService.BusinessObject.Model;
using APT.RechargeService.DattaAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using APT.RechargeService.BusinessLayer.VendorService;
using System.Data;

namespace APT.RechargeService.Controllers
{
    [Route("api/Recharge")]
    [ApiController]
    public class RechargeAPIController : ControllerBase
    {
        RechargeAPI ra=new RechargeAPI();
        VendorCall vc=new VendorCall();
        //#region Swagger
        //[SwaggerOperation("Get Topup Info", "Used to fetch top info")]
        //[SwaggerResponse(StatusCodes.Status200OK, "Ok - Request Successful", typeof(APTGenericResponse))]
        //[SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Failed - Unprocessable Entity", typeof(APTGenericResponse))]
        //#endregion
        [HttpGet]
        [Route("GetOperatorList")]
        public IActionResult? GetOperatorList(int RechargeType)
        {
            OperatorListResponseAPI objresponse = new OperatorListResponseAPI();
            try
            {
                objresponse = ra.GetOperatorList(RechargeType);
                if (objresponse!=null)
                {
                    if (objresponse.response_code == "200")
                    {
                        return Ok(objresponse);
                    }
                    else
                    {
                        return UnprocessableEntity(objresponse);
                    }
                }
                else
                {
                    return UnprocessableEntity(objresponse);
                }
            }
            catch (Exception ex)
            {
                objresponse.response_code = "103";
                objresponse.response_message =ex.Message;
                objresponse.data = null;
                return UnprocessableEntity(objresponse);
            }
        }

        [HttpGet]
        [Route("GetPlanList")]
        public IActionResult? GetPlanListAPI()
        {
            PlanListResponseAPI objresponse = new PlanListResponseAPI();
            try
            {
                objresponse = ra.GetPlanList();
                if (objresponse != null)
                {
                    if (objresponse.response_code == "200")
                    {
                        return Ok(objresponse);
                    }
                    else
                    {
                        return UnprocessableEntity(objresponse);
                    }
                }
                else
                {
                    return UnprocessableEntity(objresponse);
                }
            }
            catch (Exception ex)
            {
                objresponse.response_code = "103";
                objresponse.response_message = ex.Message;
                objresponse.data = null;
                return UnprocessableEntity(objresponse);
            }
        }

        [HttpGet]
        [Route("GetRechargeType")]
        public IActionResult? GetRechargeTypeAPI()
        {
            RechargeTypeAPI objresponse = new RechargeTypeAPI();
            try
            {
                objresponse = ra.GetRechargeType();
                if (objresponse != null)
                {
                    if (objresponse.response_code == "200")
                    {
                        return Ok(objresponse);
                    }
                    else
                    {
                        return UnprocessableEntity(objresponse);
                    }
                }
                else
                {
                    return UnprocessableEntity(objresponse);
                }
            }
            catch (Exception ex)
            {
                objresponse.response_code = "103";
                objresponse.response_message = ex.Message;
                objresponse.data = null;
                return UnprocessableEntity(objresponse);
            }
        }

        [HttpPost]
        [Route("StatusCheck")]
        public IActionResult? CheckStatus(RechargeStatusCheckRequest rscrq)
        {
            RechargeStatusCheckResponseAPI objresponse = new RechargeStatusCheckResponseAPI();
            if (Request == null)
            {
                objresponse.response_code = "100";
                objresponse.response_message = "";
                return UnprocessableEntity(objresponse);
            }
            try
            {
                objresponse = vc.CheckStatus(rscrq);
                if (objresponse != null)
                {
                    return Ok(objresponse);
                }
                else
                {
                    return UnprocessableEntity(objresponse);
                }
            }
            catch (Exception e)
            {
                objresponse.response_code = "";
                objresponse.response_message = "";
                return UnprocessableEntity(objresponse);
            }
        }

        [HttpPost]
        [Route("RechargePayment")]
        public IActionResult? RechargePaymentAPI(RechargePaymentRequest rrs)
        {
            RechargePaymentresponseAPI objresponse = new RechargePaymentresponseAPI();
            if (Request == null)
            {
                objresponse.response_code = "101";
                objresponse.response_description= "Invalid request values";
                return UnprocessableEntity(objresponse);
            }
            try
            {
                objresponse = vc.TransactionAPI(rrs);
                if (objresponse != null)
                {
                    if (objresponse.response_code == "200")
                    {
                        return Ok(objresponse);
                    }
                    else
                    {
                        return UnprocessableEntity(objresponse);
                    }
                }
                else
                {
                    return UnprocessableEntity(objresponse);
                }
            }
            catch (Exception ex)
            {
                objresponse.response_code = "606";
                objresponse.response_description = ex.Message;
                return UnprocessableEntity(objresponse);
            }
        }

        [HttpGet]
        [Route("GetDataPlanTopupType")]
        public IActionResult? GetDataPlanTopupTypeList(int RechargeTypeRefID)
        {
            RechargePlanTopupTypeResponseAPI objresponse = new RechargePlanTopupTypeResponseAPI();
            try
            {
                objresponse = ra.GetDataPlanTopupTypeList(RechargeTypeRefID);
                if (objresponse != null)
                {
                    if (objresponse.response_code == "200")
                    {
                        return Ok(objresponse);
                    }
                    else
                    {
                        return UnprocessableEntity(objresponse);
                    }
                }
                else
                {
                    return UnprocessableEntity(objresponse);
                }
            }
            catch (Exception ex)
            {
                objresponse.response_code = "103";
                objresponse.response_message = ex.Message;
                objresponse.data = null;
                return UnprocessableEntity(objresponse);
            }
        }

     
        [HttpPost]
        [Route("GetPlan")]
        public IActionResult? GetPlan(RechargePlanRequestAPI rpra)
        {
            RechargePlanResponseAPI objresponse = new RechargePlanResponseAPI();
            try
            {
                objresponse = vc.PlanList(rpra);
                if (objresponse != null)
                {
                    if (objresponse.response_code == "200")
                    {
                        return Ok(objresponse);
                    }
                    else
                    {
                        return UnprocessableEntity(objresponse);
                    }
                }
                else
                {
                    return UnprocessableEntity(objresponse);
                }
            }
            catch (Exception ex)
            {
                //objresponse.response_code = "103";
                //objresponse.response_message = ex.Message;
                //objresponse.data = null;
                return UnprocessableEntity(objresponse);
            }
        }


        [HttpPost]
        [Route("GetRetailerTransactionReportList")]
        public IActionResult? GetRetailerTransactionReportList(RetailerReportRequest rrrq)
        {
            RetailerReportResponseAPI objresponse = new RetailerReportResponseAPI();
            try
            {
                objresponse = ra.GetRetailerTransactionList(rrrq);
                if (objresponse != null)
                {
                    if (objresponse.response_code == "200")
                    {
                        return Ok(objresponse);
                    }
                    else
                    {
                        return UnprocessableEntity(objresponse);
                    }
                }
                else
                {
                    return UnprocessableEntity(objresponse);
                }
            }
            catch (Exception ex)
            {
                objresponse.response_code = "103";
                objresponse.response_message = ex.Message;
                objresponse.data = null;
                return UnprocessableEntity(objresponse);
            }
        }
    }
}
