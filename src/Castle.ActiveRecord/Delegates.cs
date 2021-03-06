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
using NHibernate;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;

namespace Castle.ActiveRecord
{
	/// <summary>
	/// Delegate for use in <see cref="AR.OnSessionFactoryHolderCreated"/>
	/// </summary>
	/// <param name="holder"></param>
	public delegate void SessionFactoryHolderDelegate(ISessionFactoryHolder holder);

	/// <summary>
	/// Delegate for use in <see cref="AR.OnSessionFactoryCreated"/>
	/// </summary>
	public delegate void SessionFactoryDelegate(ISessionFactory sf, NHibernate.Cfg.Configuration cfg, string name);

	/// <summary>
	/// Delegate for use in <see cref="AR.OnMapperCreated"/>
	/// </summary>
	public delegate void MapperDelegate(ModelMapper mapper, SessionFactoryConfig config);

	/// <summary>
	/// Delegate for use in <see cref="AR.OnHbmMappingCreated"/>
	/// </summary>
	public delegate void HbmMappingDelegate(HbmMapping mapping, SessionFactoryConfig config);

	/// <summary>
	/// Delegate for use in <see cref="AR.OnConfigurationCreated"/>
	/// </summary>
	public delegate void ConfigurationDelegate(NHibernate.Cfg.Configuration cfg, SessionFactoryConfig sfcfg);

}
