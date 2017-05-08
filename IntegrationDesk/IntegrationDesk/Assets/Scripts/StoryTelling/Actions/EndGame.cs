using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using EquilibreGames;


public class EndGame : ActionTask {

    protected override void OnExecute()
    {
        base.OnExecute();
        Quizz.Instance.ShowEEgg();
        EndAction();
    }
}
