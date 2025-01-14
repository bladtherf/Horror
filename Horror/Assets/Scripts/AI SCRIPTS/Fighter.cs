﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    [SerializeField] float weaponRange = 2f;
    [SerializeField] float timeBetweenAttacks = 1f;
    [SerializeField] float weaponDamage = 5f;

    Health target;
    float timeSinceLastAttack = Mathf.Infinity;



    private void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
        if (target == null) return;
        if (target.IsDead()) return;


        if (!GetIsInRange())
                {
                    GetComponent<AIMover>().MoveTo(target.transform.position);
                }
                else
                {
                    GetComponent<AIMover>().Stop();
                    AttackBehaviour();
                }
     }
    private void AttackBehaviour()
    {

        transform.LookAt(target.transform);
        if (timeSinceLastAttack > timeBetweenAttacks)
        {
            TriggerAttack();
            timeSinceLastAttack = 0;
        }
    }

    private void TriggerAttack()
    {
        GetComponent<Animator>().ResetTrigger("StopAttack");
        GetComponent<Animator>().SetTrigger("Attack");
    }

    void Hit()
    {

        if (target == null) { return; }
        target.TakeDamage(weaponDamage);
    }

    public bool CanAttack(GameObject combatTarget)
    {
        if (combatTarget == null) { return false; }
        Health targetToTest = combatTarget.GetComponent<Health>();
        return targetToTest != null && !targetToTest.IsDead();
    }


    private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public void Attack(GameObject combatTarget)
        {
        GetComponent<ActionScheduler>().StartAction(this);
        target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
        StopAttack();
        target = null;
        }
  

    private void StopAttack()
    {
        GetComponent<Animator>().ResetTrigger("Attack");
        GetComponent<Animator>().SetTrigger("StopAttack");
    }
}

