using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    public static Dictionary<int, Shogo> title = new Dictionary<int, Shogo>()
    {
        {0,new Shogo("�U�E������","10cm") },
        {1,new Shogo("�g�̋ߖT���","20cm") },
        {2,new Shogo("�݂��݂Ȃ���" ,"b")},
        {3,new Shogo("�F�B�ȏ���l����" ,"1m")},
        {4,new Shogo("�����܂�����","d") },
        {5,new Shogo("�͂����䂤��","e") },
        {6,new Shogo("�X�J�C�c���[","634m") },
        {7,new Shogo("����-���É�","352.1km") },
        {8,new Shogo("���ƃX�b�|��","384400km" )},
        {9,new Shogo("�A���h�����_","2537000���N") },
        {10,new Shogo("�p���������[���h" ,"��")}
    };
}
