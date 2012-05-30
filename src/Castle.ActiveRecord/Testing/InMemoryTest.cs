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

using Castle.ActiveRecord.Config;
using Castle.Core.Configuration;

namespace Castle.ActiveRecord.Testing
{
	using System;
	using System.Reflection;
	using System.Collections.Generic;

	using Castle.ActiveRecord;

	/// <summary>
	/// Base class for in memory unit tests. This class does not contain any
	/// attributes specific to a testing framework.
	/// </summary>
	public abstract class InMemoryTest
	{
		/// <summary>
		/// The common test setup code. To activate it in a specific test framework,
		/// it must be called from a framework-specific setup-Method.
		/// </summary>
		public virtual void SetUp()
		{
			ActiveRecord.ResetInitializationFlag();
			var source = ActiveRecord.Configure();

			var conf = new SessionFactoryConfig();
			foreach (var a in GetAssemblies()) {
				conf.Assemblies.Add(a);
			}

			conf.Properties.Add("connection.driver_class", "NHibernate.Driver.SQLite20Driver");
			conf.Properties.Add("dialect", "NHibernate.Dialect.SQLiteDialect");
			conf.Properties.Add("connection.provider", typeof (InMemoryConnectionProvider).AssemblyQualifiedName);
			conf.Properties.Add("connection.connection_string", "Data Source=:memory:;Version=3;New=True");
			conf.Properties.Add("proxyfactory.factory_class", "Castle.ActiveRecord.ByteCode.ProxyFactoryFactory, Castle.ActiveRecord");
			foreach (var p in GetProperties()) {
				conf.Properties.Add(p.Key, p.Value);
			}
			source.Add(conf);


			Configure(source);

			ActiveRecord.Initialize(source);
			ActiveRecord.CreateSchema();
		}

		/// <summary>
		/// The common test teardown code. To activate it in a specific test framework,
		/// it must be called from a framework-specific teardown-Method.
		/// </summary>
		public virtual void TearDown()
		{
			ActiveRecord.ResetInitializationFlag();
			InMemoryConnectionProvider.Restart();
		}

		/// <summary>
		/// Method that must be overridden by the test fixtures to return the assemblies
		/// that should be initialized. The stub returns an empty array.
		/// </summary>
		/// <returns></returns>
		public virtual Assembly[] GetAssemblies()
		{
			return new Assembly[0];
		}

		/// <summary>
		/// Hook to add additional properties for each base class' configuration. As an example, "show_sql" can
		/// be added to verify the behaviour of NHibernate in specific situations.
		/// </summary>
		/// <returns>A dictionary of additional or custom properties.</returns>
		public virtual IDictionary<string, string> GetProperties()
		{
			return new Dictionary<string, string>();
		}

		/// <summary>
		/// Hook for modifying the configuration before initialization
		/// </summary>
		/// <param name="config"></param>
		public virtual void Configure(IActiveRecordConfiguration config)
		{
		}
	}
}
