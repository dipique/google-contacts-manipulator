using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using Google.GData.Contacts;
using Google.GData.Client;

using Google.Contacts;


namespace Google_Contacts_Manipulator
{
    class Program
    {
        static void Main(string[] args)
        {
            auth();
        }

        public static void auth()
        {
            string clientId = "386262821228-762mat27equntjk3d99kbnmnkskipbp3.apps.googleusercontent.com";
            string clientSecret = "ZYcy23RgRHpFWeVnhp5dK2ji";

            string[] scopes = new string[] { "https://www.googleapis.com/auth/contacts.readonly" };

            // view your basic profile info. 
            try {
                // Use the current Google .net client library to get the Oauth2 stuff. 
                var secret = new ClientSecrets { ClientId = clientId, ClientSecret = clientSecret };
                var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(secret, scopes, "test", CancellationToken.None, new FileDataStore("store"))
                                                             .Result;

                // Translate the Oauth permissions to something the old client libray can read
                var parameters = new OAuth2Parameters() {
                    AccessToken = credential.Token.AccessToken,
                    RefreshToken = credential.Token.RefreshToken
                };
                RunContactsSample(parameters); } catch (Exception ex) { Console.WriteLine(ex.Message); } 
        }

        private static void RunContactsSample(OAuth2Parameters parameters)
        {
            try
            {
                RequestSettings settings = new RequestSettings("Google Contacts Manipulator", parameters);
                ContactsRequest cr = new ContactsRequest(settings);
                var f = cr.GetContacts();
                var l = f.Entries.ToList();

                Console.WriteLine("Test");
            }
            catch (Exception)
            {
                Console.WriteLine("A Google Apps error occurred."); Console.WriteLine();
            }
        }
    }
}
