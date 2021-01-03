using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

namespace UnitySwift.Exsamples
{
    /// <summary>
    /// iOS NativePluginの呼び出しサンプル
    /// </summary>
    public sealed class Exsample : MonoBehaviour
    {
        [SerializeField] Button _callSwiftMethod = default;
        [SerializeField] Button _callUnityMethod = default;

        void Start()
        {
            // SwiftからSendMessageを呼び出す際に指定されているGameObjectの名称を設定
            this.name = "CallbackTarget";

            _callUnityMethod.onClick.AddListener(() =>
            {
#if !UNITY_EDITOR && UNITY_IOS
                CallUnityMethod();
#endif
            });

            _callSwiftMethod.onClick.AddListener(() =>
            {
#if !UNITY_EDITOR && UNITY_IOS
                CallSwiftMethod("Gorilla");
#endif
            });
        }

        /// <summary>
        /// SwiftからSendMessageで呼び出される措定のメソッド
        /// </summary>
        /// <param name="message"></param>
        void OnCallFromSwift(string message)
        {
            Debug.Log(message);
        }

        /// <summary>
        /// ObjC++からSwiftのメソッドを呼び出す
        /// </summary>
        /// <remarks>[C# -> ObjC++ -> Swift]の流れで呼び出される</remarks>>
        [DllImport("__Internal", EntryPoint = "callSwiftMethod")]
        static extern void CallSwiftMethod(string message);

        /// <summary>
        /// SwiftのメソッドからSendMessageを呼び出す
        /// </summary>
        /// <remarks>※SwiftからUnity側で定義されているメソッドを呼び出したい意図がある</remarks>>
        [DllImport("__Internal", EntryPoint = "callUnityMethod")]
        static extern void CallUnityMethod();
    }
}
