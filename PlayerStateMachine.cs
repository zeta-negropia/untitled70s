using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public Unit player;
    private battleSystem BSM;
    public enum TurnState
    {
        PROCESSING,
        ADDTOLIST,
        WAITING,
        SELECTING,
        ACTION,
        DEAD
    }

    public TurnState currentState;
    //for bar
    private float curCooldown = 0.0f;
    private float maxCooldown = 5.0f;

    //ienu
    public GameObject targetEnemy;
    private bool actionStarted = false;
    private Vector3 startPosition;
    private float animSpeed = 10f;

    //death ref
    private bool alive = true;

    void Start()
    {
        curCooldown = Random.Range(0, 2.5f);
        currentState = TurnState.PROCESSING;
        BSM = GameObject.Find("BattleSystem").GetComponent<battleSystem>();
        startPosition = transform.position;

    }

    
       

    private bool MoveToEnemy(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }

    private bool MoveToOrigin(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }

    public void takeDamage(int damageAmount)
    {
        //reduce hp by damage amount
        player.hitPoints -= damageAmount;
        //check if dead
        if (player.hitPoints <=0)
        {
            player.hitPoints = 0;
            currentState = TurnState.DEAD;
        }

        
    }

    //do damage

}
