using INCHEQS.Common;
using INCHEQS.ConfigVerification.Resource;
using INCHEQS.DataAccessLayer;
using INCHEQS.Security.Account;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Globalization;

namespace INCHEQS.ConfigVerification.VerificationLimit
{
	public class VerificationLimitDao : IVerificationLimitDao
	{
		private readonly ApplicationDbContext dbContext;

		public VerificationLimitDao(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}
		public bool checkClassExist(string fldClass)
		{
            bool strs = false;
            DataTable dataTable = new DataTable();
            List<SqlParameter> sqlParameterNext = new List<SqlParameter>();
            sqlParameterNext.Add(new SqlParameter("@fldClass", fldClass));

            dataTable = this.dbContext.GetRecordsAsDataTableSP("spcgClassExist", sqlParameterNext.ToArray());

            if (dataTable.Rows.Count > 0)
            {
                strs = true;


            }
            else
            {
                strs = false;

            }

            return strs;
        }
        public bool checkIfaRecordPendingforApproval(string fldClass)
        {
            bool strs = false;
            DataTable dataTable = new DataTable();
            List<SqlParameter> sqlParameterNext = new List<SqlParameter>();
            sqlParameterNext.Add(new SqlParameter("@fldClass", fldClass));

            dataTable = this.dbContext.GetRecordsAsDataTableSP("spcgRecordPendingForApproval", sqlParameterNext.ToArray());

            if (dataTable.Rows.Count > 0)
            {
                strs = false;
            }
            else
            {
                strs = true;

            }

            return strs;
        }
  
        public DataTable Find(string classId)
        {
            List<SqlParameter> sqlParameterNext = new List<SqlParameter>();
            sqlParameterNext.Add(new SqlParameter("@fldClass", classId));
            return this.dbContext.GetRecordsAsDataTableSP("spcgFind", sqlParameterNext.ToArray());
        }

        public VerificationLimitModel GetVerifyLimit(string classId)
        {
            DataTable dataTable = new DataTable();
            List<SqlParameter> sqlParameterNext = new List<SqlParameter>();
            sqlParameterNext.Add(new SqlParameter("@fldClass", classId));
            dataTable = this.dbContext.GetRecordsAsDataTableSP("spcgFind", sqlParameterNext.ToArray());
            VerificationLimitModel verificationLimitModel = new VerificationLimitModel();
            
            
            if (dataTable.Rows.Count > 0)
            {
                DataRow item = dataTable.Rows[0];
                verificationLimitModel.verifyLimitClass = item["fldClass"].ToString();
                verificationLimitModel.verifyLimitDesc = item["fldLimitDesc"].ToString();
                verificationLimitModel.fldConcatenate = item["fldConcatenate"].ToString();
                verificationLimitModel.fld1stType = item["fld1stType"].ToString();
                verificationLimitModel.fld2ndType = item["fld2ndType"].ToString();
                //verificationLimitModel.fld1stAmt = Convert.ToSingle(item["fld1stAmt"].ToString());
                //verificationLimitModel.fld1stAmt = float.Parse(item["fld1stAmt"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                verificationLimitModel.fld1stAmt = Convert.ToDouble(item["fld1stAmt"].ToString());
                if (!string.IsNullOrEmpty(item["fld2ndAmt"].ToString()))
                {
                    //verificationLimitModel.fld2ndAmt = Convert.ToSingle(item["fld2ndAmt"].ToString());
                    //verificationLimitModel.fld2ndAmt = float.Parse(item["fld2ndAmt"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                    verificationLimitModel.fld2ndAmt = Convert.ToDouble(item["fld2ndAmt"].ToString());
                }
                else
                {
                    verificationLimitModel.fld2ndAmt = 0f;
                }
            }
            return verificationLimitModel;
        }

        public DataTable ListVerificationLimit()
        {
            return this.dbContext.GetRecordsAsDataTableSP("spcgListVerificationLimitMaster", null);
        }

        public void Update(FormCollection collection, string currentUserId)
        {
            string item = collection["secondType"] ?? "";
            List<SqlParameter> sqlParameterNext = new List<SqlParameter>();
            sqlParameterNext.Add(new SqlParameter("@fldClass", collection["txtclass"]));
            sqlParameterNext.Add(new SqlParameter("@fld1stAmt", Convert.ToDouble(collection["firstAmount"]))); //original: collection["firstAmount"].ToString()
            sqlParameterNext.Add(new SqlParameter("@fld2ndAmt", Convert.ToDouble(collection["secondAmount"]))); //collection["secondAmount"].ToString()
            sqlParameterNext.Add(new SqlParameter("@fld1stType", collection["firstType"]));
            sqlParameterNext.Add(new SqlParameter("@fld2ndType", item));
            sqlParameterNext.Add(new SqlParameter("@fldConcatenate", collection["concat"]));
            sqlParameterNext.Add(new SqlParameter("@fldLimitDesc", collection["VerifyLimitDesc"]));
            sqlParameterNext.Add(new SqlParameter("@fldUpdateUserId", currentUserId));
            sqlParameterNext.Add(new SqlParameter("@fldUpdateTimeStamp", (object)DateTime.Now));
            this.dbContext.ExecuteNonQuery(CommandType.StoredProcedure, "spcuVerificationLimitMaster", sqlParameterNext.ToArray());
        }

