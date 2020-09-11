
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Vocabulate
{
    public class DictionaryData
    {

        public string TextFileFolder { get; set; }
        public string OutputFileLocation { get; set; }

        public int NumCats { get; set; }
        public int MaxWords { get; set; }

        public string[] CatNames { get; set; }
        public bool RawWordCounts { get; set; }
        
        public string StopListRawText { get; set; }

        public char CSVDelimiter { get; set; }
        public char CSVQuote { get; set; }

        public bool OutputCapturedText { get; set; }

        public Dictionary<int, string> CategoryOrder { get; set; }

        //yeah, we're going full inception with this variable. dictionary inside of a dictionary inside of a dictionary
        //while it might seem unnecessarily complicated (and it might be), it makes sense.
        //the first level simply differentiates the wildcard entries from the non-wildcard entries                
        //The second level is purely to refer to the word length -- does each sub-entry include 1-word entries, 2-word entries, etc?
        //the third level contains the actual entries from the user's dictionary file


        //ConceptMap:
        //{Concept, [TopLevelCategories]}

        public Dictionary<string, string[]> ConceptMap { get; set; }

        //FullDictionaryMap

        //WordList:
            //string length (in words)
                //{Word, Concept}
        //WildCardWordList:
            //string length (in words)
                //{Word, Concept}



        public Dictionary<string, Dictionary<int, Dictionary<string, string>>> FullDictionaryMap { get; set; }

        //this dictionary simply maps the specific wildcard entries to arrays, this way we can iterate across them since we have to do a serial search
        //when we're using wildcards
        public Dictionary<int, string[]> WildCardArrays { get; set; }

        //lastly, this contains the precompiled regexes mapped to their original strings
        public Dictionary<string, Regex> PrecompiledWildcards { get; set; }

        public bool DictionaryLoaded { get; set; } = false;


    }
}
