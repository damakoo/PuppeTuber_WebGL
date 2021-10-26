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
        /// �f�[�^�x�[�X��
        /// </summary>
        private string DataBaseName;

        /// <summary>
        /// �e�[�u����
        /// </summary>
        private string StoreName = "Sample";

        /// <summary>
        /// �ۑ���L�[
        /// </summary>
        private string Key = "Test";

        /// <summary>
        /// Json�擾�f�[�^
        /// </summary>
        private Dictionary<string, object> loadData = new Dictionary<string, object>();

        // WebGL��Text�͕\���̍ۓ��{��o�͑Ή����Ă��Ȃ��̂ŁA�t�H���g��ύX����悤��
        [Header("�\���p�e�L�X�g")]
        public TextMeshProUGUI ViewText;

        // WebGL��InputField�͓��{����͑Ή����Ă��Ȃ��̂ŁA�Ή�����悤��
        [Header("���͗p�t�B�[���h")]
        public WebGLNativeTextMeshInputField inputFiled;

        private void Awake()
        {
            //�T���v���Ƃ��Đ��i���Ńf�[�^�x�[�X�쐬
            DataBaseName = Application.productName;
            StartCoroutine(OnOpen());

        }

        private void OnApplicationQuit()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            // �u���E�U�����I�ɕ����ꍇ�ɕ�����Ǝv�����Ƃ肠���������Ă����B
            IndexedDBController.OnClose();
#endif
        }

        /// <summary>
        /// �ۑ�
        /// </summary>
        public void OnSaveButton()
        {
            StartCoroutine(OnSave());
        }

        /// <summary>
        /// �ǂݍ���
        /// </summary>
        public void OnLoadButton()
        {
            StartCoroutine(OnLoad());
        }

        /// <summary>
        /// �ۑ��R���[�`��
        /// </summary>
        /// <returns></returns>
        private IEnumerator OnOpen()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            yield break;
#endif

            if (IndexedDBController.GetRunning())
            {
                Debug.Log("�f�[�^�x�[�X���������ł�");
                yield break;
            }

            yield return IndexedDBController.OnOpen(DataBaseName, StoreName);
        }

        /// <summary>
        /// �ۑ��R���[�`��
        /// </summary>
        /// <returns></returns>
        private IEnumerator OnSave()
        {

#if !UNITY_WEBGL || UNITY_EDITOR
            yield break;
#endif

            if (!IndexedDBController.GetOpen())
            {
                Debug.Log("�f�[�^�x�[�X���J���Ă��܂���");
                yield break;
            }

            if (IndexedDBController.GetRunning())
            {
                Debug.Log("�f�[�^�x�[�X���������ł�");
                yield break;
            }

            string Value = inputFiled.text;

            if (Value == null)
                Value = "";

            TestSaveClass testSaveClass = new TestSaveClass();

            testSaveClass.SetSaveText(Value);

            Debug.Log(Value.ToUniCode());

            //Json������ɕϊ�
            string jsonStr = JsonUtility.ToJson(testSaveClass);

            //Json��ۑ�����܂ő҂�
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
        /// �ǂݍ��݃R���[�`��
        /// </summary>
        /// <returns></returns>
        private IEnumerator OnLoad()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            yield break;
#endif

            if (!IndexedDBController.GetOpen())
            {
                Debug.Log("�f�[�^�x�[�X���J���Ă��܂���");
                yield break;
            }

            if (IndexedDBController.GetRunning())
            {
                Debug.Log("�f�[�^�x�[�X���������ł�");
                yield break;
            }

            //�ǂݍ��ނ܂ő҂�
            yield return IndexedDBController.OnLoad(DataBaseName, StoreName, Key);

            if (IndexedDBController.GetError())
            {
                Debug.Log("�G���[���������܂����B");
            }
            else
            {

                //JSON�e�L�X�g�̃f�R�[�h
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