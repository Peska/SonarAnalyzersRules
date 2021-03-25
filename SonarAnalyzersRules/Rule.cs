using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarAnalyzersRules
{
	public class Rule
	{
		public string title { get; set; }
		public string type { get; set; }
		public string status { get; set; }
		public Remediation remediation { get; set; }
		public string[] tags { get; set; }
		public string defaultSeverity { get; set; }
		public string ruleSpecification { get; set; }
		public string sqKey { get; set; }
		public string scope { get; set; }
	}

	public class Remediation
	{
		public string func { get; set; }
		public string constantCost { get; set; }
	}
}
