﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liberfy
{
	[JsonObject(MemberSerialization.OptIn)]
	internal class AccountSetting : NotificationObject
	{
		private static FluidCollection<Account> _accounts => App.Accounts;

		public bool ContainsId(double id)
		{
			return _accounts.Any((a) => a.Id == id);
		}

		public bool TryGetAccount(long id, out Account account)
		{
			account = _accounts.FirstOrDefault((a) => a.Id == id);
			return account != null;
		}

		public Account GetAccount(long id)
		{
			return _accounts.FirstOrDefault((a) => a.Id == id);
		}

		[JsonProperty("accounts")]
		public AccountItem[] Accounts { get; set; }

		[JsonProperty("columns")]
		public ColumnSetting[] Columns { get; set; }
	}
}
