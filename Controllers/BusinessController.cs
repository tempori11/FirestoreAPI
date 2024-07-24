using FirebaseAdmin.Auth;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace qrPaymentAPP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusinessController : ControllerBase
    {
        private readonly FirebaseAuth _firebaseAuth;
        private readonly FirestoreDb _firestoreDb;

        public BusinessController()
        {
            // Initialize Firebase Authentication
            _firebaseAuth = FirebaseAuth.DefaultInstance;

            // Initialize Firestore
            _firestoreDb = FirestoreDb.Create("barcodescanner-a69fd");
        }

        [HttpGet("currentuser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            // Retrieve the ID token from the request headers
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Unauthorized("Authorization header is missing.");
            }

            var authHeader = Request.Headers["Authorization"].ToString();
            var idToken = authHeader.Replace("Bearer ", "").Trim();

            try
            {
                // Verify the ID token
                var decodedToken = await _firebaseAuth.VerifyIdTokenAsync(idToken);
                var uid = decodedToken.Uid;

                // Fetch user data from Firestore
                var docRef = _firestoreDb.Collection("users").Document(uid);
                var snapshot = await docRef.GetSnapshotAsync();

                if (snapshot.Exists)
                {
                    return Ok(snapshot.ToDictionary());
                }
                else
                {
                    return NotFound("User not found.");
                }
            }
            catch (FirebaseAuthException)
            {
                return Unauthorized("Invalid ID token.");
            }
        }
    }
}
