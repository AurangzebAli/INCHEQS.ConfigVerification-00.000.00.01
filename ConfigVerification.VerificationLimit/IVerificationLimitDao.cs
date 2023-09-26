using INCHEQS.Security.Account;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace INCHEQS.ConfigVerification.VerificationLimit
{
	public interface IVerificationLimitDao
	{
		void AddToVerificationLimitMasterTempToDelete(string clasId);

        void AddToVerificationLimitMasterTempToUpdate(FormCollection collection, string clasId, string currentUserId);

        bool checkClassExist(string fldClass);

        bool checkIfaRecordPendingforApproval(string fldClass);

        void CreateInVerificationLimitMaster(string classId);

		void CreateInVerificationLimitMasterTemp(AccountModel currentUser, FormCollection collection, string currentUserId);

		void DeleteInVerificationLimitMaster(string delete);

		void DeleteInVerificationLimitMasterTemp(string clasId);

		DataTable Find(string classId);

		VerificationLimitModel GetVerifyLimit(string fldclass);

		DataTable ListVerificationLimit();

		void Update(FormCollection collection, string currentUserId);

		List<string> Validate(FormCollection collection);
	}
}