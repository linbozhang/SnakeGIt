using UnityEngine;
using System.Collections;
namespace SnakeOffline
{
	
		public class SnakeAIController : MonoBehaviour {

			private Vector3 decidedDirection;
			private Vector3 headPosition;
			private bool onEscape;
			private  SnakeController player;

			private void escapeCheck()
			{
            if (this.player.bodyList[0] == null)
                return;
				int maxDistance = 0x4b;
				int num2 = CollisionSystem.current.rayCast(this.player.bodyList[0].transform.position, this.player.direction, this.player.playerID, maxDistance);
				int num3 = CollisionSystem.current.rayCast(this.player.bodyList[0].transform.position, (Vector2) (Quaternion.AngleAxis(-20f, Vector3.forward) * this.player.direction), this.player.playerID, maxDistance);
				int num4 = CollisionSystem.current.rayCast(this.player.bodyList[0].transform.position, (Vector2) (Quaternion.AngleAxis(20f, Vector3.forward) * this.player.direction), this.player.playerID, maxDistance);
				if (num2 < (maxDistance - 5))
				{
					if (num3 > num4)
					{
						this.decidedDirection = (Vector3) (Quaternion.AngleAxis((float) -(90 - num4), Vector3.forward) * this.player.direction);
					}
					else if (num3 < num4)
					{
						this.decidedDirection = (Vector3) (Quaternion.AngleAxis((float) (90 - num3), Vector3.forward) * this.player.direction);
					}
					else
					{
						this.decidedDirection = (Vector3) (Quaternion.AngleAxis((float) UnityEngine.Random.Range(-45, 0x2d), Vector3.forward) * this.player.direction);
					}
				}
				else
				{
					maxDistance -= 0x19;
					num3 = CollisionSystem.current.rayCast(this.player.bodyList[0].transform.position, (Vector2) (Quaternion.AngleAxis(-45f, Vector3.forward) * this.player.direction), this.player.playerID, maxDistance);
					num4 = CollisionSystem.current.rayCast(this.player.bodyList[0].transform.position, (Vector2) (Quaternion.AngleAxis(45f, Vector3.forward) * this.player.direction), this.player.playerID, maxDistance);
					if ((num3 < (maxDistance - 5)) || (num4 < (maxDistance - 5)))
					{
						if (num3 > num4)
						{
							this.decidedDirection = (Vector3) (Quaternion.AngleAxis((float) -(50 - num4), Vector3.forward) * this.player.direction);
						}
						else if (num3 < num4)
						{
							this.decidedDirection = (Vector3) (Quaternion.AngleAxis((float) (50 - num3), Vector3.forward) * this.player.direction);
						}
					}
				}
			}

			private int getAvailableDistance(Vector3 target, int maxDistance)
			{
				target = target.normalized;
				return CollisionSystem.current.rayCast(this.player.bodyList[0].transform.position, target, this.player.playerID, maxDistance);
			}

			private void goToEatableObjects()
			{
				this.headPosition = this.player.bodyList[0].transform.position;
				GameObject gameObject = null;
				GameObject obj3 = null;
				float magnitude = 10000f;
				float num2 = 10000f;
				foreach (Collider2D colliderd in Physics2D.OverlapCircleAll(this.headPosition, 50f))
				{
					if (colliderd.gameObject.tag == "eatableObject")
					{
						Vector3 from = colliderd.transform.position - this.headPosition;
						if ((Mathf.Abs(Vector3.Angle(from, this.player.direction)) < 45f) && (from.magnitude < magnitude))
						{
							magnitude = from.magnitude;
							gameObject = colliderd.gameObject;
						}
						if (from.magnitude < num2)
						{
							num2 = from.magnitude;
							obj3 = colliderd.gameObject;
						}
					}
				}
				if (gameObject != null)
				{
					if (this.getAvailableDistance(gameObject.transform.position - this.headPosition, 0x4b) > 60)
					{
						this.decidedDirection = gameObject.transform.position - this.headPosition;
					}
				}
				else if ((obj3 != null) && (this.getAvailableDistance(obj3.transform.position - this.headPosition, 0x4b) > 60))
				{
					this.decidedDirection = obj3.transform.position - this.headPosition;
				}
			}

			private void goToEjectedMass()
			{
				this.headPosition = this.player.bodyList[0].transform.position;
				GameObject gameObject = null;
				GameObject obj3 = null;
				float magnitude = 10000f;
				float num2 = 10000f;
				foreach (Collider2D colliderd in Physics2D.OverlapCircleAll(this.headPosition, 75f))
				{
					if (colliderd.gameObject.tag == "ejectedMass")
					{
						Vector3 from = colliderd.transform.position - this.headPosition;
						if ((Mathf.Abs(Vector3.Angle(from, this.player.direction)) < 45f) && (from.magnitude < magnitude))
						{
							magnitude = from.magnitude;
							gameObject = colliderd.gameObject;
						}
						if (from.magnitude < num2)
						{
							num2 = from.magnitude;
							obj3 = colliderd.gameObject;
						}
					}
				}
				if (gameObject != null)
				{
					this.decidedDirection = gameObject.transform.position - this.headPosition;
				}
				else if (obj3 != null)
				{
					this.decidedDirection = obj3.transform.position - this.headPosition;
				}
			}

			private void makeEatDecision()
			{
				this.player.onEject = false;
				if (!this.onEscape)
				{
					this.decidedDirection = Vector3.zero;
					this.goToEjectedMass();
					if (((this.decidedDirection != Vector3.zero) && (Mathf.Abs(Vector3.Angle(this.decidedDirection, this.player.direction)) < 30f)) && (this.getAvailableDistance(this.decidedDirection, 60) > 50))
					{
						this.player.onEject = true;
					}
					if (this.decidedDirection == Vector3.zero)
					{
						this.goToEatableObjects();
					}
					if (this.decidedDirection != Vector3.zero)
					{
						this.player.direction = this.decidedDirection.normalized;
					}
				}
			}

			private void makeEscapeDecision()
			{
				this.onEscape = false;
				this.decidedDirection = Vector3.zero;
				this.escapeCheck();
				if (this.decidedDirection != Vector3.zero)
				{
					this.player.direction = this.decidedDirection.normalized;
					this.onEscape = true;
				}
			}

			private void Start()
			{
				this.player = base.GetComponent<SnakeController>();
				if (!this.player.isPlayer)
				{
					base.InvokeRepeating("makeEscapeDecision", UnityEngine.Random.RandomRange(0.1f, 1f), UnityEngine.Random.RandomRange(0.1f, 0.3f));
					//base.InvokeRepeating("makeEatDecision", UnityEngine.Random.RandomRange(0.1f, 1f), UnityEngine.Random.RandomRange(0.3f, 0.7f));
				}
			}

		}
}

