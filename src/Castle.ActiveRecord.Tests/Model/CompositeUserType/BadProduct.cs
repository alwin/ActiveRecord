﻿// Copyright 2004-2011 Castle Project - http://www.castleproject.org/
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

namespace Castle.ActiveRecord.Tests.Model.CompositeUserType
{
	//zzzz [ActiveRecord]
	public class BadProduct_WithNoType
	{
		//zzzz [PrimaryKey]
		public int Id { get; set; }

		//zzzz [CompositeUserType]
		public string[] ManufacturerName { get; set; }
	}

	//zzzz [ActiveRecord]
	public class BadProduct_WithBadType 
	{
		//zzzz [PrimaryKey]
		public int Id { get; set; }

		/*zzzz
		[CompositeUserType(CompositeType = typeof(string),
				ColumnNames = new[] { "Manufacturer_FirstName" },
				Length = new[] {1,2,3})] */
		public string[] ManufacturerName { get; set; }
	}

	//zzzz [ActiveRecord]
	public class BadProduct_WithNoColumnNames 
	{
		//zzzz [PrimaryKey]
		public int Id { get; set; }

		//zzzz [CompositeUserType(CompositeType = typeof(DoubleStringType))]
		public string[] ManufacturerName { get; set; }
	}

	//zzzz [ActiveRecord]
	public class BadProduct_WithNoColumnLength 
	{
		//zzzz [PrimaryKey]
		public int Id { get; set; }

		//zzzz [CompositeUserType(CompositeType = typeof(DoubleStringType), 
				//zzzz ColumnNames = new[] { "Manufacturer_FirstName" })]
		public string[] ManufacturerName { get; set; }
	}

	//zzzz [ActiveRecord]
	public class BadProduct_WithBadColumnLength
	{
		//zzzz [PrimaryKey]
		public int Id { get; set; }

		//zzzz [CompositeUserType(CompositeType = typeof(DoubleStringType), 
				//zzzz ColumnNames = new[] { "Manufacturer_FirstName" },
				//zzzz Length = new[] {1,2,3})]
		public string[] ManufacturerName { get; set; }
	}

}
