using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace qrPaymentAPP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
    }
}
