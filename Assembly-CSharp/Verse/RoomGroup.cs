using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Verse
{
	public class RoomGroup
	{
		private const float UseOutdoorTemperatureUnroofedFraction = 0.25f;

		public int ID = -1;

		private List<Room> rooms = new List<Room>();

		private RoomGroupTempTracker tempTracker;

		private int cachedOpenRoofCount = -1;

		private int cachedCellCount = -1;

		private static int nextRoomGroupID;

		public List<Room> Rooms
		{
			get
			{
				return this.rooms;
			}
		}

		public Map Map
		{
			get
			{
				return (!this.rooms.Any<Room>()) ? null : this.rooms[0].Map;
			}
		}

		public int RoomCount
		{
			get
			{
				return this.rooms.Count;
			}
		}

		public RoomGroupTempTracker TempTracker
		{
			get
			{
				return this.tempTracker;
			}
		}

		public float Temperature
		{
			get
			{
				return this.tempTracker.Temperature;
			}
			set
			{
				this.tempTracker.Temperature = value;
			}
		}

		public bool UsesOutdoorTemperature
		{
			get
			{
				return this.AnyRoomTouchesMapEdge || this.OpenRoofCount >= Mathf.CeilToInt((float)this.CellCount * 0.25f);
			}
		}

		public IEnumerable<IntVec3> Cells
		{
			get
			{
				RoomGroup.<>c__Iterator209 <>c__Iterator = new RoomGroup.<>c__Iterator209();
				<>c__Iterator.<>f__this = this;
				RoomGroup.<>c__Iterator209 expr_0E = <>c__Iterator;
				expr_0E.$PC = -2;
				return expr_0E;
			}
		}

		public int CellCount
		{
			get
			{
				if (this.cachedCellCount == -1)
				{
					this.cachedCellCount = 0;
					for (int i = 0; i < this.rooms.Count; i++)
					{
						this.cachedCellCount += this.rooms[i].CellCount;
					}
				}
				return this.cachedCellCount;
			}
		}

		public IEnumerable<Region> Regions
		{
			get
			{
				RoomGroup.<>c__Iterator20A <>c__Iterator20A = new RoomGroup.<>c__Iterator20A();
				<>c__Iterator20A.<>f__this = this;
				RoomGroup.<>c__Iterator20A expr_0E = <>c__Iterator20A;
				expr_0E.$PC = -2;
				return expr_0E;
			}
		}

		public int RegionCount
		{
			get
			{
				int num = 0;
				for (int i = 0; i < this.rooms.Count; i++)
				{
					num += this.rooms[i].RegionCount;
				}
				return num;
			}
		}

		public int OpenRoofCount
		{
			get
			{
				if (this.cachedOpenRoofCount == -1)
				{
					this.cachedOpenRoofCount = 0;
					for (int i = 0; i < this.rooms.Count; i++)
					{
						this.cachedOpenRoofCount += this.rooms[i].OpenRoofCount;
					}
				}
				return this.cachedOpenRoofCount;
			}
		}

		public bool AnyRoomTouchesMapEdge
		{
			get
			{
				for (int i = 0; i < this.rooms.Count; i++)
				{
					if (this.rooms[i].TouchesMapEdge)
					{
						return true;
					}
				}
				return false;
			}
		}

		public static RoomGroup MakeNew(Map map)
		{
			RoomGroup roomGroup = new RoomGroup();
			roomGroup.ID = RoomGroup.nextRoomGroupID;
			roomGroup.tempTracker = new RoomGroupTempTracker(roomGroup, map);
			RoomGroup.nextRoomGroupID++;
			return roomGroup;
		}

		public void AddRoom(Room room)
		{
			if (this.rooms.Contains(room))
			{
				Log.Error(string.Concat(new object[]
				{
					"Tried to add the same room twice to RoomGroup. room=",
					room,
					", roomGroup=",
					this
				}));
				return;
			}
			this.rooms.Add(room);
		}

		public void RemoveRoom(Room room)
		{
			if (!this.rooms.Contains(room))
			{
				Log.Error(string.Concat(new object[]
				{
					"Tried to remove room from RoomGroup but this room is not here. room=",
					room,
					", roomGroup=",
					this
				}));
				return;
			}
			this.rooms.Remove(room);
		}

		public bool PushHeat(float energy)
		{
			if (this.UsesOutdoorTemperature)
			{
				return false;
			}
			this.Temperature += energy / (float)this.CellCount;
			return true;
		}

		public void Notify_RoofChanged()
		{
			this.cachedOpenRoofCount = -1;
			this.tempTracker.RoofChanged();
		}

		public void Notify_RoomGroupShapeChanged()
		{
			this.cachedCellCount = -1;
			this.cachedOpenRoofCount = -1;
			this.tempTracker.RoomChanged();
		}

		public string DebugString()
		{
			return string.Concat(new object[]
			{
				"RoomGroup ID=",
				this.ID,
				"\n  first cell=",
				this.Cells.FirstOrDefault<IntVec3>(),
				"\n  RoomCount=",
				this.RoomCount,
				"\n  RegionCount=",
				this.RegionCount,
				"\n  CellCount=",
				this.CellCount,
				"\n  OpenRoofCount=",
				this.OpenRoofCount,
				"\n  ",
				this.tempTracker.DebugString()
			});
		}

		internal void DebugDraw()
		{
			int num = Gen.HashCombineInt(this.GetHashCode(), 1948571531);
			foreach (IntVec3 current in this.Cells)
			{
				CellRenderer.RenderCell(current, (float)num * 0.01f);
			}
			this.tempTracker.DebugDraw();
		}
	}
}