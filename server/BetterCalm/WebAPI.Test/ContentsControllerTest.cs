using BLInterfaces;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using System;
using System.Collections.Generic;
using WebAPI.Controllers;
using System.Linq;
using Domain.Exceptions;

namespace WebAPI.Test
{
    [TestClass]
	public class ContentsControllerTest
	{
		[TestMethod]
		public void GetContentsOk()
		{
            List<Content> expectedContents = GetExpectedContents();

            Mock<IContentLogic> mock = new Mock<IContentLogic>(MockBehavior.Strict);
            mock.Setup(m => m.GetContents()).Returns(expectedContents);
            ContentsController controller = new ContentsController(mock.Object);

            IActionResult result = controller.Get();
            OkObjectResult objectResult = result as OkObjectResult;
            IEnumerable<ContentBasicInfo> obtainedContents = objectResult.Value as IEnumerable<ContentBasicInfo>;

            mock.VerifyAll();
            CollectionAssert.AreEqual(expectedContents.
                Select(content => new ContentBasicInfo(content)).
                ToList(),
                obtainedContents.ToList(),
                new ContentBasicInfoComparer());

		}

        private static List<Content> GetExpectedContents()
        {
            return new List<Content>()
            {
                new Content()
                {
                    ArtistName = "Bon Jovi",
                    Categories = new List<Category>(){
                        new Category()
                        {
                            Id = 1,
                            Name = "Rock"
                        }
                    },
                    Id = 1,
                    ContentLength = new TimeSpan(0, 2, 30),
                    Name = "It's My Life",
                    ImageUrl = "http://www.images.com/image.jpg",
                    AudioUrl = "http://www.audios.com/audio.mp3"
                },
                new Content()
                {
                    ArtistName = "Celia Cruz",
                    Categories = new List<Category>(){
                        new Category()
                        {
                            Id = 2,
                            Name = "Tropical"
                        }
                    },
                    Id = 2,
                    ContentLength = new TimeSpan(0, 2, 30),
                    Name = "La vida es un carnaval",
                    ImageUrl = "http://www.images.com/image2.jpg",
                    AudioUrl = "http://www.audios.com/audio.mp3"
                }
            };
        }

