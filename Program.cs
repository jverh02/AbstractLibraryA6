using System;
using System.IO;
using System.Linq;

namespace AbstractLibraryA6
{
    class Program
    {
        public static String moviefile = "movies.csv"; //in case the path to the file needs to be changed, I only need to change it in one place
        public static String showfile = "shows.csv";
        public static String videofile = "videos.csv";
        static void Main(string[] args)
        {
            var done = false;
            while(!done)
            {
                var isValid = false;
                int choice = 0;
                string mediachoice = "";
                while (!isValid)
                {
                    Console.Write("What kind of media are you interested in? (show, movie, video) or type exit to exit>");
                    mediachoice = Console.ReadLine();
                    if (mediachoice.ToLower().Equals("movie") || mediachoice.ToLower().Equals("show") || mediachoice.ToLower().Equals("video"))
                    {
                        isValid = true;
                    }
                    else if (mediachoice.ToLower().Equals("exit"))
                    {
                        isValid = true;
                        done = true;
                    }
                    else
                    {
                        Console.WriteLine("ERROR: Select a valid media type.\n");
                    }
                }
                isValid = false;
                while (!isValid && !done)
                {
                    Console.Write("Choose an option:\n1. List all media on record\n2. Add media to file\n3. Exit\n>");
                    var response = Console.ReadLine();
                    isValid = Int32.TryParse(response, out choice);
                    if(!isValid)
                    {
                        Console.WriteLine("ERROR: Input was not a number.\n"); //Extra newline to make output cleaner.
                    }
                }
               
                
                switch (choice)
                { 
                    case 1:
                        ReadMovies(mediachoice);
                        break;
                    case 2:
                        AddMovie(mediachoice);
                        break;
                    case 3:
                        done = true;
                        Console.WriteLine("Exiting...\n");
                        break;
                    default:
                        if (!done) // it outputs this error if the user tries to quit when asked what media type to use, this line is a band-aid
                        {
                            Console.WriteLine("ERROR: Please choose one of the given options.\n");
                        }
                        break;
                }
            }
        }
        public static void ReadMovies(string media) //NOTE: Change method names later, it's no longer just movies
        {
            try
            {
                switch (media) {

                    case "movie":
                        StreamReader movieReader = new StreamReader(moviefile); //I know calling it "sr" is conventional, but it gives me an error later because the cases share a scope (and I don't have time or patience to fix it)
                        movieReader.ReadLine(); //Skipping the first line
                        var linecount = 0;
                        while (!movieReader.EndOfStream) //I wanted to get all the lines in the file, there are probably cleaner ways to do it but none I found worked.
                        {
                            movieReader.ReadLine();
                            linecount++;
                        }
                        movieReader.Close();
                        StreamReader movieReader2 = new StreamReader(moviefile);
                        var count = 0;
                        var goal = 1000;
                        var doneCounting = false;
                        movieReader2.ReadLine();
                        while (!movieReader2.EndOfStream && !doneCounting)
                        {
                            if (count < goal)
                            {

                                string[] splitLine = movieReader2.ReadLine().Split(',');
                                Media newMovie = new Movie(Int32.Parse(splitLine[0]), splitLine[1], splitLine[2]);
                                Console.WriteLine(newMovie.Display());
                                count++;
                            }
                            else
                            {
                                Console.WriteLine($"\nRead {goal} of {linecount} lines.");
                                Console.Write("Continue reading? (Y/N)\n>");
                                char response = Char.ToUpper(Console.ReadLine()[0]);
                                if (!response.Equals('Y'))
                                {
                                    doneCounting = true;
                                }
                                else
                                {
                                    if (goal + 1000 > linecount)
                                    {
                                        goal = linecount;
                                    }
                                    else
                                    {
                                        goal = goal + 1000;
                                    }
                                }
                            }
                            if (movieReader2.EndOfStream)
                            {
                                Console.WriteLine($"\nRead {linecount} of {linecount} lines.");
                            }
                        }
                        movieReader2.Close();
                        break;

                    case "show":
                        StreamReader showReader = new StreamReader(showfile);
                        showReader.ReadLine(); //Skipping the first line
                        linecount = 0;
                        while (!showReader.EndOfStream)
                        {
                            showReader.ReadLine();
                            linecount++;
                        }
                        showReader.Close();
                        StreamReader showReader2 = new StreamReader(showfile);
                        count = 0;
                        goal = 1000;
                        doneCounting = false;
                        showReader2.ReadLine();
                        while (!showReader2.EndOfStream && !doneCounting)
                        {
                            if (count < goal)
                            {
                                string[] splitLine = showReader2.ReadLine().Split(',');
                                //Console.WriteLine(string.Join("\n", splitLine)); (just for debugging)
                                Media newShow = new Show(Int32.Parse(splitLine[0]), splitLine[1], Int32.Parse(splitLine[2]), Int32.Parse(splitLine[3]), splitLine[4]);
                                Console.WriteLine(newShow.Display());
                                count++;
                            }
                            else
                            {
                                Console.WriteLine($"\nRead {goal} of {linecount} lines.");
                                Console.Write("Continue reading? (Y/N)\n>");
                                char response = Char.ToUpper(Console.ReadLine()[0]);
                                if (!response.Equals('Y'))
                                {
                                    doneCounting = true;
                                }
                                else
                                {
                                    if (goal + 1000 > linecount)
                                    {
                                        goal = linecount;
                                    }
                                    else
                                    {
                                        goal = goal + 1000;
                                    }
                                }
                            }
                            if (showReader2.EndOfStream)
                            {
                                Console.WriteLine($"\nRead {linecount} of {linecount} lines.");
                            }
                        }
                        showReader2.Close();
                        break;

                    case "video":
                        StreamReader vidReader = new StreamReader(videofile);
                        vidReader.ReadLine(); //Skipping the first line
                        linecount = 0;
                        while (!vidReader.EndOfStream)
                        {
                            vidReader.ReadLine();
                            linecount++;
                        }
                        vidReader.Close();
                        StreamReader vidReader2 = new StreamReader(videofile);
                        count = 0;
                        goal = 1000;
                        doneCounting = false;
                        vidReader2.ReadLine();
                        while (!vidReader2.EndOfStream && !doneCounting)
                        {
                            if (count < goal)
                            {
                                string[] splitLine = vidReader2.ReadLine().Split(',');
                                //Console.WriteLine(string.Join("\n", splitLine)); (just for debugging)
                                string[] regions = splitLine[4].Split('|');
                                int[] intRegions = Array.ConvertAll(regions, i => Int32.Parse(i));

                                Media newVideo = new Video(Int32.Parse(splitLine[0]), splitLine[1], splitLine[2], Int32.Parse(splitLine[3]), intRegions);
                                Console.WriteLine(newVideo.Display());
                                count++;
                            }
                            else
                            {
                                Console.WriteLine($"\nRead {goal} of {linecount} lines.");
                                Console.Write("Continue reading? (Y/N)\n>");
                                char response = Char.ToUpper(Console.ReadLine()[0]);
                                if (!response.Equals('Y'))
                                {
                                    doneCounting = true;
                                }
                                else
                                {
                                    if (goal + 1000 > linecount)
                                    {
                                        goal = linecount;
                                    }
                                    else
                                    {
                                        goal = goal + 1000;
                                    }
                                }
                            }
                            if (vidReader2.EndOfStream)
                            {
                                Console.WriteLine($"\nRead {linecount} of {linecount} lines.");
                            }
                        }
                        vidReader2.Close();
                        break;
                    default:
                        break;
                }
                
            }
            catch(IOException e)
            {
                Console.WriteLine("ERROR: File not found.\n");
            }
        }
        public static bool AddMovie(string media)
        {
            try
            {
                switch (media)
                {
                    case "movie":
                        String lastMovie = File.ReadLines(moviefile).Last(); //getting the last line in the movie list
                        String[] splitMovie = lastMovie.Split(',');
                        bool canParse = Int32.TryParse(splitMovie[0], out int result); //get the last movie's ID, since it's the highest
                        if (!canParse)
                        {
                            return false;
                        }
                        result++;
                        Console.Write("Please input the movie's name.\n>");
                        var name = Console.ReadLine();
                        StreamReader movieReader = new StreamReader(moviefile);
                        while (movieReader != null && !movieReader.EndOfStream)
                        {
                            var line = movieReader.ReadLine();
                            String[] splitline = line.Split(",");
                            if (splitline[1].Equals(name))
                            {
                                Console.WriteLine("ERROR: Movie with that name already exists.\n");
                                movieReader.Close();
                                movieReader = null;
                                return false;
                            }

                        }
                        movieReader.Close();
                        Console.Write("Input the movie's genre.\n>");
                        var genre = Console.ReadLine();
                        Media newMovie = new Movie(result, name, genre);
                        StreamWriter movieWriter = new StreamWriter(moviefile, true);
                        movieWriter.WriteLine(newMovie.Display());
                        movieWriter.Close();
                        return true;
                        break;

                    case "show":
                        String lastShow = File.ReadLines(showfile).Last(); 
                        String[] splitShow = lastShow.Split(',');
                        canParse = Int32.TryParse(splitShow[0], out result);
                        if (!canParse)
                        {
                            return false;
                        }
                        result++;
                        Console.Write("Please input the show's name.\n>");
                        var showName = Console.ReadLine();
                        StreamReader showReader = new StreamReader(showfile);
                        while (showReader != null && !showReader.EndOfStream)
                        {
                            var line = showReader.ReadLine();
                            String[] splitline = line.Split(",");
                            if (splitline[1].Equals(showName))
                            {
                                Console.WriteLine("ERROR: Show with that name already exists.\n");
                                showReader.Close();
                                showReader = null;
                                return false;
                            }

                        }
                        showReader.Close();
                        Console.Write("Input the show's season.\n>");
                        //var season = Console.ReadLine();
                        Int32.TryParse(Console.ReadLine(), out int season);
                        Console.Write("Input the show's last episode number.\n>");
                        //var episode = Console.ReadLine();
                        Int32.TryParse(Console.ReadLine(), out int episode);
                        Console.Write("Input the show's writers.\n>");
                        var writers = Console.ReadLine();
                        Media newShow = new Show(result, showName, season, episode, writers);
                        StreamWriter showWriter = new StreamWriter(showfile, true);
                        showWriter.WriteLine(newShow.Display());
                        showWriter.Close();
                        return true;
                        break;
                    case "video":
                        String lastVid = File.ReadLines(videofile).Last();
                        String[] splitVid = lastVid.Split(',');
                        canParse = Int32.TryParse(splitVid[0], out result);
                        if (!canParse)
                        {
                            return false;
                        }
                        result++;
                        Console.Write("Please input the video's name.\n>");
                        var vidName = Console.ReadLine();
                        StreamReader vidReader = new StreamReader(videofile);
                        while (vidReader != null && !vidReader.EndOfStream)
                        {
                            var line = vidReader.ReadLine();
                            String[] splitline = line.Split(",");
                            if (splitline[1].Equals(vidName))
                            {
                                Console.WriteLine("ERROR: Video with that name already exists.\n");
                                vidReader.Close();
                                vidReader = null;
                                return false;
                            }

                        }
                        vidReader.Close();
                        Console.Write("Input the video's format(s).\n>");
                        string format = Console.ReadLine();
                        Console.Write("Input the video's length.\n>");
                        Int32.TryParse(Console.ReadLine(), out int vidLength);
                        Console.Write("Input the video's regions.\n>");
                        string regionInput = Console.ReadLine();
                        string[] regions = regionInput.Split('|');
                        int[] intRegions = Array.ConvertAll(regions, i => Int32.Parse(i));
                        Media newVideo = new Video(result, vidName, format, vidLength, intRegions);
                        StreamWriter vidWriter = new StreamWriter(videofile, true);
                        vidWriter.WriteLine(newVideo.Display());
                        vidWriter.Close();
                        return true;
                        break;
                    default:
                        return false; //Shouldn't happen, but VS complained about it, and just in case it happens it's there
                }
                
            }
            catch(IOException e)
            {
                Console.WriteLine("ERROR: File not found.\n");
                return false;
            }
        }
    }
}
