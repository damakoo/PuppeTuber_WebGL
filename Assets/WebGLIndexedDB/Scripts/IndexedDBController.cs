using AOT;
using System;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;


namespace IndexDB
{
    /// <summary>
    /// WebGL_IndexDB�Ńf�[�^��ۑ��A�ǂݍ���
    /// </summary>
    sealed public class IndexedDBController
    {
        /// <summary>
        /// �f�[�^�x�[�X���J���Ă��邩
        /// </summary>
        private static bool isOpen = false;

        /// <summary>
        /// ��������
        /// </summary>
        private static bool RunningTask = false;

        /// <summary>
        /// ������������true
        /// </summary>
        private static bool Result = false;

        /// <summary>
        /// �������s����true
        /// </summary>
        private static bool Error = false;

        /// <summary>
        /// �ǂݍ��񂾒l
        /// </summary>
        private static string ResultData = "";

        /// <summary>
        /// �����f�[�^�̏�����
        /// </summary>
        private static void Init()
        {
            RunningTask = true;
            Result = false;
            ResultData = "";
            Error = false;
        }

        /// <summary>
        /// �f�[�^�x�[�X���J��
        /// </summary>
        /// <param name="IndexeDBName">�f�[�^�x�[�X��</param>
        /// <param name="version">�o�[�W����</param>
        /// <param name="successCallback">�������̃R�[���o�b�N</param>
        /// <param name="errorCallback">���s���̃R�[���o�b�N</param>
        [DllImport("__Internal")]
        private static extern void OpenIndexedDB(string IndexeDBName,
                                                int version,
                                                Action successCallback, Action errorCallback);

        /// <summary>
        /// �f�[�^�x�[�X�����
        /// </summary>
        [DllImport("__Internal")]
        private static extern void CloseIndexedDB();

        /// <summary>
        /// �f�[�^�x�[�X���J���AStore��������΍쐬����
        /// </summary>
        /// <param name="IndexeDBName">�f�[�^�x�[�X��</param>
        /// <param name="version">�o�[�W����</param>
        /// <param name="StoreName">�e�[�u����</param>
        /// <param name="successCallback">�������̃R�[���o�b�N</param>
        /// <param name="errorCallback">���s���̃R�[���o�b�N</param>
        [DllImport("__Internal")]
        private static extern void OpenIndexedDBAndCreateStore(string IndexeDBName,
                                                int version, string StoreName,
                                                Action successCallback, Action errorCallback);

        /// <summary>
        /// �f�[�^��IndexDB�ɃZ�b�g����
        /// </summary>
        /// <param name="StoreName">�e�[�u����</param>
        /// <param name="key">�L�[</param>
        /// <param name="val">�l</param>
        /// <param name="successCallback">�������̃R�[���o�b�N</param>
        /// <param name="errorCallback">���s���̃R�[���o�b�N</param>
        [DllImport("__Internal")]
        private static extern void SetIndexedDB(string StoreName,
                                                string key, string val,
                                                Action successCallback, Action errorCallback);

        /// <summary>
        /// �f�[�^���擾����
        /// </summary>
        /// <param name="StoreName">�e�[�u����</param>
        /// <param name="key">�L�[</param>
        /// <param name="successCallback">�������̃R�[���o�b�N</param>
        /// <param name="errorCallback">���s���̃R�[���o�b�N</param>
        [DllImport("__Internal")]
        private static extern void GetIndexedDB(string StoreName,
                                                string key,
                                                Action<string> successCallback, Action errorCallback);

        /// <summary>
        /// �ۑ��������̃R�[���o�b�N
        /// </summary>
        [MonoPInvokeCallback(typeof(Action))]
        static void OnOpenSuccessCallback()
        {
            Result = true;
            isOpen = true;
            RunningTask = false;
        }

        /// <summary>
        /// �ۑ����s���̃R�[���o�b�N
        /// </summary>
        [MonoPInvokeCallback(typeof(Action))]
        static void OnOpenErrorCallback()
        {
            Error = true;
            isOpen = false;
            RunningTask = false;
        }

        /// <summary>
        /// �ۑ��������̃R�[���o�b�N
        /// </summary>
        [MonoPInvokeCallback(typeof(Action))]
        static void OnSaveSuccessCallback()
        {
            Result = true;
            RunningTask = false;
        }

