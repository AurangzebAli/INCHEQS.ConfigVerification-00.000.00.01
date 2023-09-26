using INCHEQS.Common;
using INCHEQS.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace INCHEQS.ConfigVerification.ReleaseLockedCheque
{
	public class ReleaseLockedChequeDao : IReleaseLockedChequeDao
	{
		private readonly ApplicationDbContext dbContext;

		public ReleaseLockedChequeDao(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public void DeleteProcessUsingCheckbox(string InwardItemId)
		{
			string[] strArrays = InwardItemId.Split(new char[] { ',' });
			if (strArrays.Length != 0)
			{
				string str = string.Concat("UPDATE tblInwardItemInfoStatus SET fldAssignedUserId = NULL WHERE fldInwardItemId in (", DatabaseUtils.getParameterizedStatementFromArray(strArrays, ""), ") ;");
				str = string.Concat(str, "Delete from tblVerificationLock WHERE fldInwardItemId in (", DatabaseUtils.getParameterizedStatementFromArray(strArrays, ""), ") ;");
				this.dbContext.ExecuteNonQuery(str, DatabaseUtils.getSqlParametersFromArray(strArrays, "").ToArray());
			}
		}

		public DataTable ListLockedCheque(string bankCode)
		{
			DataTable dataTable = new DataTable();
			string str = "SELECT inw.fldinwarditemid, inw.fldClearDate, inw.fldUIC, inw.fldChequeSerialNo, inw.fldAccountNumber, inw.fldAmount, usr.fldUserAbb FROM View_InwardItem inw LEFT JOIN tblUserMaster usr ON inw.fldAssignedUserId = usr.fldUserId WHERE /*isnull(fldApprovalStatus, '') = '' AND*/ isnull(fldAssignedUserId,'') <> '' AND inw.fldIssueBankCode=@fldBankCode";
			return this.dbContext.GetRecordsAsDataTable(str, new SqlParameter[] { new SqlParameter("@fldBankCode", bankCode) });
		}
	}
}