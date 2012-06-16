// Copyright 2004-2011 Castle Project - http://www.castleproject.org/
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Castle.ActiveRecord.Tests.Event
{
	using NUnit.Framework;
	using NHibernate.Event;
	using System;
	using System.Collections.Generic;
	using Castle.ActiveRecord.Tests.Model;
	using NHibernate.Cfg;

	[TestFixture]
	public class ContributorTest : AbstractActiveRecordTest
	{
		protected override Castle.ActiveRecord.Config.IActiveRecordConfiguration GetConfigSource() {
			var source = base.GetConfigSource();
			var contributor = new MockContributor();
			source.GetConfiguration(string.Empty).AddContributor(contributor);
			return source;
		}

		[Test]
		public void ContributorGetsCalled()
		{
			Assert.IsTrue(MockContributor.Called);
		}
		
		public class MockContributor : INHContributor
		{
			public void Contribute(Configuration configuration)
			{
				Called = true;
			}

			public static bool Called { get; private set; }
		}
	}
}
