using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultSceneManager : MonoBehaviour
{
    [SerializeField] Text Synchronization_title;
    [SerializeField] Text Synchronization_value;
    [SerializeField] Text Instinct_title;
    [SerializeField] Text Instinct_value;
    [SerializeField] Text _name;
    [SerializeField] GameObject Unitychan_running;
    [SerializeField] GameObject UnityWaitBase;
    [SerializeField] GameObject UnityRunBase;
    private Animator runanimator;
    public static double result = 0;
    private int Number_Display = 0;
    private string name_title;
    private bool Synchronization_finished = false;
    private Vector3 destination;
    private int shogo_num;
    int intResult;

    // Start is called before the first frame update
    void Start()
    {
        runanimator = Unitychan_running.GetComponent<Animator>();
        Synchronization_title.text = "";
        Synchronization_value.text = "";
        Instinct_title.text = "";
        Instinct_value.text = "";
        _name.text = "";
        FadeManager.FadeIn();
        intResult = (int)(result * 100);
        shogo_num = intResult / 10;
        destination = UnityWaitBase.transform.position - (UnityRunBase.transform.position - UnityWaitBase.transform.position) * (float)result;
    }

    // Update is called once per frame


    public void Restart()
    {
        FadeManager.FadeOut(2);
    }
    private IEnumerator PlusValue(int _intResult)
    {
        int Number_Display = 0;
        while(_intResult > Number_Display)
        {
            Number_Display += 1;
            Synchronization_value.text = Number_Display.ToString() + "%";
            yield return null;
        }
    }
    private IEnumerator Character_Expansion(Text _character,float expandtime = 0.1f,float expandratio = 1.2f)
    {
        float _time = 0;
        int originsize = _character.fontSize;
        int sizedx = (int)(_character.fontSize * (expandratio - 1.0f) * Time.fixedDeltaTime / expandtime); 
        while(_time < expandtime)
        {
            _character.fontSize += sizedx;
            _time += Time.fixedDeltaTime;
            yield return null;
        }
        yield return null;
        while (_time > 0)
        {
            _character.fontSize -= sizedx;
            _time -= Time.fixedDeltaTime;
            yield return null;
        }
        _character.fontSize = originsize;
    }
    private IEnumerator MoveUnitychan()
    {
        runanimator.SetBool("Ruuning", true);
        while(destination.x < Unitychan_running.transform.position.x)
        {
            Unitychan_running.transform.position -= new Vector3(0.1f,0,0);
            yield return null;
        }
        runanimator.SetBool("Running", false);
    }
    private IEnumerator ShowKyori()
    {
        yield return null;
    }

}
