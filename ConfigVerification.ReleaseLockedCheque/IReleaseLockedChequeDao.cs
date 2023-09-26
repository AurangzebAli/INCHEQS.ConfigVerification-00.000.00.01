using System;
using System.Data;

namespace INCHEQS.ConfigVerification.ReleaseLockedCheque
{
	public interface IReleaseLockedChequeDao
	{
		void DeleteProcessUsingCheckbox(string dataProcess);

		DataTable ListLockedCheque(string bankCode);
	}
}