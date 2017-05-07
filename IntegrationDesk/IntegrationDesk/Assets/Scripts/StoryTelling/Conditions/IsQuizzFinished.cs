using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using EquilibreGames;


/// Retourne vrai si le joueur a terminé de remplir le quizz
public class IsQuizzFinished : ConditionTask {
    
    protected override bool OnCheck()
    {
        if (Quizz.Instance.IsFinished)
        {
            Quizz.Instance.Hide();
            HUD.Instance.RefreshFromSavedSettings();
            return true;
        }
        return false;
    }
}
