// refered to:
//    - https://qiita.com/mybdesign/items/fe3e390741799c1814ad

#if UNITY_IOS
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

namespace UnitySwift.Editor
{
    /// <summary>
    /// Swiftを実装するにあたって必要な設定を自動で適用する
    /// </summary>
    sealed class XcodePostProcess
    {
        [PostProcessBuild]
        static void OnPostProcessBuild(BuildTarget target, string path)
        {
            if (target != BuildTarget.iOS) return;

            var projectPath = PBXProject.GetPBXProjectPath(path);
            var project = new PBXProject();
            project.ReadFromString(File.ReadAllText(projectPath));

            var targetGuid = project.TargetGuidByName(PBXProject.GetUnityTargetName());
            project.SetBuildProperty(targetGuid, "SWIFT_OBJC_BRIDGING_HEADER", "Libraries/UnitySwift/UnitySwift-Bridging-Header.h");
            project.SetBuildProperty(targetGuid, "SWIFT_OBJC_INTERFACE_HEADER_NAME", "unityswift-Swift.h");
            project.AddBuildProperty(targetGuid, "LD_RUNPATH_SEARCH_PATHS", "@executable_path/Frameworks");

            // Swift version: 5.0
            // NOTE: 明示的に指定しないと3.0ぐらいの古いのが設定されるっぽいので、Xcodeによっては`Unspecified`扱いになる
            project.AddBuildProperty(targetGuid, "SWIFT_VERSION", "5.0");

            File.WriteAllText(projectPath, project.WriteToString());
        }
    }
}
#endif
