
using UnityEngine;

public class PlayerManager : GenericManager<PlayerManager>
{ 
    private bool _isAlive;
    [SerializeField] int _playerHealth;

    public bool IsAlive() { return _isAlive; }
    public int GetPlayerHealth() { return _playerHealth; }


    private void Awake()
    {
        _isAlive = true;
    }

    private void Update()
    {
        if (Time.time > 20f)
        {
            _isAlive = false;
        }

        //if (_playerHealth <= 0)
        //{
        //    _isAlive = false;
        //}
    }
}