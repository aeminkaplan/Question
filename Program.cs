using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            /*These containers provide mapping */
            Dictionary<int, LinkedList<int>> userUniqueSongList = new Dictionary<int, LinkedList<int>>();
            Dictionary<int, int> songPlayerCounts = new Dictionary<int, int>();
            

            LinkedList<int> songList = new LinkedList<int>();

            FileStream fileStream=null;

            string line;
            /*File should be in same directory with executeable file*/
            var path = Path.Combine(Directory.GetCurrentDirectory(), "exhibitA-input.csv");

            try
            {
                fileStream = new FileStream(@path, FileMode.Open, FileAccess.Read);

            }
            catch (FileNotFoundException e) {

                Environment.Exit(1);
            }

           
            var streamReader = new StreamReader(fileStream, Encoding.UTF8);

            while ((line = streamReader.ReadLine()) != null)
            {
                string[] words = line.Split('\t');
                string[] dateAndTime = words[3].Split(' ');

                /*After reading each line , just I check the date of entry*/
                if (dateAndTime[0] != null && dateAndTime[0].CompareTo("10/08/2016") == 0)
                {

                    int clientID = Int32.Parse(words[2]);
                    int songID = Int32.Parse(words[1]);

                    /*If the user has a list already just try to add new item*/
                    if (userUniqueSongList.ContainsKey(clientID))
                    {
                        /*If the user has a list and the list doesnt contain the song*/
                        if (!userUniqueSongList[clientID].Contains(songID))
                            userUniqueSongList[clientID].AddLast(songID);
                    }
                    //User doesnt have a list yet
                    else

                    {
                        songList = new LinkedList<int>();
                        songList.AddLast(songID);
                        userUniqueSongList.Add(clientID, songList);
                    }
                }


            }

            streamReader.Close();
            fileStream.Close();

            int[] clients = userUniqueSongList.Keys.ToArray();

            /*Look at unique lists of clients*/
            for (int i = 0; i < clients.Length; ++i)
            {


                /*Put the datas to the other map container which has unique played songs count  and its distrubution*/
                if (songPlayerCounts.ContainsKey(userUniqueSongList[clients[i]].Count))
                {
                    songPlayerCounts[userUniqueSongList[clients[i]].Count] += 1;
                }
                else
                {

                    songPlayerCounts.Add(userUniqueSongList[clients[i]].Count, 1);
                }

            }
            


            int[] counts = songPlayerCounts.Keys.ToArray();

            Array.Sort(counts);

            Console.WriteLine("DISTINCT_PLAY_COUNT\tCLIENT_COUNT");

            for (int i = 0; i < counts.Length; ++i)
            {
                Console.WriteLine(counts[i] + "\t" + songPlayerCounts[counts[i]]);
            }

            while (true) ;
        }
    }
}
