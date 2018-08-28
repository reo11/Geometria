using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine.UI;
public class UserList : MonoBehaviour {
    public DatabaseReference UserListDB;
    private int mapNum = 1;
    private GameObject rankList;

  // Use this for initialization
    void Start () {
        // 参考 http://sleepnel.hatenablog.com/entry/2017/01/26/124500
        // https://firebase.google.com/docs/database/unity/start
        // 公式ドキュメントの方がわかりやすい
        // データベースURLを設定
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://geometria-161bd.firebaseio.com/");
        // DB参照をprivateフィールドにとっておく
        UserListDB = FirebaseDatabase.DefaultInstance.GetReference("UserList");
        // for test
            // addNewUser("aaa","aaa","aaa");
            // insertScore(0, "aaa", "aaa", 5000);
    }

    private void initialSet(DatabaseReference DB){
        DB.ChildAdded += HandleChildAdded;
        DB.ChildChanged += HandleChildChanged;
        DB.ChildRemoved += HandleChildRemoved;
        DB.ChildMoved += HandleChildMoved;
    }

    void HandleChildAdded(object sender, ChildChangedEventArgs args) {
      if (args.DatabaseError != null) {
        Debug.LogError(args.DatabaseError.Message);
        return;
      }
      Debug.Log("HandleChildAdded");
      // Do something with the data in args.Snapshot
    }

    void HandleChildChanged(object sender, ChildChangedEventArgs args) {
      if (args.DatabaseError != null) {
        Debug.LogError(args.DatabaseError.Message);
        return;
      }
      Debug.Log("HandleChildChanged");
      // Do something with the data in args.Snapshot
    }

    void HandleChildRemoved(object sender, ChildChangedEventArgs args) {
      if (args.DatabaseError != null) {
        Debug.LogError(args.DatabaseError.Message);
        return;
      }
      Debug.Log("HandleChildRemoved");
      // Do something with the data in args.Snapshot
    }

    void HandleChildMoved(object sender, ChildChangedEventArgs args) {
      if (args.DatabaseError != null) {
        Debug.LogError(args.DatabaseError.Message);
        return;
      }
      Debug.Log("HandleChildMoved");
      // Do something with the data in args.Snapshot
    }
    private void addNewUser(string userId, string name, string email) {
        User user = new User(name, email);
        string json = JsonUtility.ToJson(user);

        UserListDB.Child("users").Child(userId).SetRawJsonValueAsync(json);
    }
}
