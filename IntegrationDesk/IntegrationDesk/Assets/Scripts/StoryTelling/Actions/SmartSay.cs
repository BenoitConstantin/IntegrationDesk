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

        Statement tempStatement = new Statement("");

        protected override Status OnExecute(Component agent, IBlackboard bb)
        {
            tempStatement = new Statement(SmartLocalization.LanguageManager.Instance.GetTextValue(statement.text));
            tempStatement = tempStatement.BlackboardReplace(bb);
            DialogueTree.RequestSubtitles(new SubtitlesRequestInfo((IDialogueActor)agent, tempStatement, OnStatementFinish));
             return Status.Running;
        }

        void OnStatementFinish()
        {
            status = Status.Success;
            DLGTree.Continue();
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
            var areaStyle = new GUIStyle(GUI.skin.GetStyle("TextArea"));
            areaStyle.wordWrap = true;

            GUILayout.Label("Dialogue Text");
            statement.text = UnityEditor.EditorGUILayout.TextArea(statement.text, areaStyle, GUILayout.Height(100));
            statement.audio = UnityEditor.EditorGUILayout.ObjectField("Audio File", statement.audio, typeof(AudioClip), false) as AudioClip;
            statement.meta = UnityEditor.EditorGUILayout.TextField("Metadata", statement.meta);
        }

    #endif
}