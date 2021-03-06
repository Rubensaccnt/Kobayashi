﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Reflection;
using FleetHackersLib.Cards.Enums;

namespace FleetHackersLib.Cards.Abilities
{
	[DataContract]
	public class Trigger
	{

		public TriggerType TriggerType { get; set; }

		[DataMember(Name = "triggerType")]
		public string TriggerTypeString
		{
			get { return TriggerType.ToString(); }
			set { TriggerType = (TriggerType)Enum.Parse(typeof(TriggerType), value); }
		}

		private readonly List<Target> _targets = new List<Target>();
		private ReadOnlyCollection<Target> _targetsView;
		public ReadOnlyCollection<Target> Targets
		{
			get
			{
				if (_targetsView == null)
				{
					_targetsView = new ReadOnlyCollection<Target>(_targets);
				}
				return _targetsView;
			}
		}

		[DataMember(Name = "targets")]
		private List<String> TargetsStrings
		{
			get
			{
				List<string> stringList = new List<string>();
				foreach (Target tgt in _targets)
				{
					stringList.Add(tgt.ToString());
				}
				return stringList;
			}
			set
			{
				_targets.Clear();
				foreach (string str in value)
				{
					_targets.Add((Target)Enum.Parse(typeof(Target), str));
				}
			}
		}

		[OnDeserializing]
		private void OnDeserializing(StreamingContext c)
		{
			if (_targets == null)
			{
				var field = GetType().GetField("_targets", BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.NonPublic);
				field.SetValue(this, new List<Target>());
			}
		}

		public Target Actor { get; set; }

		[DataMember(Name = "actor")]
		public string ActorString
		{
			get { return Actor.ToString(); }
			set { Actor = (Target)Enum.Parse(typeof(Target), value); }
		}

		public Condition Condition { get; set; }

		[DataMember(Name = "condition")]
		public string ConditionString
		{
			get { return Condition.ToString(); }
			set { Condition = (Condition)Enum.Parse(typeof(Condition), value); }
		}

		[DataMember(Name = "variableBinding")]
		public VariableBinding VariableBinding { get; set; }

		[DataMember(Name = "replacement")]
		public bool Replacement { get; set; }

		public string ToString(Card card)
		{
			StringBuilder toStringBuilder = new StringBuilder();

			switch (Actor)
			{
				case Target.OpponentShip:
					toStringBuilder.Append("a ship controlled by an opponent ");
					break;
				case Target.AnyShip:
					toStringBuilder.Append("any ship ");
					break;
				case Target.This:
					toStringBuilder.Append(card.Title);
					toStringBuilder.Append(" ");
					break;
				case Target.None:
					break;
				case Target.You:
					toStringBuilder.Append("your home base ");
					break;
				case Target.YourShip:
					toStringBuilder.Append("a ship you control ");
					break;
				case Target.AnySource:
					toStringBuilder.Append("any source ");
					break;
				case Target.AttachedShip:
					toStringBuilder.Append("attached ship ");
					break;
				case Target.Opponent:
					toStringBuilder.Append("an opponent ");
					break;
				default:
					throw new InvalidOperationException("Unsupported Actor for Trigger.");
			}

			string actorOwns = string.Empty;
			switch (TriggerType)
			{
				case TriggerType.Attack:
					toStringBuilder.Append("attacks");
					actorOwns = "the attacking ship's";
					break;
				case TriggerType.Interception:
					toStringBuilder.Append("intercepts");
					actorOwns = "the intercepting ship's";
					break;
				case TriggerType.EntersTheBattleZone:
					toStringBuilder.Append("enters the battle zone");
					actorOwns = "that card's";
					break;
				case TriggerType.EndOfYourTurn:
					toStringBuilder.Append("At the end of your turn");
					actorOwns = card.Title + "'s";
					break;
				case TriggerType.LeavesTheBattleZone:
					toStringBuilder.Append("leaves the battle zone");
					actorOwns = "that card's";
					break;
				case TriggerType.Annihilated:
					if (Replacement)
					{
						toStringBuilder.Append("would be annihilated");
					}
					else
					{
						toStringBuilder.Append("is annihilated");
					}
					actorOwns = "the annihilated ship's";
					break;
				case TriggerType.LifeLoss:
					toStringBuilder.Append("loses health");
					actorOwns = "that home base's controller's";
					break;
				case TriggerType.Damage:
					toStringBuilder.Append("inflicts damage to");
					actorOwns = "that ship's";
					break;
				case TriggerType.AssaultDamage:
					toStringBuilder.Append("inflicts assault damage to");
					actorOwns = "that ship's";
					break;
				case TriggerType.ActivateShipAbility:
					toStringBuilder.Append("activates an ability of a ship");
					if (Actor == Target.Opponent)
					{
						toStringBuilder.Append(" he or she controls");
					}
					actorOwns = "that player's";
					break;
				default:
					throw new InvalidOperationException("Unsupported TriggerType for Trigger.");
			}

			List<string> targetStrings = new List<string>();
			foreach (Target target in _targets)
			{
				switch (target)
				{
					case Target.YourShip:
						targetStrings.Add("a ship you control");
						break;
					case Target.OpponentShip:
						targetStrings.Add("a ship controlled by an opponent");
						break;
					case Target.YourHomeBase:
						targetStrings.Add("your home base");
						break;
					case Target.This:
						targetStrings.Add(card.Title);
						break;
					case Target.AnyShip:
						targetStrings.Add("a ship");
						break;
					case Target.AnyHomeBase:
						targetStrings.Add("a home base");
						break;
					default:
						throw new InvalidOperationException("Unsupported Target for Trigger.");
				}
			}
			if (targetStrings.Count > 0)
			{
				toStringBuilder.Append(" ");
			}
			toStringBuilder.Append(string.Join(" or ", targetStrings));

			switch (Condition)
			{
				case Condition.None:
					break;
				case Condition.OneOtherShip:
					toStringBuilder.Append(" and another ship attacked this turn");
					break;
				case Condition.DuringCombat:
					toStringBuilder.Append(" during a battle");
					break;
				case Condition.ByAttack:
					toStringBuilder.Append(" by an attack");
					break;
				default:
						throw new InvalidOperationException("Unsupported Condition for Trigger.");
			}

			if (VariableBinding != null)
			{
				toStringBuilder.Append(", ");
				toStringBuilder.Append(Description.ToDescription(VariableBinding.Variable));
				toStringBuilder.Append(" is ");
				string attributeDescription = Description.ToDescription(VariableBinding.Attribute);
				toStringBuilder.Append(string.Format(Description.ToDescription(VariableBinding.ValueModifier), actorOwns + " " + attributeDescription));
			}

			return toStringBuilder.ToString();
		}
	}
}
