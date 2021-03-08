using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "LevelSO/Level")]
public class Level : ScriptableObject
{
  public int idLevel;
  public string numberLevel;
  public string nameLevel;
  public int totalQuests;
  public int completedQuests = 0;
  public List<Quests> quests;

  [System.Serializable]
  public class Quests { 
    public string nameQuest;
    public string textQuests;
    public float valueQuest;
    public Sprite imageQuests;
  }
}
