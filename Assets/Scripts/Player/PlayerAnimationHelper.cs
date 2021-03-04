using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHelper : MonoBehaviour
{
    [SerializeField]
    private Weapon weapon;

    private Animator anim;
    private bool checkCombo;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        

        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Attack");

            if(checkCombo)
            {
                anim.SetTrigger("Attack2");
            } 
            else
            {
                anim.SetTrigger("Attack");
            }
        }

    }

    public void MeleeAttackStart()
    {
        weapon.AttackStart(); 
    }

    public void MeleeAttackEnd()
    {
        weapon.AttackEnd();
        anim.ResetTrigger("Attack");
        checkCombo = true;
    }

    public void ComboStart()
    {

        checkCombo = true;
    }

    public void ComboEnd()
    {
        
        checkCombo = false;
    }


}
