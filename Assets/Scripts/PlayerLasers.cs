using System.Collections;
using UnityEngine;

public class PlayerLasers : MonoBehaviour {

    /*
     * Script placed on empty gameObjects at the tips of
     * the player's guns to enable laser fire
     */

    public ReticleManager reticleManager;

    private LineRenderer _line;
    private Transform _reticle;
    private GameObject _gunTip;


    private void Start()
    {
        _gunTip = this.gameObject;
        _line = _gunTip.GetComponent<LineRenderer>();
        _line.enabled = false;
        reticleManager = ReticleManager.Instance;
    }

    public void ShootLasers()
    {
        //StopCoroutine("FireLasers");
        //StartCoroutine("FireLasers");

        _reticle = reticleManager.GetReticleTransform();

        if (_reticle != null)
        {
            Debug.Log("Firing lasers");
            _line.enabled = true;

            Ray ray = new Ray(_gunTip.transform.localPosition, _gunTip.transform.forward);

            _line.SetPosition(0, _gunTip.transform.position);
            _line.SetPosition(1, _reticle.position);

            // thsi is happening so fast that the laser doesnt show up
            // use a coroutine!
            _line.enabled = false;
        }
    }

    //private IEnumerator FireLasers()
    //{
    //    Debug.Log("Got to FireLasers");
    //    _reticle = reticleManager.GetReticleTransform();

    //    if (_reticle != null)
    //    {
    //        _line.enabled = true;

    //        Ray ray = new Ray(transform.position, transform.forward);

    //        _line.SetPosition(0, ray.origin);
    //        _line.SetPosition(1, _reticle.position);

    //        _line.enabled = false;

    //        yield return null;
    //    }
    //    else
    //    {
    //        yield return null;
    //    }        
    //}

    private void OnEnable()
    {
        GameManager.OnShoot += ShootLasers;
    }

    private void OnDisable()
    {
        GameManager.OnShoot -= ShootLasers;
    }
}
