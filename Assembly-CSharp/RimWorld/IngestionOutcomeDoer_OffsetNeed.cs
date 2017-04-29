using System;
using System.Collections.Generic;
using System.Diagnostics;
using Verse;

namespace RimWorld
{
	public class IngestionOutcomeDoer_OffsetNeed : IngestionOutcomeDoer
	{
		public NeedDef need;

		public float offset;

		public ChemicalDef toleranceChemical;

		protected override void DoIngestionOutcomeSpecial(Pawn pawn, Thing ingested)
		{
			if (pawn.needs == null)
			{
				return;
			}
			Need need = pawn.needs.TryGetNeed(this.need);
			if (need == null)
			{
				return;
			}
			float num = this.offset;
			AddictionUtility.ModifyChemicalEffectForToleranceAndBodySize(pawn, this.toleranceChemical, ref num);
			need.CurLevel += num;
		}

		[DebuggerHidden]
		public override IEnumerable<StatDrawEntry> SpecialDisplayStats(ThingDef parentDef)
		{
			IngestionOutcomeDoer_OffsetNeed.<SpecialDisplayStats>c__Iterator82 <SpecialDisplayStats>c__Iterator = new IngestionOutcomeDoer_OffsetNeed.<SpecialDisplayStats>c__Iterator82();
			<SpecialDisplayStats>c__Iterator.<>f__this = this;
			IngestionOutcomeDoer_OffsetNeed.<SpecialDisplayStats>c__Iterator82 expr_0E = <SpecialDisplayStats>c__Iterator;
			expr_0E.$PC = -2;
			return expr_0E;
		}
	}
}
