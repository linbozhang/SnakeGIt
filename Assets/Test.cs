using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
//		Debug.Log (Quaternion.identity);
//		Quaternion q = Quaternion.FromToRotation (new Vector3(1,0,0),new Vector3(-1,0,0));
//		Debug.Log (q);
//		Quaternion q1 = Quaternion.FromToRotation (new Vector3(1,0,0),new Vector3(0,-1,0));
//		Debug.Log (q1);

//		Quaternion q= Quaternion.AngleAxis (10, Vector3.forward);
//		Debug.Log( q.eulerAngles);
//		Quaternion q1= Quaternion.AngleAxis (-10, Vector3.forward);
//		Debug.Log( q1.eulerAngles);
		Debug.Log( Quaternion.FromToRotation(Vector2.left,Vector2.left).eulerAngles);
		Debug.Log( Quaternion.FromToRotation(Vector2.left,Vector2.right).eulerAngles);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
