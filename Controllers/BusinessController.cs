using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FirestoreAPI.Models;

namespace FirestoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class BusinessController : ControllerBase
    {
        private readonly FirestoreDb _firestoreDb;

        public BusinessController()
        {
            _firestoreDb = FirestoreDb.Create("barcodescanner-a69fd");
        }

        [HttpPost("business_uid")]
        public async Task<IActionResult> AddBusinessUid([FromBody] UidPayload data)
        {
            string businessUid = data.Uid;
            //temp collectionýnda saklama
            DocumentReference docRef = _firestoreDb.Collection("temp").Document(businessUid);
            await docRef.SetAsync(new { businessUid });
            return Ok(new {status = "Business UID stored successfully"});
        }

        


    }
}