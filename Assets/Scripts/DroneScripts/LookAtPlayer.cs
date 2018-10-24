using UnityEngine;

public class LookAtPlayer : MonoBehaviour {

    public Camera cam;

    private Transform _thisTransform;


    private void Awake()
    {
        _thisTransform = this.gameObject.transform;
    }

    
    void Update ()
    {
        LookAt();		
	}

    /*
    * Changes position and rotation of turret to look at target using only y axis
    * void -> void
    */
    public void LookAt()
    {
        Vector3 target = cam.transform.position;

        Vector3 newVector = new Vector3(_thisTransform.position.x - target.x,
                                        0f,
                                        _thisTransform.position.z - target.z);

        _thisTransform.rotation = Quaternion.LookRotation(newVector);
    }
}
