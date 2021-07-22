#if UNITY_IOS

// iOSで動かす為に必要なスクリプト


using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.Collections;
using System.IO;

public class PostBuildProcess {
	[PostProcessBuild]
	public static void OnPostProcessBuild (BuildTarget buildTarget, string path) {
		if (buildTarget == BuildTarget.iOS) {
			ProcessForiOS (path);
		}
	}

	private static void ProcessForiOS (string path) {
		string pjPath = path + "/Unity-iPhone.xcodeproj/project.pbxproj";
		PBXProject pj = new PBXProject ();
		pj.ReadFromString (File.ReadAllText (pjPath));

		string target = pj.TargetGuidByName ("Unity-iPhone");

		pj.SetBuildProperty (target, "CLANG_ENABLE_MODULES", "YES");

		File.WriteAllText (pjPath, pj.WriteToString ());
	}
}

#endif
