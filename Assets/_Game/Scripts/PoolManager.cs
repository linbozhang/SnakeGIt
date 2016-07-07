﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace SnakeOffline
{
    public class PoolManager : MonoBehaviour
    {
        public static PoolManager current;

        public GameObject snakePrefab;

        public GameObject bodyPartPrefab;
        public GameObject foodPartPrefab;

        List<GameObject> bodyParts = new List<GameObject>();

        List<byte> usedIDs = new List<byte>();
        
        void Awake()
        {
            current = this;
        }

        public byte GetPlayerID()
        {
            byte id =(byte) Random.Range(1,256);
            

            while (usedIDs.Contains(id))
            {
                id= (byte)Random.Range(1, 256);
            }
            return id;
        }


        private GameObject poolBodyPart(Vector2 pos)
        {
            GameObject go=Instantiate<GameObject>(bodyPartPrefab);
            bodyParts.Add(go);
			go.transform.position = pos;
            go.SetActive(false);
            return go;
        }

        public GameObject poolSnake(Vector2 pos)
        {
            GameObject go = Instantiate<GameObject>(snakePrefab);
			go.transform.position = pos;
            bodyParts.Add(go);
            return go;
        }

        public GameObject spawnBodyPart(Vector2 pos)
        {
            GameObject go = null;
            for (int i=0;i<bodyParts.Count;i++)
            {
                if(!bodyParts[i].activeInHierarchy)
                {
                    go = bodyParts[i];
                    go.transform.position = pos;
                    go.SetActive(true);
                    return go;
                }
            }
            go = poolBodyPart(pos);
            go.SetActive(true);
            return go;

        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

