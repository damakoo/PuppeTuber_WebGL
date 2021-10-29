using System.Collections.Generic;
using System;

public class ConsistencyTables
{
  public Dictionary<handState, ComparateParameter> ConsistencyTable { get; set; } = new Dictionary<handState, ComparateParameter>();
  public ConsistencyTables()
  {
    foreach (var key in Enum.GetNames(typeof(handState)))
    {
      handState handState = (handState)Enum.Parse(typeof(handState), key);
      ConsistencyTable.Add(handState, new ComparateParameter());
    }
  }
  public float ConsistencyResult()
  {
    int n = 0;
    float result = 0;
    foreach(var state in ConsistencyTable.Values)
    {
      if(state.isUsed)
      {
        n += 1;
        result += state.ConsistencyRate();
      }
    }
    return result / n;
  }
}
