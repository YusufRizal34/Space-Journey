using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Character : MonoBehaviour
{
    public int characterID;
    public string characterName;
    [TextArea] public string characterDescription;
    [TextArea] public string skillDescription;
    public int characterPrice;
    public Sprite characterImage;
    public Sprite characterButton;
    public bool isUnlock;

    public int ID{ get{ return characterID; } }
    public string Name{ get{ return characterName; } }
    public string Description{ get{ return characterDescription; } }
    public string SkillDescription{ get{ return skillDescription; } }
    public int Price{ get{ return characterPrice; } }
    public Sprite Image{ get{ return characterImage; } }
    public Sprite Button{ get{ return characterButton; } }
    public bool IsUnlock{ get{ return isUnlock; } set{ isUnlock = value; } }
}
