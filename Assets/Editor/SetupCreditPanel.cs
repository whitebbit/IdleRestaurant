using UnityEditor;
using UnityEngine;

namespace CreditPanel
{
  class SetupCreditPanel : AssetPostprocessor
  {
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
      SetupMessageWindow window = (SetupMessageWindow)EditorWindow.GetWindow(typeof(SetupMessageWindow), true, "Credit Panel");
      window.maxSize = new Vector2(390f, 290f);
      window.minSize = window.maxSize;
    }
  }

  public class SetupMessageWindow : EditorWindow
  {
    void OnGUI() {
      string txt = "\nThis asset was uploaded by https://unityassetcollection.com \nGo to our site to find more interesting. \nThank you!!!";
      EditorGUILayout.LabelField(txt, EditorStyles.wordWrappedLabel);
    }
  }
}