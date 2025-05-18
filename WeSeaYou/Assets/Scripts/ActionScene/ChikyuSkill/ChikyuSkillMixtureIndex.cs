using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Index(ScObj)")]
public class ChikyuSkillMixtureIndex : ScriptableObject
{
    //�X�e�[�W����possibleDictionary���ݒ肳��Ă���
    //����possibleDictionary���茳�Ɏ�����ActionScene�͎n�܂�

    //�A�C�e�����E���x��possibleDictionary�X�V�B�X�V�Ƃ́A�u���̑f�ނ������ꂽDidtionary�����B�������ꂽDidtionary�͖{Didtionary�ɂȂ�v
    //��L�̏������d���ꍇ�ɘa�������B�܂�A��L�͕ʃX���ōs���A�ʃX������ԓ����Ԃ�Ȃ�����n���\�͂̑����J���Ȃ��B

    //�f�ޑI����Didtionary�X�V&�f�ޓE�o�B�X�V�Ƃ́A�u���̑f�ނ������ꂽDidtionary�����i���̑f�ނ��܂܂Ȃ����͂͂����j�v

    public int[] RequiredMaterialsArray = new int[ChikyuSkillCursor.ItemVariety];
    public int[] RequiredMaterialsLeftArray = new int[ChikyuSkillCursor.ItemVariety];
    public Item MixtureItem;
}