using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneGlow : MonoBehaviour {

    public float glowSpeed;
    public Color secondGlow;

    private Renderer _glowRend;
    private Color _defaultGlow;

    
    void Start ()
    {
        _glowRend = this.GetComponent<Renderer>();
        _defaultGlow = _glowRend.material.color;
    }
	
	
	void Update ()
    {
        LerpColor();		
	}


    /*
     * Pingpongs drone glow steadily from one color to another
     * void -> void
     */
    void LerpColor()
    {
        if (_glowRend)
        {
            float pingpong = Mathf.PingPong(Time.time * glowSpeed, 1.0f);
            _glowRend.material.color = Color.Lerp(_defaultGlow, secondGlow, pingpong);
        }
    }
}
