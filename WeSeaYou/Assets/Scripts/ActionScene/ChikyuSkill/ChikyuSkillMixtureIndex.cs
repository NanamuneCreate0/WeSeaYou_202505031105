using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Index(ScObj)")]
public class ChikyuSkillMixtureIndex : ScriptableObject
{
    public int[] RequiredMaterialsArray = new int[2];
    public Item MixtureItem;
}


///�v��

//�X�e�[�W����AllDictionary���ݒ肳��Ă���
//����AllDictionary���茳�Ɏ�����ActionScene�͎n�܂�

//�A�C�e�����E���x��possibleDictionary�X�V�B�X�V�Ƃ́A�u���̑f�ނ������ꂽALLDidtionary�����B�������ꂽDidtionary��possibleDidtionary�ɂȂ�v
//��L�̏������d���ꍇ�ɘa�������B�܂�A��L�͕ʃX���ōs���A�ʃX������ԓ����Ԃ�Ȃ�����n���\�͂̑����J���Ȃ��B

//�f�ޑI����Didtionary�X�V&�f�ޓE�o�B�X�V�Ƃ́A�u���̑f�ނ������ꂽDidtionary�����i���̑f�ނ��܂܂Ȃ����͂͂����j�v
