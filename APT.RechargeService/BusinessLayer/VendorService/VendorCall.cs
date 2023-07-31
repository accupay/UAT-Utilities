using APT.RechargeService.BusinessObject.Model;
using APT.RechargeService.DattaAccessLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Security;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Channels;
using System.Transactions;
using APT.RechargeService.BusinessLayer;
using System;

namespace APT.RechargeService.BusinessLayer.VendorService
{
    public class VendorCall
    {
        RechargeRepository rrr = new RechargeRepository();
        RechargeAPI ra= new RechargeAPI();
        private readonly string AMRCheckStatus;
        public static IConfiguration Configuration { get; set; }
        public static LogService _logService = new LogService();
        private readonly string statuscheckurl;
        private readonly string transactionurl;
        private readonly string userid;
        private readonly string token;
        private readonly string PlanSecretKey;
        private readonly string _txnconnString;
        private readonly string _masterconnString;
        private readonly string Userkey;
        public VendorCall()
        {
            Configuration = GetConfigurations();
            statuscheckurl = Configuration["VendorUrls:AMRStatuscheckURL"];
            transactionurl = Configuration["VendorUrls:AMRTransactionURL"];
            userid = Configuration["VendorUrls:userid"];
            token = Configuration["VendorUrls:token"];
            PlanSecretKey = Configuration["VendorUrls:PlanSecretKey"];
            Userkey = Configuration["VendorUrls:PlannAPIUserID"];
            _masterconnString = Configuration.GetConnectionString("MasterDBConnection");
            _txnconnString = Configuration.GetConnectionString("TransactionDBConnection");
        }
        public IConfiguration GetConfigurations()
        {
            Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
            return Configuration;
        }
        public RechargeStatusCheckResponseAPI CheckStatus(RechargeStatusCheckRequest rscrq)
        {
            _logService.WriteToFile("CheckStatus transactionid :" + rscrq.transactionid + "--User id :" + rscrq.user_id);
            string lstrReturn = string.Empty;
            string lstrRequest = string.Empty;
            SqlCommand objCMD = new SqlCommand();
            HttpResponseMessage Response = new HttpResponseMessage();
            Dictionary<string, string> objResp = new Dictionary<string, string>();
            RechargeStatusCheckResponseAPI rscr = new RechargeStatusCheckResponseAPI();
            RechargePaymentUpdateRequestAPI objRequest = new RechargePaymentUpdateRequestAPI();
            try
            {
                string rpid = "";
                string trdate = "";
                DataSet dst = rrr.GetRechargePendinglist();
                if(dst!= null)
                {
                    if(dst.Tables.Count>0)
                    {
                        if (dst.Tables[0].Rows.Count>0)
                        {
                            for (int i = 0; i < dst.Tables[0].Rows.Count; i++)
                            {

                                if (dst.Tables[0].Rows[i]["AgentRefID"].ToString() == rscrq.user_id && dst.Tables[0].Rows[i]["TransactionID"].ToString() == rscrq.transactionid)
                                {
                                    rpid = dst.Tables[0].Rows[i]["RPID"].ToString();
                                    trdate = dst.Tables[0].Rows[i]["TransactionDate"].ToString();
                                    _logService.WriteToFile("CheckStatus transactionid :" + rscrq.transactionid + "--User id :" + rscrq.user_id + "--RPID:" + rpid + "--Transaction date:" + trdate);
                                    DateTime ddt = Convert.ToDateTime(trdate);
                                    trdate = ddt.ToString("dd MMM yyyy");                                
                                   
                                    //http://status.ambikamultiservices.com/API/StatusCheck?UserID=12666&Token=9539363f2b5a2022e100b606445feca0&RPID=S23071219280506ABC2D&AGENTID=R20230712157&Optional1=12 JUL 2023&Format=1
                                    string requrl = statuscheckurl + "UserID=" + userid + "&Token=" + token + "&RPID=" + rpid + "&AGENTID=" + rscrq.transactionid + "&Optional1=" + trdate + "&Format=1";
                                    _logService.WriteToFile("CheckStatus transactionid :" + rscrq.transactionid + "--requrl :" + requrl);
                                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(requrl);
                                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });    //	 Could Not Establish Trust Relationship For The SSL/ TLS Secure Channel
                                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                                    httpWebRequest.ContentType = "application/json; charset=utf-8";
                                    httpWebRequest.Method = "POST";
                                    var hr = (HttpWebResponse)httpWebRequest.GetResponse();
                                    using (var sr = new StreamReader(hr.GetResponseStream()))
                                    {
                                        var jres = sr.ReadToEnd();
                                        _logService.WriteToFile("CheckStatus transactionid :" + rscrq.transactionid + "--response :" + jres);
                                        dynamic jsplit = JsonConvert.DeserializeObject(jres);
                                        if (jsplit != null)
                                        {
                                            if (jsplit.status == "2" || jsplit.status == "3")
                                            {
                                                objRequest.message = jsplit.msg;
                                                objRequest.rpid = jsplit.rpid;
                                                objRequest.transaction_id = rscrq.transactionid;
                                                objRequest.opid = jsplit.opid;
                                                objRequest.request = requrl;
                                                objRequest.response = jres;
                                                objRequest.response_code = jsplit.status;
                                                objRequest.response_description = jsplit.msg;
                                                objRequest.vendor_agent_no = jsplit.agentid;
                                                DataSet dsts = rrr.UpdateTransaction(objRequest);
                                                if (dsts != null)
                                                {
                                                    if (dsts.Tables.Count > 0)
                                                    {
                                                        if (dsts.Tables[0].Rows.Count > 0)
                                                        {
                                                            rscr.response_code = dsts.Tables[0].Rows[0][0].ToString();
                                                            rscr.response_message = dsts.Tables[0].Rows[0][1].ToString();
                                                            _logService.WriteToFile("CheckStatus transactionid :" + rscrq.transactionid + "--response_code :" + rscr.response_code + "--response_message :" + rscr.response_message);
                                                            return rscr;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    rscr.response_code = "201";
                                                    rscr.response_message = "No record found";
                                                }
                                            }
                                            else
                                            {
                                                rscr.response_code = "202";
                                                rscr.response_message = "Pending";
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    rscr.response_code = "202";
                                    rscr.response_message = "No record found";
                                }
                            }
                        }
                    }
                }
                _logService.WriteToFile("CheckStatus transactionid :" + rscrq.transactionid + "--response_code :" + rscr.response_code + "--response_message :" + rscr.response_message);
                return rscr;
            }
            catch (System.Exception ex)
            {
                rscr.response_code = "101";
                _logService.WriteToFile("CheckStatus transactionid :" + rscrq.transactionid + "--response_code :" + rscr.response_code + "--response_message :" + ex.Message);
                return rscr;
            }
        }

        public RechargePaymentresponseAPI TransactionAPI(RechargePaymentRequest rrs)
        {
            _logService.WriteToFile("TransactionAPI Request :" + rrs + "--CustomerMobile :" + rrs.customer_mobile_no);
            string lstrReturn = string.Empty;
            string lstrRequest = string.Empty;
            SqlCommand objCMD = new SqlCommand();
            HttpResponseMessage Response = new HttpResponseMessage();
            Dictionary<string, string> objResp = new Dictionary<string, string>();
            RechargePaymentresponseAPI data = new RechargePaymentresponseAPI();
            RechargeStatusUpdate rurq = new RechargeStatusUpdate();
            try
            {
                DataSet dst = rrr.InsertTransaction(rrs);
                if (dst != null)
                {
                    if (dst.Tables.Count > 0)
                    {
                        if (dst.Tables[0].Rows.Count > 0)
                        {
                            if (dst.Tables[0].Rows[0][0].ToString() == "100")
                            {
                                _logService.WriteToFile("TransactionAPI After transaction id generate :" + dst.Tables[0].Rows[0][2].ToString());
                                string requrl = transactionurl + "UserID=" + userid + "&&Token=" + token + "&Account=" + rrs.customer_mobile_no + "&Amount=" + rrs.amount + "&SPKey=" + rrs.spkey + "&APIRequestID=" + dst.Tables[0].Rows[0][2].ToString() + "&Optional1=&Optional2=&Optional3=&Optional4=&GEOCode=" + rrs.lat + "," + rrs.longs + "&CustomerNumber=" + rrs.customer_mobile_no + "&Pincode=" + rrs.pincode + "&Format=1";
                                _logService.WriteToFile("TransactionAPI request URL :" + requrl);
                                var httpWebRequest = (HttpWebRequest)WebRequest.Create(requrl);
                                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });    //	 Could Not Establish Trust Relationship For The SSL/ TLS Secure Channel
                                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                                httpWebRequest.ContentType = "application/json; charset=utf-8";
                                httpWebRequest.Method = "GET";
                                var hr = (HttpWebResponse)httpWebRequest.GetResponse();
                                using (var sr = new StreamReader(hr.GetResponseStream()))
                                {
                                    var jres = sr.ReadToEnd();
                                    dynamic jsplit = JsonConvert.DeserializeObject(jres);
                                    _logService.WriteToFile("TransactionAPI response:" + jres);
                                    if (jsplit != null)
                                    {
                                        rurq.message = jsplit.msg;
                                        rurq.rpid = jsplit.rpid;
                                        rurq.transaction_id = dst.Tables[0].Rows[0][2].ToString();
                                        rurq.opid = jsplit.opid;
                                        rurq.request = requrl;
                                        rurq.response = jres;
                                        rurq.response_code = jsplit.status;
                                        rurq.response_description = jsplit.msg;
                                        rurq.vendor_agent_id = jsplit.agentid;
                                        DataSet dsts = rrr.InitialStatusUpdate(rurq);
                                        _logService.WriteToFile("TransactionAPI update response:" + dsts.Tables[0].Rows[0][1].ToString());
                                        if (dsts != null)
                                        {
                                            if (dsts.Tables.Count > 0)
                                            {
                                                if (dsts.Tables[0].Rows.Count > 0)
                                                {
                                                    data.response_code = dsts.Tables[0].Rows[0][0].ToString();
                                                    data.response_description = dsts.Tables[0].Rows[0][1].ToString();
                                                    data.amount = rrs.amount;
                                                    data.transaction_id = dst.Tables[0].Rows[0][2].ToString();
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        rurq.message = "";
                                        rurq.rpid = "";
                                        rurq.transaction_id = dst.Tables[0].Rows[0][2].ToString();
                                        rurq.opid = "";
                                        rurq.request = requrl;
                                        rurq.response = jres;
                                        rurq.response_code = "1";
                                        rurq.response_description = "Pending";
                                        rurq.vendor_agent_id = "";
                                        DataSet dsts = rrr.InitialStatusUpdate(rurq);
                                        _logService.WriteToFile("TransactionAPI update response:" + dsts.Tables[0].Rows[0][1].ToString());
                                        if (dsts != null)
                                        {
                                            if (dsts.Tables.Count > 0)
                                            {
                                                if (dsts.Tables[0].Rows.Count > 0)
                                                {
                                                    data.response_code = dsts.Tables[0].Rows[0][0].ToString();
                                                    data.response_description = dsts.Tables[0].Rows[0][1].ToString();
                                                    data.amount = rrs.amount;
                                                    data.transaction_id = dst.Tables[0].Rows[0][2].ToString();
                                                }
                                            }
                                        }
                                        _logService.WriteToFile("TransactionAPI failed jsplit response:" + jsplit);
                                    }
                                }
                                return data;
                            }
                            else
                            {
                                data.response_code = dst.Tables[0].Rows[0][0].ToString();
                                data.response_description = dst.Tables[0].Rows[0][1].ToString();
                                _logService.WriteToFile("TransactionAPI failed response:" + data.response_description);
                            }
                        }
                    }
                }
                else
                {
                    data.response_code = "101";
                    _logService.WriteToFile("TransactionAPI failed response:TransactionID generation Failed");
                }
                return data;
            }
            catch (System.Exception ex)
            {
                data.response_code = "101";
                data.response_description = ex.Message;
                _logService.WriteToFile("TransactionAPI failed response:" + ex.Message);
                return data;
            }
        }

        public RechargePlanResponseAPI PlanList(RechargePlanRequestAPI rpra)
        {
            RechargePlanResponseAPI objRres = new RechargePlanResponseAPI();
            DataSet data = new DataSet();
            string lstrReturn = string.Empty;
            string lstrRequest = string.Empty;
            SqlCommand objCMD = new SqlCommand();
            HttpResponseMessage Response = new HttpResponseMessage();
            Dictionary<string, string> objResp = new Dictionary<string, string>();
            List<RechargePlanResponse> vcs = new List<RechargePlanResponse>();
            try
            {
                if(rpra.recharge_type=="1")
                {
                    rpra.recharge_type = "prepaid";
                }
                else if (rpra.recharge_type == "3")
                {
                    rpra.recharge_type = "dth";
                }

                if(rpra.sp_key=="3")//airtel
                {
                    rpra.sp_key = "1";
                }
                else if (rpra.sp_key == "116")//jio
                {
                    rpra.sp_key = "31";
                }
                else if(rpra.sp_key == "VIL")//v and i
                {
                    rpra.sp_key = "499";
                }
                else if(rpra.sp_key == "5") // bsnl
                {
                    rpra.sp_key = "6";
                }
                string txnid = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.DayOfYear.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                string cnct = txnid + "|" + PlanSecretKey;
                string securehash = SHA512(cnct);
                string requrl = "https://api.komparify.com/api/v2/topuptype/" + rpra.topup_type + ".json?region_id=5&unique_provider_id=" + rpra.sp_key + "&type=" + rpra.recharge_type + "&&api_user_id=" + Userkey + "&txnid=" + txnid + "&securehash=" + securehash;
                _logService.WriteToFile("Plan API Request URL:" + requrl);
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(requrl);
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });    //	 Could Not Establish Trust Relationship For The SSL/ TLS Secure Channel
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "GET";
                var hr = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var sr = new StreamReader(hr.GetResponseStream()))
                {
                    var jres = sr.ReadToEnd();
                    _logService.WriteToFile("Plan API Response URL:" + jres);
                    List<RechargePlanResponse> myDeserializedClass = JsonConvert.DeserializeObject<List<RechargePlanResponse>>(jres);
                    if (myDeserializedClass != null)
                    {
                        for (int i = 0; i < myDeserializedClass.Count; i++)
                        {
                            RechargePlanResponse vc = new RechargePlanResponse();
                            vc.price = myDeserializedClass[i].price;
                            vc.description = myDeserializedClass[i].description;
                            vc.weight = myDeserializedClass[i].weight;
                            vc.plan_url = myDeserializedClass[i].plan_url;
                            vc.talktime = myDeserializedClass[i].talktime;
                            vc.id = myDeserializedClass[i].id;
                            vc.name = myDeserializedClass[i].name;
                            vc.validity = myDeserializedClass[i].validity;
                            vc.validity_string = myDeserializedClass[i].validity_string;
                            vcs.Add(vc);

                        }
                        objRres.response_code = "200";
                        objRres.response_message = "Success";
                        objRres.data = vcs;
                    }
                    else
                    {
                        objRres.response_code = "101";
                        objRres.response_message = "Failed";
                        return objRres;
                    }
                }
                return objRres;
            }
            catch (System.Exception ex)
            {
                objRres.response_code = "101";
                objRres.response_message = "Failed";
                return objRres;
            }
        }

        public static string SHA512(string input)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(input);
            using (var hash = System.Security.Cryptography.SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);
                var hashedInputStringBuilder = new System.Text.StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                return hashedInputStringBuilder.ToString();
            }
        }


    }
}
