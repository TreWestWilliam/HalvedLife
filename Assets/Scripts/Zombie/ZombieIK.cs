using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class ZombieIK : MonoBehaviour
{
    
    public Animator animator;
    public Transform LeftFoot;
    public Transform RightFoot;
    public GameObject Player;
    public float MaxSnapDistance = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (Player == null) { Player = Camera.main.gameObject; }
    }

    void OnAnimatorIK() 
    {

        if (animator) 
        {
            animator.SetLookAtWeight(1);
            animator.SetLookAtPosition(Player.transform.position);

            //Ray LeftFootRay = new Ray(animator.)
            if (LeftFoot != null) 
            {
                Ray LFRay = new Ray(LeftFoot.position, -LeftFoot.up);
                if (Physics.Raycast(LFRay, out RaycastHit info, MaxSnapDistance)) 
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
                    animator.SetIKPosition(AvatarIKGoal.LeftFoot, info.point);
                }
                else 
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
                }

            }

            if (RightFoot != null)
            {
                Ray LFRay = new Ray(RightFoot.position, -RightFoot.up);
                if (Physics.Raycast(LFRay, out RaycastHit info, MaxSnapDistance))
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
                    animator.SetIKPosition(AvatarIKGoal.RightFoot, info.point);
                }
                else
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
                }

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
