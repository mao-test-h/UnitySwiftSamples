import Foundation

class Example : NSObject {

    // ObjC++からSwiftのメソッドを呼び出す
    @objc static func callSwiftMethod(_ message: String) {
        print("\(#function) is called with message: \(message)")
    }
    
    // SwiftのメソッドからSendMessageを呼び出す
    @objc static func callUnityMethod() {
        // Call a method on a specified GameObject.
        UnitySendMessage("CallbackTarget", "OnCallFromSwift", "Hello, Unity!")
    }
}
