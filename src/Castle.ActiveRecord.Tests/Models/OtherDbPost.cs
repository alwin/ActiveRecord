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
using System.Collections;

namespace Castle.ActiveRecord.Tests.Models
{
	public class OtherDbPost : Test2ARBase<OtherDbBlog>
	{
		private int _id;
		private String _title;
		private String _contents;
		private String _category;
		private DateTime _created;
		private bool _published;
		private OtherDbBlog _blog;

		public OtherDbPost()
		{
		}

		public OtherDbPost(OtherDbBlog blog, String title, String contents, String category)
		{
			_blog = blog;
			_title = title;
			_contents = contents;
			_category = category;
		}

		public int Id
		{
			get { return _id; }
			set { _id = value; }
		}

		public String Title
		{
			get { return _title; }
			set { _title = value; }
		}

		public String Contents
		{
			get { return _contents; }
			set { _contents = value; }
		}

		public String Category
		{
			get { return _category; }
			set { _category = value; }
		}

		public OtherDbBlog Blog
		{
			get { return _blog; }
			set { _blog = value; }
		}

		public DateTime Created
		{
			get { return _created; }
			set { _created = value; }
		}

		public bool Published
		{
			get { return _published; }
			set { _published = value; }
		}

		protected override bool BeforeSave(IDictionary state)
		{
			state["Created"] = DateTime.Now;
			return true;
		}

		public void SaveWithException()
		{
			Save();

			throw new ApplicationException("Fake Exception");
		}
	}
}
