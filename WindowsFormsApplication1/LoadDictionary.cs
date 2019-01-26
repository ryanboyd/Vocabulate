using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;



namespace Vocabulate
{
    class LoadDictionary
    {

        public DictionaryData LoadDictionaryFile(DictionaryData DictData, string InputFile, System.Text.Encoding SelectedEncoding, char CSVDelimiter, char CSVQuote)
        {


            //parse out the the dictionary file
            DictData.MaxWords = 0;

            //yeah, there's levels to this thing
            DictData.FullDictionaryMap = new Dictionary<string, Dictionary<int, Dictionary<string, string>>>();

            DictData.FullDictionaryMap.Add("Wildcards", new Dictionary<int, Dictionary<string, string>>());
            DictData.FullDictionaryMap.Add("Standards", new Dictionary<int, Dictionary<string, string>>());

            DictData.WildCardArrays = new Dictionary<int, string[]>();
            DictData.PrecompiledWildcards = new Dictionary<string, Regex>();


            Dictionary<int, List<string>> WildCardLists = new Dictionary<int, List<string>>();

            DictData.ConceptMap = new Dictionary<string, string[]>();






            using (var stream = File.OpenRead(InputFile))
            using (var reader = new StreamReader(stream, encoding: SelectedEncoding))
            {
                var data = Votabulate.CsvParser.ParseHeadAndTail(reader, CSVDelimiter, CSVQuote);

                var header = data.Item1;
                var lines = data.Item2;



                //  ____                   _       _         ____  _      _   ____        _           ___  _     _           _   
                // |  _ \ ___  _ __  _   _| | __ _| |_ ___  |  _ \(_) ___| |_|  _ \  __ _| |_ __ _   / _ \| |__ (_) ___  ___| |_ 
                // | |_) / _ \| '_ \| | | | |/ _` | __/ _ \ | | | | |/ __| __| | | |/ _` | __/ _` | | | | | '_ \| |/ _ \/ __| __|
                // |  __/ (_) | |_) | |_| | | (_| | ||  __/ | |_| | | (__| |_| |_| | (_| | || (_| | | |_| | |_) | |  __/ (__| |_ 
                // |_|   \___/| .__/ \__,_|_|\__,_|\__\___| |____/|_|\___|\__|____/ \__,_|\__\__,_|  \___/|_.__// |\___|\___|\__|
                //            |_|                                                                             |__/               




                



                DictData.NumCats = header.Count - 1;

                //now that we know the number of categories, we can fill out the arrays
                DictData.CatNames = new string[DictData.NumCats];
                //DictData.CatValues = new string[DictData.NumCats];

                DictData.CategoryOrder = new Dictionary<int, string>();
                //Map Out the Categories
                for (int i = 1; i < DictData.NumCats + 1; i++)
                {
                    DictData.CatNames[i-1] = header[i];
                    DictData.CategoryOrder.Add(i-1, header[i]);
                }


                foreach (var lineobject in lines)
                {
                    string[] line = (string[])lineobject.ToArray();

                    string[] WordsInLine = line[0].Trim().Split('|');

                    string Concept = WordsInLine[0];

                    //set the new item into our conceptmap dictionary
                    string[] CategoriesArray = line.Skip(1).Take(line.Length - 1).ToArray();


                    DictData.ConceptMap.Add(WordsInLine[0], new string[] { });

                    //fill out the list of concepts associated with each word
                    for (int i = 0; i < CategoriesArray.Length; i++)
                    {

                        if (!String.IsNullOrWhiteSpace(CategoriesArray[i].Trim()))
                        {
                            var obj = DictData.ConceptMap[Concept];
                            Array.Resize(ref obj, obj.Length + 1);
                            DictData.ConceptMap[Concept] = obj;
                            DictData.ConceptMap[Concept][obj.Length - 1] = DictData.CatNames[i];
                        }
                    }

                    //now we add the actual entries for each word in the row into our FullDictionary
                    foreach (string WordToCode in WordsInLine)
                    {
                        string WordToCodeTrimmed = WordToCode.Trim();

                        int Words_In_Entry = WordToCodeTrimmed.Split(' ').Length;
                        if (Words_In_Entry > DictData.MaxWords) DictData.MaxWords = Words_In_Entry;


                        if (WordToCodeTrimmed.Contains("*"))
                        {

                            if (DictData.FullDictionaryMap["Wildcards"].ContainsKey(Words_In_Entry))
                            {
                                DictData.FullDictionaryMap["Wildcards"][Words_In_Entry].Add(WordToCodeTrimmed.ToLower(), Concept);
                                WildCardLists[Words_In_Entry].Add(WordToCodeTrimmed);
                            }
                            else
                            {
                                DictData.FullDictionaryMap["Wildcards"].Add(Words_In_Entry, new Dictionary<string, string> { { WordToCodeTrimmed.ToLower(), Concept } });
                                WildCardLists.Add(Words_In_Entry, new List<string>());
                                WildCardLists[Words_In_Entry].Add(WordToCodeTrimmed);

                            }

                                
                            DictData.PrecompiledWildcards.Add(WordToCodeTrimmed.ToLower(), new Regex(Regex.Escape(WordToCodeTrimmed.ToLower()).Replace("\\*", ".*"), RegexOptions.Compiled));

                        }
                        else
                        {
                            if (DictData.FullDictionaryMap["Standards"].ContainsKey(Words_In_Entry))
                            {
                                DictData.FullDictionaryMap["Standards"][Words_In_Entry].Add(WordToCodeTrimmed.ToLower(), Concept);
                            }
                            else
                            {
                                DictData.FullDictionaryMap["Standards"].Add(Words_In_Entry, new Dictionary<string, string> { { WordToCodeTrimmed.ToLower(), Concept } });
                            }
                            
                        }

                    }


                }

                for (int i = DictData.MaxWords; i > 0; i--)
                {
                    if (WildCardLists.ContainsKey(i)) DictData.WildCardArrays.Add(i, WildCardLists[i].ToArray());
                }
                WildCardLists.Clear();
                DictData.DictionaryLoaded = true;


            }


            return DictData;


        }


    }
}
