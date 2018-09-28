
using UnityEngine;

public class PlayerManager : GenericManager<PlayerManager>
{ 
    /*
     * Holds all values for player (health, alive?)
     */

    private bool _isAlive;
    [SerializeField] int _playerHealth;

    public bool IsAlive() { return _isAlive; }
    public int GetPlayerHealth() { return _playerHealth; }


    private void Awake()
    {
        _isAlive = true;
    }

}