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

using Castle.ActiveRecord.Scopes;

namespace Castle.ActiveRecord.Config
{
	/// <summary>
	/// Determines the default flushing behaviour of <see cref="SessionScope"/>
	/// and <see cref="TransactionScope"/>
	/// </summary>
	public enum DefaultFlushType
	{
		/// <summary>
		/// New recommended behaviour. Both types of scope flush automatically, consolidating behaviour between
		/// scoped and non-scoped code.
		/// </summary>
		Auto,

		/// <summary>
		/// Both scope types do only flush on disposal.
		/// </summary>
		Leave,

		/// <summary>
		/// NH2.0-alike behaviour. The <see cref="SessionScope"/> won't flush at all unless
		/// called manually. <see cref="TransactionScope"/> flushes automatically. This
		/// allows to use the scopes like the NH-ISession-ITransaction-block.
		/// </summary>
		Transaction
	}
}
