using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyAI : MonoBehaviour
{
    //CONST 
    public static readonly Vector4 MapLimit = new Vector4(-20, 20, -11.3f, 11.3f);
    public static readonly Vector3[] Directions = {Vector2.right, -Vector2.right, Vector2.up, Vector2.down};

    public enum EnemyTypes
    {
        Kamikaze,
        StaticGatling,
        StraightGatling,
        Pump
    }

    public EnemyTypes type;
    public float targetUpdateDelay = 1f;
    public float speedModifier = 1f;

    private float _delay;
    private Transform _player;
    private Vector3 _target;
    private float _currentSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;

        _target = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.DOKill(); //FIX of a bug made by the wave manager !

        _delay -= Time.deltaTime;

        if (_delay < 0)//We dont always update the target to give a more organic reaction to AI
        {
            _delay = targetUpdateDelay;
            AcquireTarget(type);
        }

        //We move only when neeeded
        if (Vector3.Distance(transform.position, _target) > 1f)
            Move();
    }


    void AcquireTarget(EnemyTypes t)
    {
        if (!_player) //Destroy when no player
            Destroy(gameObject);
        
        Vector3 playerPos = _player.position;
        float dist = Vector3.Distance(playerPos, transform.position);
        switch (t)
        {
            case EnemyTypes.Kamikaze:
                _target = playerPos;
                _currentSpeed = 1 + (5 - Mathf.Clamp( dist/ 5f, 0, 5)); //Kamikaze go faster when closer
                break;
            case EnemyTypes.StaticGatling:
                Vector3 posClamped = ClampMapLimit(transform.position);
                _target = posClamped - Random.Range(0.2f, 0.3f)*posClamped; //We take the limit of the map and center a little
                _currentSpeed = 3f;
                break;
            case EnemyTypes.StraightGatling:
                _target = transform.position + 20 * Directions[Random.Range(0, 4)]; //Move in one direction
                _currentSpeed = 2f;
                break;
            case EnemyTypes.Pump:
                if (dist < 3)
                {
                    //ON Shoot et dash !
                   // SendMessage("StartFire");
                    _target = transform.position + transform.right * Random.Range(-10, 10);
                    _currentSpeed = 10;
                    _delay = 2f;

                }
                else
                {
                    //On va voir le joueur
                    //SendMessage("EndFire");
                    _target = playerPos;
                    _currentSpeed = 2.5f;
                }
                break;

        }
    }

    void Move()
    {
        _target = ClampMapLimit(_target); 
        Vector3 path = _target - transform.position;
        path.Normalize();

        transform.position += path * ((_currentSpeed*speedModifier) * Time.deltaTime) ;
    }

    Vector3 ClampMapLimit(Vector3 pos)
    {
       return new Vector3(Mathf.Clamp(pos.x, MapLimit.x, MapLimit.y),
            Mathf.Clamp(pos.y, MapLimit.z, MapLimit.w), 0);
        
    }

    
}
