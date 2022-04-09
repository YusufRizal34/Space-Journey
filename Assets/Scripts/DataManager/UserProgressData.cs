using System.Collections.Generic;

[System.Serializable]
public class UserProgressData
{
    public bool isTutorialDone;
    public bool isSoundMuted;

    public float lastSpeed;
    public int acceleration;

    public int currentScore;
    public int lastScore;
    public int highScore;

    public bool IsTutorialDone{ get{ return isTutorialDone; } set{ isTutorialDone = value; } }
    public bool IsSoundMuted{ get{ return isSoundMuted; } set{ isSoundMuted = value; } }
    
    public int Acceleration{ get{ return acceleration; } set{ acceleration = value; } }
    public float LastSpeed{ get{ return lastSpeed; } set{ lastSpeed = value; } }

    public int LastScore{ get{ return lastScore; } set{ lastScore = value; } }
    public int CurrentScore{ get{ return currentScore; } set{ currentScore = value; } }
    public int HighScore{ get{ return highScore; } set{ highScore = value; } }
}