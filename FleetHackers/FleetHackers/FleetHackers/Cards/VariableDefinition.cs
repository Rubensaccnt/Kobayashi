﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using FleetHackers.Cards.Enums;

namespace FleetHackers.Cards
{
	[DataContract]
	public class VariableDefinition
	{
		public Variable Variable { get; set; }

		[DataMember(Name = "variable")]
		public string VariableString
		{
			get
			{
				return Variable.ToString();
			}
			set
			{
				Variable = (Variable)Enum.Parse(typeof(Variable), value);
			}
		}

		public AmountType ValueType { get; set; }

		[DataMember(Name = "valueType")]
		public string ValueTypeString
		{
			get
			{
				return ValueType.ToString();
			}
			set
			{
				ValueType = (AmountType)Enum.Parse(typeof(AmountType), value);
			}
		}

		public int Value { get; set; }

		public CardAttribute ValueAttribute { get; set; }

		[DataMember(Name = "valueAttribute")]
		public string ValueAttributeString
		{
			get
			{
				return ValueAttribute.ToString();
			}
			set
			{
				ValueAttribute = (CardAttribute)Enum.Parse(typeof(CardAttribute), value);
			}
		}
	}
}
