using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultSceneManager : MonoBehaviour
{
    ResultStep resultStep;
    [SerializeField] Text Synchronization_title;
    [SerializeField] Text Synchronization_value;
    [SerializeField] Text Instinct_title;
    [SerializeField] Text Instinct_kyori;
    [SerializeField] Text _shogo;
    [SerializeField] GameObject Unitychan_running;
    [SerializeField] GameObject UnityWaitBase;
    [SerializeField] GameObject UnityRunBase;
    private Animator runanimator;
    Color color_dx;
    public static double result = 0;
    private int Number_Display = 0;
    private string name_title;
    private bool Synchronization_finished = false;
    private Vector3 destination;
    private int shogo_num;
    int intResult;
    float unitychandx;

    // Start is called before the first frame update
    void Start()
    {
        resultStep = ResultStep.Start;
        runanimator = Unitychan_running.GetComponent<Animator>();
        Synchronization_title.text = "シンクロ率は．．．";
        Synchronization_value.text = "";
        Instinct_title.text = "あなたと○○の直感キョリは．．．";
        Synchronization_title.color = new Color(0, 0, 0, 0);
        Instinct_title.color = new Color(0,0,0,0);
        FadeManager.FadeIn();
        intResult = (int)(result * 100);
        shogo_num = 10 - intResult / 10;
        Instinct_kyori.text = Title.title[shogo_num].kyori;
        _shogo.text ="称号：" + Title.title[shogo_num].shogo;
        Instinct_kyori.color = new Color(1, 0, 0, 0);
        _shogo.color = new Color(0,0,0,0);
        unitychandx = Unitychan_running.transform.position.x - destination.x;
        destination = UnityWaitBase.transform.position + (UnityRunBase.transform.position - UnityWaitBase.transform.position) * (1-(float)result);
        color_dx = Color.black - Synchronization_title.color;

    }
    private void FixedUpdate()
    {
        if (resultStep == ResultStep.Start)
        {
            StartsynTitle();
        }
        else if (resultStep == ResultStep.Synchronization_title)
        {
            fadein(2.0f,Color.black,color_dx,Synchronization_title,ResultStep.Synchronization_value);
        }
        else if (resultStep == ResultStep.Synchronization_value)
        {
            PlusValue(intResult);
        }
        else if (resultStep == ResultStep.Instinct_title)
        {
            fadein(2.0f,Color.black,color_dx,Instinct_title,ResultStep.Unitychanmove);
        }
        else if (resultStep == ResultStep.Unitychanmove)
        {
            MoveUnitychan(2.0f);
        }
        else if (resultStep == ResultStep.Instinct_kyori)
        {
            fadein(2.0f,Color.red,color_dx,Instinct_kyori,ResultStep.Shogo);
        }
        else if (resultStep == ResultStep.Shogo)
        {
            fadein(1.0f,Color.black, new Color(0, 0, 0, 1), _shogo, ResultStep.Finish);
        }
    }
    // Update is called once per frame
    public void Restart()
    {
        FadeManager.FadeOut(2);
    }
    private void StartsynTitle()
    {
        SwitchStep(ResultStep.Synchronization_title);
        Number_Display = 0;
    }
    private void StartInsTitle()
    {
        color_dx = Color.black - Instinct_title.color;
        SwitchStep(ResultStep.Instinct_title);
    }
    private void PlusValue(int _intResult)
    {
        if(_intResult > Number_Display)
        {
            Number_Display += 1;
            Synchronization_value.text = Number_Display.ToString() + "%";
        }
        else
        {
            Synchronization_value.text = Number_Display.ToString() + "%";
            StartInsTitle();
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
    private void MoveUnitychan(float _movetime)
    {
        runanimator.SetBool("Running", true);
        if(destination.x < Unitychan_running.transform.position.x)
        {
            Unitychan_running.transform.position -= new Vector3(unitychandx * Time.fixedDeltaTime / _movetime, 0, 0);
        }
        else
        {
            runanimator.SetBool("Running", false);
            StartInsKyori();
        }
    }
   private  void StartInsKyori()
    {
        color_dx = Color.red - Instinct_kyori.color;
        SwitchStep(ResultStep.Instinct_kyori);
    }
    private void fadein(float _changetime,Color color_destination,Color color_dx,Text _text,ResultStep _resultStep)
    {;
        if(_text.color.a < 1)
        {
            _text.color += color_dx * Time.fixedDeltaTime / _changetime;
        }
        else
        {
            _text.color = color_destination;
            SwitchStep(_resultStep);
        }
    }
    private void SwitchStep(ResultStep _resultStep)
    {
        resultStep = _resultStep;
    }
}
