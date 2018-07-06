using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    public delegate void NewObjectFound();
    public static event NewObjectFound OnNewObjectFound;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(RaycastManager._hasNewObject)
        {
            if(OnNewObjectFound != null)
            {
                OnNewObjectFound();
            }
        }
	} 
}
