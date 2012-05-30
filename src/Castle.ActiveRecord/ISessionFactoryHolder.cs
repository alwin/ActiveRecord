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

using System;
using NHibernate;
using NHibernate.Cfg;

namespace Castle.ActiveRecord
{
	/// <summary>
	/// Keeps an association of SessionFactories to a object model 
	/// tree;
	/// </summary>
	public interface ISessionFactoryHolder
	{
		/// <summary>
		/// Pendent
		/// </summary>
		/// <returns></returns>
		Configuration[] GetAllConfigurations();

		/// <summary>
		/// Requests the Configuration associated to the type.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		Configuration GetConfiguration(Type type);

		/// <summary>
		/// Obtains the SessionFactory associated to the type.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		ISessionFactory GetSessionFactory(Type type);

		/// <summary>
		/// Gets the all the session factories.
		/// </summary>
		/// <returns></returns>
		ISessionFactory[] GetSessionFactories();

		/// <summary>
		/// Creates a session for the associated type
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		ISession CreateSession(Type type);

		/// <summary>
		/// Releases the specified session
		/// </summary>
		/// <param name="session"></param>
		void ReleaseSession(ISession session);

		/// <summary>
		/// Called if an action on the session fails
		/// </summary>
		/// <param name="session"></param>
		void FailSession(ISession session);

		/// <summary>
		/// Gets or sets the implementation of <see cref="IThreadScopeInfo"/>
		/// </summary>
		IThreadScopeInfo ThreadScopeInfo { get; set; }

		///<summary>
		/// This method allows direct registration of Configuration
		///</summary>
		void RegisterConfiguration(Configuration cfg);
	}
}