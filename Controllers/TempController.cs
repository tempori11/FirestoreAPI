using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using FirestoreAPI.Models;

namespace FirestoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TempController : ControllerBase
    {
        private readonly FirestoreDb _firestoreDb;

        public TempController(FirestoreDb firestoreDb)
        {
            _firestoreDb = firestoreDb;
        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> SaveUidsToTemp([FromBody] UidModel uids)
        {
            if (uids == null || string.IsNullOrEmpty(uids.UserUid) || string.IsNullOrEmpty(uids.BusinessUid))
            {
                return BadRequest("Invalid UIDs");
            }

            var tempEntry = new Temp
            {
                UserUid = uids.UserUid,
                BusinessUid = uids.BusinessUid,
                Timestamp = DateTime.UtcNow,
                Amount = uids.Amount
            };

            await _firestoreDb.Collection("temp").AddAsync(tempEntry);
            return Ok("UIDs saved successfully");
        }
    }

    

   
}
