using Microsoft.Extensions.Configuration;
using APT.RechargeService.BusinessObject.Model;
using System.Data;
using APT.RechargeService.DattaAccessLayer;

namespace APT.RechargeService.BusinessLayer
{
    public class RechargeAPI
    {
        RechargeRepository RR = new RechargeRepository();
        public static IConfiguration Configuration { get; set; }
        private readonly string _masterconnString;
        private readonly string _txnconnString;
        public static LogService _logService = new LogService();
        public RechargeAPI()
        {
            Configuration = GetConfiguration();
            _masterconnString = Configuration.GetConnectionString("MasterDBConnection");
            _txnconnString = Configuration.GetConnectionString("TransactionDBConnection");

        }
        public IConfiguration GetConfiguration()
        {
            Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
            return Configuration;
        }
        public OperatorListResponseAPI GetOperatorList(int RechargeType)
        {
            OperatorListResponseAPI ds = new OperatorListResponseAPI();
            List<OperatorListResponse> lst_operator = new List<OperatorListResponse>();
            DataSet dst = RR.GetOperatorList(RechargeType);
            if(dst!= null)
            {
                if(dst.Tables.Count>0)
                {
                    if (dst.Tables[0].Rows.Count>0)
                    {
                        for(int i = 0; i < dst.Tables[0].Rows.Count; i++)
                        {
                            OperatorListResponse ds2= new OperatorListResponse();
                            ds2.vendor_ref_id = dst.Tables[0].Rows[i]["VendorRefID"].ToString();
                            ds2.recharge_operator = dst.Tables[0].Rows[i]["RechargeOperator"].ToString();
                            ds2.product_key = dst.Tables[0].Rows[i]["ProductKey"].ToString();
                            ds2.recharge_operator_ref_id = dst.Tables[0].Rows[i]["RechargeOperatorRefID"].ToString();
                            ds2.recharge_type_ref_id = dst.Tables[0].Rows[i]["RechargeTypeRefID"].ToString();
                            lst_operator.Add(ds2);
                        }
                        ds.response_code = "200";
                        ds.response_message = "Success";
                        ds.data = lst_operator;
                    }
                    else
                    {
                        ds.response_code = "100";
                        ds.response_message = "No record found";
                        ds.data = lst_operator;
                    }
                }
                else
                {
                    ds.response_code = "101";
                    ds.response_message = "No record found";
                    ds.data = lst_operator;
                }
            }
            else
            {
                ds.response_code = "102";
                ds.response_message = "No record found";
                ds.data = lst_operator;
            }
            return ds;
        }
               
        public PlanListResponseAPI GetPlanList()
        {
            PlanListResponseAPI ds = new PlanListResponseAPI();
            List<PlanListResponse> lst_operator = new List<PlanListResponse>();
            DataSet dst = RR.GetPlanList();
            if (dst != null)
            {
                if (dst.Tables.Count > 0)
                {
                    if (dst.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dst.Tables[0].Rows.Count; i++)
                        {
                            PlanListResponse ds2 = new PlanListResponse();
                            ds2.price = dst.Tables[0].Rows[i]["Price"].ToString();
                            ds2.talktime = dst.Tables[0].Rows[i]["Talktime"].ToString();
                            ds2.validity_string = dst.Tables[0].Rows[i]["ValidityString"].ToString();
                            ds2.validity = dst.Tables[0].Rows[i]["Validity"].ToString();
                            ds2.id = dst.Tables[0].Rows[i]["Id"].ToString();
                            ds2.name = dst.Tables[0].Rows[i]["Name"].ToString();
                            ds2.plan_url = dst.Tables[0].Rows[i]["PlanURL"].ToString();
                            ds2.validate_ref_id = dst.Tables[0].Rows[i]["ValidateRefID"].ToString();
                            lst_operator.Add(ds2);
                        }
                        ds.response_code = "200";
                        ds.response_message = "Success";
                        ds.data = lst_operator;
                    }
                    else
                    {
                        ds.response_code = "100";
                        ds.response_message = "No record found";
                        ds.data = lst_operator;
                    }
                }
                else
                {
                    ds.response_code = "101";
                    ds.response_message = "No record found";
                    ds.data = lst_operator;
                }
            }
            else
            {
                ds.response_code = "102";
                ds.response_message = "No record found";
                ds.data = lst_operator;
            }
            return ds;
        }

