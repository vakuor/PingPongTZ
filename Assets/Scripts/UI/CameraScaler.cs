using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class CameraScaler : MonoBehaviour
{
    private void Update() {
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = 1.777f;
        float sizey = 10f;
        
        if(screenRatio >= targetRatio){
            Camera.main.orthographicSize = sizey / 2;
        }
        else {
            float differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = sizey / 2 * differenceInSize;
        }
    }
}
