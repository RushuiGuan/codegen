using Albatross.CodeGen.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen
{
	public class CustomCodeSectionStrategy : ICustomCodeSectionStrategy {
		Dictionary<string, ICustomCodeSection> registrations = new Dictionary<string, ICustomCodeSection>();
		public CustomCodeSectionStrategy(IEnumerable<ICustomCodeSection> items) {
			foreach (var reader in items) {
				registrations.Add(reader.ApplyTo, reader);
			}
		}

		public ICustomCodeSection Get(string target) {
			ICustomCodeSection item;
			if (registrations.TryGetValue(target, out item)) {
				return item;
			} else {
				throw new Faults.CustomCodeSectionNotRegisteredException(target);
			}
		}
	}
}
