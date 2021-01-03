import Foundation

// NOTE: ObjCに公開する物はアクセスレベルを`public` or `open`に設定する必要あり

public class Example : NSObject {

    // ObjC++からSwiftのメソッドを呼び出す
    @objc public static func callSwiftMethod(_ message: String) {
        print("\(#function) is called with message: \(message)")
    }

    // SwiftのメソッドからSendMessageを呼び出す
    @objc public static func callUnityMethod() {
    
        // NOTE: 従来の`UnityInterface.h`にある`SendMessage`は参照でき無さそう？なので、以下のsendMessageToGOを利用する。
    
        // Call a method on a specified GameObject.
        if let uf = UnityFramework.getInstance() {
            uf.sendMessageToGO(
                withName: "CallbackTarget",
                functionName: "OnCallFromSwift",
                message: "Hello, Unity!")
        }
    }
}
