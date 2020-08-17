using System;
using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine;

public class DatabaseAPI : MonoBehaviour
{
    private DatabaseReference reference;

    private EventHandler<ChildChangedEventArgs> test;
    
    void Awake()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://mobile-desktop.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void PostMessage(Message message, Action callback, Action<AggregateException> fallback)
    {
        var messageJSON = StringSerializationAPI.Serialize(typeof(Message), message);
        reference.Child("messages").Push().SetRawJsonValueAsync(messageJSON).ContinueWith(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                fallback(task.Exception);
            }
            else
            {
                callback();
            }
        });
    }

    public void ListenForMessages(Action<Message> callback, Action<AggregateException> fallback)
    {
        void CurrentListener(object o, ChildChangedEventArgs args)
        {
            if (args.DatabaseError != null)
            {
                fallback(new AggregateException(new Exception(args.DatabaseError.Message)));
            }
            else
            {
                callback(StringSerializationAPI.Deserialize(typeof(Message), 
                    args.Snapshot.GetRawJsonValue()) as Message);
            }

        }

        // for stopping listening, it is not used for now.
        test = CurrentListener;
        
        reference.Child("messages").ChildAdded += CurrentListener;
    }
}
