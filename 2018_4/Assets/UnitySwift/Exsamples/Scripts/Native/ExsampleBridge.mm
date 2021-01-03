#import <Foundation/Foundation.h>

// ObjC++からSwiftのクラスにアクセスする際に必要
// NOTE: `unityswift-Swift.h`の実態はビルド時に`DerivedData`以下に自動生成される
#import "unityswift-Swift.h"    // Required

// P/Invoke
extern "C" {

    void callSwiftMethod(const char *message) {
        [Example callSwiftMethod:[NSString stringWithUTF8String:message]];
    }

    void callUnityMethod() {
        [Example callUnityMethod];
    }
}