        public RechargeTypeAPI GetRechargeType()
        {
            RechargeTypeAPI ds = new RechargeTypeAPI();
            List<RechargeTypeListResponse> lst_operator = new List<RechargeTypeListResponse>();
            DataSet dst = RR.GetRechargeType();
            if (dst != null)
            {
                if (dst.Tables.Count > 0)
                {
                    if (dst.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dst.Tables[0].Rows.Count; i++)
                        {
                            RechargeTypeListResponse ds2 = new RechargeTypeListResponse();
                            ds2.recharger_type_ref_id = dst.Tables[0].Rows[i]["RechargeTypeRefID"].ToString();
                            ds2.recharge_type = dst.Tables[0].Rows[i]["RechargeType"].ToString();
                            lst_operator.Add(ds2);
                        }
                        ds.response_code = "200";
                        ds.response_message = "Success";
                        ds.data = lst_operator;
                    }
                    else
                    {
                        ds.response_code = "100";
                        ds.response_message = "No record found";
                        ds.data = lst_operator;
                    }
                }
                else
                {
                    ds.response_code = "101";
                    ds.response_message = "No record found";
                    ds.data = lst_operator;
                }
            }
            else
            {
                ds.response_code = "102";
                ds.response_message = "No record found";
                ds.data = lst_operator;
            }
            return ds;
        }


        public RechargeTypeAPI StatusUpdate()
        {
            RechargeTypeAPI ds = new RechargeTypeAPI();
            List<RechargeTypeListResponse> lst_operator = new List<RechargeTypeListResponse>();
            DataSet dst = RR.GetRechargeType();
            if (dst != null)
            {
                if (dst.Tables.Count > 0)
                {
                    if (dst.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dst.Tables[0].Rows.Count; i++)
                        {
                            RechargeTypeListResponse ds2 = new RechargeTypeListResponse();
                            ds2.recharger_type_ref_id = dst.Tables[0].Rows[i]["RechargeTypeRefID"].ToString();
                            ds2.recharge_type = dst.Tables[0].Rows[i]["RechargeType"].ToString();
                            lst_operator.Add(ds2);
                        }
                        ds.response_code = "200";
                        ds.response_message = "Success";
                        ds.data = lst_operator;
                    }
                    else
                    {
                        ds.response_code = "100";
                        ds.response_message = "No record found";
                        ds.data = lst_operator;
                    }
                }
                else
                {
                    ds.response_code = "101";
                    ds.response_message = "No record found";
                    ds.data = lst_operator;
                }
            }
            else
            {
                ds.response_code = "102";
                ds.response_message = "No record found";
                ds.data = lst_operator;
            }
            return ds;
        }

        public RechargePlanTopupTypeResponseAPI GetDataPlanTopupTypeList(int RechargeTypeRefID)
        {
            RechargePlanTopupTypeResponseAPI ds = new RechargePlanTopupTypeResponseAPI();
            List<RechargePlanTopupTypeResponse> lst_operator = new List<RechargePlanTopupTypeResponse>();
            DataSet dst = RR.GetDataPlanTopupTypeList(RechargeTypeRefID);
            if (dst != null)
            {
                if (dst.Tables.Count > 0)
                {
                    if (dst.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dst.Tables[0].Rows.Count; i++)
                        {
                            RechargePlanTopupTypeResponse ds2 = new RechargePlanTopupTypeResponse();
                            ds2.recharge_topup_type_ref_id = dst.Tables[0].Rows[i]["RechargeTopupTypeRefID"].ToString();
                            ds2.recharge_topup_type = dst.Tables[0].Rows[i]["RechargeTopupType"].ToString();
                            lst_operator.Add(ds2);
                        }
                        ds.response_code = "200";
                        ds.response_message = "Success";
                        ds.data = lst_operator;
                    }
                    else
                    {
                        ds.response_code = "100";
                        ds.response_message = "No record found";
                        ds.data = lst_operator;
                    }
                }
                else
                {
                    ds.response_code = "101";
                    ds.response_message = "No record found";
                    ds.data = lst_operator;
                }
            }
            else
            {
                ds.response_code = "102";
                ds.response_message = "No record found";
                ds.data = lst_operator;
            }
            return ds;
        }

        public OperatorListResponseAPI GetDataPlanRegionsList()
        {
            OperatorListResponseAPI ds = new OperatorListResponseAPI();
            List<OperatorListResponse> lst_operator = new List<OperatorListResponse>();
            DataSet dst = RR.GetDataPlanRegionsList();
            if (dst != null)
            {
                if (dst.Tables.Count > 0)
                {
                    if (dst.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dst.Tables[0].Rows.Count; i++)
                        {
                            OperatorListResponse ds2 = new OperatorListResponse();
                            ds2.vendor_ref_id = dst.Tables[0].Rows[i]["VendorRefID"].ToString();
                            ds2.recharge_operator = dst.Tables[0].Rows[i]["RechargeOperator"].ToString();
                            ds2.product_key = dst.Tables[0].Rows[i]["ProductKey"].ToString();
                            ds2.recharge_operator_ref_id = dst.Tables[0].Rows[i]["RechargeOperatorRefID"].ToString();
                            ds2.recharge_type_ref_id = dst.Tables[0].Rows[i]["RechargeTypeRefID"].ToString();
                            lst_operator.Add(ds2);
                        }
                        ds.response_code = "200";
                        ds.response_message = "Success";
                        ds.data = lst_operator;
                    }
                    else
                    {
                        ds.response_code = "100";
                        ds.response_message = "No record found";
                        ds.data = lst_operator;
                    }
                }
                else
                {
                    ds.response_code = "101";
                    ds.response_message = "No record found";
                    ds.data = lst_operator;
                }
            }
            else
            {
                ds.response_code = "102";
                ds.response_message = "No record found";
                ds.data = lst_operator;
            }
            return ds;
        }

        public OperatorListResponseAPI GetDataPlanTopupType()
        {
            OperatorListResponseAPI ds = new OperatorListResponseAPI();
            List<OperatorListResponse> lst_operator = new List<OperatorListResponse>();
            DataSet dst = RR.GetDataPlanTopupType();
            if (dst != null)
            {
                if (dst.Tables.Count > 0)
                {
                    if (dst.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dst.Tables[0].Rows.Count; i++)
                        {
                            OperatorListResponse ds2 = new OperatorListResponse();
                            ds2.vendor_ref_id = dst.Tables[0].Rows[i]["VendorRefID"].ToString();
                            ds2.recharge_operator = dst.Tables[0].Rows[i]["RechargeOperator"].ToString();
                            ds2.product_key = dst.Tables[0].Rows[i]["ProductKey"].ToString();
                            ds2.recharge_operator_ref_id = dst.Tables[0].Rows[i]["RechargeOperatorRefID"].ToString();
                            ds2.recharge_type_ref_id = dst.Tables[0].Rows[i]["RechargeTypeRefID"].ToString();
                            lst_operator.Add(ds2);
                        }
                        ds.response_code = "200";
                        ds.response_message = "Success";
                        ds.data = lst_operator;
                    }
                    else
                    {
                        ds.response_code = "100";
                        ds.response_message = "No record found";
                        ds.data = lst_operator;
                    }
                }
                else
                {
                    ds.response_code = "101";
                    ds.response_message = "No record found";
                    ds.data = lst_operator;
                }
            }
            else
            {
                ds.response_code = "102";
                ds.response_message = "No record found";
                ds.data = lst_operator;
            }
            return ds;
        }

        public RetailerReportResponseAPI GetRetailerTransactionList(RetailerReportRequest rrrq)
        {
            RetailerReportResponseAPI ds = new RetailerReportResponseAPI();
            List<RetailerReportResponse> lst_operator = new List<RetailerReportResponse>();
            DataSet dst = RR.GetRetailerTransactionList(rrrq);
            if (dst != null)
            {
                if (dst.Tables.Count > 0)
                {
                    if (dst.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dst.Tables[0].Rows.Count; i++)
                        {
                            RetailerReportResponse ds2 = new RetailerReportResponse();
                            ds2.transaction_date = dst.Tables[0].Rows[i]["TransactionDate"].ToString();
                            ds2.transaction_id = dst.Tables[0].Rows[i]["TransactionID"].ToString();
                            ds2.operator_id = dst.Tables[0].Rows[i]["RechargeOperatorRefID"].ToString();
                            ds2.operator_name = dst.Tables[0].Rows[i]["RechargeOperatorName"].ToString();
                            ds2.recharge_type = dst.Tables[0].Rows[i]["RechargeType"].ToString();
                            ds2.customer_number = dst.Tables[0].Rows[i]["CustomerMobileNo"].ToString();                           
                            ds2.amount = dst.Tables[0].Rows[i]["Amount"].ToString();

                            ds2.agent_commision = dst.Tables[0].Rows[i]["AgentComm"].ToString();
                            ds2.agent_tds_amount = dst.Tables[0].Rows[i]["AgentCommTDS"].ToString();
                            ds2.transaction_status = dst.Tables[0].Rows[i]["TransactionStatus"].ToString();
                            lst_operator.Add(ds2);
                        }
                        ds.response_code = "200";
                        ds.response_message = "Success";
                        ds.data = lst_operator;
                    }
                    else
                    {
                        ds.response_code = "100";
                        ds.response_message = "No record found";
                        ds.data = lst_operator;
                    }
                }
                else
                {
                    ds.response_code = "101";
                    ds.response_message = "No record found";
                    ds.data = lst_operator;
                }
            }
            else
            {
                ds.response_code = "102";
                ds.response_message = "No record found";
                ds.data = lst_operator;
            }
            return ds;
        }


        public RechargeStatusCheckResponseAPI getPendingrecord()
        {
            RechargeStatusCheckResponseAPI ds = new RechargeStatusCheckResponseAPI();
            List<RechargeStatusCheckResponse> lst_operator = new List<RechargeStatusCheckResponse>();
            DataSet dst = RR.GetRechargePendinglist();
            if (dst != null)
            {
                if (dst.Tables.Count > 0)
                {
                    if (dst.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dst.Tables[0].Rows.Count; i++)
                        {
                            RechargeStatusCheckResponse ds2 = new RechargeStatusCheckResponse();
                            ds2.agent_ref_id = dst.Tables[0].Rows[i]["AgentRefID"].ToString();
                            ds2.transaction_date = dst.Tables[0].Rows[i]["TransactionDate"].ToString();
                            ds2.rpid = dst.Tables[0].Rows[i]["RPID"].ToString();
                            ds2.transaction_id = dst.Tables[0].Rows[i]["TransactionID"].ToString();
                            lst_operator.Add(ds2);
                        }
                        ds.response_code = "200";
                        ds.response_message = "Success";
                        ds.data = lst_operator;
                    }
                    else
                    {
                        ds.response_code = "100";
                        ds.response_message = "No record found";
                        ds.data = lst_operator;
                    }
                }
                else
                {
                    ds.response_code = "101";
                    ds.response_message = "No record found";
                    ds.data = lst_operator;
                }
            }
            else
            {
                ds.response_code = "102";
                ds.response_message = "No record found";
                ds.data = lst_operator;
            }
            return ds;
        }
    }
}
