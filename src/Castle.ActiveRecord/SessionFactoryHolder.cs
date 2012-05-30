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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Collections;
using System.Runtime.CompilerServices;
using Castle.ActiveRecord.Scopes;
using Iesi.Collections;
using NHibernate;
using NHibernate.Cfg;

namespace Castle.ActiveRecord
{
	/// <summary>
	/// Default implementation of <seealso cref="ISessionFactoryHolder"/>
	/// </summary>
	/// <remarks>
	/// This class is thread safe
	/// </remarks>
	public class SessionFactoryHolder : MarshalByRefObject, ISessionFactoryHolder {
		readonly IDictionary<Type, Configuration> type2Conf = new ConcurrentDictionary<Type, Configuration>();
		readonly IDictionary<Type, ISessionFactory> type2SessFactory = new ConcurrentDictionary<Type, ISessionFactory>();
		IThreadScopeInfo threadScopeInfo;

		/// <summary>
		/// Requests the Configuration associated to the type.
		/// </summary>
		public Configuration GetConfiguration(Type type)
		{
			return type2Conf.ContainsKey(type) ? type2Conf[type] : GetConfiguration(type.BaseType);
		}

		/// <summary>
		/// Pendent
		/// </summary>
		public Configuration[] GetAllConfigurations()
		{
			return type2Conf.Values.Distinct().ToArray();
		}

		/// <summary>
		/// Gets the all the session factories.
		/// </summary>
		/// <returns></returns>
		public ISessionFactory[] GetSessionFactories()
		{
			List<ISessionFactory> factories = new List<ISessionFactory>();

			foreach(ISessionFactory factory in type2SessFactory.Values)
			{
				factories.Add(factory);
			}

			return factories.ToArray();
		}

		/// <summary>
		/// Optimized with reader/writer lock.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public ISessionFactory GetSessionFactory(Type type)
		{
			if (type == null || !type2SessFactory.ContainsKey(type))
			{
				throw new ActiveRecordException("No configuration for ActiveRecord found in the type hierarchy -> " + type.FullName);
			}


			ISessionFactory sessFactory = type2SessFactory[type] as ISessionFactory;

			if (sessFactory != null)
			{
				return sessFactory;
			}


			sessFactory = type2SessFactory[type] as ISessionFactory;

			if (sessFactory != null)
			{
				return sessFactory;
			}
			Configuration cfg = GetConfiguration(type);

			sessFactory = cfg.BuildSessionFactory();

			type2SessFactory[type] = sessFactory;

			return sessFactory;
		}

		///<summary>
		/// This method allows direct registration of Configuration
		///</summary>
		public void RegisterConfiguration(Configuration cfg)
		{
			var sf = cfg.BuildSessionFactory();

			foreach (var classMetadata in sf.GetAllClassMetadata()) {
				var entitytype = classMetadata.Value.GetMappedClass(EntityMode.Poco);

				if (!type2SessFactory.ContainsKey(entitytype))
					type2SessFactory[entitytype] = sf;

				if (!type2Conf.ContainsKey(entitytype))
					type2Conf[entitytype] = cfg;
			}
		}

		/// <summary>
		/// Creates a session for the associated type
		/// </summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		public ISession CreateSession(Type type)
		{
			if (threadScopeInfo.HasInitializedScope)
			{
				return CreateScopeSession(type);
			}

			ISessionFactory sessionFactory = GetSessionFactory(type);

			ISession session = OpenSession(sessionFactory);

			return session;
		}

		private static ISession OpenSession(ISessionFactory sessionFactory)
		{
			lock(sessionFactory)
			{
				return sessionFactory.OpenSession(InterceptorFactory.Create());
			}
		}

		internal static ISession OpenSessionWithScope(ISessionScope scope, ISessionFactory sessionFactory)
		{
			lock(sessionFactory)
			{
				return scope.OpenSession(sessionFactory, InterceptorFactory.Create());
			}
		}

		/// <summary>
		/// Releases the specified session
		/// </summary>
		/// <param name="session"></param>
		public void ReleaseSession(ISession session)
		{
			if (threadScopeInfo.HasInitializedScope) return;

			session.Flush();
			session.Dispose();
		}

		/// <summary>
		/// Called if an action on the session fails
		/// </summary>
		/// <param name="session"></param>
		public void FailSession(ISession session)
		{
			if (threadScopeInfo.HasInitializedScope)
			{
				ISessionScope scope = threadScopeInfo.GetRegisteredScope();
				scope.FailSession(session);
			}
			else
			{
				session.Clear();
			}
		}

		/// <summary>
		/// Gets or sets the implementation of <see cref="IThreadScopeInfo"/>
		/// </summary>
		/// <value></value>
		public IThreadScopeInfo ThreadScopeInfo
		{
			get { return threadScopeInfo; }
			set
			{
				ThreadScopeAccessor.Instance.ScopeInfo = value;
				threadScopeInfo = value;
			}
		}

		private ISession CreateScopeSession(Type type)
		{
			ISessionScope scope = threadScopeInfo.GetRegisteredScope();
			ISessionFactory sessionFactory = GetSessionFactory(type);
#if DEBUG
			System.Diagnostics.Debug.Assert(scope != null);
			System.Diagnostics.Debug.Assert(sessionFactory != null);
#endif
			if (scope.IsKeyKnown(sessionFactory))
			{
				return scope.GetSession(sessionFactory);
			}

			ISession session;

			session = scope.WantsToCreateTheSession
				? OpenSessionWithScope(scope, sessionFactory)
				: OpenSession(sessionFactory);
#if DEBUG
			System.Diagnostics.Debug.Assert(session != null);
#endif
			scope.RegisterSession(sessionFactory, session);

			return session;
		}
	}
}