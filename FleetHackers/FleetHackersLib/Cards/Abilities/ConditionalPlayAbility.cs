﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using FleetHackersLib.Cards.Effects;
using System.Collections.ObjectModel;
using FleetHackersLib.Cards.Enums;
using FleetHackersLib.Cards.Effects.Conditions;

namespace FleetHackersLib.Cards.Abilities
{
	[DataContract]
	public class ConditionalPlayAbility : Ability
	{
		public override AbilityType AbilityType
		{
			get
			{
				return AbilityType.ConditionalPlay;
			}
		}

		[DataMember(Name = "condition")]
		public EffectCondition Condition { get; set; }

		public override string ToString(Card card)
		{
			StringBuilder toStringBuilder = new StringBuilder();

			toStringBuilder.Append("Only ");

			if (card.Supertype == Supertype.Ship)
			{
				toStringBuilder.Append("deploy ");
			}
			else
			{
				toStringBuilder.Append("play ");
			}

			toStringBuilder.Append(card.Title);
			toStringBuilder.Append(" if ");

			toStringBuilder.Append(Condition.ToString());

			toStringBuilder.Append(".");

			return toStringBuilder.ToString();
		}
	}
}
