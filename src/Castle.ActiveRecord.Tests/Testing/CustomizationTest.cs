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


using System.Reflection;

namespace Castle.ActiveRecord.Tests.Testing
{
	using System;
	using NUnit.Framework;
	using NHibernate;

	using Castle.ActiveRecord.Config;
	using Castle.ActiveRecord.Scopes;
	using Castle.ActiveRecord.Tests.Models;


	[TestFixture]
	public class CustomizationTest : NUnitInMemoryTest
	{
		public override System.Reflection.Assembly[] GetAssemblies()
		{
			return new Assembly[] { typeof(Blog).Assembly };
		}

		public override void Configure(IActiveRecordConfiguration config)
		{
			config.Flush(DefaultFlushType.Leave);
		}

		[Test]
		public void ConfigurationIsCustomizable()
		{
			using (new SessionScope())
			{
				Blog.FindAll();
				Assert.AreEqual(FlushMode.Commit, ActiveRecord.Holder.CreateSession(typeof(Blog)).FlushMode);
			}
		}
	}
}
