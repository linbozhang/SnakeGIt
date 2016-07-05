using UnityEngine;
using System.Collections;
namespace SnakeOffline
{
	public class CollisionSystem : MonoBehaviour {

		public int Resolution=10000;
		private byte [,] board=new byte[Resolution,Resolution];
		public static CollisionSystem current;
		void Awake()
		{
			current = this;
		}

		bool castCheck(int i,int j,byte id)
		{
			if (((i >= 0) && (j >= 0)) && (((i < Resolution) && (j < Resolution)) && (this.board [i, j] == id))) {
				return false;
			}
			return true;
		}
		void deletePoint(int i,int j,byte id)
		{
			if (((i >= 0) && (j >= 0)) && (((i < Resolution) && (j < Resolution)) && (this.board [i, j] == id))) {
				this.board [i, j] = 0;
			}
		}
		public void drawLine(Vector2 start,Vector2 end,byte id,bool delete)
		{
			//this.plotLineAA(Mathf.RoundToInt(start.x),Mathf.RoundToInt(start.y),Mathf.RoundToInt(end.x),
			//Mathf.RoundToInt(end.y));
		}

		byte hitPointCheck(int i,int j,byte id)
		{
			if (((i < 0) || (j < 0)) || ((i >= Resolution) || (j >= Resolution))) {
				return 1;
			}
			if ((this.board [i, j] != 0) && (this.board [i, j] != id)) {
				return this.board [i, j];
			}
			return 0;
		}
	}
}

