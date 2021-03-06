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
using Castle.ActiveRecord.Config;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Metadata;

namespace Castle.ActiveRecord {
    /// <summary>
    /// Keeps an association of SessionFactories to a object model 
    /// tree;
    /// </summary>
    public interface ISessionFactoryHolder : IDisposable {
        /// <summary>
        /// Pendent
        /// </summary>
        /// <returns></returns>
        Configuration[] GetAllConfigurations();

        IActiveRecordConfiguration ConfigurationSource { get; }

        /// <summary>
        /// Gets or sets the implementation of <see cref="IThreadScopeInfo"/>
        /// </summary>
        IThreadScopeInfo ThreadScopeInfo { get; }

        /// <summary>
        /// Requests the registered types
        /// </summary>
        Type[] GetRegisteredTypes();

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
        /// Obtains the IClassMetadata of the type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IClassMetadata GetClassMetadata(Type type);

        /// <summary>
        /// Obtains the Model of the type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        Model GetModel(Type type);

        /// <summary>
        /// Checks if type config is initialized
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        bool IsInitialized(Type type);

        /// <summary>
        /// Gets the all the session factories.
        /// </summary>
        /// <returns></returns>
        ISessionFactory[] GetSessionFactories();
    }
}
