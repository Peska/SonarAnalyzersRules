using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SonarAnalyzersRules
{
	class Program
	{
		static void Main(string[] args)
		{
			string[] files = Directory.GetFiles(@"C:\Users\Peska\source\repos\sonar-dotnet\analyzers\rspec\cs", "*.json");

			List<Rule> allRules = new(files.Length);

			foreach (string file in files)
				allRules.Add(JsonConvert.DeserializeObject<Rule>(File.ReadAllText(file)));

			StringBuilder builder = new StringBuilder();

			foreach (IGrouping<string, Rule> group in allRules.GroupBy(x => x.type))
			{
				builder.AppendLine($"# {group.Key}\n\n");

				string severity = GetSeverity(group.Key);

				foreach (var rule in group.OrderBy(x => x.defaultSeverity))
				{
					if (rule.sqKey == null || rule.sqKey[0] != 'S')
						continue;

					builder.AppendLine($"# {rule.sqKey}: {rule.defaultSeverity} :: {rule.title}");
					builder.AppendLine($"dotnet_diagnostic.{rule.sqKey}.severity = {severity}\n");
				}

				builder.AppendLine();
			}

			File.WriteAllText(@"C:\Users\Peska\source\repos\SonarAnalyzersRules\SonarAnalyzersRules\rules.txt", builder.ToString());
		}

		public static string GetSeverity(string severity)
		{
			switch (severity)
			{
				case "CODE_SMELL": return "warning";
				case "BUG": return "error";
				case "SECURITY_HOTSPOT": return "error";
				case "VULNERABILITY": return "error";
				default: return string.Empty;
			}
		}
	}
}
