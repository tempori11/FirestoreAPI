using Google.Cloud.Firestore;
using System;

namespace FirestoreAPI.Models

{

    [FirestoreData] // Add this attribute to indicate it's a Firestore data model
    public class Temp
    {
        [FirestoreProperty] // Each property should be marked with this attribute
        public string UserUid { get; set; }

        [FirestoreProperty] // Mark BusinessUid as a Firestore property
        public string BusinessUid { get; set; }

        [FirestoreProperty] // Mark Timestamp as a Firestore property
        public DateTime Timestamp { get; set; }

        [FirestoreProperty]
        public double Amount { get; set; }
    }

}