        public void CreateInVerificationLimitMaster(string classId)
		{
            List<SqlParameter> sqlParameterNext = new List<SqlParameter>();
            try
            {
                sqlParameterNext.Add(new SqlParameter("@fldClass", classId));
                this.dbContext.ExecuteNonQuery(CommandType.StoredProcedure, "spciVerificationLimitMaster", sqlParameterNext.ToArray());


            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

		public void CreateInVerificationLimitMasterTemp(AccountModel currentUser, FormCollection collection, string currentUserId)
		{
            string item = collection["secondType"] ?? "";
            List<SqlParameter> sqlParameterNext = new List<SqlParameter>();
            sqlParameterNext.Add(new SqlParameter("@fldBankCode", currentUser.BankCode));
            sqlParameterNext.Add(new SqlParameter("@fldClass", collection["class"]));
            sqlParameterNext.Add(new SqlParameter("@fld1stAmt", Convert.ToDouble(collection["firstAmount"]))); //collection["firstAmount"].ToString()
            sqlParameterNext.Add(new SqlParameter("@fld2ndAmt", Convert.ToDouble(collection["secondAmount"]))); //collection["secondAmount"].ToString()
            sqlParameterNext.Add(new SqlParameter("@fld1stType", collection["firstType"]));
            sqlParameterNext.Add(new SqlParameter("@fld2ndType", item));
            sqlParameterNext.Add(new SqlParameter("@fldConcatenate", collection["concat"]));
            sqlParameterNext.Add(new SqlParameter("@fldLimitDesc", collection["VerifyLimitDesc"]));
            sqlParameterNext.Add(new SqlParameter("@fldUpdateUserId", currentUserId));
            sqlParameterNext.Add(new SqlParameter("@fldUpdateTimeStamp", DateUtils.GetCurrentDatetimeForSql()));
            sqlParameterNext.Add(new SqlParameter("@fldCreateUserId", currentUserId));
            sqlParameterNext.Add(new SqlParameter("@fldCreateTimeStamp", DateUtils.GetCurrentDatetimeForSql()));
            sqlParameterNext.Add(new SqlParameter("@fldApproveStatus", "A"));
            this.dbContext.ExecuteNonQuery(CommandType.StoredProcedure, "spciVerificationLimitMasterTemp", sqlParameterNext.ToArray());
        }
        public void AddToVerificationLimitMasterTempToDelete(string clasId)
        {
            List<SqlParameter> sqlParameterNext = new List<SqlParameter>();
            try
            {
                sqlParameterNext.Add(new SqlParameter("@fldClass", clasId));
                sqlParameterNext.Add(new SqlParameter("@fldApproveStatus", "D"));
                this.dbContext.ExecuteNonQuery(CommandType.StoredProcedure, "spciVerificationLimitMasterTempToUpdateDelete", sqlParameterNext.ToArray());


            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public void AddToVerificationLimitMasterTempToUpdate(FormCollection collection, string clasId,string currentUserId)
        {
            string item = collection["secondType"] ?? "";
            List<SqlParameter> sqlParameterNext = new List<SqlParameter>();
            sqlParameterNext.Add(new SqlParameter("@fldClass", collection["txtclass"]));
            sqlParameterNext.Add(new SqlParameter("@fld1stAmt", Convert.ToDouble(collection["firstAmount"]))); //collection["firstAmount"].ToString()
            sqlParameterNext.Add(new SqlParameter("@fld2ndAmt", Convert.ToDouble(collection["secondAmount"]))); //collection["secondAmount"].ToString()
            sqlParameterNext.Add(new SqlParameter("@fld1stType", collection["firstType"]));
            sqlParameterNext.Add(new SqlParameter("@fld2ndType", item));
            sqlParameterNext.Add(new SqlParameter("@fldConcatenate", collection["concat"]));
            sqlParameterNext.Add(new SqlParameter("@fldLimitDesc", collection["VerifyLimitDesc"]));
            sqlParameterNext.Add(new SqlParameter("@fldUpdateUserId", currentUserId));
            sqlParameterNext.Add(new SqlParameter("@fldUpdateTimeStamp", (object)DateTime.Now));
            sqlParameterNext.Add(new SqlParameter("@fldApproveStatus", "U"));
            this.dbContext.ExecuteNonQuery(CommandType.StoredProcedure, "spcuVerificationLimitMasterTemp", sqlParameterNext.ToArray());
        }

        public void DeleteInVerificationLimitMaster(string delete)
		{
            List<SqlParameter> sqlParameterNext = new List<SqlParameter>();
            string[] strArrays = delete.Split(new char[] { ',' });
            string idString = String.Join(",", strArrays);

            if (strArrays.Length != 0)
            {
                sqlParameterNext.Add(new SqlParameter("@fldClass", idString));
                this.dbContext.ExecuteNonQuery(CommandType.StoredProcedure, "spcdVerificationLimitMaster", sqlParameterNext.ToArray());
            }
        }

		public void DeleteInVerificationLimitMasterTemp(string clasId)
		{
            List<SqlParameter> sqlParameterNext = new List<SqlParameter>();
            try
            {
                sqlParameterNext.Add(new SqlParameter("@fldClass", clasId));
                this.dbContext.ExecuteNonQuery(CommandType.StoredProcedure, "spcdVerificationLimitMasterTemp", sqlParameterNext.ToArray());


            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
		public List<string> Validate(FormCollection collection)
		{
			List<string> strs = new List<string>();
			if (this.checkClassExist(collection["class"]))
			{
				strs.Add(Locale.Classalreadyexist);
			}else if (collection["class"]==""){
                strs.Add("Verfication Class cannot be empty.");
            }
            else if (collection["firstAmount"] == "")
            {
                strs.Add("1st Amount cannot be empty.");  
            }
			return strs;
		}
	}
}