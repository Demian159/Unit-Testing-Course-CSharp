﻿using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using TestNinja.UnitTests;
using TestNinja4.Mocking;

namespace TestNinja4.UnitTests.Mocking
{
    [TestFixture]
    class VideoServiceTests
    {
        private Mock<IFileReader> _fileReader;
        private Mock<IVideoRepository> _repository;
        private VideoService _videoService;

        [SetUp]
        public void SetUp()
        {
            _fileReader = new Mock<IFileReader>();
            _repository = new Mock<IVideoRepository>(); ;
            _videoService = new VideoService(_fileReader.Object, _repository.Object);

        }
        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
            _fileReader.Setup(fr => fr.Read("video.txt")).Returns("");

            var result = _videoService.ReadVideoTitle();

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }
        [Test]
        public void GetUnprocessedVideosCsv_AllVideosAreProcessed_ReturnAnEmptyString()
        {
            _repository.Setup(r => r.GetUnProcessedVideos()).Returns(new List<Video>());

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo(""));
        }
        [Test]
        public void GetUnprocessedVideosCsv_AFewUnProcessedVideos_ReturnsAStringWithIdOfUnprocessedVideos()
        {
            _repository.Setup(r => r.GetUnProcessedVideos()).Returns(new List<Video>
            { 
                new Video { Id = 1 },
                new Video { Id = 2 },
                new Video { Id = 3 }
            });

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo("1,2,3"));
        }
    }
}