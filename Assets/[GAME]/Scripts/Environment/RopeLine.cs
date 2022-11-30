using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RopeLine : MonoBehaviour
{
    #region Private-Public Variables
    LineRenderer line;
    [Space]
    public GameObject rope;
    public float ropeWidth = 0.5f;
    #endregion

    #region Monobehaviour
    void Start()
    {
        SetRopeLine();
    }

    void Update()
    {
        if (!GameManager.Instance.isStarted)
            return;
        ConnectRopeLine();
    }
    #endregion


    #region Rope Line
    void SetRopeLine()
    {
        if (rope == null)
        {
            return;
        }
        line = GetComponent<LineRenderer>();

        line.startWidth = ropeWidth;
        line.endWidth = ropeWidth;



        line.useWorldSpace = true;

        line.positionCount = 2;

        line.SetPosition(0, gameObject.transform.position);
        line.SetPosition(1, rope.transform.position);
    }

    void ConnectRopeLine()
    {
        line.SetPosition(0, gameObject.transform.position);
        line.SetPosition(1, rope.transform.position);
    }
    #endregion
}
