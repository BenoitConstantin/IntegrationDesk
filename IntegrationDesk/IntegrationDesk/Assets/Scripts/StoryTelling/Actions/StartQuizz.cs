using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using EquilibreGames;


/// Permet de lancer un quizz en fin de journée
public class StartQuizz : ActionTask {

    public IntegrationDeskQuizz quizz;

    protected override void OnExecute()
    {
        base.OnExecute();
        Quizz.Instance.questions = quizz.questions;
        Quizz.Instance.idquizz = quizz;
        Quizz.Instance.RefreshQuestionsGUI();
        Quizz.Instance.Show();
        HUD.Instance.RefreshFromSavedSettings();
        EndAction();
    }
}
