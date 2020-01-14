using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSS : SingletonMonoBehavior<FPSS>
{
    public int FinishInSpecifySec = 0;
    public float SpecifySec;

    [SerializeField]
    float FinshPerSpecifySecView;
    private void Update()
    {
        FinshPerSpecifySecView = FinshPerSpecifySec;
    }

    public float FinshPerSpecifySec {
        get {
            return FinishInSpecifySec / SpecifySec;
        }
    }

    public void RecordFinish()
    {
        FinishInSpecifySec++;
        StartCoroutine(FinishSecRemoverEnumerator());
    }

    IEnumerator FinishSecRemoverEnumerator()
    {
        yield return new WaitForSeconds(SpecifySec);
        FinishInSpecifySec--;
    }
}