        [TestMethod]
        public void GetContentOk()
        {
            Content expectedContent = new Content()
            {
                ArtistName = "Bon Jovi",
                Categories = new List<Category>(){
                        new Category()
                        {
                            Id = 1,
                            Name = "Rock"
                        }
                    },
                Id = 1,
                ContentLength = new TimeSpan(0, 2, 30),
                Name = "It's My Life",
                ImageUrl = "http://www.images.com/image.jpg",
                AudioUrl = "http://www.audios.com/audio.mp3"
            };

            Mock<IContentLogic> mock = new Mock<IContentLogic>(MockBehavior.Strict);
            mock.Setup(m => m.GetContent(expectedContent.Id)).Returns(expectedContent);
            ContentsController controller = new ContentsController(mock.Object);

            IActionResult result = controller.Get(expectedContent.Id);
            OkObjectResult objectResult = result as OkObjectResult;
            ContentBasicInfo obtainedContent = objectResult.Value as ContentBasicInfo;

            mock.VerifyAll();
            Assert.IsTrue((new ContentBasicInfoComparer()).
               Compare(new ContentBasicInfo(expectedContent), obtainedContent) == 0);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void GetContentNotFound()
        {
            int toGetContentId = 1;

            Mock<IContentLogic> mock = new Mock<IContentLogic>(MockBehavior.Strict);
            mock.Setup(m => m.GetContent(toGetContentId)).Throws(new NotFoundException(toGetContentId.ToString()));
            ContentsController controller = new ContentsController(mock.Object);

            IActionResult result = controller.Get(toGetContentId);
            OkObjectResult objectResult = result as OkObjectResult;
            ContentBasicInfo obtainedContent = objectResult.Value as ContentBasicInfo;

            mock.VerifyAll();
            Assert.IsNull(obtainedContent);
        }

        [TestMethod]
        public void PostContentOk()
        {
            ContentModel contentModel = new ContentModel()
            {
                ArtistName = "Bon Jovi",
                Categories = new int[] { 1 },
                Playlists = new List<PlaylistBasicInfo>()
                {
                    new PlaylistBasicInfo()
                    {
                        Id = 1,
                        Name = "Best of Bon Jovi"
                    }
                },
                Id = 1,
                ContentLength = "00:01:30",
                Name = "It's My Life",
                ImageUrl = "http://www.images.com/image.jpg",
                AudioUrl = "http://www.audios.com/audio.mp3"
            };
            Content contentEntity = contentModel.ToEntity();

            Mock<IContentLogic> contentLogic = new Mock<IContentLogic>(MockBehavior.Strict);
            contentLogic.Setup(m => m.CreateContent(It.IsAny<Content>()));
            contentLogic.Setup(m => m.GetContent(contentEntity.Id)).Returns(contentEntity);

            ContentsController controller = new ContentsController(contentLogic.Object);

            controller.Post(contentModel);
            IActionResult result = controller.Get(contentEntity.Id);
            OkObjectResult objectResult = result as OkObjectResult;
            ContentBasicInfo obtainedContent = objectResult.Value as ContentBasicInfo;
            Assert.IsTrue(obtainedContent.Id == contentEntity.Id);
        }

        [TestMethod]
        public void PostContentWithoutPlaylistOk()
        {
            ContentModel contentModel = new ContentModel()
            {
                ArtistName = "Bon Jovi",
                Categories = new int[] { 1 },
                Id = 1,
                ContentLength = "00:01:30",
                Name = "It's My Life",
                ImageUrl = "http://www.images.com/image.jpg",
                AudioUrl = "http://www.audios.com/audio.mp3"
            };
            Content contentEntity = contentModel.ToEntity();

            Mock<IContentLogic> contentLogic = new Mock<IContentLogic>(MockBehavior.Strict);
            contentLogic.Setup(m => m.CreateContent(It.IsAny<Content>()));
            contentLogic.Setup(m => m.GetContent(contentEntity.Id)).Returns(contentEntity);

            ContentsController controller = new ContentsController(contentLogic.Object);

            controller.Post(contentModel);
            IActionResult result = controller.Get(contentEntity.Id);
            OkObjectResult objectResult = result as OkObjectResult;
            ContentBasicInfo obtainedContent = objectResult.Value as ContentBasicInfo;
            Assert.IsTrue(obtainedContent.Id == contentEntity.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void PostContentCategoryNotFound()
        {
            ContentModel contentModel = new ContentModel()
            {
                ArtistName = "Bon Jovi",
                Categories = new int[] { 9999 },
                Playlists = new List<PlaylistBasicInfo>()
                {
                    new PlaylistBasicInfo()
                    {
                        Id = 1,
                        Name = "Best of Bon Jovi"
                    }
                },
                Id = 1,
                ContentLength = "00:01:30",
                Name = "It's My Life",
                ImageUrl = "http://www.images.com/image.jpg",
                AudioUrl = "http://www.audios.com/audio.mp3"
            };
            Content contentEntity = contentModel.ToEntity();

            Mock<IContentLogic> contentLogic = new Mock<IContentLogic>(MockBehavior.Strict);
            contentLogic.Setup(m => m.CreateContent(It.IsAny<Content>())).Throws(new NotFoundException(contentEntity.Id.ToString()));
            contentLogic.Setup(m => m.GetContent(contentEntity.Id)).Throws(new NotFoundException(contentEntity.Id.ToString()));

            ContentsController controller = new ContentsController(contentLogic.Object);

            controller.Post(contentModel);
            IActionResult result = controller.Get(contentEntity.Id);
            OkObjectResult objectResult = result as OkObjectResult;
            ContentBasicInfo obtainedContent = objectResult.Value as ContentBasicInfo;
            Assert.IsNull(obtainedContent);
        }

        [TestMethod]
        [ExpectedException(typeof(UnableToCreatePlaylistException))]
        public void PostContentUnableToCreateNewPlaylist()
        {
            ContentModel contentModel = new ContentModel()
            {
                ArtistName = "Bon Jovi",
                Categories = new int[] { 9999 },
                Playlists = new List<PlaylistBasicInfo>()
                {
                    new PlaylistBasicInfo()
                    {
                        Id = 1
                    }
                },
                Id = 1,
                ContentLength = "00:01:30",
                Name = "It's My Life",
                ImageUrl = "http://www.images.com/image.jpg",
                AudioUrl = "http://www.audios.com/audio.mp3"
            };
            Content contentEntity = contentModel.ToEntity();

            Mock<IContentLogic> contentLogic = new Mock<IContentLogic>(MockBehavior.Strict);
            contentLogic.Setup(m => m.CreateContent(It.IsAny<Content>())).Throws(new UnableToCreatePlaylistException());
            contentLogic.Setup(m => m.GetContent(contentEntity.Id)).Throws(new NotFoundException(contentEntity.Id.ToString()));

            ContentsController controller = new ContentsController(contentLogic.Object);

            controller.Post(contentModel);
            IActionResult result = controller.Get(contentEntity.Id);
            OkObjectResult objectResult = result as OkObjectResult;
            ContentBasicInfo obtainedContent = objectResult.Value as ContentBasicInfo;
            Assert.IsNull(obtainedContent);
        }

        [TestMethod]
        [ExpectedException(typeof(MissingCategoriesException))]
        public void PostContentMissingCategories()
        {
            ContentModel contentModel = new ContentModel()
            {
                ArtistName = "Bon Jovi",
                Playlists = new List<PlaylistBasicInfo>()
                {
                    new PlaylistBasicInfo()
                    {
                        Id = 1
                    }
                },
                Id = 1,
                ContentLength = "00:01:30",
                Name = "It's My Life",
                ImageUrl = "http://www.images.com/image.jpg",
                AudioUrl = "http://www.audios.com/audio.mp3"
            };
            Content contentEntity = contentModel.ToEntity();

            Mock<IContentLogic> contentLogic = new Mock<IContentLogic>(MockBehavior.Strict);
            contentLogic.Setup(m => m.CreateContent(It.IsAny<Content>())).Throws(new MissingCategoriesException());
            contentLogic.Setup(m => m.GetContent(contentEntity.Id)).Throws(new NotFoundException(contentEntity.Id.ToString()));

            ContentsController controller = new ContentsController(contentLogic.Object);

            controller.Post(contentModel);
            IActionResult result = controller.Get(contentEntity.Id);
            OkObjectResult objectResult = result as OkObjectResult;
            ContentBasicInfo obtainedContent = objectResult.Value as ContentBasicInfo;
            Assert.IsNull(obtainedContent);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void DeleteContentOk()
        {
            int toDeleteContent = 1;

            Mock<IContentLogic> mock = new Mock<IContentLogic>(MockBehavior.Strict);
            mock.Setup(m => m.DeleteContent(toDeleteContent));
            mock.Setup(m => m.GetContent(toDeleteContent)).Throws(new NotFoundException(toDeleteContent.ToString()));
            ContentsController controller = new ContentsController(mock.Object);

            IActionResult result = controller.Delete(toDeleteContent);
            IActionResult getResult = controller.Get(toDeleteContent);
            OkObjectResult objectResult = getResult as OkObjectResult;
            ContentBasicInfo obtainedContent = objectResult.Value as ContentBasicInfo;

            mock.VerifyAll();
            Assert.IsNull(obtainedContent);
        }
    }
}
