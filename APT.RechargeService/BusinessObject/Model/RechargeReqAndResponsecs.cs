using System.Transactions;

namespace APT.RechargeService.BusinessObject.Model
{
    public class RechargeReqAndResponsecs
    {

    }
    #region Operator List
    public class OperatorListResponseAPI
    {
        public string response_code { get; set; } = string.Empty;
        public string response_message { get; set; } = string.Empty;
        public object data { get; set; }
    }

    public class OperatorListResponse
    {
        public string recharge_operator_ref_id { get; set; }
        public string recharge_operator { get; set; }
        public string recharge_type_ref_id { get; set; }
        public string product_key { get; set; }
        public string vendor_ref_id { get; set; }
    }
    #endregion

    #region PlanList
    public class PlanListResponseAPI
    {
        public string response_code { get; set; } = string.Empty;
        public string response_message { get; set; } = string.Empty;
        public object data { get; set; }
    }
    public class PlanListResponse
    {
        public string validate_ref_id { get; set; }
        public string name { get; set; }
        public string id { get; set; }
        public string price { get; set; }
        public string validity { get; set; }
        public string talktime { get; set; }
        public string validity_string { get; set; }
        public string plan_url { get; set; }
    }
    #endregion
   

    #region Recharge Type
    public class RechargeTypeAPI
    {
        public string response_code { get; set; } = string.Empty;
        public string response_message { get; set; } = string.Empty;
        public object data { get; set; }
    }
    public class RechargeTypeListResponse
    {
        public string recharger_type_ref_id { get; set; }
        public string recharge_type { get; set; }
    }
    #endregion

    public class RechargePaymentRequest
    {
        public string recharge_vendor_ref_id { get; set; }
        public string recharge_operator_ref_id { get; set; }
        public string recharge_type_ref_id { get; set; }
        public string amount { get; set; }
        public string customer_mobile_no { get; set; }
        public string agent_mobile_no { get; set; }
        public string agent_ref_id { get; set; }
        public string user_ref_id { get; set; }
        public string spkey { get; set; }
        public string lat { get; set; }
        public string longs { get; set; }
        public string pincode { get; set; }
    }

    #region Payment API
    public class RechargePaymentResponseAPI
    {
        public string reponse_code { get; set; }
        public string reponse_description { get; set; }
        public object data { get; set; }
    }
    public class RechargePaymentResponse
    {
        public string transaction_id { get; set; }
    }
    
    public class RechargePaymentRequestAPI
    {
        public string agent_mobilen_no { get; set; }
        public string customer_mobile_no { get; set; }
        public string amount { get; set; }
        public string spkey { get; set; }
        public string transactionid { get; set; }
        public string lat { get; set; }
        public string longs { get; set; }
        public string pincode { get; set; }
    }
    public class RechargePaymentUpdateRequestAPI
    {
        public string status { get; set; }
        public string message { get; set; }
        public string user_balance { get; set; }
        public string error_code { get; set; }
        public string user_account { get; set; }
        public string amount { get; set; }
        public string rpid { get; set; }
        public string transaction_id { get; set; }
        public string opid { get; set; }
        public string request { get; set; }
        public string response { get; set; }
        public string response_code { get; set; }
        public string response_description { get; set; }
        public string vendor_agent_no { get; set; }
    }
    public class RechargePaymentresponseAPI
    {
        public string status { get; set; }
        public string message { get; set; }
        public string amount { get; set; }
        public string transaction_id { get; set; }
        public string response_code { get; set; }
        public string response_description { get; set; }

    }
    #endregion


    #region status Check
    public class RechargeStatusCheckRequest
    {
        public string transactionid { get; set; }
        public string user_id { get; set; }
    }
    public class RechargeStatusCheckResponse
    {
        public string rpid { get; set; }
        public string transaction_id { get; set; }
        public string agent_ref_id { get; set; }
        public string transaction_date { get; set; }
    }


    public class RechargeStatusCheckResponseAPI
    {
        public string response_code { get; set; }
        public string response_message { get; set; }
        public object data { get; set; }
    }
    #endregion

    public class RechargeStatusUpdate
    {
        public string transaction_id { get; set; }
        public string rpid { get; set; }
        public string vendor_agent_id { get; set; }
        public string opid { get; set; }
        public string request { get; set; }
        public string response { get; set; }
        public string message { get; set; }
        public string response_code { get; set; }
        public string response_description { get; set; }
    }


    #region Operator List
    public class RechargePlanTopupTypeResponseAPI
    {
        public string response_code { get; set; } = string.Empty;
        public string response_message { get; set; } = string.Empty;
        public object data { get; set; }
    }

    public class RechargePlanTopupTypeResponse
    {
        public string recharge_topup_type_ref_id { get; set; }
        public string recharge_topup_type { get; set; }
    }
    #endregion

    #region Operator List

    public class RechargePlanRequestAPI
    {
        public string topup_type { get; set; }
        public string sp_key { get; set; }
        public string recharge_type { get; set; }
    }
    public class RechargePlanResponseAPI
    {
        public string response_code { get; set; } = string.Empty;
        public string response_message { get; set; } = string.Empty;
        public object data { get; set; }
    }

    public class RechargePlanResponse
    {
        public string name { get; set; }
        public string id { get; set; }
        public string price { get; set; }
        public string validity { get; set; }
        public string talktime { get; set; }
        public string weight { get; set; }
        public string validity_string { get; set; }
        public string plan_url { get; set; }
        public string description { get; set; }
        public string recharge_url { get; set; }
        public string recharge_type { get; set; }
        public string operator_only { get; set; }
    }
    #endregion

    public class RetailerReportRequest
    {
        public DateTime from_date { get; set; } 
        public DateTime to_date { get; set; }
        public string search_option { get; set; } 
        public string search_Value { get; set; } 
        public string status { get; set; }
        public string user_id { get; set; }
    }
    public class RetailerReportResponseAPI
    {
        public string response_code { get; set; } 
        public string response_message { get; set; }
        public object data { get; set; }
    }
    public class RetailerReportResponse
    {
        public string transaction_date { get; set; }
        public string transaction_id { get; set; }
        public string operator_id { get; set; }
        public string operator_name { get; set; }
        public string customer_number { get; set; }
        public string amount { get; set; }
        public string agent_commision { get; set; }
        public string retailer_tax { get; set; }
        public string tds_perc { get; set; }
        public string agent_tds_amount { get; set; }
        public string transaction_status { get; set; }
        public string recharge_type { get; set; }
        public string comments { get; set; }
    }
}
