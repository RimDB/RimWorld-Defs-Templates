using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Verse;

namespace RimWorld
{
	public class TrainableDef : Def
	{
		public float difficulty = -1f;

		public float minBodySize;

		public List<TrainableDef> prerequisites;

		[NoTranslate]
		public List<string> tags = new List<string>();

		public bool defaultTrainable;

		public TrainableIntelligenceDef requiredTrainableIntelligence;

		public int steps = 1;

		public float listPriority;

		public string icon;

		[Unsaved]
		public int indent;

		[Unsaved]
		private Texture2D iconTex;

		public Texture2D Icon
		{
			get
			{
				if (this.iconTex == null)
				{
					this.iconTex = ContentFinder<Texture2D>.Get(this.icon, true);
				}
				return this.iconTex;
			}
		}

		public bool MatchesTag(string tag)
		{
			if (tag == this.defName)
			{
				return true;
			}
			for (int i = 0; i < this.tags.Count; i++)
			{
				if (this.tags[i] == tag)
				{
					return true;
				}
			}
			return false;
		}

		[DebuggerHidden]
		public override IEnumerable<string> ConfigErrors()
		{
			TrainableDef.<ConfigErrors>c__Iterator9C <ConfigErrors>c__Iterator9C = new TrainableDef.<ConfigErrors>c__Iterator9C();
			<ConfigErrors>c__Iterator9C.<>f__this = this;
			TrainableDef.<ConfigErrors>c__Iterator9C expr_0E = <ConfigErrors>c__Iterator9C;
			expr_0E.$PC = -2;
			return expr_0E;
		}
	}
}
