using UnityEngine;
using System.Collections;
namespace SnakeOffline
{
	public class CollisionSystem : MonoBehaviour {

		public const int resolution=10000;
		private byte [,] board=new byte[resolution,resolution];
		public static CollisionSystem current;
		void Awake()
		{
			current = this;
		}

		bool castCheck(int i,int j,byte id)
		{
			if (((i >= 0) && (j >= 0)) && (((i < resolution) && (j < resolution)) && (this.board [i, j] == id))) {
				return false;
			}
			return true;
		}
		void deletePoint(int i,int j,byte id)
		{
			if (((i >= 0) && (j >= 0)) && (((i < resolution) && (j < resolution)) && (this.board [i, j] == id))) {
				this.board [i, j] = 0;
			}
		}
		public void drawLine(Vector2 start,Vector2 end,byte id,bool delete)
		{
			this.plotLineAA(Mathf.RoundToInt(start.x),Mathf.RoundToInt(start.y),Mathf.RoundToInt(end.x),
				Mathf.RoundToInt(end.y),id,delete);
		}

		private void plotLineAA(int x0,int y0,int x1,int y1,byte id,bool delete)
		{
			int num = Mathf.Abs((int) (x1 - x0));
			int num2 = (x0 >= x1) ? -1 : 1;
			int num3 = Mathf.Abs((int) (y1 - y0));
			int num4 = (y0 >= y1) ? -1 : 1;
			int num7 = num - num3;
			int num8 = ((num + num3) != 0) ? ((int) Mathf.Sqrt((num * num) + (num3 * num3))) : 1;
			Label_005D:
			this.setPixelAA(x0, y0, id, delete);
			int num6 = num7;
			int num5 = x0;
			if ((2 * num6) >= -num)
			{
				if (x0 == x1)
				{
					return;
				}
				if ((num6 + num3) < num8)
				{
					this.setPixelAA(x0, y0 + num4, id, delete);
				}
				num7 -= num3;
				x0 += num2;
			}
			if ((2 * num6) > num3)
			{
				goto Label_005D;
			}
			if (y0 == y1)
			{
				return;
			}
			if ((num - num6) < num8)
			{
				this.setPixelAA(num5 + num2, y0, id, delete);
			}
			num7 += num;
			y0 += num4;
			goto Label_005D;
		}
		public int rayCast(Vector2 start,Vector2 direction,byte casterID,int maxDistance)
		{
			int num = -1;
			bool flag = false;
			direction = direction.normalized;
			Vector2 vector = start;
			while (!flag) {
				vector += direction;
				int i = Mathf.RoundToInt (vector.x);
				flag = this.castCheck (i, Mathf.RoundToInt (vector.y), casterID);
				num++;
				Vector2 vector2 = vector - start;
				if (vector2.sqrMagnitude > (maxDistance * maxDistance)) {
					flag = true;
				}
			}
			return num;
		}
		public int hitCheck(Vector3 current, Vector3 old, byte id)
		{
			int i = Mathf.RoundToInt(current.x);
			int j = Mathf.RoundToInt(current.y);
			int num3 = Mathf.RoundToInt(old.x);
			int num4 = Mathf.RoundToInt(old.y);
			int num5 = 0;
			int num6 = Mathf.Abs((int) (num3 - i));
			int num7 = (i >= num3) ? -1 : 1;
			int num8 = -Mathf.Abs((int) (num4 - j));
			int num9 = (j >= num4) ? -1 : 1;
			int num10 = num6 + num8;
			while (true)
			{
				num5 = this.hitPointCheck(i, j, id);
				if (num5 != 0)
				{
					return num5;
				}
				int num11 = 2 * num10;
				if (num11 >= num8)
				{
					if (i == num3)
					{
						return num5;
					}
					num10 += num8;
					i += num7;
				}
				if (num11 <= num6)
				{
					if (j == num4)
					{
						return num5;
					}
					num10 += num6;
					j += num9;
				}
			}
		}

		byte hitPointCheck(int i,int j,byte id)
		{
			if (((i < 0) || (j < 0)) || ((i >= resolution) || (j >= resolution))) {
				return 1;
			}
			if ((this.board [i, j] != 0) && (this.board [i, j] != id)) {
				return this.board [i, j];
			}
			return 0;
		}
		public bool rebuildCheck(Vector2 pos,int id)
		{
			int num = 1;
			int num2 = Mathf.RoundToInt (pos.x);
			int num3 = Mathf.RoundToInt (pos.y);
			for (int i = -num + 1; i < num; i++) {
				if (this.board [num2 + i, num3] == id) {
					return false;
				}
				if (this.board [num2, num3 + i] == id) {
					return false;
				}
			}
			return true;
		}
		private void setPixelAA(int x,int y,byte id,bool delete)
		{
			if (((x >= 0) && (y >= 0)) && (x < resolution) && (y < resolution)) {
				if (!delete) {
					this.board [x, y] = id;

				} else if (this.board [x, y] == id) {
					this.board [x, y] = 0;
				}
			}
		}
		void Start()
		{
			for (int i = 0; i < resolution; i++) {
				for (int j = 0; j < resolution; j++) {
					this.board [i, j] = 0;
				}
			}
		}
	}
}

