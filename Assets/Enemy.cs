using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float stunDuration = 2f;
    private bool isStunned = false;

    public void Stun()
    {
        if (!isStunned)
        {
            isStunned = true;
            StartCoroutine(StunTimer());
        }
    }

    private IEnumerator StunTimer()
    {
        yield return new WaitForSeconds(stunDuration);
        isStunned = false;
    }
}

