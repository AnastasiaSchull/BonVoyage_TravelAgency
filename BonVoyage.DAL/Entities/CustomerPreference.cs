using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonVoyage.DAL.Entities
{
	public class CustomerPreference
	{
		public int PreferenceId { get; set; }
		public int UserId { get; set; }
		public string? PreferencesDetails { get; set; }

		public virtual User? User { get; set; }
	}
}
