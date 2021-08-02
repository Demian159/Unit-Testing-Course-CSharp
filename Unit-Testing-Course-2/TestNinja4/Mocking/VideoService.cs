using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace TestNinja4.Mocking
{
    public interface IVideoService
    {
        string GetUnprocessedVideosAsCsv();
        string ReadVideoTitle();
    }

    public class VideoService : IVideoService
    {
        private IFileReader _fileReader;
        private IVideoRepository _videoRepository;

        public VideoService(IFileReader fileReader = null, IVideoRepository videoRepository = null)
        {
            _fileReader = fileReader ?? new FileReader();
            _videoRepository = videoRepository ?? new VideoRepository();
        }

        public string ReadVideoTitle()
        {
            var str = _fileReader.Read("video.txt");
            var video = JsonConvert.DeserializeObject<Video>(str);
            if (video == null)
                return "Error parsing the video.";
            return video.Title;
        }

        public string GetUnprocessedVideosAsCsv()
        {
            var videoIds = new List<int>();
            var videos = _videoRepository.GetUnProcessedVideos();
            foreach (var v in videos)
                videoIds.Add(v.Id);

            return String.Join(",", videoIds);
        }
    }

    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsProcessed { get; set; }
    }

    public class VideoContext : DbContext
    {
        public DbSet<Video> Videos { get; set; }
    }
}