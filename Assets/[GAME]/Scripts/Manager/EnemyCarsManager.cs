using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCarsManager : MonoBehaviour
{
	#region Singleton
	public static EnemyCarsManager Instance;
    #endregion

    #region Cars List References
    public List<GameObject> enemyCars = new List<GameObject>();
    #endregion

    #region Public Variables
    [Space]
    public bool isEmptyCarList = false;
    #endregion

    #region Monobehavior
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (enemyCars.Count <= 0)
        {
            Debug.LogWarning("Win");
            isEmptyCarList = true;
            //win panel active
        }
    }
    #endregion
}
