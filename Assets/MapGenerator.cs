using UnityEngine;
using System.Collections;
namespace SnakeOffline
{
	public class MapGenerator : MonoBehaviour {
		public Transform mapHolder;
		public Sprite mapTail;

		const float mapSizeX=6.60f;
		const float mapSizeY=5.87f;


		private bool hasGenerated = false;

		public void GeneratorMap()
		{
			if (hasGenerated)
				return;
			int x = Mathf.CeilToInt( GameConfig.MapRadius / mapSizeX);
			int y = Mathf.CeilToInt( GameConfig.MapRadius / mapSizeY);

			for (int i = 0; i < x; i++) {
				for (int j = 0; j < y; j++) {
					GameObject go= new GameObject ();
					go.AddComponent<SpriteRenderer> ().sprite = mapTail;
					go.transform.SetParent (mapHolder);
					go.transform.position = new Vector2 (i * mapSizeX, j * mapSizeY);
				}
			}
		}
	}

}
