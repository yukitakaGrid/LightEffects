using System;

[Serializable]
public class SaveData
{
    private int[] buffer;
    private int frameRate;
    private int playtime;
    
    public void SetSaveData(int[] buffer)
    {
        this.buffer = (int[])buffer.Clone(); 
    }
    
    public int[] GetSaveData()
    {
        int[] buffer = (int[])this.buffer.Clone();
        return buffer;
    }

    public int GetFrameRate()
    {
        return frameRate;
    }

    public void SetFrameRate(int frameRate)
    {
        this.frameRate = frameRate;
    }

    public int GetPlayTime()
    {
        return playtime;
    }

    public void SetPlayTime(int playTime)
    {
        this.playtime = playTime;
    }
}