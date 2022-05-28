using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, IInteractable
{
    public void Interaction()
    {
        GameManager.Instance.PlayerDead();
    }
}