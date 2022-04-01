using System.Collections.Generic;

[System.Serializable]
public class UserProgressData
{
    public bool isTutorialDone;
    public bool isSoundMuted;

    public float speed;

    public int currentScore;
    public int lastScore;
    public int highScore;

    public bool IsTutorialDone{ get{ return isTutorialDone; } set{ isTutorialDone = value; } }
    public bool IsSoundMuted{ get{ return isSoundMuted; } set{ isSoundMuted = value; } }
    
    public float LastSpeed{ get{ return speed; } set{ speed = value; } }

    public int CurrentScore{ get{ return currentScore; } set{ currentScore = value; } }
    public int LastScore{ get{ return lastScore; } set{ lastScore = value; } }
    public int HighScore{ get{ return highScore; } set{ highScore = value; } }
}