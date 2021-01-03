#if UNITY_IOS
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

namespace UnitySwift.Editor
{
    sealed class XcodePostProcess
    {
        /// <summary>
        /// Swiftを実装するにあたって必要な設定を自動で適用する
        /// </summary>
        [PostProcessBuild]
        static void OnPostProcessBuild(BuildTarget target, string path)
        {
            if (target != BuildTarget.iOS) return;

            var projectPath = PBXProject.GetPBXProjectPath(path);
            var project = new PBXProject();
            project.ReadFromString(File.ReadAllText(projectPath));

            // 2019.3からは`UnityFramework`に分離しているので、targetGuidはこちらを指定刷る必要がある。
            // NOTE: 前バージョンと共存させたい場合には「#if UNITY_2019_3_OR_NEWER」で分けることも可能
            var targetGuid = project.GetUnityFrameworkTargetGuid();

            // NOTE:
            // 以前まで設定していた`Bridging-Header.h`の設定の類は2019.3からは不要な模様。
            // 寧ろCocoa touch FrameworkがBridging-Headerに対応していないので、設定していると怒られる。

            // Swift version: 5.0
            // NOTE: 明示的に指定しないと3.0ぐらいの古いのが設定されるっぽいので、Xcodeによっては`Unspecified`扱いになる
            project.AddBuildProperty(targetGuid, "SWIFT_VERSION", "5.0");

            File.WriteAllText(projectPath, project.WriteToString());
        }
    }
}
#endif