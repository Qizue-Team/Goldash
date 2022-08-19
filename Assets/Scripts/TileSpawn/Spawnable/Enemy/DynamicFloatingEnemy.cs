using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicFloatingEnemy : Enemy
{
    [Header("Floating Settings")]
    [SerializeField]
    private float floatingSpeed = 1.0f;

    [SerializeField]
    private float minWaitTime = 0.2f;
    [SerializeField]
    private float maxWaitTime = 1.0f;

    private float _destinationY = 2.7f;
    private bool _canFloat = false;

    private const float MAX_Y = 2.7f;
    private const float MIN_Y = 1.0f;

    protected override void Start()
    {
        base.Start();
        _canFloat = false;
        float waitTime = Random.Range(minWaitTime, maxWaitTime);
        StartCoroutine(COWaitBeforeFloating(waitTime));
    }

    protected override void Update()
    {
        base.Update();
        if (_isDying)
            return;

        if (!_canFloat)
            return;

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector3.up, 1.0f);
        foreach(RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.gameObject.tag.Equals("LeftEdge"))
            {
                StopAllCoroutines();
                _canFloat = false;
                return;
            }
        }

        transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(transform.localPosition.y, _destinationY, Time.deltaTime * floatingSpeed), transform.localPosition.z);
        if (transform.localPosition.y >= MAX_Y - 0.1f)
        {
            _destinationY = MIN_Y;
        }
        else if (transform.localPosition.y <= MIN_Y + 0.1f)
        {
            _destinationY = MAX_Y;
        }

    }

    private IEnumerator COWaitBeforeFloating(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _canFloat = true;
    }
}
