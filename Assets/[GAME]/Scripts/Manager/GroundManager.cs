using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    #region Ground References
    [SerializeField] List<GameObject> grounds = new List<GameObject>();
    [Space]
    [SerializeField] float minWaitNewGroundTime;
    [SerializeField] float minDestroyNewGroundTime;
    float timer;
    #endregion

    #region Monobehaviour
    void Update()
    {
        GroundCheck();
    }
    #endregion

    #region Color Change
    void GroundCheck()
    {
        if (grounds.Count > 1)
        {
            timer += Time.deltaTime;
            if (timer >= minDestroyNewGroundTime)
            {
                int ground = grounds.Count;
                var index = grounds[ground - 1];
                var index1 = grounds[ground - 2];

                Ground(index);

                if (timer >= minWaitNewGroundTime)
                {

                    timer = 0.0f;

                    grounds.Remove(index.gameObject);
                    Destroy(index.gameObject);
                    index1.SetActive(true);
                }
            }
        }
    }


    void Ground(GameObject index)
    {
        StartCoroutine(ColorChange(index));
        index.transform.localScale -= new Vector3(10, 0, 10) * Time.deltaTime;
    }


    IEnumerator ColorChange(GameObject index)
    {
        var renderer = index.GetComponent<Renderer>();
        renderer.material.color = Color.red;
        yield return new WaitForSeconds(.3f);
        renderer.material.color = Color.white;
        yield return new WaitForSeconds(.3f);
        renderer.material.color = Color.red;
    }
    #endregion
}
