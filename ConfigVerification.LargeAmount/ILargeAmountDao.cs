using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace INCHEQS.ConfigVerification.LargeAmount
{
	public interface ILargeAmountDao
	{
		DataTable GetLargeAmount();

		void UpdateLargeAmount(string Amount, string currentUser);

		List<string> Validate(FormCollection col);
	}
}