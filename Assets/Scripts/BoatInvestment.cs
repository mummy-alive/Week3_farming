using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Data/BoatInvestment", fileName = "BoatInvestment")]
public class BoatInvestment : ScriptableObject    //seed 성장정보
{
    public Dialogue AskInvestmentDialogue;
    public Sprite BoatSprite;
    public int Price;
    public int SuccessProb;
    public int MaxSeed;
    public int MaxGold;
}
