#import <Foundation/Foundation.h>

// 2019.3からはこちらをimportする必要がある
#import <UnityFramework/UnityFramework-Swift.h>

// P/Invoke
extern "C" {

    void callSwiftMethod(const char *message) {
        [Example callSwiftMethod:[NSString stringWithUTF8String:message]];
    }

    void callUnityMethod() {
        [Example callUnityMethod];
    }
}
