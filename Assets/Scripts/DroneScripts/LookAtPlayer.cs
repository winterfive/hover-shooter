using UnityEngine;

public class LookAtPlayer : MonoBehaviour {
    
    private Transform _thisTransform;
    private Vector3 cam;

    private void Awake()
    {
        _thisTransform = this.gameObject.transform;
        cam = FindObjectOfType<Camera>().transform.position;
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
        Vector3 newVector = new Vector3(_thisTransform.position.x - cam.x,
                                        0f,
                                        _thisTransform.position.z - cam.z);

        _thisTransform.rotation = Quaternion.LookRotation(newVector);
    }
}
