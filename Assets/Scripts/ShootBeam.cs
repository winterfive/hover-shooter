using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBeam : MonoBehaviour {

    public int droneShotRange;

    private RaycastHit _hit;
    private LineRenderer _line;

    private void Start()
    {
        _line = this.GetComponent<LineRenderer>();
        _line.enabled = true;
    }

    private void Update()
    {
        if (Time.frameCount % 10 == 0)
        {
            if (Physics.Raycast(this.transform.position, this.transform.forward, out _hit, droneShotRange))
            {
                if (_hit.transform.gameObject.tag == "Player")
                {
                    Debug.Log("Got into tag check");
                    _line.SetPosition(0, this.transform.position);
                    _line.SetPosition(1, _hit.point);
                    _line.enabled = false;

                    // Call event to notify UI (score) and gameManger or playermanager (player health)
                }
            }
        }
    }
}
