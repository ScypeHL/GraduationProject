using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEditor.Search;

public class UIPoolBar : MonoBehaviour
{
    [SerializeField] Image hpbar;
    Stats targetPool;

    public void Show(Stats _targetPool) 
    {
        targetPool = _targetPool;
        gameObject.SetActive(true);
    }

    public void Clear()
    {
        targetPool = null;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (targetPool == null) { return; }
        hpbar.fillAmount = Mathf.InverseLerp(0f, targetPool.maxValue, targetPool.value);
    }
}
