using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public static class JsonHelper
{

    public static T[] FromJson<T>(string json)
    {

        string dummy_json = $"{{\"{DummyNode<T>.ROOT_NAME}\": {json}}}";

        var obj = JsonUtility.FromJson<DummyNode<T>>(dummy_json);
        return obj.array;
    }


    public static string ToJson<T>(IEnumerable<T> collection)
    {
        string json = JsonUtility.ToJson(new DummyNode<T>(collection)); // ダミールートごとシリアル化する
        int start = DummyNode<T>.ROOT_NAME.Length + 4;
        int len = json.Length - start - 1;
        return json.Substring(start, len); // 追加ルートの文字を取り除いて返す
    }

    // 内部で使用するダミーのルート要素
    [System.Serializable]
    private struct DummyNode<T>
    {

        public const string ROOT_NAME = nameof(array);
        // 疑似的な子要素
        public T[] array;
        // コレクション要素を指定してオブジェクトを作成する
        public DummyNode(IEnumerable<T> collection) => this.array = collection.ToArray();
    }
}