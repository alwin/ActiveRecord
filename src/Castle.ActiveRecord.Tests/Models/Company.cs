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

using System.Collections;
using Iesi.Collections.Generic;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Castle.ActiveRecord.Tests.Models
{
    public class Company : ActiveRecordBase<Company>
    {
        public Company() { }

        public Company(string name)
        {
            this.Name = name;
        }

        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual PostalAddress Address { get; set; }

        readonly ISet<Person> _people = new HashedSet<Person>();
        public virtual ISet<Person> People
        {
            get { return _people; }
        }
    }
}
