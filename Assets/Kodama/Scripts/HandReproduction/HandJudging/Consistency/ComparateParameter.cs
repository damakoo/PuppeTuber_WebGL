using System.Collections.Generic;

public class ComparateParameter
{
  public bool isUsed { get; set; }
  public List<float> ConsistencyList { get; set; } = new List<float>();
  public float ConsistencyRate()
  {
    float _consistencyRate = 0;
    foreach (var num in ConsistencyList) _consistencyRate += num;
    return _consistencyRate / ConsistencyList.Count;
  }
}
