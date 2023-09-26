using INCHEQS.ConfigVerification.Resource;
using INCHEQS.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace INCHEQS.ConfigVerification.LargeAmount
{
	public class LargeAmountDao : ILargeAmountDao
	{
		private readonly ApplicationDbContext dbContext;

		public LargeAmountDao(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public DataTable GetLargeAmount()
		{
			DataTable dataTable = new DataTable();
			return this.dbContext.GetRecordsAsDataTable("SELECT top 1 fldAmount,convert(varchar(50),fldUpdateTimestamp,120)[fldUpdateTimeStamp] FROM tblBigAmount", null);
		}

		public void UpdateLargeAmount(string Amount, string currentUser)
		{
			string str = "Update tblBigAmount Set fldupdateTimestamp = getdate(), fldUpdateUserId = 1, fldAmount = @fldAmount";
			this.dbContext.ExecuteNonQuery(str, new SqlParameter[] { new SqlParameter("@fldUpdateUserId", currentUser), new SqlParameter("@fldAmount", Amount) });
		}

		public List<string> Validate(FormCollection col)
		{
			List<string> strs = new List<string>();
			if (col["txtAmount"].Equals(""))
			{
				strs.Add(Locale.LargeAmountCannotBlank);
			}
			return strs;
		}
	}
}