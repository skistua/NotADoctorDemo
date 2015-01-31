using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System;

public class GearMenu 
{
	[MenuItem ("gear/Bundle/Export Selected Object")]
	static void ExportSavePanel()
	{
		Caching.CleanCache ();

		UnityEngine.Object[] SelectedAsset = Selection.GetFiltered (typeof(UnityEngine.Object), SelectionMode.DeepAssets);
 
        //遍历所有的游戏对象
		foreach (UnityEngine.Object obj in SelectedAsset) 
		{
			
			string targetPath = Application.dataPath + "/StreamingAssets/" + obj.name + ".assetbundle";

			if(File.Exists(targetPath))
			{
				File.Delete(targetPath);
			}

			if (BuildPipeline.BuildAssetBundle (obj, null, targetPath, BuildAssetBundleOptions.CollectDependencies)) 
            {
	        	Debug.Log(obj.name +" build complete");
            }
            else
            {
            	Debug.Log(obj.name +" build faild");
            }	
        }
        //刷新编辑器
		AssetDatabase.Refresh ();
	}

	[MenuItem ("gear/Bundle/Export Scene")]
	static void exportScene()
	{
		Caching.CleanCache();
		DirectoryInfo theFolder = new DirectoryInfo(Application.dataPath + "/Level/");
		FileInfo[] fileInfos = theFolder.GetFiles();
		ArrayList sceneFilesPath = new ArrayList();

		foreach(FileInfo fi in fileInfos)
		{
			string fileName = fi.Name;
			if(fileName.EndsWith("unity"))
			{
				sceneFilesPath.Add("Assets/Level/"+fileName);
			}
		}

		if(sceneFilesPath.Count > 0)
		{
			string[] scenes = (string[])sceneFilesPath.ToArray(typeof(String));
			BuildPipeline.BuildPlayer( scenes, 
				Application.dataPath + "/StreamingAssets/scenes.unity3d",BuildTarget.StandaloneWindows64, BuildOptions.BuildAdditionalStreamedScenes);

			AssetDatabase.Refresh ();
		}
	}



	[MenuItem ("gear/Animation/Split Animation by file")]
	static void splitAnimationByFile()
	{
		string path = EditorUtility.OpenFilePanel("Select Animation Config File", "E:/art/3D美术/动作/LOL英雄/", "txt");
		StreamReader sr = null;
		try
		{
			sr = File.OpenText(path);
		}
		catch(Exception e)
		{
			Debug.Log(e.Message);
			return;
		}

		ArrayList arrlist = new ArrayList();
		string line = null;
		while((line = sr.ReadLine()) != null)
		{
			if(line.Length > 0)
				arrlist.Add(line);	
		}

		ModelImporterClipAnimation[] animationClips = new ModelImporterClipAnimation[arrlist.Count];
		string clipInfo = null;
		for (int i = 0 ; i < animationClips.Length; i++)
		{
			clipInfo = arrlist[i] as string;
			String[] args = clipInfo.Split(' ');
			bool loop = (args[0] == "stand" || args[0] == "run");
			animationClips[i] = SetClipAnimation(args[0], int.Parse(args[1]), int.Parse(args[2]), loop);
		}


		string assetPath = AssetDatabase.GetAssetPath(Selection.activeObject);
		ModelImporter mi = AssetImporter.GetAtPath(assetPath) as ModelImporter;
		mi.animationType = ModelImporterAnimationType.Legacy;
		mi.clipAnimations = animationClips;
		AssetDatabase.Refresh();
	}

	static ModelImporterClipAnimation SetClipAnimation(string _name, int _first, int _last, bool _isLoop)  
	{  
	    ModelImporterClipAnimation tempClip = new ModelImporterClipAnimation();  
	    tempClip.name = _name;  
	    tempClip.firstFrame = _first;  
	    tempClip.lastFrame = _last;  
	    tempClip.loop = _isLoop;  
	    if (_isLoop)  
	        tempClip.wrapMode = WrapMode.Loop;  
	    else  
	        tempClip.wrapMode = WrapMode.Default;  
	  
	    return tempClip;  
	}  
	// Add a menu item named "Do Something" to MyMenu in the menu bar.
	// [MenuItem ("gear/Change Material Color")]
	// static void DoSomething () {
	// 	Object[] selectRoot = Selection.objects;
		
	// 	// Debug.Log ("Doing Something..." + selectRoot.name);

	// 	for (int i = 0; i < selectRoot.Length; i++)
	// 	{
	// 		Material mat = selectRoot[i] as Material;
	// 		mat.SetColor("_Color", Color.white);
	// 	}
	// }


	// Validated menu item.
	// Add a menu item named "Log Selected Transform Name" to MyMenu in the menu bar.
	// We use a second function to validate the menu item
	// so it will only be enabled if we have a transform selected.
	// [MenuItem ("MyMenu/Log Selected Transform Name")]
	// static void LogSelectedTransformName ()
	// {
	// 	Debug.Log ("Selected Transform is on " + Selection.activeTransform.gameObject.name + ".");
	// }

	// Validate the menu item defined by the function above.
	// The menu item will be disabled if this function returns false.
	// [MenuItem ("MyMenu/Log Selected Transform Name", true)]
	// static bool ValidateLogSelectedTransformName () {
	// 	// Return false if no transform is selected.
	// 	return Selection.activeTransform != null;
	// }


	// Add a menu item named "Do Something with a Shortcut Key" to MyMenu in the menu bar
	// and give it a shortcut (ctrl-g on Windows, cmd-g on OS X).
	// [MenuItem ("MyMenu/Do Something with a Shortcut Key %g")]
	// static void DoSomethingWithAShortcutKey () {
	// 	Debug.Log ("Doing something with a Shortcut Key...");
	// }

	// Add a menu item called "Double Mass" to a Rigidbody's context menu.
	// [MenuItem ("CONTEXT/Rigidbody/Double Mass")]
	// static void DoubleMass (MenuCommand command) {
	// 	Rigidbody body = (Rigidbody)command.context;
	// 	body.mass = body.mass * 2;
	// 	Debug.Log ("Doubled Rigidbody's Mass to " + body.mass + " from Context Menu.");
	// }
}
