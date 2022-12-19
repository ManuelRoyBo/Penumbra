using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Dialogue : ScriptableObject
{
    public string characterName;
    public Sprite speakerImage;
    public string[] dialogues;
}
