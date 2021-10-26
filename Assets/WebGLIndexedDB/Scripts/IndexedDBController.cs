using AOT;
using System;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;


namespace IndexDB
{
    /// <summary>
    /// WebGL_IndexDBでデータを保存、読み込み
    /// </summary>
    sealed public class IndexedDBController
    {
        /// <summary>
        /// データベースが開いているか
        /// </summary>
        private static bool isOpen = false;

        /// <summary>
        /// 処理中か
        /// </summary>
        private static bool RunningTask = false;

        /// <summary>
        /// 処理成功時にtrue
        /// </summary>
        private static bool Result = false;

        /// <summary>
        /// 処理失敗時にtrue
        /// </summary>
        private static bool Error = false;

        /// <summary>
        /// 読み込んだ値
        /// </summary>
        private static string ResultData = "";

        /// <summary>
        /// 内部データの初期化
        /// </summary>
        private static void Init()
        {
            RunningTask = true;
            Result = false;
            ResultData = "";
            Error = false;
        }

        /// <summary>
        /// データベースを開く
        /// </summary>
        /// <param name="IndexeDBName">データベース名</param>
        /// <param name="version">バージョン</param>
        /// <param name="successCallback">成功時のコールバック</param>
        /// <param name="errorCallback">失敗時のコールバック</param>
        [DllImport("__Internal")]
        private static extern void OpenIndexedDB(string IndexeDBName,
                                                int version,
                                                Action successCallback, Action errorCallback);

        /// <summary>
        /// データベースを閉じる
        /// </summary>
        [DllImport("__Internal")]
        private static extern void CloseIndexedDB();

        /// <summary>
        /// データベースを開き、Storeが無ければ作成する
        /// </summary>
        /// <param name="IndexeDBName">データベース名</param>
        /// <param name="version">バージョン</param>
        /// <param name="StoreName">テーブル名</param>
        /// <param name="successCallback">成功時のコールバック</param>
        /// <param name="errorCallback">失敗時のコールバック</param>
        [DllImport("__Internal")]
        private static extern void OpenIndexedDBAndCreateStore(string IndexeDBName,
                                                int version, string StoreName,
                                                Action successCallback, Action errorCallback);

        /// <summary>
        /// データをIndexDBにセットする
        /// </summary>
        /// <param name="StoreName">テーブル名</param>
        /// <param name="key">キー</param>
        /// <param name="val">値</param>
        /// <param name="successCallback">成功時のコールバック</param>
        /// <param name="errorCallback">失敗時のコールバック</param>
        [DllImport("__Internal")]
        private static extern void SetIndexedDB(string StoreName,
                                                string key, string val,
                                                Action successCallback, Action errorCallback);

        /// <summary>
        /// データを取得する
        /// </summary>
        /// <param name="StoreName">テーブル名</param>
        /// <param name="key">キー</param>
        /// <param name="successCallback">成功時のコールバック</param>
        /// <param name="errorCallback">失敗時のコールバック</param>
        [DllImport("__Internal")]
        private static extern void GetIndexedDB(string StoreName,
                                                string key,
                                                Action<string> successCallback, Action errorCallback);

        /// <summary>
        /// 保存成功時のコールバック
        /// </summary>
        [MonoPInvokeCallback(typeof(Action))]
        static void OnOpenSuccessCallback()
        {
            Result = true;
            isOpen = true;
            RunningTask = false;
        }

        /// <summary>
        /// 保存失敗時のコールバック
        /// </summary>
        [MonoPInvokeCallback(typeof(Action))]
        static void OnOpenErrorCallback()
        {
            Error = true;
            isOpen = false;
            RunningTask = false;
        }

        /// <summary>
        /// 保存成功時のコールバック
        /// </summary>
        [MonoPInvokeCallback(typeof(Action))]
        static void OnSaveSuccessCallback()
        {
            Result = true;
            RunningTask = false;
        }

        /// <summary>
        /// 保存失敗時のコールバック
        /// </summary>
        [MonoPInvokeCallback(typeof(Action))]
        static void OnSaveErrorCallback()
        {
            Error = true;
            RunningTask = false;
        }

        /// <summary>
        /// 読み込み成功時のコールバック
        /// </summary>
        /// <param name="str"></param>
        [MonoPInvokeCallback(typeof(Action<string>))]
        static void OnLoadSuccessCallback(string str)
        {
            ResultData = str;
            Result = true;
            RunningTask = false;
        }

