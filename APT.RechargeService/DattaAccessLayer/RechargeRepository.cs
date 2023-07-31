using APT.RechargeService.BusinessLayer;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using APT.RechargeService.BusinessObject.Model;

namespace APT.RechargeService.DattaAccessLayer
{
    public class RechargeRepository
    {
        private readonly string _connString;
        private readonly string _txnconnString;
        private readonly string _masterconnString;

        public static IConfiguration Configuration { get; set; }
        public static LogService _log = new LogService();
        public RechargeRepository()
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
        public DataSet GetOperatorList(int RechargeType)
        {
            using (var connection = new SqlConnection(_masterconnString))
            {
                connection.Open();
                using (var cmd = new SqlCommand("APT_GetRechargeOperator", connection))
                {
                    cmd.Parameters.Add(new SqlParameter("RechargeType", RechargeType));
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        var dataAdaper = new SqlDataAdapter(cmd);
                        DataSet dataSet = new DataSet();
                        dataAdaper.Fill(dataSet);

                        return dataSet;
                    }
                    catch (Exception ex)
                    {
                        //_log.WriteExceptionLog(MethodBase.GetCurrentMethod().Name, ex.Message, ex.StackTrace, AgentRefID);

                        return null;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
        public DataSet GetDataPlanTopupTypeList(int RechargeTypeRefID)
        {
            using (var connection = new SqlConnection(_masterconnString))
            {
                connection.Open();
                using (var cmd = new SqlCommand("APT_GetRechargePlanTopupType", connection))
                {
                    cmd.Parameters.Add(new SqlParameter("RechargeTypeRefID", RechargeTypeRefID));
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        var dataAdaper = new SqlDataAdapter(cmd);
                        DataSet dataSet = new DataSet();
                        dataAdaper.Fill(dataSet);
                        DataSet dt=new DataSet();
                        return dataSet;
                    }
                    catch (Exception ex)
                    {
                        //_log.WriteExceptionLog(MethodBase.GetCurrentMethod().Name, ex.Message, ex.StackTrace, AgentRefID);

                        return null;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        public DataSet GetDataPlanRegionsList()
        {
            using (var connection = new SqlConnection(_masterconnString))
            {
                connection.Open();
                using (var cmd = new SqlCommand("APT_GetRechargeOperator", connection))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        var dataAdaper = new SqlDataAdapter(cmd);
                        DataSet dataSet = new DataSet();
                        dataAdaper.Fill(dataSet);

                        return dataSet;
                    }
                    catch (Exception ex)
                    {
                        //_log.WriteExceptionLog(MethodBase.GetCurrentMethod().Name, ex.Message, ex.StackTrace, AgentRefID);

                        return null;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        public DataSet GetDataPlanTopupType()
        {
            using (var connection = new SqlConnection(_masterconnString))
            {
                connection.Open();
                using (var cmd = new SqlCommand("APT_GetRechargeOperator", connection))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        var dataAdaper = new SqlDataAdapter(cmd);
                        DataSet dataSet = new DataSet();
                        dataAdaper.Fill(dataSet);

                        return dataSet;
                    }
                    catch (Exception ex)
                    {
                        //_log.WriteExceptionLog(MethodBase.GetCurrentMethod().Name, ex.Message, ex.StackTrace, AgentRefID);

                        return null;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
        public DataSet GetPlanList()
        {
            using (var connection = new SqlConnection(_masterconnString))
            {
                connection.Open();
                using (var cmd = new SqlCommand("APT_GetRechargeAmountPlanValidation", connection))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        var dataAdaper = new SqlDataAdapter(cmd);
                        DataSet dataSet = new DataSet();
                        dataAdaper.Fill(dataSet);

                        return dataSet;
                    }
                    catch (Exception ex)
                    {
                        //_log.WriteExceptionLog(MethodBase.GetCurrentMethod().Name, ex.Message, ex.StackTrace, AgentRefID);

                        return null;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        public DataSet GetRechargeType()
        {
            using (var connection = new SqlConnection(_masterconnString))
            {
                connection.Open();
                using (var cmd = new SqlCommand("APT_GetRechargeType", connection))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        var dataAdaper = new SqlDataAdapter(cmd);
                        DataSet dataSet = new DataSet();
                        dataAdaper.Fill(dataSet);

                        return dataSet;
                    }
                    catch (Exception ex)
                    {
                        //_log.WriteExceptionLog(MethodBase.GetCurrentMethod().Name, ex.Message, ex.StackTrace, AgentRefID);

                        return null;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
        public DataSet GetRechargePendinglist()
        {
            using (var connection = new SqlConnection(_txnconnString))
            {
                connection.Open();
                using (var cmd = new SqlCommand("APT_GetPendingRechargeTransaction", connection))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        var dataAdaper = new SqlDataAdapter(cmd);
                        DataSet dataSet = new DataSet();
                        dataAdaper.Fill(dataSet);

                        return dataSet;
                    }
                    catch (Exception ex)
                    {
                        //_log.WriteExceptionLog(MethodBase.GetCurrentMethod().Name, ex.Message, ex.StackTrace, AgentRefID);

                        return null;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
        //public DataSet StatusUpdate(RechargeStatusCheckRequest Request)
        //{
        //    using (var connection = new SqlConnection(_txnconnString))
        //    {
        //        connection.Open();
        //        using (var cmd = new SqlCommand("APT_UpdateRechargeTransaction", connection))
        //        {
        //            cmd.Parameters.Add(new SqlParameter("TransactionID", Request.transaction_id));
        //            cmd.Parameters.Add(new SqlParameter("RPID", ""));
        //            cmd.Parameters.Add(new SqlParameter("VendorAgentID", ""));
        //            cmd.Parameters.Add(new SqlParameter("OPID", ""));
        //            cmd.Parameters.Add(new SqlParameter("Request", Request.request));
        //            cmd.Parameters.Add(new SqlParameter("Response", Request.response));
        //            cmd.Parameters.Add(new SqlParameter("Message", Request.message));
        //            cmd.Parameters.Add(new SqlParameter("ResponseCode", Request.response_code));
        //            cmd.Parameters.Add(new SqlParameter("ResponseDescription", Request.response_description));
        //            try
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                var dataAdaper = new SqlDataAdapter(cmd);
        //                DataSet dataSet = new DataSet();
        //                dataAdaper.Fill(dataSet);
        //                return dataSet;
        //            }
        //            catch (Exception ex)
        //            {
        //                return null;
        //            }
        //            finally
        //            {
        //                connection.Close();
        //            }
        //        }
        //    }
        //}


        public DataSet InitialStatusUpdate(RechargeStatusUpdate Request)
        {
            using (var connection = new SqlConnection(_txnconnString))
            {
                connection.Open();
                using (var cmd = new SqlCommand("APT_UpdateRechargeTransaction", connection))
                {
                    cmd.Parameters.Add(new SqlParameter("TransactionID", Request.transaction_id));
                    cmd.Parameters.Add(new SqlParameter("RPID", Request.rpid));
                    cmd.Parameters.Add(new SqlParameter("VendorAgentID", Request.vendor_agent_id));
                    cmd.Parameters.Add(new SqlParameter("OPID", Request.opid));
                    cmd.Parameters.Add(new SqlParameter("Request", Request.request));
                    cmd.Parameters.Add(new SqlParameter("Response", Request.response));
                    cmd.Parameters.Add(new SqlParameter("Message", Request.message));
                    cmd.Parameters.Add(new SqlParameter("ResponseCode", Request.response_code));
                    cmd.Parameters.Add(new SqlParameter("ResponseDescription", Request.response_description));
                    cmd.Parameters.Add(new SqlParameter("StatusCheck", 2));//from initialupdate
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        var dataAdaper = new SqlDataAdapter(cmd);
                        DataSet dataSet = new DataSet();
                        dataAdaper.Fill(dataSet);
                        return dataSet;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
        public DataSet InsertTransaction(RechargePaymentRequest Request)
        {
            using (var connection = new SqlConnection(_txnconnString))
            {
                connection.Open();
                using (var cmd = new SqlCommand("APT_InsertRechargeTransaction", connection))
                {
                    cmd.Parameters.Add(new SqlParameter("RechargeVendorRefID",Convert.ToInt32(Request.recharge_vendor_ref_id)));
                    cmd.Parameters.Add(new SqlParameter("RechargeOperatorRefID", Convert.ToInt32(Request.recharge_operator_ref_id)));
                    cmd.Parameters.Add(new SqlParameter("RechargeTypeRefID", Convert.ToInt32(Request.recharge_type_ref_id)));
                    cmd.Parameters.Add(new SqlParameter("Amount", Request.amount));
                    cmd.Parameters.Add(new SqlParameter("CustomerMobileNo", Request.customer_mobile_no));
                    cmd.Parameters.Add(new SqlParameter("AgentMobileNo", Request.agent_mobile_no));
                    cmd.Parameters.Add(new SqlParameter("AgentRefID", Convert.ToInt32(Request.agent_ref_id)));
                    cmd.Parameters.Add(new SqlParameter("UserRefID", Convert.ToInt32(Request.user_ref_id)));
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        var dataAdaper = new SqlDataAdapter(cmd);
                        DataSet dataSet = new DataSet();
                        dataAdaper.Fill(dataSet);
                        return dataSet;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        public DataSet UpdateTransaction(RechargePaymentUpdateRequestAPI Request)
        {
            using (var connection = new SqlConnection(_txnconnString))
            {
                connection.Open();
                using (var cmd = new SqlCommand("APT_UpdateRechargeTransaction", connection))
                {
                    cmd.Parameters.Add(new SqlParameter("TransactionID", Request.transaction_id));
                    cmd.Parameters.Add(new SqlParameter("RPID", Request.rpid));
                    cmd.Parameters.Add(new SqlParameter("VendorAgentID", Request.vendor_agent_no));
                    cmd.Parameters.Add(new SqlParameter("OPID", Request.opid));
                    cmd.Parameters.Add(new SqlParameter("Request", Request.request));
                    cmd.Parameters.Add(new SqlParameter("Response", Request.response));
                    cmd.Parameters.Add(new SqlParameter("Message", Request.message));
                    cmd.Parameters.Add(new SqlParameter("ResponseCode", Request.response_code));
                    cmd.Parameters.Add(new SqlParameter("ResponseDescription", Request.response_description));
                    cmd.Parameters.Add(new SqlParameter("StatusCheck", 1));//from Page
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        var dataAdaper = new SqlDataAdapter(cmd);
                        DataSet dataSet = new DataSet();
                        dataAdaper.Fill(dataSet);
                        return dataSet;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
        
        public DataSet GetRetailerTransactionList(RetailerReportRequest rrrq)
        {
            using (var connection = new SqlConnection(_txnconnString))
            {
                connection.Open();
                using (var cmd = new SqlCommand("APT_GetRetailerRechargeTransactionReport", connection))
                {
                    cmd.Parameters.Add(new SqlParameter("FromDate", rrrq.from_date));
                    cmd.Parameters.Add(new SqlParameter("ToDate", rrrq.to_date));
                    cmd.Parameters.Add(new SqlParameter("SearchOption", rrrq.search_option));
                    cmd.Parameters.Add(new SqlParameter("SearchValue", rrrq.search_Value));
                    cmd.Parameters.Add(new SqlParameter("RetailerRefID", rrrq.user_id));
                    cmd.Parameters.Add(new SqlParameter("TransactionStatusRefID", rrrq.status));
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        var dataAdaper = new SqlDataAdapter(cmd);
                        DataSet dataSet = new DataSet();
                        dataAdaper.Fill(dataSet);

                        return dataSet;
                    }
                    catch (Exception ex)
                    {
                        //_log.WriteExceptionLog(MethodBase.GetCurrentMethod().Name, ex.Message, ex.StackTrace, AgentRefID);

                        return null;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
    }
}
