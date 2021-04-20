using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerInputController
{
    public static Vector2 GetFirstTouch() {
        Vector2 touchPosition = Vector2.zero;

#if (UNITY_EDITOR || UNITY_STANDALONE_WIN)
        touchPosition = Input.mousePosition;

#elif (UNITY_ANDROID || UNITY_IOS)
        if(IsTouching()){
            touchPosition = Input.GetTouch(0).position;
            //Debug.Log("Touch 0:" + touchPosition);
        } else{
            //Debug.Log("Not touching: " + touchPosition);
        }
#endif
        return touchPosition;
    }

    public static bool IsTouching(){
        
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        return true;

#elif UNITY_ANDROID || UNITY_IOS
        return Input.touchCount>0;
#endif
    }
}
