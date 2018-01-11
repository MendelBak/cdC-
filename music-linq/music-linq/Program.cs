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
            IEnumerable<Artist> mountVernonArtist = Artists.Where(artist => artist.Hometown == "Mount Vernon");
            foreach (var artist in mountVernonArtist)
            {
                System.Console.WriteLine("The one artist that lives in Mount Vernon: " + artist.ArtistName);
            }

            //Who is the youngest artist in our collection of artists?
            var yungVanessa = Artists.OrderBy(artist => artist.Age).First();
            System.Console.WriteLine("The youngest artist of them all is " + yungVanessa.ArtistName);


            //Display all artists with 'William' somewhere in their real name
            var WilliamsSearch = Artists.Where(artist => artist.RealName.Contains("William")).ToList();
            System.Console.WriteLine("Here are all the artists with 'William' somewhere in their name");
            foreach (var artist in WilliamsSearch)
            {
                System.Console.WriteLine(artist.ArtistName);
            }

            //Display the 3 oldest artists from Atlanta
            var AtlantaOldest = Artists.Where(artist => artist.Hometown == "Atlanta").OrderByDescending(artist => artist.Age).Take(3);
            System.Console.WriteLine("Here are the three oldest artists in Atlanta:");
            foreach (var artist in AtlantaOldest)
            {
                System.Console.WriteLine(artist.ArtistName);
            }

            //  Display all groups with names less than 8 characters in length.
            var EightCharGroups = Groups.Where(group => group.GroupName.Length < 8);
            System.Console.WriteLine("Here are the groups with names less than 8 characters");
            foreach (var group in EightCharGroups)
            {
                System.Console.WriteLine(group.GroupName);
            }

            //(Optional) Display the Group Name of all groups that have members that are not from New York City
            var NotNYers = Groups.Join(Artists, group => group.Id, artist => artist.GroupId, (group, artist) =>
            {
                if (artist.Hometown != "New York City")
                {
                    return group.GroupName;
                }
                else
                {
                    return null;
                }
            }).Distinct();
            System.Console.WriteLine("Here are the groups that don't contain New Yorkers in them");
            foreach (var groupname in NotNYers)
            {
                System.Console.WriteLine(groupname);
            }

            //(Optional) Display the artist names of all members of the group 'Wu-Tang Clan'
            var WuTangMembers = Artists.Join(Groups, artist => artist.GroupId, group => group.Id, (artist, group) =>
            {
                if (artist.GroupId == 1)
                {
                    return artist.ArtistName;
                }
                else
                {
                    return null;

                }
            }).ToList().Distinct();
            if (WuTangMembers.Any())
            {
                System.Console.WriteLine("Here are all the members of the Wu Tang Clan");
                foreach (var person in WuTangMembers)
                {
                    System.Console.WriteLine(person);
                }
            };
        }
    }
}
