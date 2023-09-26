using System;
using System.Runtime.CompilerServices;

namespace INCHEQS.ConfigVerification.VerificationLimit
{
	public class VerificationLimitModel
	{
		public double fld1stAmt
		{
			get;
			set;
		}

		public string fld1stType
		{
			get;
			set;
		}

		public double fld2ndAmt
		{
			get;
			set;
		}

		public string fld2ndType
		{
			get;
			set;
		}

		public string fldConcatenate
		{
			get;
			set;
		}

		public string verifyLimitClass
		{
			get;
			set;
		}

		public string verifyLimitDesc
		{
			get;
			set;
		}

		public VerificationLimitModel()
		{
		}
	}
}