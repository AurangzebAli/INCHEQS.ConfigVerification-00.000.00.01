using INCHEQS.Common;
using INCHEQS.ConfigVerification.Resource;
using INCHEQS.DataAccessLayer;
using INCHEQS.Security.Account;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace INCHEQS.ConfigVerification.ThresholdSetting
{
	public class ThresholdSettingDao : IThresholdSettingDao
	{
		private readonly ApplicationDbContext dbContext;

		public ThresholdSettingDao(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}
        //20200522
		public void AddtoThresholdSettingTempToDelete(string bankcode, string type, string level)

		{
            List<SqlParameter> sqlParameterNext = new List<SqlParameter>();
            try
            {
                sqlParameterNext.Add(new SqlParameter("@fldBankCode", bankcode));
                sqlParameterNext.Add(new SqlParameter("@fldVerificationThresholdType", type));
                sqlParameterNext.Add(new SqlParameter("@fldVerificationThresholdLevel", level));
                sqlParameterNext.Add(new SqlParameter("@fldApproveStatus", "D"));
                this.dbContext.ExecuteNonQuery(CommandType.StoredProcedure, "spciThresholdSettingTemptabletoDelete", sqlParameterNext.ToArray());


            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void CreateThresholdSetting(FormCollection col, string currentUser, string BankCode)
        {
            string str = "";
           // str = (col["fldSequence"] != "1" ? "Second Level Amount" : "First Level Amount");

            if (col["fldSequence"] == "4")
            {

                str = "Four Level Amount";
            }
            else if (col["fldSequence"] == "2")
            {

                str = "Second Level Amount";
            }
            else if (col["fldSequence"] == "1")
            {

                str = "First Level Amount";
            }
            else
            {

                str = "Third Level Amount";
            }

            List<SqlParameter> sqlParameterNext = new List<SqlParameter>();
            try
            {
                sqlParameterNext.Add(new SqlParameter("@fldVerificationThresholdType", col["fldType"]));
                sqlParameterNext.Add(new SqlParameter("@fldVerificationThresholdSeq", col["fldSequence"].ToString()));
                sqlParameterNext.Add(new SqlParameter("@fldVerificationThresholdLevel", str));
                sqlParameterNext.Add(new SqlParameter("@fldVerificationThresholdAmt", col["fldAmount"].ToString()));
                sqlParameterNext.Add(new SqlParameter("@fldBankCode", BankCode));
                sqlParameterNext.Add(new SqlParameter("@fldCreateUserId", currentUser));
                sqlParameterNext.Add(new SqlParameter("@fldCreateTimeStamp", (object)DateTime.Now));
                sqlParameterNext.Add(new SqlParameter("@fldUpdateUserId", currentUser));
                sqlParameterNext.Add(new SqlParameter("@fldUpdateTimeStamp", (object)DateTime.Now));

                this.dbContext.ExecuteNonQuery(CommandType.StoredProcedure, "spciThresholdSettingMainTable", sqlParameterNext.ToArray());


            }
            catch (Exception exception)
            {
                throw exception;
            }

        }

      
        public void CreateThresholdSettingfromChecker(string bankcode, string type, string level)
        {

            List<SqlParameter> sqlParameterNext = new List<SqlParameter>();
            try
            {
                sqlParameterNext.Add(new SqlParameter("@fldBankCode", bankcode));
                sqlParameterNext.Add(new SqlParameter("@fldVerificationThresholdType", type));
                sqlParameterNext.Add(new SqlParameter("@fldVerificationThresholdLevel", level));
                this.dbContext.ExecuteNonQuery(CommandType.StoredProcedure, "spciThresholdSettingMainTable2", sqlParameterNext.ToArray());


            }
            catch (Exception exception)
            {
                throw exception;
            }

        }
        public void DeleteInThresholdSettingTemp(FormCollection col)
        {
            List<SqlParameter> sqlParameterNext = new List<SqlParameter>();
            try
            {
                sqlParameterNext.Add(new SqlParameter("@fldVerificationThresholdType", col["fldType"]));
                sqlParameterNext.Add(new SqlParameter("@fldVerificationThresholdSeq", col["fldSequence"].ToString()));
                sqlParameterNext.Add(new SqlParameter("@fldVerificationThresholdAmt", col["fldAmount"].ToString()));
                this.dbContext.ExecuteNonQuery(CommandType.StoredProcedure, "spcdThresholdSettingTempTable", sqlParameterNext.ToArray());


            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        //20200522
        public void DeleteInThresholdSettingTemp2(string bankcode, string type, string level)
        {
            List<SqlParameter> sqlParameterNext = new List<SqlParameter>();
            try
            {
                sqlParameterNext.Add(new SqlParameter("@fldBankCode", bankcode));
                sqlParameterNext.Add(new SqlParameter("@fldVerificationThresholdType", type));
                sqlParameterNext.Add(new SqlParameter("@fldVerificationThresholdLevel", level));
                this.dbContext.ExecuteNonQuery(CommandType.StoredProcedure, "spcdThresholdSettingTempTable2", sqlParameterNext.ToArray());


            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        //20200522
        public void DeleteInThresholdSetting(string bankcode, string type, string level)
        {
            List<SqlParameter> sqlParameterNext = new List<SqlParameter>();
            try
            {
                sqlParameterNext.Add(new SqlParameter("@fldBankCode", bankcode));
                sqlParameterNext.Add(new SqlParameter("@fldVerificationThresholdType", type));
                sqlParameterNext.Add(new SqlParameter("@fldVerificationThresholdLevel", level));
                this.dbContext.ExecuteNonQuery(CommandType.StoredProcedure, "spcdThresholdSettingMainTable", sqlParameterNext.ToArray());


            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public void CreateThresholdSettingTemp(FormCollection col, string currentUser, string BankCode)
        {
            string str = "";
            str = (col["fldSequence"] != "1" ? "Second Level Amount" : "First Level Amount");

            List<SqlParameter> sqlParameterNext = new List<SqlParameter>();
            sqlParameterNext.Add(new SqlParameter("@fldVerificationThresholdType", col["fldType"]));
            sqlParameterNext.Add(new SqlParameter("@fldVerificationThresholdSeq", col["fldSequence"].ToString()));
            sqlParameterNext.Add(new SqlParameter("@fldVerificationThresholdLevel", str));
            sqlParameterNext.Add(new SqlParameter("@fldVerificationThresholdAmt", col["fldAmount"].ToString()));
            sqlParameterNext.Add(new SqlParameter("@fldBankCode", BankCode));
            sqlParameterNext.Add(new SqlParameter("@fldApproveStatus", "A"));
            sqlParameterNext.Add(new SqlParameter("@fldCreateUserId", currentUser));
            sqlParameterNext.Add(new SqlParameter("@fldCreateTimeStamp", (object)DateTime.Now));
            sqlParameterNext.Add(new SqlParameter("@fldUpdateUserId", currentUser));
            sqlParameterNext.Add(new SqlParameter("@fldUpdateTimeStamp", (object)DateTime.Now));

            this.dbContext.ExecuteNonQuery(CommandType.StoredProcedure, "spciThresholdSettingTempTable", sqlParameterNext.ToArray());

        }

        public DataTable GetThreshold(string thresholdSettingType, string thresholdSettingTitle, string thresholdSettingTypeAmt)
        {
            decimal AmountoDecimal = Convert.ToDecimal(thresholdSettingTypeAmt, CultureInfo.InvariantCulture);
            List<SqlParameter> sqlParameterNext = new List<SqlParameter>();
            sqlParameterNext.Add(new SqlParameter("@fldVerificationThresholdType", thresholdSettingType));
            sqlParameterNext.Add(new SqlParameter("@fldVerificationThresholdLevel", thresholdSettingTitle));
            sqlParameterNext.Add(new SqlParameter("@fldVerificationThresholdAmt", AmountoDecimal));
            return this.dbContext.GetRecordsAsDataTableSP("spcgThreshold", sqlParameterNext.ToArray());
        }

        public int UpdateThreshold(FormCollection col)
        {
            string str = col["thresholdid_1"];
            string codebank = str.Substring(0, 3);
            string type = str.Substring(3, 1);
            string level = str.Substring(4, str.Length - 4);



            string item = col["fldAmount_1"];
            item = item.Replace(",", "");
            string str1 = "";
            //str1 = (col["fldSequence"] != "1" ? "Second Level Amount" : "First Level Amount");
            if (col["fldSequence"] == "1")
            {
                str1 = "First Level Amount";
            }
            else if (col["fldSequence"] == "2")
            {
                str1 = "Second Level Amount";
            }
            else
            {
                str1 = "Third Level Amount";
            }

            List<SqlParameter> sqlParameterNext = new List<SqlParameter>();
            try
            {
                sqlParameterNext.Add(new SqlParameter("@amount", item));
                //sqlParameterNext.Add(new SqlParameter("@id", str));
                sqlParameterNext.Add(new SqlParameter("@fldType", col["fldType"]));
                sqlParameterNext.Add(new SqlParameter("@fldSequence", col["fldSequence"]));
                sqlParameterNext.Add(new SqlParameter("@fldTitle", str1));

                sqlParameterNext.Add(new SqlParameter("@fldBankCode", codebank));
                sqlParameterNext.Add(new SqlParameter("@fldVerificationThresholdType", type));
                sqlParameterNext.Add(new SqlParameter("@fldVerificationThresholdLevel", level));


                return this.dbContext.ExecuteNonQuery(CommandType.StoredProcedure, "spcuThresholdSettingMainTable", sqlParameterNext.ToArray());


            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public void AddtoThresholdSettingTempToUpdate(FormCollection col)
        {

            string str = col["thresholdid_1"];
            string codebank = str.Substring(0, 3);
            string type = str.Substring(3, 1);
            string level = str.Substring(4, str.Length - 4);


            string item = col["fldAmount_1"].Replace(",","");
            string str1 = "";
            //str1 = (col["fldSequence"] != "1" ? "Second Level Amount" : "First Level Amount");
            if (col["fldSequence"] == "1")
            {
                str1 = "First Level Amount";
            }
            else if (col["fldSequence"] == "2")
            {
                str1 = "Second Level Amount";
            }
            else
            {
                str1 = "Third Level Amount";
            }
            List<SqlParameter> sqlParameterNext = new List<SqlParameter>();
            try
            {
                sqlParameterNext.Add(new SqlParameter("@amount", item));
                sqlParameterNext.Add(new SqlParameter("@fldType", col["fldType"]));
                sqlParameterNext.Add(new SqlParameter("@fldSequence", col["fldSequence"]));
                sqlParameterNext.Add(new SqlParameter("@fldTitle", str1));
                sqlParameterNext.Add(new SqlParameter("@fldApproveStatus", "U"));
                sqlParameterNext.Add(new SqlParameter("@fldBankCode", codebank));
                sqlParameterNext.Add(new SqlParameter("@fldVerificationThresholdType", type));
                sqlParameterNext.Add(new SqlParameter("@fldVerificationThresholdLevel", level));

                this.dbContext.ExecuteNonQuery(CommandType.StoredProcedure, "spciThresholdSettingTempTabletoUpdate", sqlParameterNext.ToArray());


            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public double GetThresholdLimit(string thresholdType, int sequence, string BankCode)
        {
            double num = 0;
            try
            {
                DataTable dataTable = new DataTable();
                List<SqlParameter> sqlParameterNext = new List<SqlParameter>();
                sqlParameterNext.Add(new SqlParameter("@fldBankCode", BankCode));
                sqlParameterNext.Add(new SqlParameter("@fldType", thresholdType));
                sqlParameterNext.Add(new SqlParameter("@fldSequence", (object)sequence));
                dataTable =  this.dbContext.GetRecordsAsDataTableSP("spcgThresholdLimit", sqlParameterNext.ToArray());
                if (dataTable.Rows.Count > 0)
                {
                    num = Convert.ToDouble(dataTable.Rows[0]["fldVerificationThresholdAmt"].ToString());
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        public DataTable ListAllThreshold(AccountModel currentUser)
        {
            List<SqlParameter> sqlParameterNext = new List<SqlParameter>();
            sqlParameterNext.Add(new SqlParameter("@fldBankCode", currentUser.BankCode));
            return this.dbContext.GetRecordsAsDataTableSP("spcgAllThreshold", sqlParameterNext.ToArray());
        }
       

		public bool CheckExist2(string seq, string type, string bankCode)
		{
            bool strs = false;
            DataTable dataTable = new DataTable();
            List<SqlParameter> sqlParameterNext = new List<SqlParameter>();
            sqlParameterNext.Add(new SqlParameter("@fldVerificationThresholdSeq", seq));
            sqlParameterNext.Add(new SqlParameter("@fldVerificationThresholdType", type));
            sqlParameterNext.Add(new SqlParameter("@fldBankCode", bankCode));

            dataTable = this.dbContext.GetRecordsAsDataTableSP("spcgCheckExist2", sqlParameterNext.ToArray());

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

		public bool CheckTempExist2(string seq, string type, string bankCode)
		{
            bool strs = false;
            DataTable dataTable = new DataTable();
            List<SqlParameter> sqlParameterNext = new List<SqlParameter>();
            sqlParameterNext.Add(new SqlParameter("@fldVerificationThresholdSeq", seq));
            sqlParameterNext.Add(new SqlParameter("@fldVerificationThresholdType", type));
            sqlParameterNext.Add(new SqlParameter("@fldBankCode", bankCode));

            dataTable = this.dbContext.GetRecordsAsDataTableSP("spcgCheckTempExist2", sqlParameterNext.ToArray());

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

        public bool checkIfaRecordPendingforApproval(string id)
        {
            bool strs = false;
            DataTable dataTable = new DataTable();

            string codebank = id.Substring(0, 3);
            string type = id.Substring(3, 1);
            string level = id.Substring(4, id.Length - 4);
            List<SqlParameter> sqlParameterNext = new List<SqlParameter>();
            sqlParameterNext.Add(new SqlParameter("@fldBankCode", codebank));
            sqlParameterNext.Add(new SqlParameter("@fldVerificationThresholdType", type));
            sqlParameterNext.Add(new SqlParameter("@fldVerificationThresholdLevel", level));

            dataTable = this.dbContext.GetRecordsAsDataTableSP("spcgRecordPendingForApproval2", sqlParameterNext.ToArray());

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
        public bool NoChanges(FormCollection col, string id)
        {
            int counter = 0;
            bool strs = false;
            string str = col["thresholdid_1"];
            string codebank = str.Substring(0, 3);
            string type = str.Substring(3, 1);
            string level = str.Substring(4, str.Length - 4);
            DataTable datatable = new DataTable();
            List<SqlParameter> sqlParameterNext = new List<SqlParameter>();
            //sqlParameterNext.Add(new SqlParameter("@fldVerificationThresholdId", id));
            sqlParameterNext.Add(new SqlParameter("@fldBankCode", codebank));
            sqlParameterNext.Add(new SqlParameter("@fldVerificationThresholdType", type));
            sqlParameterNext.Add(new SqlParameter("@fldVerificationThresholdLevel", level));

            datatable = this.dbContext.GetRecordsAsDataTableSP("spcgCheckIfNoChanges", sqlParameterNext.ToArray());

            if (datatable.Rows.Count > 0)
            {

                DataRow drItem = datatable.Rows[0];
                string x = col["fldAmount_1"];
                string y = drItem["fldVerificationThresholdAmt"].ToString();



                if (col["fldType"].Equals(drItem["fldVerificationThresholdType"].ToString()))
                {
                    counter++;
                }
                if (col["fldSequence"].Equals(drItem["fldVerificationThresholdSeq"].ToString()))
                {
                    counter++;
                }
                if (col["fldAmount_1"].ToString().Equals(drItem["fldVerificationThresholdAmt"].ToString()))
                {
                    counter++;
                }

                if (counter == 3)
                {
                    strs = true;
                }

            }
            return strs;
        }

        public List<string> Validate(FormCollection col, string bankCode)
		{
			float single;
			List<string> strs = new List<string>();
			if (!float.TryParse(col["fldAmount_1"], out single))
			{
				strs.Add("Threshold Amount must be numeric");
            }
			return strs;
		}

		public List<string> ValidateCreate(FormCollection col, string bankCode)
		{
			float single;
			List<string> strs = new List<string>();

			if (this.CheckExist2(col["fldSequence"], col["fldType"], bankCode))
			{
				strs.Add("Level and type already exist");
			}
			if (this.CheckTempExist2(col["fldSequence"], col["fldType"], bankCode))
			{
				strs.Add("Threshold level and type already created and need to be verified in Approve Checker.");
			}
			if (!float.TryParse(col["fldAmount"], out single))
			{
				strs.Add("Threshold Amount must be numeric");
			}
			return strs;
		}
	}
}