using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using nltk.tokenize.casual.NET;



namespace VocabulateApplication
{

    public partial class VocabulateMainForm : Form
    {


        //initialize the space for our dictionary data
        Vocabulate.DictionaryData DictData = new Vocabulate.DictionaryData();



        //this is what runs at initialization
        public VocabulateMainForm()
        {

            InitializeComponent();

            foreach(var encoding in Encoding.GetEncodings())
            {
                EncodingDropdown.Items.Add(encoding.Name);
            }

            try
            {
                EncodingDropdown.SelectedIndex = EncodingDropdown.FindStringExact("utf-8");
            }
            catch
            {
                EncodingDropdown.SelectedIndex = EncodingDropdown.FindStringExact(Encoding.Default.BodyName);
            }

            //load stoplist
            StopListTextBox.Text = Vocabulate.Properties.Resources.StopListCharacters + Environment.NewLine + Vocabulate.Properties.Resources.StopListEN;


        }







        private void StartButton_Click(object sender, EventArgs e)
        {

                    if (CSVDelimiterTextbox.Text.Length < 1) CSVDelimiterTextbox.Text = ",";
                    if (CSVQuoteTextbox.Text.Length < 1) CSVQuoteTextbox.Text = "\"";

                    //make sure that our dictionary is loaded before anything else
                    if (DictData.DictionaryLoaded != true)
                    {
                        MessageBox.Show("You must first load a dictionary file before you can analyze your texts.", "Dictionary not loaded!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
            

                    FolderBrowser.Description = "Please choose the location of your .txt files to analyze";
                    if (FolderBrowser.ShowDialog() != DialogResult.Cancel) {

                        DictData.TextFileFolder = FolderBrowser.SelectedPath.ToString();
                
                        if (DictData.TextFileFolder != "")
                        {

                            saveFileDialog.FileName = "Vocabulate_Output.csv";

                            saveFileDialog.InitialDirectory = DictData.TextFileFolder;
                            if (saveFileDialog.ShowDialog() != DialogResult.Cancel) {


                                DictData.OutputFileLocation = saveFileDialog.FileName;
                                DictData.RawWordCounts = RawWCCheckbox.Checked;
                                DictData.StopListRawText = StopListTextBox.Text;
                                DictData.CSVDelimiter = CSVDelimiterTextbox.Text[0];
                                DictData.CSVQuote = CSVQuoteTextbox.Text[0];

                                if (DictData.OutputFileLocation != "") {

                                    StopListTextBox.Enabled = false;
                                    StartButton.Enabled = false;
                                    ScanSubfolderCheckbox.Enabled = false;
                                    EncodingDropdown.Enabled = false;
                                    LoadDictionaryButton.Enabled = false;
                                    RawWCCheckbox.Enabled = false;
                                    CSVDelimiterTextbox.Enabled = false;
                                    CSVQuoteTextbox.Enabled = false;
                            
                                    BgWorker.RunWorkerAsync(DictData);
                                }
                            }
                        }

                    }

                

        }

        




        private void BgWorkerClean_DoWork(object sender, DoWorkEventArgs e)
        {


            Vocabulate.DictionaryData DictData = (Vocabulate.DictionaryData)e.Argument;
            TwitterAwareTokenizer Tokenizer = new TwitterAwareTokenizer();
            Tokenizer.Initialize_Regex();
            Vocabulate.StopWordRemover StopList = new Vocabulate.StopWordRemover();
            StopList.BuildStopList(DictData.StopListRawText);


            //selects the text encoding based on user selection
            Encoding SelectedEncoding = null;
            this.Invoke((MethodInvoker)delegate ()
            {
                SelectedEncoding = Encoding.GetEncoding(EncodingDropdown.SelectedItem.ToString());
            });

            

            //get the list of files
            var SearchDepth = SearchOption.TopDirectoryOnly;
            if (ScanSubfolderCheckbox.Checked)
            {
                SearchDepth = SearchOption.AllDirectories;
            }
            var files = Directory.EnumerateFiles(DictData.TextFileFolder, "*.txt", SearchDepth);

            string CSVQuote = DictData.CSVQuote.ToString();
            string CSVDelimiter = DictData.CSVDelimiter.ToString();

            try {

            //open up the output file
            using (StreamWriter outputFile = new StreamWriter(new FileStream(DictData.OutputFileLocation, FileMode.Create), SelectedEncoding))
            {

                short NumberOfHeaderLeadingColumns = 8;
                //write the header row to the output file
                StringBuilder HeaderString = new StringBuilder();
                HeaderString.Append(CSVQuote + "Filename" + CSVQuote + CSVDelimiter +
                                     CSVQuote + "TC_Raw" + CSVQuote + CSVDelimiter +
                                     CSVQuote + "TTR_Raw" + CSVQuote + CSVDelimiter +
                                     CSVQuote + "TC_Clean" + CSVQuote + CSVDelimiter +
                                     CSVQuote + "TTR_Clean" + CSVQuote + CSVDelimiter +
                                     CSVQuote + "TC_NonDict" + CSVQuote + CSVDelimiter +
                                     CSVQuote + "TTR_NonDict" + CSVQuote + CSVDelimiter +
                                     CSVQuote + "DictPercent" + CSVQuote);
                for (int i = 0; i < DictData.NumCats; i++) HeaderString.Append(CSVDelimiter + CSVQuote + 
                                                                               DictData.CatNames[i].Replace(CSVQuote, CSVQuote + CSVQuote) + "_CCR" +
                                                                              CSVQuote);

                if(DictData.RawWordCounts)
                {
                    for (int i = 0; i < DictData.NumCats; i++) HeaderString.Append(CSVDelimiter + CSVQuote +
                                                                               DictData.CatNames[i].Replace(CSVQuote, CSVQuote + CSVQuote) + "_Count" +
                                                                              CSVQuote);
                    for (int i = 0; i < DictData.NumCats; i++) HeaderString.Append(CSVDelimiter + CSVQuote +
                                                                                DictData.CatNames[i].Replace(CSVQuote, CSVQuote + CSVQuote) + "_Unique" +
                                                                                CSVQuote);
                    }

                    outputFile.WriteLine(HeaderString.ToString());


                    foreach (string fileName in files)
                    {

                        //set up our variables to report
                        string Filename_Clean = Path.GetFileName(fileName);
                        Dictionary<string, ulong> DictionaryResults = new Dictionary<string, ulong>();
                        foreach (string Concept in DictData.ConceptMap.Keys) DictionaryResults.Add(Concept, 0);
                        
                        //structure of DictionaryResults will look like this:

                        //Concept -> Total

                        //this will make it far easier to go through and calculate number of unique concepts divided by total number of words
                        //at the top level categories down the road





                        //for (int i = 0; i < DictData.NumCats; i++) DictionaryResults.Add(DictData.CatValues[i], 0);

                        //report what we're working on
                        FilenameLabel.Invoke((MethodInvoker)delegate
                        {
                            FilenameLabel.Text = "Analyzing: " + Filename_Clean;
                        });




                        //read in the text file, convert everything to lowercase
                        string readText = File.ReadAllText(fileName, SelectedEncoding).ToLower();



                        int NumberOfMatches = 0;


                        //splits everything out into words
                        string[] Words = Tokenizer.tokenize(readText.Trim());
                        Words = StopList.ClearStopWords(Words);

                        int TotalStringLength_BeforeStopList = Words.Length;
                        double TTR_Raw = (Words.Distinct().Count() / (double)TotalStringLength_BeforeStopList) * 100;


                        Words = Words.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                        int TotalStringLength_AfterStopList = Words.Length;
                        double TTR_Clean = (Words.Distinct().Count() / (double)TotalStringLength_AfterStopList) * 100;


                        List<string> NonmatchedTokens = new List<string>();


                        //     _                _                 _____         _   
                        //    / \   _ __   __ _| |_   _ _______  |_   _|____  _| |_ 
                        //   / _ \ | '_ \ / _` | | | | |_  / _ \   | |/ _ \ \/ / __|
                        //  / ___ \| | | | (_| | | |_| |/ /  __/   | |  __/>  <| |_ 
                        // /_/   \_\_| |_|\__,_|_|\__, /___\___|   |_|\___/_/\_\\__|
                        //                        |___/                             


                        //iterate over all words in the text file
                        for (int i = 0; i < TotalStringLength_AfterStopList; i++)
                        {


                            bool TokenMatched = false;
                            //iterate over n-grams, starting with the largest possible n-gram (derived from the user's dictionary file)
                            for (int NumberOfWords = DictData.MaxWords; NumberOfWords > 0; NumberOfWords--)
                            {



                                //make sure that we don't overextend past the array
                                if (i + NumberOfWords - 1 >= TotalStringLength_AfterStopList) continue;

                                //make the target string

                                string TargetString;

                                if (NumberOfWords > 1)
                                {
                                    TargetString = String.Join(" ", Words.Skip(i).Take(NumberOfWords).ToArray());
                                }
                                else
                                {
                                    TargetString = Words[i];
                                }


                                //look for an exact match

                                if (DictData.FullDictionaryMap["Standards"].ContainsKey(NumberOfWords))
                                {
                                    if (DictData.FullDictionaryMap["Standards"][NumberOfWords].ContainsKey(TargetString))
                                    {

                                        //add in the number of words found
                                        NumberOfMatches += NumberOfWords;

                                        //increment results
                                        DictionaryResults[DictData.FullDictionaryMap["Standards"][NumberOfWords][TargetString]] += 1;
                                        

                                        //manually increment the for loop so that we're not testing on words that have already been picked up
                                        i += NumberOfWords - 1;
                                        //break out of the lower level for loop back to moving on to new words altogether
                                        TokenMatched = true;
                                        break;
                                    }
                                }
                                //if there isn't an exact match, we have to go through the wildcards
                                if (DictData.WildCardArrays.ContainsKey(NumberOfWords))
                                {
                                    for (int j = 0; j < DictData.WildCardArrays[NumberOfWords].Length; j++)
                                    {
                                        if (DictData.PrecompiledWildcards[DictData.WildCardArrays[NumberOfWords][j]].Matches(TargetString).Count > 0)
                                        {

                                            //add in the number of words found
                                            NumberOfMatches += NumberOfWords;

                                            //increment results
                                            DictionaryResults[DictData.FullDictionaryMap["Wildcards"][NumberOfWords][DictData.WildCardArrays[NumberOfWords][j]]] += 1;

                                            //manually increment the for loop so that we're not testing on words that have already been picked up
                                            i += NumberOfWords - 1;
                                            //break out of the lower level for loop back to moving on to new words altogether
                                            TokenMatched = true;
                                            break;

                                        }
                                    }
                                }
                            }

                            //this is what we do if we didn't find any match in our dictionary
                            if (!TokenMatched) NonmatchedTokens.Add(Words[i]);



                        }







                        // __        __    _ _          ___        _               _   
                        // \ \      / / __(_) |_ ___   / _ \ _   _| |_ _ __  _   _| |_ 
                        //  \ \ /\ / / '__| | __/ _ \ | | | | | | | __| '_ \| | | | __|
                        //   \ V  V /| |  | | ||  __/ | |_| | |_| | |_| |_) | |_| | |_ 
                        //    \_/\_/ |_|  |_|\__\___|  \___/ \__,_|\__| .__/ \__,_|\__|
                        //                                            |_|              


                        short OutputColumnsModifier = 1;
                        if (DictData.RawWordCounts) OutputColumnsModifier = 3;
                        
                        string[] OutputString = new string[NumberOfHeaderLeadingColumns + (DictData.NumCats * OutputColumnsModifier)];

                        for (int i = 0; i < OutputString.Length; i++) OutputString[i] = "";

                        
                        OutputString[0] = CSVQuote + Filename_Clean + CSVQuote; //filename
                        OutputString[1] = TotalStringLength_BeforeStopList.ToString(); //total number of words
                        if (TotalStringLength_BeforeStopList > 0) OutputString[2] = TTR_Raw.ToString(); //TTR_Raw
                        OutputString[3] = TotalStringLength_AfterStopList.ToString(); //total number of tokens after stoplist processing
                        if (TotalStringLength_AfterStopList > 0) OutputString[4] = TTR_Clean.ToString(); // TTR_Clean
                        OutputString[5] = (TotalStringLength_AfterStopList - NumberOfMatches).ToString(); //number of non-dictionary tokens
                        if (NonmatchedTokens.Count() > 0) OutputString[6] = (((double)NonmatchedTokens.Distinct().Count() / NonmatchedTokens.Count()) * 100).ToString(); //TTR for non-dictionary words
                        
                    

                    if (TotalStringLength_BeforeStopList > 0)
                        {
                            
                            OutputString[7] = (((double)NumberOfMatches / TotalStringLength_BeforeStopList) * 100).ToString(); //dictpercent


                            //pull together the results here
                            Dictionary<string, ulong[]> CompiledResults = new Dictionary<string, ulong[]>();
                            foreach(string TopLevelCategory in DictData.CatNames)
                            {
                                CompiledResults.Add(TopLevelCategory, new ulong[2] { 0, 0 });
                            }

                            foreach(string ConceptKey in DictData.ConceptMap.Keys)
                            {
                                if(DictionaryResults[ConceptKey] > 0)
                                {
                                    for (int i = 0; i < DictData.ConceptMap[ConceptKey].Length; i++)
                                    {
                                        //if the Concept was found in the text, increment the first index (i.e., the number of unique concepts) by 1
                                        CompiledResults[DictData.ConceptMap[ConceptKey][i]][0] += 1;
                                        //if the Concept was found in the text, add the number of times it occurred
                                        CompiledResults[DictData.ConceptMap[ConceptKey][i]][1] += DictionaryResults[ConceptKey];
                                    }
                                }
                            }


                            for(int i = 0; i < DictData.CategoryOrder.Count; i++)
                            {

                                if (CompiledResults[DictData.CategoryOrder[i]][0] > 0)
                                {
                                    OutputString[i + NumberOfHeaderLeadingColumns] = (((double)CompiledResults[DictData.CategoryOrder[i]][0] / CompiledResults[DictData.CategoryOrder[i]][1]) * 100.0).ToString();
                                }
                                
                            }

                            //this is if the user asked for the raw counts per category
                            if (DictData.RawWordCounts)
                            {

                                for (int i = 0; i < DictData.CategoryOrder.Count; i++)
                                {

                                       OutputString[i + NumberOfHeaderLeadingColumns + DictData.NumCats] = CompiledResults[DictData.CategoryOrder[i]][1].ToString();
                                       OutputString[i + NumberOfHeaderLeadingColumns + (DictData.NumCats * 2)] = CompiledResults[DictData.CategoryOrder[i]][0].ToString();

                                }


                            }

                                

                        }
                        else
                        {
                            OutputString[2] = "";
                            for (int i = 0; i < DictData.NumCats; i++) OutputString[i + NumberOfHeaderLeadingColumns] = "";
                        }


                        outputFile.WriteLine(String.Join(CSVDelimiter, OutputString));








                    }


                }

            }
            catch
            {
                MessageBox.Show("Vocabulate encountered an issue somewhere while trying to analyze your texts. The most common cause of this is trying to open your output file while Vocabulate is still running. Did any of your input files move, or is your output file being opened/modified by another application?", "Error while analyzing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }




        //when the bgworker is done running, we want to re-enable user controls and let them know that it's finished
        private void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            StartButton.Enabled = true;
            ScanSubfolderCheckbox.Enabled = true;
            EncodingDropdown.Enabled = true;
            StopListTextBox.Enabled = true;
            LoadDictionaryButton.Enabled = true;
            RawWCCheckbox.Enabled = true;
            CSVDelimiterTextbox.Enabled = true;
            CSVQuoteTextbox.Enabled = true;
            FilenameLabel.Text = "Finished!";
            MessageBox.Show("Vocabulate has finished analyzing your texts.", "Analysis Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
















        //  _                    _   ____  _      _   _                              
        // | |    ___   __ _  __| | |  _ \(_) ___| |_(_) ___  _ __   __ _ _ __ _   _ 
        // | |   / _ \ / _` |/ _` | | | | | |/ __| __| |/ _ \| '_ \ / _` | '__| | | |
        // | |__| (_) | (_| | (_| | | |_| | | (__| |_| | (_) | | | | (_| | |  | |_| |
        // |_____\___/ \__,_|\__,_| |____/|_|\___|\__|_|\___/|_| |_|\__,_|_|   \__, |
        //                                                                     |___/ 



        private void LoadDictionaryButton_Click(object sender, EventArgs e)
        {


            DictData = new Vocabulate.DictionaryData();

            DictStructureTextBox.Text = "";


         
            openFileDialog.Title = "Please choose your dictionary file";

            if (openFileDialog.ShowDialog() != DialogResult.Cancel)
            {

                FolderBrowser.SelectedPath = System.IO.Path.GetDirectoryName(openFileDialog.FileName);

                //Load dictionary file now
                try
                {
                    Encoding SelectedEncoding = null;
                    SelectedEncoding = Encoding.GetEncoding(EncodingDropdown.SelectedItem.ToString());

                    Vocabulate.LoadDictionary DictionaryLoader = new Vocabulate.LoadDictionary();
                    DictData = DictionaryLoader.LoadDictionaryFile(DictData, openFileDialog.FileName, SelectedEncoding, CSVDelimiterTextbox.Text[0], CSVQuoteTextbox.Text[0]);

                    //this is where we load up the dictionary preview
                    StringBuilder DictPreview = new StringBuilder();

                    DictPreview.AppendLine("TERM -> CONCEPT -> [CATEGORIES]");
                    DictPreview.AppendLine("-------------------------------");

                    foreach (string StemType in DictData.FullDictionaryMap.Keys) { 
                        foreach (int WordCountKey in DictData.FullDictionaryMap[StemType].Keys)
                        {

                            foreach(var Word in DictData.FullDictionaryMap[StemType][WordCountKey])
                            {

                                DictPreview.AppendLine(Word.Key + " -> " + Word.Value + " -> [" + string.Join(", ", DictData.ConceptMap[Word.Value]) + "]");                                

                            }

                        }
                    }


                    DictStructureTextBox.Text = DictPreview.ToString();

                    MessageBox.Show("Your dictionary has been successfully loaded.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch
                {
                    MessageBox.Show("Vocabulate is having trouble loading your dictionary file. The most common causes of this problem are:" + Environment.NewLine + Environment.NewLine +
                        "-> Your dictionary file is already being used by another application" + Environment.NewLine +
                        "-> Your dictionary is formatted incorrectly" + Environment.NewLine +
                        "-> You dictionary contains duplicate words (the same word appearing more than once)" + Environment.NewLine + Environment.NewLine + 
                        "Please check to make sure that none of these issues exist in your dictionary file.", 
                        "Dictionary Load Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DictData.DictionaryLoaded = false;
                    return;
                }

            }
            else
            {
                return;
            }


        }



    }
    


}
