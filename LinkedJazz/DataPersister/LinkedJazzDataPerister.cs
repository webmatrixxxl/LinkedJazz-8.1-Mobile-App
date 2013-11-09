using LinkedJazz;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LinkedJazz.DataPersister
{
    public class LinkedJazzDataPerister
    {
        private const string BaseServicesUrl = "http://linkedjazz.org/api/";


        public async static Task<ObservableCollection<ArtistLinkedJazzModel>> GetAllPeople()
        {
            ObservableCollection<ArtistLinkedJazzModel> people = await HttpRequester.Get<ObservableCollection<ArtistLinkedJazzModel>>(BaseServicesUrl + "people/all");

            return people;
        }

        public async static Task<ObservableCollection<RelationshipArtistLinkedJazzModel>> GetRelationshipsOfArtist(string encodedUrl)
        {
            ObservableCollection<RelationshipArtistLinkedJazzModel> artistRelationships = await HttpRequester.Get<ObservableCollection<RelationshipArtistLinkedJazzModel>>(BaseServicesUrl + "relationships/" + encodedUrl);

            return artistRelationships;
        }

        public async static Task<ObservableCollection<ArtistLinkedJazzModel>> SearchForArtists(string query)
        {
            ObservableCollection<ArtistLinkedJazzModel> artistSearchResult = await HttpRequester.Get<ObservableCollection<ArtistLinkedJazzModel>>(BaseServicesUrl + "people/search/" + query);

            return artistSearchResult;
        }

        public async static Task<ArtistLinkedJazzModel> GetArtist(string name)
        {
            ObservableCollection<ArtistLinkedJazzModel> artist = await HttpRequester.Get<ObservableCollection<ArtistLinkedJazzModel>>(BaseServicesUrl + "people/search/" + name);

            return artist[0];
        }
    }
}