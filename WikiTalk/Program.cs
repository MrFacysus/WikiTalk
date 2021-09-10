using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WikiTalk
{

    class Program
    {

        static void Main(string[] args)
        {

            string path = "";
            string text = "";
            string output = "";
            string lastchosen = "";
            string midchosen = "";
            string newchosen = "";
            int ran = 0;
            string[] words;
            Random random = new Random();
            Dictionary<string, List<string>> Dict = new Dictionary<string, List<string>>();

            Console.ForegroundColor = ConsoleColor.Green;
            path = args[0];
            text = File.ReadAllText(path);
            words = text.Split(' ','\n','\r');

            for (int wordindex = 0; wordindex < words.Length; wordindex++)
            {

                if (!Dict.ContainsKey(words[wordindex]))
                {

                    if (wordindex < words.Length - 2)
                    {

                        List<string> newest = new List<string>();

                        if (words[wordindex + 1] != "")
                        {
                            newest.Add(words[wordindex + 1]);
                        }
                        else
                        {
                            newest.Add("\n");
                        }
                        newest.Add("1");

                        Dict.Add(words[wordindex], newest);

                    }

                }
                else
                {

                    if (wordindex == words.Length - 2)
                    {

                        List<string> oldest = new List<string>();
                        oldest = Dict[words[wordindex]];

                        if (oldest.Contains(words[wordindex + 1]))
                        {

                            oldest[oldest.IndexOf(words[wordindex + 1]) + 1] = oldest[oldest.IndexOf(words[wordindex + 1]) + 1] + 1;

                        }
                        else
                        {

                            oldest.Add(words[wordindex + 1]);
                            oldest.Add("1");

                        }

                        Dict.Remove(words[wordindex]);
                        Dict.Add(words[wordindex], oldest);

                    }

                }

            }

            lastchosen = Dict.ElementAt(random.Next(Dict.Count)).Key;
            output = lastchosen;
            
            for (int x = 0; x < 50; x++)
            {

                ran = random.Next(Dict[lastchosen].Count - 1);

                midchosen = Dict[lastchosen][random.Next(Dict[lastchosen].Count - 1)];

                if (!output.Contains(midchosen))
                {

                    if (ran % 2 == 0)
                    {

                        newchosen = Dict[lastchosen][ran];

                    }
                    else
                    {

                        newchosen = Dict[lastchosen][ran-1];

                    }

                }
                else
                {

                    if (Dict[midchosen].Count > 2)
                    {

                        if ( (Dict[midchosen].Count - 1 - ran) % 2 == 0)
                        {

                            newchosen = Dict[midchosen][Dict[midchosen].Count - 1 - ran];
                        
                        }
                        else
                        {

                            newchosen = Dict[midchosen][Dict[midchosen].Count - 2 - ran];

                        }

                    }
                    else
                    {
 
                        newchosen = Dict.ElementAt(random.Next(Dict.Count)).Key;

                    }

                }

                Console.ForegroundColor = ConsoleColor.Green;
                output = output + " " + newchosen;

            }

            Console.WriteLine(output.ToLower());
            Console.ReadLine();

        }

    }

}