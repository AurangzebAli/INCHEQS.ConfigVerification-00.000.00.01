using INCHEQS.Security.Account;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace INCHEQS.ConfigVerification.ThresholdSetting
{
	public interface IThresholdSettingDao
	{
		void AddtoThresholdSettingTempToDelete(string bankcode, string type, string level);

		void CreateThresholdSetting(FormCollection col, string currentUser, string BankCode);

        void CreateThresholdSettingfromChecker(string bankcode, string type, string level);

        void CreateThresholdSettingTemp(FormCollection col, string currentUser, string BankCode);

		void DeleteInThresholdSetting(string bankcode,string type,string level);
        bool NoChanges(FormCollection col, string id);

        void DeleteInThresholdSettingTemp(FormCollection col);
        void DeleteInThresholdSettingTemp2(string bankcode, string type, string level);

        DataTable GetThreshold(string thresholdSettingType, string thresholdSettingTitle,string  thresholdSettingTypeAmt);

		double GetThresholdLimit(string thresholdType, int sequence, string BankCode);

		DataTable ListAllThreshold(AccountModel currentUser);

		int UpdateThreshold(FormCollection col);

        void AddtoThresholdSettingTempToUpdate(FormCollection col);

        bool checkIfaRecordPendingforApproval(string id);

        List<string> Validate(FormCollection col,string bankCode);

		List<string> ValidateCreate(FormCollection col, string bankCode);
	}
}