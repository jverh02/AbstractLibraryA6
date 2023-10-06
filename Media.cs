using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractLibraryA6
{
    public abstract class Media
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public Media()
        {
            
        }

        public abstract string Display();
    }
    public class Movie : Media
    {
        public string Genres { get; set; }
        public override string Display()
        {
            return Id + "," + Title + "," + Genres;
        }

        public Movie(int Id, string Title,string Genres)
        {
            this.Id = Id;
            this.Title = Title;
            this.Genres = Genres;
        }

    }

    public class Show : Media
    {
        public int Season { get; set; }
        public int Episode { get; set; }
        public string Writers { get; set; }
        public Show(int Id, string Title, int Season, int Episode, string Writers)
        {
            this.Id = Id;
            this.Title = Title;
            this.Season = Season;
            this.Episode = Episode;
            this.Writers = Writers;
        }
        public override string Display()
        {
            return Id + "," + Title + "," + Season + "," + Episode + "," + Writers;
        }
    }
    public class Video : Media
    {
        public string Format { get; set; }
        public int Length { get; set; }
        int[] Regions { get; set; }
        public Video(int Id, string Title, string Format, int Length, int[] Regions)
        {

            this.Id = Id;
            this.Title = Title;
            this.Format = Format;
            this.Length = Length;
            this.Regions = Regions;
        }
        public override string Display()
        {
            string regionsOutput = string.Join("|", Regions); // outputting an array isn't clean, need this extra bit of work for readable output
            return Id + "," + Title + "," + Format + "," + Length + "," + regionsOutput;
        }
    }
}
