using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companion.FaceApi
{
    // setup service to save and recognise people, couldn't be finished due to azure subscription issues
    class PersonRecognition
    {
        readonly static IFaceClient _client = Instances.Authenticate();

        // create a group to hold our people
        public static async Task CreateGroup(string groupId, string name = null, string userData = null)
        {
            // large person group can hold 1 000 000 people, that is the limit atm, normal group holds 1000
            await _client.LargePersonGroup.CreateWithHttpMessagesAsync(groupId, name, userData);   
        }

        // get a group
        public static async Task<LargePersonGroup> GetGroup(string groupId)
        {
            return await _client.LargePersonGroup.GetAsync(groupId);
        }

        // get all people in a group
        public static async Task<List<Person>> GetAllPeople(string groupId)
        {
            var list = await _client.LargePersonGroupPerson.ListAsync(groupId);
            // cast from IList to List
            return new List<Person>(list);
        }

        // add a person to our group
        public static async Task<Person> AddPerson(string groupId, string name = null, string userData = null)
        {
            return await _client.LargePersonGroupPerson.CreateAsync(groupId, name, userData);
        }

        // add face to a person through saved files
        // only works if you have an azure subscription that allows for storing faces
        public static async Task AddFaces(string groupId, Person person, string imageDir)
        {
            //string imageDir = @"D:\Pictures\MyFriends\SomeFriend\";
            foreach (string imagePath in Directory.GetFiles(imageDir, "*.jpg"))
            {
                using (Stream s = File.OpenRead(imagePath))
                {
                    await _client.LargePersonGroupPerson.AddFaceFromStreamAsync(groupId, person.PersonId, s);
                }
            }
        }

        // add face too a person through bitmaps
        public static async Task AddFaces(string groupId, Person person, List<Bitmap> bitmaps)
        {
            foreach (var bitmap in bitmaps)
            {
                using (Stream s = new MemoryStream())
                {
                    bitmap.Save(s, ImageFormat.Jpeg);
                    await _client.LargePersonGroupPerson.AddFaceFromStreamAsync(groupId, person.PersonId, s);
                }
            }
        }

        // (re)train the group, needed after you add faces
        public static async Task TrainGroup(string groupId)
        {
            await _client.LargePersonGroup.TrainAsync(groupId);

            while (true)
            {
                var trainingStatus = await _client.LargePersonGroup.GetTrainingStatusAsync(groupId);

                if (trainingStatus.Status != TrainingStatusType.Running)
                {
                    break;
                }

                await Task.Delay(200);
            }
        }

        // get people from an image
        public static async Task<List<Person>> Identify(string groupId, Bitmap capture)
        {
            //string testImageFile = @"D:\Pictures\test_img1.jpg";
            
            using (Stream s = new MemoryStream())
            {
                capture.Save(s, ImageFormat.Png);

                var faces = await _client.Face.DetectWithStreamAsync(s);
                var faceIds = faces.Select(face => face.FaceId.Value).ToArray();

                var results = await _client.Face.IdentifyAsync(faceIds, groupId);

                List<Person> people = new List<Person>();

                foreach (var identifyResult in results)
                {
                    if (identifyResult.Candidates.Count == 0)
                    {
                        break;
                    }
                    else
                    {
                        // Get top 1 among all candidates returned
                        var candidateId = identifyResult.Candidates[0].PersonId;
                        var person = await _client.LargePersonGroupPerson.GetAsync(groupId, candidateId);
                        people.Add(person);
                    }
                }

                return people;
            }
        }
    }
}
