using UnityEngine;
using System.Collections;
using System;

public class FPSCount : MonoBehaviour {
    
    int[] fpsBuffer;
    int fpsBufferIndex;
    int frameRange = 60;
    public int AverageFPS = 0;
    void InitializeBuffer()
    {
        if(frameRange<=0)
        {
            frameRange = 1;
        }
        fpsBuffer = new int[frameRange];
        fpsBufferIndex = 0;
    }
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (fpsBuffer == null || fpsBuffer.Length != frameRange)
        {
            InitializeBuffer();
        }
        UpdateBuffer();
        CalculateFPS();
    }

    void UpdateBuffer()
    {
        fpsBuffer[fpsBufferIndex++] = (int)(1f / Time.unscaledDeltaTime);
        if(fpsBufferIndex>=frameRange)
        {
            fpsBufferIndex = 0;
        }
    }
    void CalculateFPS()
    {
        int sum = 0;
        for(int i=0;i<frameRange;i++)
        {
            sum += fpsBuffer[i];
        }
        AverageFPS = (int)(float)(sum / frameRange);
    }
}
