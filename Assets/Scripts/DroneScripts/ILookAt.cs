using UnityEngine;
using System.Collections;

/*
* Turns drone object towards another object
* Transform -> void
*/
interface ILookAt<T>
{   
    void LookAt(T transformBeingLookedAt);
}