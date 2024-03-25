using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    [Header("Enemy Variables")]
    public int health = 3;

    [Header("AI Variables")]
    public GameObject Player;
    public RaycastHit canSeePlayer;
    private NavMeshAgent agent;
    public bool PlayerInAttackRange;

    [Header("Animation Variables")]
    public Animator _Animator;
    public ZombieStates AnimationState = ZombieStates.idle;
    public bool CanMoveToTarget;
    public bool TargetVisible;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        SwapState(AnimationState);
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, (Player.transform.position-transform.position ), out canSeePlayer)) 
        {
            if (canSeePlayer.transform.gameObject == Player)
            {
                agent.destination = new(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z);
                //Debug.Log("Can See player");
                TargetVisible = true;
                agent.gameObject.transform.LookAt(Player.transform);
                agent.transform.rotation = Quaternion.Euler(0, agent.transform.rotation.eulerAngles.y, 0);
            }
            else 
            {
                TargetVisible = false; 
            }
            //Debug.Log(canSeePlayer.collider.gameObject.name);
        }

        switch (AnimationState) 
        {
            case ZombieStates.idle: IdleDetection(); break;
            case ZombieStates.walking: Walking(); break;
            case ZombieStates.attacking:CheckAttacking(); break;
            case ZombieStates.dead:break;
        }
        
    }

    void IdleDetection() 
    {
        if (PlayerInAttackRange) 
        {
            SwapState(ZombieStates.attacking);
        }
        if (TargetVisible) 
        {
            SwapState( ZombieStates.walking );
        }
    }

    void Walking() 
    {
        if (PlayerInAttackRange)
        {
            SwapState(ZombieStates.attacking);
        }
        // We quit here
        if (!TargetVisible && (agent.velocity.x == 0 && agent.velocity.z == 0))
        {
            SwapState(ZombieStates.idle);

        }
        //else { Debug.Log($"{agent.velocity.x} + {agent.velocity.y}"); }
    }
    /// <summary>
    /// Swap the zombie's current state
    /// </summary>
    /// <param name="state"></param>
    void SwapState( ZombieStates state ) 
    {
        //First make sure we do everything to end the current state
        switch (AnimationState) 
        {
            case ZombieStates.attacking: StopAttacking();break;
        }

        SetAnimationState(state);
        AnimationState = state;
        switch (state) // Stuff goes here when I need it to 
        {
            case ZombieStates.idle: break;
            case ZombieStates.walking: break;
            case ZombieStates.dead: _Animator.SetTrigger("Die"); break;
            case ZombieStates.attacking: StartAttacking(); break;
        }
    }

  
    /// <summary>
    /// Useful for swapping animation only
    /// </summary>
    void ClearAnimationStates() 
    {
        _Animator.SetBool("Dead", false);
        _Animator.SetBool("Walking", false);
        _Animator.SetBool("Attacking", false);
        _Animator.SetBool("Idling", false);
    }
    /// <summary>
    /// DONT USE THIS LET OTHER THIGNS CONTROL ANIMATION STATE 90% OF THE TIME
    /// </summary>
    /// <param name="myState"></param>
    void SetAnimationState(ZombieStates myState) 
    {
        ClearAnimationStates();
        switch (myState) 
        {
            case ZombieStates.walking: _Animator.SetBool("Walking", true); break;
            case ZombieStates.dead: _Animator.SetBool("Dead", true); break;
            case ZombieStates.attacking: _Animator.SetBool("Attacking", true); break;
            case ZombieStates.idle: _Animator.SetBool("Idling", true); break;
        }
    }

    public void Damage(GameObject damager) 
    {
        health--;
        if (health <= 0) 
        {
            //Death here
            SetAnimationState(ZombieStates.dead);
        }
        agent.SetDestination(new(damager.transform.position.x, damager.transform.position.y, damager.transform.position.z));
        _Animator.SetTrigger("Damage");
        if (AnimationState == ZombieStates.idle) 
        {
            SwapState(ZombieStates.walking);
        }

    }

    public void StartAttacking() 
    {
        agent.isStopped = true;
    }

    public void StopAttacking()
    {
        agent.isStopped = false;
    }

    public void CheckAttacking() 
    {
        if (!PlayerInAttackRange) 
        {
            StopAttacking();
            SwapState(ZombieStates.idle);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            PlayerInAttackRange = true;
        }
        Debug.Log($"Object {other.name} entered {gameObject.name}'s range.  It's tag is {other.tag} ");
    }

    public void OnTriggerExit(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            PlayerInAttackRange = false;
        }
    }

}

[System.Serializable]
public enum ZombieStates 
{
    walking,
    dead,
    idle,
    attacking
}