        /// <summary>
        /// �ۑ����s���̃R�[���o�b�N
        /// </summary>
        [MonoPInvokeCallback(typeof(Action))]
        static void OnSaveErrorCallback()
        {
            Error = true;
            RunningTask = false;
        }

        /// <summary>
        /// �ǂݍ��ݐ������̃R�[���o�b�N
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
        /// �ǂݍ��ݎ��s���̃R�[���o�b�N
        /// </summary>
        [MonoPInvokeCallback(typeof(Action))]
        static void OnLoadErrorCallback()
        {
            Error = true;
            RunningTask = false;
        }

        /// <summary>
        /// �f�[�^�x�[�X���J��
        /// </summary>
        /// <param name="dbName">�f�[�^�x�[�X��</param>
        /// <param name="storeName">�e�[�u����</param>
        /// <param name="key">�L�[</param>
        /// <param name="value">�l</param>
        /// <returns>�������I������܂ő҂�</returns>
        public static CustomYieldInstruction OnOpen(string dbName, string storeName, int version = 1)
        {

            Init();

            // Store�������ꍇ�͍쐬����悤�ɂ��邽�߁AOpenIndexedDB()�͊�{�g�p���Ȃ��Ǝv����
            OpenIndexedDBAndCreateStore(dbName, version, storeName,
                                                OnOpenSuccessCallback, OnOpenErrorCallback);

            //���������������s����܂ő҂�
            return new WaitWhile(() => !Result && !Error);

        }

        /// <summary>
        /// �f�[�^�x�[�X�����
        /// </summary>
        public static void OnClose()
        {
            CloseIndexedDB();
        }

        /// <summary>
        /// �f�[�^��ۑ�����
        /// </summary>
        /// <param name="dbName">�f�[�^�x�[�X��</param>
        /// <param name="storeName">�e�[�u����</param>
        /// <param name="key">�L�[</param>
        /// <param name="value">�l</param>
        /// <returns>�������I������܂ő҂�</returns>
        public static CustomYieldInstruction OnSave(string dbName, string storeName, string key, string value)
        {
            Init();

            SetIndexedDB(storeName,
                                                key,
                                                value,
                                                OnSaveSuccessCallback, OnSaveErrorCallback);

            //���������������s����܂ő҂�
            return new WaitWhile(() => !Result && !Error);

        }

        /// <summary>
        /// �f�[�^���擾����
        /// </summary>
        /// <param name="dbName">�f�[�^�x�[�X��</param>
        /// <param name="storeName">�e�[�u����</param>
        /// <param name="key">�L�[</param>
        /// <returns>�������I������܂ő҂�</returns>
        public static CustomYieldInstruction OnLoad(string dbName, string storeName, string key)
        {
            Init();

            GetIndexedDB(storeName,
                                                key,
                                                OnLoadSuccessCallback, OnLoadErrorCallback);

            //���������������s����܂ő҂�
            return new WaitWhile(() => !Result && !Error);

        }

        /// <summary>
        /// �f�[�^�x�[�X�𑀍삵�Ă��邩�擾
        /// </summary>
        /// <returns>���쒆�Ȃ�true</returns>
        public static bool GetRunning()
        {
            return RunningTask;
        }

        /// <summary>
        /// �f�[�^�x�[�X���J���Ă��邩�擾
        /// </summary>
        /// <returns>�J���Ă���Ȃ�true</returns>
        public static bool GetOpen()
        {
            return isOpen;
        }

        /// <summary>
        /// �G���[�������������擾
        /// </summary>
        /// <returns>�ۑ��A�ǂݍ��݌�ɃG���[������������true</returns>
        public static bool GetError()
        {
            return Error;
        }

        /// <summary>
        /// ���[�h��̒l���擾����
        /// </summary>
        /// <returns></returns>
        public static string GetValue()
        {
            return ResultData;
        }

    }

    /// <summary>
    /// ���{��Ή���UniCode������ɕϊ����ĕۑ�����ׂ̊g���N���X
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// ��������G�X�P�[�vUniCode�ɕϊ�
        /// </summary>
        /// <param name="str">UniCode������</param>
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