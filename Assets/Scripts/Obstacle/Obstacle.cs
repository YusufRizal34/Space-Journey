using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, IInteractable
{
    public void Interaction()
    {
        var player = FindObjectOfType<CharacterControllers>();
        GameManager.Instance.PlayerDead();
    }
}