﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using FleetHackersLib.Cards.Effects.Enums;

namespace FleetHackersLib.Cards.Effects
{
	[DataContract]
	[KnownType(typeof(DamageEffect))]
	[KnownType(typeof(StatPumpEffect))]
	[KnownType(typeof(AnnihilateEffect))]
	[KnownType(typeof(CoinFlipEffect))]
	[KnownType(typeof(ExhaustEffect))]
	[KnownType(typeof(ForfeitEffect))]
	[KnownType(typeof(StateCheckEffect))]
	[KnownType(typeof(CardDrawEffect))]
	[KnownType(typeof(PutCounterEffect))]
	[KnownType(typeof(BounceEffect))]
	[KnownType(typeof(DiscardEffect))]
	[KnownType(typeof(StaticCostReductionEffect))]
	[KnownType(typeof(AttachToShipEffect))]
	[KnownType(typeof(StatPumpForCountersEffect))]
	[KnownType(typeof(ScryEffect))]
	[KnownType(typeof(PacifismEffect))]
	[KnownType(typeof(TutorEffect))]
	[KnownType(typeof(FreePlayEffect))]
	[KnownType(typeof(CloakEffect))]
	[KnownType(typeof(LifeGainEffect))]
	[KnownType(typeof(RemoveCounterEffect))]
	[KnownType(typeof(FogEffect))]
	[KnownType(typeof(LifeLossEffect))]
	[KnownType(typeof(RechargeEffect))]
	[KnownType(typeof(RestoreEffect))]
	[KnownType(typeof(TapDownEffect))]
	[KnownType(typeof(HealthLossReductionEffect))]
	[KnownType(typeof(MillEffect))]
	[KnownType(typeof(RangePumpEffect))]
	[KnownType(typeof(ModalEffect))]
	[KnownType(typeof(UnblockableEffect))]
	[KnownType(typeof(ChooseTargetEffect))]
	[KnownType(typeof(GainControlEffect))]
	[KnownType(typeof(MicrodroneEffect))]
	[KnownType(typeof(CostReductionEffect))]
	[KnownType(typeof(AddEnergyCrystalEffect))]
	[KnownType(typeof(RechargeCrystalsEffect))]
	[KnownType(typeof(RemoveDamageEffect))]
	[KnownType(typeof(NoRulesTextEffect))]
	[KnownType(typeof(TerminateEffect))]
	[KnownType(typeof(ProvidesInfluenceEffect))]
	[KnownType(typeof(FightEffect))]
	public abstract class Effect
	{
		public abstract EffectType EffectType { get; }
		public abstract string ToString(Card card, bool capitalize = false);
		public virtual bool HasReminderText { get { return false; } }
		public virtual string ReminderText { get { return string.Empty; } }
	}
}
