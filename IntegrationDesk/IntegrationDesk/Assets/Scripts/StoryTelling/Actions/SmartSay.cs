using UnityEngine;
using ParadoxNotion.Design;
using NodeCanvas.Framework;
using NodeCanvas.DialogueTrees;
using EquilibreGames;
using NodeCanvas;

    [Name("SmartSay")]
    [Description("You can use a variable inline with the text by using brackets likeso: [myVarName] or [Global/myVarName].\nThe bracket will be replaced with the variable value ToString")]
    public class SmartSay : DTNode {

        public Statement statement = new Statement("Smart ID...");
        public int textsCount = 1;

        Statement tempStatement = new Statement("");
        int count = 0;

        protected override Status OnExecute(Component agent, IBlackboard bb)
        {
            tempStatement = new Statement(SmartLocalization.LanguageManager.Instance.GetTextValue(statement.text+ "."+ count));
            tempStatement = tempStatement.BlackboardReplace(bb);

            DialogueTree.RequestSubtitles(new SubtitlesRequestInfo(finalActor, tempStatement, OnStatementFinish));
            return Status.Running;
        }

        void OnStatementFinish()
        {
            count++;
            if (count < textsCount)
            {
                DLGTree.EnterNode(this);
            }
            else
            {
                count = 0;
                status = Status.Success;
                DLGTree.Continue();
            }
        }

        ////////////////////////////////////////
        ///////////GUI AND EDITOR STUFF/////////
        ////////////////////////////////////////
    #if UNITY_EDITOR

        protected override void OnNodeGUI()
        {
            var displayText = statement.text.Length > 60 ? statement.text.Substring(0, 60) + "..." : statement.text;
            GUILayout.Label("\"<i> " + displayText + "</i> \"");
        }

        protected override void OnNodeInspectorGUI()
        {
            base.OnNodeInspectorGUI();

            GUILayout.Label("Dialogue Text Smart ID");
            statement.text = UnityEditor.EditorGUILayout.TextField(statement.text);
            statement.meta = UnityEditor.EditorGUILayout.TextField("Metadata", statement.meta);
            textsCount = UnityEditor.EditorGUILayout.IntField("Texts Count", textsCount);
    }

    #endif
}