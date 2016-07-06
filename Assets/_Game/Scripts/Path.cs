using UnityEngine;
using System.Collections;
namespace SnakeOffline
{
    public class PathPoint
    {
        public PathPoint nextPoint;
        public Vector2 position;
        public GameObject pv;
    }

    public class Path
    {
        private static bool debug;
        public PathPoint head;
        private const float minPointDistance = 5f;
        private const float sqrMinPointDis = minPointDistance * minPointDistance;
        public SnakeController player;
        public GameObject pointVisual;
        private PathPoint tempPoint;

        public void addPointToHead(Vector2 pos)
        {
            if(this.head!=null)
            {
                Vector2 vector = this.head.position - pos;
                if (vector.sqrMagnitude < sqrMinPointDis)
                    return;
            }
            this.tempPoint = new PathPoint();
            this.tempPoint.position = pos;
            if(this.head!=null)
            {
                this.head = this.tempPoint;
            }
            else
            {
                this.tempPoint.nextPoint = this.head;
                this.head = this.tempPoint;
            }
            if((this.head!=null)&&(this.head.nextPoint!=null))
            {
                //
            }
        }

        public void deletePoint(PathPoint toDelete)
        {
            if((toDelete!=null)&&(toDelete.nextPoint!=null))
            {
                //
                this.deletePoint(toDelete.nextPoint);
                toDelete.nextPoint = null;
            }
        }
    }
}

