using UnityEngine;

public class PrefData
{

    public static int Score
    {
        get => PlayerPrefs.GetInt(ConstUtils.SCORE, 0);
        set => PlayerPrefs.SetInt(ConstUtils.SCORE, value);
    }

    public static int BestScore
    {
        get => PlayerPrefs.GetInt(ConstUtils.BESTSCORE, 0);
        set => PlayerPrefs.SetInt(ConstUtils.BESTSCORE, value);
    }
}

/// <summary>
/// STATIC CLASS 
/// Không thể kế thừa hoặc bị kế thừa
/// Không thế khởi tạo new Class()
/// Chỉ chứa static members
/// </summary>

public class ConstUtils
{
    public static string SCORE = "SCORE";
    public static string BESTSCORE = "BESTSCORE";
}