using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Vocabulate
{
    class StopWordRemover
    {

        private HashSet<string> StopWordsNoWildcards { get; set; }
        private Regex[] StopWordsWildCards { get; set; }

        public void BuildStopList(string StopListRawText)
        {

            StopWordsNoWildcards = new HashSet<string>();

            string[] StopListArray = StopListRawText.ToLower().Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            foreach(string StopWord in StopListArray)
            {
                StopWordsNoWildcards.Add(StopWord.Trim());
            }
        }

        public string[] ClearStopWords(string[] WordList)
        {

            for(int i = 0; i < WordList.Length; i++)
            {
                if (StopWordsNoWildcards.Contains(WordList[i])) WordList[i] = "";
            }

            return WordList;

        }

    }
}
