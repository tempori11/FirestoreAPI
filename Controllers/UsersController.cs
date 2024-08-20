using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FirestoreAPI.Models;

namespace FirestoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly FirestoreDb _firestoreDb;

        public UsersController()
        {
            _firestoreDb = FirestoreDb.Create("barcodescanner-a69fd");
        }

        [HttpGet("{uid}")]
        public async Task<IActionResult> GetUserByUid(string uid)
        {
            var docRef = _firestoreDb.Collection("users").Document(uid);
            var snapshot = await docRef.GetSnapshotAsync();

            if (snapshot.Exists)
            {
                return Ok(snapshot.ToDictionary());
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("user_uid")]
        public async Task<IActionResult> AddUserUid([FromBody] UidPayload data)
        {
            string userUid = data.Uid;
            // Store userUid in the temp collection
            DocumentReference docRef = _firestoreDb.Collection("temp").Document(userUid);
            await docRef.SetAsync(new { userUid });
            return Ok(new { status = "User UID stored successfully" });
        }
    }
}
