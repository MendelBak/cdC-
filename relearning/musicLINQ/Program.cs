using System;
using System.Collections.Generic;
using System.Linq;
using JsonData;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Collections to work with
            List<Artist> Artists = JsonToFile<Artist>.ReadJson();
            List<Group> Groups = JsonToFile<Group>.ReadJson();

            //========================================================
            //Solve all of the prompts below using various LINQ queries
            //========================================================

            //There is only one artist in this collection from Mount Vernon, what is their name and age?
            var vernonArtist = Artists.Where(artist => artist.Hometown == "Mount Vernon");
            foreach (var artist in vernonArtist)
            {
                System.Console.WriteLine(artist.ArtistName);
            }


            //Who is the youngest artist in our collection of artists?

            var youngerArtist = Artists.OrderBy(artist => artist.Age).FirstOrDefault();
            // foreach(var artist in youngerArtist)
            // {
            System.Console.WriteLine(youngerArtist.ArtistName + " is " + youngerArtist.Age + " years old");
            // }

            //Display all artists with 'William' somewhere in their real name
            var williamArtists = Artists.Where(artist => artist.RealName.Contains("William"));
            System.Console.WriteLine("******************");
            foreach (var artist in williamArtists)
            {
                System.Console.WriteLine(artist.RealName);
            }

            // Display all groups with names less than 8 characters in length.
            var shortGroupNames = Groups.Where(g => g.GroupName.Length < 8);
            System.Console.WriteLine("******************");
            System.Console.WriteLine("Group names less than 8 chars");
            System.Console.WriteLine("******************");
            foreach (var group in shortGroupNames)
            {
                System.Console.WriteLine(group.GroupName);
            }

            //Display the 3 oldest artist from Atlanta
            var oldFromAtlanta = Artists.Where(a => a.Hometown == "Atlanta").OrderByDescending(a => a.Age).Take(3);
            System.Console.WriteLine("******************");
            System.Console.WriteLine("3 Oldest artists from Atlanta");
            System.Console.WriteLine("******************");
            foreach (var artist in oldFromAtlanta)
            {
                System.Console.WriteLine(artist.RealName + "-" + artist.Age + "-" + artist.Hometown);
            }

            //(Optional) Display the Group Name of all groups that have members that are not from New York City
            var nonNYGroupNames = Groups.Join(Artists, g => g.Id, a => a.GroupId, (g, a) => 
            {
                if(a.Hometown != "New York City")
                {
                    return g.GroupName;
                }
                else
                {
                    return null;
                }
            }).Distinct();
            System.Console.WriteLine("******************");
            foreach (var group in nonNYGroupNames)
            {
                System.Console.WriteLine(group);
            }

            //(Optional) Display the artist names of all members of the group 'Wu-Tang Clan'
            var WuTangClanMembers = Groups.Where(g => g.GroupName == "Wu-Tang Clan").Join(Artists, g => g.Id, a => a.GroupId, (g, a) => {
                return a.ArtistName;
            }).ToList();
            System.Console.WriteLine("******************");
            System.Console.WriteLine("Members of the WuTangClan");
            System.Console.WriteLine("******************");
            foreach (var member in WuTangClanMembers)
            {
                System.Console.WriteLine(member);
            }
        }
    }
}
