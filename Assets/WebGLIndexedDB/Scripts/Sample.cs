using IndexDB;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

namespace IndexDB
{
    sealed public class Sample : MonoBehaviour
    {

        /// <summary>
        /// データベース名
        /// </summary>
        private string DataBaseName;

        /// <summary>
        /// テーブル名
        /// </summary>
        private string StoreName = "Sample";

        /// <summary>
        /// 保存先キー
        /// </summary>
        private string Key = "Test";

        /// <summary>
        /// Json取得データ
        /// </summary>
        private Dictionary<string, object> loadData = new Dictionary<string, object>();

        // WebGLのTextは表示の際日本語出力対応していないので、フォントを変更するように
        [Header("表示用テキスト")]
        public TextMeshProUGUI ViewText;

        // WebGLのInputFieldは日本語入力対応していないので、対応するように
        [Header("入力用フィールド")]
        public WebGLNativeTextMeshInputField inputFiled;

        private void Awake()
        {
            //サンプルとして製品名でデータベース作成
            DataBaseName = Application.productName;
            StartCoroutine(OnOpen());

        }

        private void OnApplicationQuit()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            // ブラウザ強制的に閉じた場合に閉じられると思うがとりあえず書いておく。
            IndexedDBController.OnClose();
#endif
        }

        /// <summary>
        /// 保存
        /// </summary>
        public void OnSaveButton()
        {
            StartCoroutine(OnSave());
        }

        /// <summary>
        /// 読み込み
        /// </summary>
        public void OnLoadButton()
        {
            StartCoroutine(OnLoad());
        }

        /// <summary>
        /// 保存コルーチン
        /// </summary>
        /// <returns></returns>
        private IEnumerator OnOpen()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            yield break;
#endif

            if (IndexedDBController.GetRunning())
            {
                Debug.Log("データベースを処理中です");
                yield break;
            }

            yield return IndexedDBController.OnOpen(DataBaseName, StoreName);
        }

        /// <summary>
        /// 保存コルーチン
        /// </summary>
        /// <returns></returns>
        private IEnumerator OnSave()
        {

#if !UNITY_WEBGL || UNITY_EDITOR
            yield break;
#endif

            if (!IndexedDBController.GetOpen())
            {
                Debug.Log("データベースが開いていません");
                yield break;
            }

            if (IndexedDBController.GetRunning())
            {
                Debug.Log("データベースを処理中です");
                yield break;
            }

            string Value = inputFiled.text;

            if (Value == null)
                Value = "";

            TestSaveClass testSaveClass = new TestSaveClass();

            testSaveClass.SetSaveText(Value);

            Debug.Log(Value.ToUniCode());

            //Json文字列に変換
            string jsonStr = JsonUtility.ToJson(testSaveClass);

            //Jsonを保存するまで待つ
            yield return IndexedDBController.OnSave(DataBaseName, StoreName, Key, jsonStr);

            if (IndexedDBController.GetError())
            {
                ViewText.text = "Save Failed";
            }
            else
            {
                ViewText.text = "Save Success";
            }

        }

        /// <summary>
        /// 読み込みコルーチン
        /// </summary>
        /// <returns></returns>
        private IEnumerator OnLoad()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            yield break;
#endif

            if (!IndexedDBController.GetOpen())
            {
                Debug.Log("データベースが開いていません");
                yield break;
            }

            if (IndexedDBController.GetRunning())
            {
                Debug.Log("データベースを処理中です");
                yield break;
            }

            //読み込むまで待つ
            yield return IndexedDBController.OnLoad(DataBaseName, StoreName, Key);

            if (IndexedDBController.GetError())
            {
                Debug.Log("エラーが発生しました。");
            }
            else
            {

                //JSONテキストのデコード
                var saveData = JsonUtility.FromJson<TestSaveClass>(IndexedDBController.GetValue());

                if (ViewText != null)
                    ViewText.text = saveData.GetSaveText();

                Debug.Log(saveData.GetSaveText());
            }

        }

    }

    [SerializeField]
    sealed public class TestSaveClass
    {
        [SerializeField]
        private string saveText = "";

        public void SetSaveText(string str)
        {
            saveText = str.ToUniCode();

            Debug.Log(saveText);
        }

        public string GetSaveText()
        {
            return Regex.Unescape(saveText);
        }

    }

}