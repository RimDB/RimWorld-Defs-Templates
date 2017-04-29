using System;
using System.Collections.Generic;
using System.Diagnostics;
using Verse;

namespace RimWorld
{
	public class Apparel : ThingWithComps
	{
		private bool wornByCorpseInt;

		public Pawn Wearer
		{
			get
			{
				Pawn_ApparelTracker pawn_ApparelTracker = base.ParentHolder as Pawn_ApparelTracker;
				return (pawn_ApparelTracker == null) ? null : pawn_ApparelTracker.pawn;
			}
		}

		public bool WornByCorpse
		{
			get
			{
				return this.wornByCorpseInt;
			}
		}

		public void Notify_Stripped(Pawn pawn)
		{
			if (pawn.Dead && this.def.apparel.careIfWornByCorpse)
			{
				this.wornByCorpseInt = true;
			}
		}

		public override void ExposeData()
		{
			base.ExposeData();
			Scribe_Values.Look<bool>(ref this.wornByCorpseInt, "wornByCorpse", false, false);
		}

		public virtual void DrawWornExtras()
		{
		}

		public virtual bool CheckPreAbsorbDamage(DamageInfo dinfo)
		{
			return false;
		}

		public virtual bool AllowVerbCast(IntVec3 root, Map map, LocalTargetInfo targ)
		{
			return true;
		}

		[DebuggerHidden]
		public virtual IEnumerable<Gizmo> GetWornGizmos()
		{
			Apparel.<GetWornGizmos>c__Iterator15E <GetWornGizmos>c__Iterator15E = new Apparel.<GetWornGizmos>c__Iterator15E();
			Apparel.<GetWornGizmos>c__Iterator15E expr_07 = <GetWornGizmos>c__Iterator15E;
			expr_07.$PC = -2;
			return expr_07;
		}

		public override string GetInspectString()
		{
			string text = base.GetInspectString();
			if (this.WornByCorpse)
			{
				text += "WasWornByCorpse".Translate();
			}
			return text;
		}

		public virtual float GetSpecialApparelScoreOffset()
		{
			return 0f;
		}
	}
}
