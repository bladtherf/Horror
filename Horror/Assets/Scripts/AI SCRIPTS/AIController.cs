﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] float chaseDistance = 5f;

    Fighter fighter;
    GameObject player;
    Health health;

    private void Start()
    {
        fighter = GetComponent<Fighter>();
        player = GameObject.FindWithTag("Player");
        health = GetComponent<Health>();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }


    private void Update()
    {

        if (health.IsDead()) return;
        if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
        {
            fighter.Attack(player);
        }
        else
        {
            
            fighter.Cancel();
        }
    }

    private bool InAttackRangeOfPlayer()
    {
       
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        return distanceToPlayer < chaseDistance;
    }

}