        /// <summary>
        /// 読み込み失敗時のコールバック
        /// </summary>
        [MonoPInvokeCallback(typeof(Action))]
        static void OnLoadErrorCallback()
        {
            Error = true;
            RunningTask = false;
        }

        /// <summary>
        /// データベースを開く
        /// </summary>
        /// <param name="dbName">データベース名</param>
        /// <param name="storeName">テーブル名</param>
        /// <param name="key">キー</param>
        /// <param name="value">値</param>
        /// <returns>処理が終了するまで待つ</returns>
        public static CustomYieldInstruction OnOpen(string dbName, string storeName, int version = 1)
        {

            Init();

            // Storeが無い場合は作成するようにするため、OpenIndexedDB()は基本使用しないと思われる
            OpenIndexedDBAndCreateStore(dbName, version, storeName,
                                                OnOpenSuccessCallback, OnOpenErrorCallback);

            //処理が成功か失敗するまで待つ
            return new WaitWhile(() => !Result && !Error);

        }

        /// <summary>
        /// データベースを閉じる
        /// </summary>
        public static void OnClose()
        {
            CloseIndexedDB();
        }

        /// <summary>
        /// データを保存する
        /// </summary>
        /// <param name="dbName">データベース名</param>
        /// <param name="storeName">テーブル名</param>
        /// <param name="key">キー</param>
        /// <param name="value">値</param>
        /// <returns>処理が終了するまで待つ</returns>
        public static CustomYieldInstruction OnSave(string dbName, string storeName, string key, string value)
        {
            Init();

            SetIndexedDB(storeName,
                                                key,
                                                value,
                                                OnSaveSuccessCallback, OnSaveErrorCallback);

            //処理が成功か失敗するまで待つ
            return new WaitWhile(() => !Result && !Error);

        }

        /// <summary>
        /// データを取得する
        /// </summary>
        /// <param name="dbName">データベース名</param>
        /// <param name="storeName">テーブル名</param>
        /// <param name="key">キー</param>
        /// <returns>処理が終了するまで待つ</returns>
        public static CustomYieldInstruction OnLoad(string dbName, string storeName, string key)
        {
            Init();

            GetIndexedDB(storeName,
                                                key,
                                                OnLoadSuccessCallback, OnLoadErrorCallback);

            //処理が成功か失敗するまで待つ
            return new WaitWhile(() => !Result && !Error);

        }

        /// <summary>
        /// データベースを操作しているか取得
        /// </summary>
        /// <returns>操作中ならtrue</returns>
        public static bool GetRunning()
        {
            return RunningTask;
        }

        /// <summary>
        /// データベースが開いているか取得
        /// </summary>
        /// <returns>開いているならtrue</returns>
        public static bool GetOpen()
        {
            return isOpen;
        }

        /// <summary>
        /// エラーが発生したか取得
        /// </summary>
        /// <returns>保存、読み込み後にエラーが発生したらtrue</returns>
        public static bool GetError()
        {
            return Error;
        }

        /// <summary>
        /// ロード後の値を取得する
        /// </summary>
        /// <returns></returns>
        public static string GetValue()
        {
            return ResultData;
        }

    }

    /// <summary>
    /// 日本語対応でUniCode文字列に変換して保存する為の拡張クラス
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 文字列をエスケープUniCodeに変換
        /// </summary>
        /// <param name="str">UniCode文字列</param>
        /// <returns></returns>
        public static string ToUniCode(this string str)
        {
            StringBuilder builder = new StringBuilder();

            char[] charArray = str.ToCharArray();
            foreach (var c in charArray)
            {
                switch (c)
                {
                    case '"':
                        builder.Append("\\\"");
                        break;
                    case '\\':
                        builder.Append("\\\\");
                        break;
                    case '\b':
                        builder.Append("\\b");
                        break;
                    case '\f':
                        builder.Append("\\f");
                        break;
                    case '\n':
                        builder.Append("\\n");
                        break;
                    case '\r':
                        builder.Append("\\r");
                        break;
                    case '\t':
                        builder.Append("\\t");
                        break;
                    default:
                        int codepoint = System.Convert.ToInt32(c);
                        if ((codepoint >= 32) && (codepoint <= 126))
                        {
                            builder.Append(c);
                        }
                        else
                        {
                            builder.Append("\\u");
                            builder.Append(codepoint.ToString("x4"));
                        }
                        break;
                }
            }

            return builder.ToString();

        }

    }

}