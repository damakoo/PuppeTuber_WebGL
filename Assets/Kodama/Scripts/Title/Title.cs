using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    public static Dictionary<int, Shogo> title = new Dictionary<int, Shogo>()
    {
        {0,new Shogo("�U�E������","0.3cm") },//�U�E�������̐g����
        {1,new Shogo("�g�̋ߖT���","20cm") },
        {2,new Shogo("�F�B�ȏ���l����" ,"1m")},
        {3,new Shogo("�I�����C���Œ��ǂ��l�Ƃ̃L����" ,"3m")},
        {4,new Shogo("���Ζʂ̃L����","5m") },
        {5,new Shogo("����Ȃ��n���A�C�h���Ƃ̃L����","10m") },
        {6,new Shogo("�X�J�C�c���[","634m") },
        {7,new Shogo("����-���É��ԃL����","352.1km") },
        {8,new Shogo("���ƃX�b�|��","384400km" )},//�n���ƌ��̋���
        {9,new Shogo("�A���h�����_","2537000���N") },//�A���h�����_��͂Ƃ̋���
        {10,new Shogo("�p���������[���h" ,"��")}
    };
}
