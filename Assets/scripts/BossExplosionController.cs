using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossExplosionController : MonoBehaviour
{
    public GameObject singleExplosion;
    public List<ExplostionSequenceElement> explosionSequence;

    private int nextElement;

    private void Awake()
    {
        explosionSequence.Sort();
        nextElement = 0;
        StartCoroutine(ExplosionCoroutine());
    }

    IEnumerator ExplosionCoroutine()
    {
        while (nextElement != explosionSequence.Count - 1)
        {
            spawnExplosion(explosionSequence[nextElement]);
            nextElement++;
            yield return new WaitForSeconds(explosionSequence[nextElement].startTime - explosionSequence[nextElement-1].startTime);
        }
        spawnExplosion(explosionSequence[nextElement]);
        Destroy(gameObject);
    }

    private void spawnExplosion(ExplostionSequenceElement explostion)
    {
        Instantiate(singleExplosion, (Vector2)transform.position + explostion.position, Quaternion.identity).SetActive(true);
    }

    [Serializable]
    public class ExplostionSequenceElement : IComparable<ExplostionSequenceElement>
    {
        public Vector2 position;
        public float startTime;

        public int CompareTo(ExplostionSequenceElement a)
        {
            return startTime.CompareTo(a.startTime);
        }
    }
}
