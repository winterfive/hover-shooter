using System.Collections;
using UnityEngine;

public class PlayerLasers : MonoBehaviour {

    /*
     * Script placed on empty gameObject at the tips of
     * the player's guns to enable laser fire
     */

    public ReticleManager reticleManager;
    public float laserDuration;

    private LineRenderer _line;
    private Transform _reticle;


    private void Start()
    {
        _line = this.gameObject.GetComponent<LineRenderer>();
        _line.enabled = false;
        reticleManager = ReticleManager.Instance;
    }

    public void ShootLasers()
    {
        StopCoroutine("FireLasers");
        StartCoroutine("FireLasers");
    }

    private IEnumerator FireLasers()
    {
        _reticle = reticleManager.GetReticleTransform();

        if (_reticle != null)
        {
            _line.enabled = true;

            Ray ray = new Ray(transform.position, transform.forward);

            _line.SetPosition(0, ray.origin);
            _line.SetPosition(1, _reticle.position);

            yield return new WaitForSeconds(laserDuration);

            _line.enabled = false;
        }
    }

    private void OnEnable()
    {
        GameManager.OnShoot += ShootLasers;
    }

    private void OnDisable()
    {
        GameManager.OnShoot -= ShootLasers;
    }
}
