using System;
using System.Collections.Generic;
using System.Diagnostics;
using Verse;

namespace RimWorld
{
	public class StockGenerator_BuyArt : StockGenerator
	{
		[DebuggerHidden]
		public override IEnumerable<Thing> GenerateThings(int forTile)
		{
			StockGenerator_BuyArt.<GenerateThings>c__Iterator174 <GenerateThings>c__Iterator = new StockGenerator_BuyArt.<GenerateThings>c__Iterator174();
			StockGenerator_BuyArt.<GenerateThings>c__Iterator174 expr_07 = <GenerateThings>c__Iterator;
			expr_07.$PC = -2;
			return expr_07;
		}

		public override bool HandlesThingDef(ThingDef thingDef)
		{
			return thingDef.thingClass == typeof(Building_Art);
		}
	}
}
