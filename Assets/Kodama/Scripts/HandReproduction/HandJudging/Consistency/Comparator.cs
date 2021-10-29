using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Comparator : MonoBehaviour
{
  [SerializeField] UserStudyAnimator NowAnimator;
  [SerializeField] UserStudyAnimator BeforeAnimator;
  [SerializeField] Text resultUI;
  private handState nowState => NowAnimator._handState;
  private handState beforeState => BeforeAnimator._handState;
  private ConsistencyTables consistencytables = new ConsistencyTables();
  private Dictionary<handState, ComparateParameter> ConsistencyTable => consistencytables.ConsistencyTable;
  public void ShowResult()
  {
    Debug.Log("Result : " + ConsistencyTable[handState.defaultstate].ConsistencyRate().ToString());
    resultUI.enabled = true;
    double result = consistencytables.ConsistencyResult();
    resultUI.text = "Result : " + result.ToString();
    ResultSceneManager.result = result;
  }
  public void UpdateParameter()
  {
    if(beforeState == nowState)
    {
      ConsistencyTable[beforeState].isUsed = true;
      ConsistencyTable[beforeState].ConsistencyList.Add(1);
    }
    else
    {
      ConsistencyTable[beforeState].isUsed = true;
      ConsistencyTable[beforeState].ConsistencyList.Add(0);
    }
  }
}
