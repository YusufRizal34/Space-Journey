using System.Collections.Generic;

[System.Serializable]
public class UserProgressData
{
    public float lastSpeed;
    public int acceleration;

    public int currentScore;
    public int lastScore;
    public int highScore;
    
    public int Acceleration{ get{ return acceleration; } set{ acceleration = value; } }
    public float LastSpeed{ get{ return lastSpeed; } set{ lastSpeed = value; } }

    public int LastScore{ get{ return lastScore; } set{ lastScore = value; } }
    public int CurrentScore{ get{ return currentScore; } set{ currentScore = value; } }
    public int HighScore{ get{ return highScore; } set{ highScore = value; } }
}