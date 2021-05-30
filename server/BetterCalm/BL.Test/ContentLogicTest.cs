using DataAccessInterfaces;
using Domain;
using Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL.Test
{
	[TestClass]
	public class ContentLogicTest
	{
        [TestMethod]
        public void GetAll_ContentsExist_ContentsFetched()
		{
            List<Content> expectedContents = GetExpectedContents();
            Mock<IContentRepository> contentRepositoryMock = new Mock<IContentRepository>(MockBehavior.Strict);
            contentRepositoryMock.Setup(m => m.GetAll()).Returns(expectedContents);

			Mock<IPlaylistRepository> playlistRepository = new Mock<IPlaylistRepository>(MockBehavior.Strict);
			Mock<ICategoryRepository> categoryRepository = new Mock<ICategoryRepository>(MockBehavior.Strict);
            ContentLogic contentLogic = new ContentLogic(contentRepositoryMock.Object, playlistRepository.Object, categoryRepository.Object);

            IEnumerable<Content> obtainedContents = contentLogic.GetContents();

            contentRepositoryMock.VerifyAll();
            Assert.IsTrue(obtainedContents.SequenceEqual(expectedContents));
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
					ContentUrl = "http://www.audios.com/audio.mp3"
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
					ContentUrl = "http://www.audios.com/audio.mp3"
				}
            };
        }

		[TestMethod]
		public void GetAllByPlaylist_PlaylistFound_ContentsForPlaylistFetched()
		{
			List<Content> expectedContents = GetExpectedPlaylistContents();
			Playlist expectedPlaylist = expectedContents.First().PlayLists.First();
			Mock<IContentRepository> contentRepositoryMock = new Mock<IContentRepository>(MockBehavior.Strict);
			contentRepositoryMock.Setup(m => m.GetAll(expectedPlaylist)).Returns(expectedContents);

			Mock<IPlaylistRepository> playlistRepository = new Mock<IPlaylistRepository>(MockBehavior.Strict);
			Mock<ICategoryRepository> categoryRepository = new Mock<ICategoryRepository>(MockBehavior.Strict);
			ContentLogic contentLogic = new ContentLogic(contentRepositoryMock.Object, playlistRepository.Object, categoryRepository.Object);

			IEnumerable<Content> obtainedContents = contentLogic.GetContents(expectedPlaylist);
			Assert.IsTrue(expectedContents.SequenceEqual(obtainedContents));
		}

		private static List<Content> GetExpectedPlaylistContents()
		{
			Category rock = new Category()
			{
				Id = 1,
				Name = "Rock"
			};

			Playlist bestOfBonJovi = new Playlist() { };

			Content itsMyLife = new Content()
			{
				ArtistName = "Bon Jovi",
				Categories = new List<Category>() { },
				PlayLists = new List<Playlist>() { bestOfBonJovi },
				Id = 1,
				ContentLength = new TimeSpan(0, 2, 30),
				Name = "It's My Life",
				ImageUrl = "http://www.images.com/image.jpg",
				ContentUrl = "http://www.audios.com/audio.mp3"
			};

			Content livinOnAPrayer = new Content()
			{
				ArtistName = "Bon Jovi",
				Categories = new List<Category>() { },
				PlayLists = new List<Playlist>() { bestOfBonJovi },
				Id = 2,
				ContentLength = new TimeSpan(0, 4, 10),
				Name = "Livin' On A Prayer",
				ImageUrl = "http://www.images.com/image.jpg",
				ContentUrl = "http://www.audios.com/audio.mp3"
			};

			bestOfBonJovi = new Playlist()
			{
				Id = 1,
				Name = "Best of Bon Jovi",
				Description = "The Best songs by Bon Jovi",
				ImageUrl = "http://www.images.com/image.jpg",
				Categories = new List<Category>() { rock },
				Contents = new List<Content>() { itsMyLife, livinOnAPrayer }
			};

			List<Content> expectedContent = new List<Content>();
			foreach (Content song in bestOfBonJovi.Contents)
			{
				expectedContent.Add(song);
			}

			return expectedContent;
		}

		[TestMethod]
		public void GetAllByCategory_CategoryExists_ContentsForCategoryFetched()
		{
			List<Content> expectedContents = GetExpectedCategoryContents();
			Category expectedCategory = expectedContents.First().Categories.First();
			Mock<IContentRepository> contentRepositoryMock = new Mock<IContentRepository>(MockBehavior.Strict);
			contentRepositoryMock.Setup(m => m.GetAll(expectedCategory)).Returns(expectedContents);

			Mock<IPlaylistRepository> playlistRepository = new Mock<IPlaylistRepository>(MockBehavior.Strict);
			Mock<ICategoryRepository> categoryRepository = new Mock<ICategoryRepository>(MockBehavior.Strict);
			ContentLogic contentLogic = new ContentLogic(contentRepositoryMock.Object, playlistRepository.Object, categoryRepository.Object);

			IEnumerable<Content> obtainedContents = contentLogic.GetContents(expectedCategory);
			Assert.IsTrue(expectedContents.SequenceEqual(obtainedContents));
		}

		private static List<Content> GetExpectedCategoryContents()
		{
			Category rock = new Category()
			{
				Id = 1,
				Name = "Rock"
			};

			Content itsMyLife = new Content()
			{
				ArtistName = "Bon Jovi",
				Categories = new List<Category>() { rock },
				PlayLists = new List<Playlist>() { },
				Id = 1,
				ContentLength = new TimeSpan(0, 2, 30),
				Name = "It's My Life",
				ImageUrl = "http://www.images.com/image.jpg"
			};

			Content livinOnAPrayer = new Content()
			{
				ArtistName = "Bon Jovi",
				Categories = new List<Category>() { rock },
				PlayLists = new List<Playlist>() { },
				Id = 2,
				ContentLength = new TimeSpan(0, 4, 10),
				Name = "Livin' On A Prayer",
				ImageUrl = "http://www.images.com/image.jpg"
			};

			rock = new Category()
			{
				Id = 1,
				Name = "Rock",
				Contents = new List<Content>() { itsMyLife, livinOnAPrayer }
			};

			List<Content> expectedContent = new List<Content>();
			foreach (Content song in rock.Contents)
			{
				expectedContent.Add(song);
			}
			return expectedContent;
		}

		[TestMethod]
		public void Get_ContentExists_ContentFetched()
		{
			Content expectedContent = new Content()
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
				ContentUrl = "http://www.audios.com/audio.mp3"
			};

			Mock<IContentRepository> contentRepositoryMock = new Mock<IContentRepository>(MockBehavior.Strict);
			contentRepositoryMock.Setup(m => m.Get(expectedContent.Id)).Returns(expectedContent);

			Mock<IPlaylistRepository> playlistRepository = new Mock<IPlaylistRepository>(MockBehavior.Strict);
			Mock<ICategoryRepository> categoryRepository = new Mock<ICategoryRepository>(MockBehavior.Strict);
			ContentLogic contentLogic = new ContentLogic(contentRepositoryMock.Object, playlistRepository.Object, categoryRepository.Object);

			Content obtainedContent = contentLogic.GetContent(expectedContent.Id);

			contentRepositoryMock.VerifyAll();
			Assert.AreEqual(expectedContent, obtainedContent);
		}

		[TestMethod]
		[ExpectedException(typeof(NotFoundException))]
		public void Get_ContentNotFound_ExceptionThrown()
		{
			int testContentId = 1;

			Mock<IContentRepository> contentRepositoryMock = new Mock<IContentRepository>(MockBehavior.Strict);
			contentRepositoryMock.Setup(m => m.Get(testContentId)).Throws(new NotFoundException(testContentId.ToString()));

			Mock<IPlaylistRepository> playlistRepository = new Mock<IPlaylistRepository>(MockBehavior.Strict);
			Mock<ICategoryRepository> categoryRepository = new Mock<ICategoryRepository>(MockBehavior.Strict);
			ContentLogic contentLogic = new ContentLogic(contentRepositoryMock.Object, playlistRepository.Object, categoryRepository.Object);

			Content obtainedContent = contentLogic.GetContent(testContentId);

			contentRepositoryMock.VerifyAll();
			Assert.IsNull(obtainedContent);
		}

		[TestMethod]
		public void CreateContent_NewPlaylist_ContentAndPlaylistCreated()
		{
			Category music = new Category()
			{
				Id = 1,
				Name = "Musica"
			};

			Playlist playlist = new Playlist()
			{
				Id = 1,
				Name = "Besto of Bon Jovi"
			};

			Content toSaveContent = new Content()
			{
				ArtistName = "Bon Jovi",
				Categories = new List<Category>(){
						music
					},
				PlayLists = new List<Playlist>()
				{
					playlist
				},
				Id = 1,
				ContentLength = new TimeSpan(0, 2, 30),
				Name = "It's My Life",
				ImageUrl = "http://www.images.com/image.jpg",
				ContentUrl = "http://www.audios.com/audio.mp3"
			};

			Mock<IContentRepository> contentRepositoryMock = new Mock<IContentRepository>(MockBehavior.Strict);
			contentRepositoryMock.Setup(m => m.Add(toSaveContent));
			contentRepositoryMock.Setup(m => m.Get(toSaveContent.Id)).Returns(toSaveContent);

			Mock<IPlaylistRepository> playlistRepository = new Mock<IPlaylistRepository>(MockBehavior.Strict);
			playlistRepository.Setup(m => m.Get(playlist.Id)).Throws(new NotFoundException(playlist.Id.ToString()));

			Mock<ICategoryRepository> categoryRepository = new Mock<ICategoryRepository>(MockBehavior.Strict);
			categoryRepository.Setup(m => m.Get(music.Id)).Returns(music);

			ContentLogic contentLogic = new ContentLogic(contentRepositoryMock.Object, playlistRepository.Object, categoryRepository.Object);

			contentLogic.CreateContent(toSaveContent);
			Content obtainedContent = contentLogic.GetContent(toSaveContent.Id);

			contentRepositoryMock.VerifyAll();
			Assert.AreEqual(toSaveContent, obtainedContent);
		}

		[TestMethod]
		public void CreateContent_ExistingPlaylist_ContentCreated()
		{
			Category music = new Category()
			{
				Id = 1,
				Name = "Musica"
			};

			Playlist playlist = new Playlist()
			{
				Id = 1,
				Name = "Besto of Bon Jovi"
			};

			Content toSaveContent = new Content()
			{
				ArtistName = "Bon Jovi",
				Categories = new List<Category>(){
						music
					},
				PlayLists = new List<Playlist>()
				{
					playlist
				},
				Id = 1,
				ContentLength = new TimeSpan(0, 2, 30),
				Name = "It's My Life",
				ImageUrl = "http://www.images.com/image.jpg",
				ContentUrl = "http://www.audios.com/audio.mp3"
			};

			Mock<IContentRepository> contentRepositoryMock = new Mock<IContentRepository>(MockBehavior.Strict);
			contentRepositoryMock.Setup(m => m.Add(toSaveContent));
			contentRepositoryMock.Setup(m => m.Get(toSaveContent.Id)).Returns(toSaveContent);

			Mock<IPlaylistRepository> playlistRepository = new Mock<IPlaylistRepository>(MockBehavior.Strict);
			playlistRepository.Setup(m => m.Get(playlist.Id)).Returns(playlist);

			Mock<ICategoryRepository> categoryRepository = new Mock<ICategoryRepository>(MockBehavior.Strict);
			categoryRepository.Setup(m => m.Get(music.Id)).Returns(music);

			ContentLogic contentLogic = new ContentLogic(contentRepositoryMock.Object, playlistRepository.Object, categoryRepository.Object);

			contentLogic.CreateContent(toSaveContent);
			Content obtainedContent = contentLogic.GetContent(toSaveContent.Id);

			contentRepositoryMock.VerifyAll();
			Assert.AreEqual(toSaveContent, obtainedContent);
		}

		[TestMethod]
		public void CreateContent_WithoutPlaylist_ContentCreated()
		{
			Category music = new Category()
			{
				Id = 1,
				Name = "Musica"
			};

			Content toSaveContent = new Content()
			{
				ArtistName = "Bon Jovi",
				Categories = new List<Category>(){
						music
					},
				Id = 1,
				ContentLength = new TimeSpan(0, 2, 30),
				Name = "It's My Life",
				ImageUrl = "http://www.images.com/image.jpg",
				ContentUrl = "http://www.audios.com/audio.mp3"
			};

			Mock<IContentRepository> contentRepositoryMock = new Mock<IContentRepository>(MockBehavior.Strict);
			contentRepositoryMock.Setup(m => m.Add(toSaveContent));
			contentRepositoryMock.Setup(m => m.Get(toSaveContent.Id)).Returns(toSaveContent);

			Mock<IPlaylistRepository> playlistRepository = new Mock<IPlaylistRepository>(MockBehavior.Strict);

			Mock<ICategoryRepository> categoryRepository = new Mock<ICategoryRepository>(MockBehavior.Strict);
			categoryRepository.Setup(m => m.Get(music.Id)).Returns(music);

			ContentLogic contentLogic = new ContentLogic(contentRepositoryMock.Object, playlistRepository.Object, categoryRepository.Object);

			contentLogic.CreateContent(toSaveContent);
			Content obtainedContent = contentLogic.GetContent(toSaveContent.Id);

			contentRepositoryMock.VerifyAll();
			Assert.AreEqual(toSaveContent, obtainedContent);
		}

		[TestMethod]
		[ExpectedException(typeof(NotFoundException))]
		public void CreateContent_InvalidCategory_ExceptionThrown()
		{
			Category music = new Category()
			{
				Id = 1,
				Name = "Musica"
			};

			Playlist playlist = new Playlist()
			{
				Id = 1,
				Name = "Besto of Bon Jovi"
			};

			Content toSaveContent = new Content()
			{
				ArtistName = "Bon Jovi",
				Categories = new List<Category>(){
						music
					},
				PlayLists = new List<Playlist>()
				{
					playlist
				},
				Id = 1,
				ContentLength = new TimeSpan(0, 2, 30),
				Name = "It's My Life",
				ImageUrl = "http://www.images.com/image.jpg",
				ContentUrl = "http://www.audios.com/audio.mp3"
			};

			Mock<IContentRepository> contentRepositoryMock = new Mock<IContentRepository>(MockBehavior.Strict);
			contentRepositoryMock.Setup(m => m.Add(toSaveContent)).Throws(new NotFoundException(music.Id.ToString()));
			contentRepositoryMock.Setup(m => m.Get(toSaveContent.Id)).Returns(toSaveContent);

			Mock<IPlaylistRepository> playlistRepository = new Mock<IPlaylistRepository>(MockBehavior.Strict);
			playlistRepository.Setup(m => m.Get(playlist.Id)).Returns(playlist);

			Mock<ICategoryRepository> categoryRepository = new Mock<ICategoryRepository>(MockBehavior.Strict);
			Category notFound = null;
			categoryRepository.Setup(m => m.Get(music.Id)).Returns(notFound);

			ContentLogic contentLogic = new ContentLogic(contentRepositoryMock.Object, playlistRepository.Object, categoryRepository.Object);

			contentLogic.CreateContent(toSaveContent);
			Content obtainedContent = contentLogic.GetContent(toSaveContent.Id);

			contentRepositoryMock.VerifyAll();
			Assert.IsNull(obtainedContent);
		}

		[TestMethod]
		[ExpectedException(typeof(UnableToCreatePlaylistException))]
		public void CreateContent_NewPlaylistMissingData_ExceptionThrown()
		{
			Category music = new Category()
			{
				Id = 1,
				Name = "Musica"
			};

			Playlist playlist = new Playlist()
			{
				Id = 1
			};

			Content toSaveContent = new Content()
			{
				ArtistName = "Bon Jovi",
				Categories = new List<Category>(){
						music
					},
				PlayLists = new List<Playlist>()
				{
					playlist
				},
				Id = 1,
				ContentLength = new TimeSpan(0, 2, 30),
				Name = "It's My Life",
				ImageUrl = "http://www.images.com/image.jpg",
				ContentUrl = "http://www.audios.com/audio.mp3"
			};

			Mock<IContentRepository> contentRepositoryMock = new Mock<IContentRepository>(MockBehavior.Strict);
			contentRepositoryMock.Setup(m => m.Add(toSaveContent));
			contentRepositoryMock.Setup(m => m.Get(toSaveContent.Id)).Returns(toSaveContent);

			Mock<IPlaylistRepository> playlistRepository = new Mock<IPlaylistRepository>(MockBehavior.Strict);
			Playlist notFound = null;
			playlistRepository.Setup(m => m.Get(playlist.Id)).Returns(notFound);

			Mock<ICategoryRepository> categoryRepository = new Mock<ICategoryRepository>(MockBehavior.Strict);
			categoryRepository.Setup(m => m.Get(music.Id)).Returns(music);

			ContentLogic contentLogic = new ContentLogic(contentRepositoryMock.Object, playlistRepository.Object, categoryRepository.Object);

			contentLogic.CreateContent(toSaveContent);
			Content obtainedContent = contentLogic.GetContent(toSaveContent.Id);

			contentRepositoryMock.VerifyAll();
			Assert.IsNull(obtainedContent);
		}

		[TestMethod]
		[ExpectedException(typeof(MissingCategoriesException))]
		public void CreateContent_NoCategoriesEntered_ExceptionThrown()
		{
			Category music = new Category()
			{
				Id = 1
			};

			Playlist playlist = new Playlist()
			{
				Id = 1,
				Name = "Besto of Bon Jovi"
			};

			Content toSaveContent = new Content()
			{
				ArtistName = "Bon Jovi",
				PlayLists = new List<Playlist>()
				{
					playlist
				},
				Id = 1,
				ContentLength = new TimeSpan(0, 2, 30),
				Name = "It's My Life",
				ImageUrl = "http://www.images.com/image.jpg",
				ContentUrl = "http://www.audios.com/audio.mp3"
			};

			Mock<IContentRepository> contentRepositoryMock = new Mock<IContentRepository>(MockBehavior.Strict);
			contentRepositoryMock.Setup(m => m.Add(toSaveContent));
			contentRepositoryMock.Setup(m => m.Get(toSaveContent.Id)).Returns(toSaveContent);

			Mock<IPlaylistRepository> playlistRepository = new Mock<IPlaylistRepository>(MockBehavior.Strict);
			playlistRepository.Setup(m => m.Get(playlist.Id)).Returns(playlist);

			Mock<ICategoryRepository> categoryRepository = new Mock<ICategoryRepository>(MockBehavior.Strict);
			categoryRepository.Setup(m => m.Get(music.Id)).Returns(music);

			ContentLogic contentLogic = new ContentLogic(contentRepositoryMock.Object, playlistRepository.Object, categoryRepository.Object);

			contentLogic.CreateContent(toSaveContent);
			Content obtainedContent = contentLogic.GetContent(toSaveContent.Id);

			contentRepositoryMock.VerifyAll();
			Assert.IsNull(obtainedContent);
		}

		[TestMethod]
		[ExpectedException(typeof(NotFoundException))]
		public void Delete_ContentExists_Deleted()
		{
			Category music = new Category()
			{
				Id = 1,
				Name = "Musica"
			};

			Content toDeleteContent = new Content()
			{
				ArtistName = "Bon Jovi",
				Categories = new List<Category>(){
						music
					},
				Id = 1,
				ContentLength = new TimeSpan(0, 2, 30),
				Name = "It's My Life",
				ImageUrl = "http://www.images.com/image.jpg",
				ContentUrl = "http://www.audios.com/audio.mp3"
			};

			Mock<IContentRepository> contentRepositoryMock = new Mock<IContentRepository>(MockBehavior.Strict);
			contentRepositoryMock.Setup(m => m.Get(toDeleteContent.Id)).Throws(new NotFoundException(toDeleteContent.Id.ToString()));
			contentRepositoryMock.Setup(m => m.Delete(toDeleteContent.Id));

			Mock<IPlaylistRepository> playlistRepository = new Mock<IPlaylistRepository>(MockBehavior.Strict);
			Mock<ICategoryRepository> categoryRepository = new Mock<ICategoryRepository>(MockBehavior.Strict);
			ContentLogic contentLogic = new ContentLogic(contentRepositoryMock.Object, playlistRepository.Object, categoryRepository.Object);

			contentLogic.DeleteContent(toDeleteContent.Id);
			Content obtainedContent = contentLogic.GetContent(toDeleteContent.Id);

			contentRepositoryMock.VerifyAll();
			Assert.AreNotEqual(toDeleteContent, obtainedContent);
		}

		[TestMethod]
		public void UpdateContent_NewPlaylist_ContentUpdated()
		{
			Category music = new Category()
			{
				Id = 1,
				Name = "Musica"
			};

			Playlist playlist = new Playlist()
			{
				Id = 1,
				Name = "Besto of Bon Jovi"
			};

			Content toSaveContent = new Content()
			{
				ArtistName = "Bon Jovi",
				Categories = new List<Category>(){
						music
					},
				PlayLists = new List<Playlist>()
				{
					playlist
				},
				Id = 1,
				ContentLength = new TimeSpan(0, 2, 30),
				Name = "It's My Life",
				ImageUrl = "http://www.images.com/image.jpg",
				ContentUrl = "http://www.audios.com/audio.mp3"
			};

			Playlist newPlaylist = new Playlist()
			{
				Id = 2,
				Name = "Playlist2"
			};

			Mock<IContentRepository> contentRepositoryMock = new Mock<IContentRepository>(MockBehavior.Strict);
			contentRepositoryMock.Setup(m => m.Add(toSaveContent));

			contentRepositoryMock.Setup(m => m.Update(toSaveContent));
			contentRepositoryMock.Setup(m => m.Get(toSaveContent.Id)).Returns(toSaveContent);

			Mock<IPlaylistRepository> playlistRepository = new Mock<IPlaylistRepository>(MockBehavior.Strict);
			playlistRepository.Setup(m => m.Get(playlist.Id)).Returns(playlist);
			playlistRepository.Setup(m => m.Get(newPlaylist.Id)).Throws(new NotFoundException(playlist.Id.ToString()));
			
			Mock<ICategoryRepository> categoryRepository = new Mock<ICategoryRepository>(MockBehavior.Strict);
			categoryRepository.Setup(m => m.Get(music.Id)).Returns(music);

			ContentLogic contentLogic = new ContentLogic(contentRepositoryMock.Object, playlistRepository.Object, categoryRepository.Object);

			contentLogic.CreateContent(toSaveContent);
			toSaveContent.PlayLists = new List<Playlist>(){
				playlist,
				newPlaylist
			};
			contentLogic.UpdateContent(toSaveContent);
			
			Content obtainedContent = contentLogic.GetContent(toSaveContent.Id);

			contentRepositoryMock.VerifyAll();
			Assert.AreEqual(toSaveContent, obtainedContent);
		}

		[TestMethod]
		public void UpdateContent_ExistingPlaylist_ContentUpdated()
		{
			Category music = new Category()
			{
				Id = 1,
				Name = "Musica"
			};

			Playlist playlist = new Playlist()
			{
				Id = 1,
				Name = "Besto of Bon Jovi"
			};

			Content toSaveContent = new Content()
			{
				ArtistName = "Bon Jovi",
				Categories = new List<Category>(){
						music
					},
				Id = 1,
				ContentLength = new TimeSpan(0, 2, 30),
				Name = "It's My Life",
				ImageUrl = "http://www.images.com/image.jpg",
				ContentUrl = "http://www.audios.com/audio.mp3"
			};

			Mock<IContentRepository> contentRepositoryMock = new Mock<IContentRepository>(MockBehavior.Strict);
			contentRepositoryMock.Setup(m => m.Add(toSaveContent));
			contentRepositoryMock.Setup(m => m.Update(toSaveContent));
			contentRepositoryMock.Setup(m => m.Get(toSaveContent.Id)).Returns(toSaveContent);

			Mock<IPlaylistRepository> playlistRepository = new Mock<IPlaylistRepository>(MockBehavior.Strict);
			playlistRepository.Setup(m => m.Get(playlist.Id)).Returns(playlist);

			Mock<ICategoryRepository> categoryRepository = new Mock<ICategoryRepository>(MockBehavior.Strict);
			categoryRepository.Setup(m => m.Get(music.Id)).Returns(music);

			ContentLogic contentLogic = new ContentLogic(contentRepositoryMock.Object, playlistRepository.Object, categoryRepository.Object);

			contentLogic.CreateContent(toSaveContent);
			toSaveContent.PlayLists = new List<Playlist>()
			{
				playlist
			};
			contentLogic.UpdateContent(toSaveContent);
			Content obtainedContent = contentLogic.GetContent(toSaveContent.Id);

			contentRepositoryMock.VerifyAll();
			Assert.AreEqual(toSaveContent, obtainedContent);
		}

		[TestMethod]
		public void UpdateContent_WithoutPlaylist_ContentUpdated()
		{
			Category music = new Category()
			{
				Id = 1,
				Name = "Musica"
			};

			Playlist playlist = new Playlist()
			{
				Id = 1,
				Name = "Besto of Bon Jovi"
			};

			Content toSaveContent = new Content()
			{
				ArtistName = "Bon Jovi",
				Categories = new List<Category>(){
						music
					},
				Id = 1,
				ContentLength = new TimeSpan(0, 2, 30),
				Name = "It's My Life",
				ImageUrl = "http://www.images.com/image.jpg",
				ContentUrl = "http://www.audios.com/audio.mp3"
			};

			Mock<IContentRepository> contentRepositoryMock = new Mock<IContentRepository>(MockBehavior.Strict);
			contentRepositoryMock.Setup(m => m.Add(toSaveContent));
			contentRepositoryMock.Setup(m => m.Update(toSaveContent));
			contentRepositoryMock.Setup(m => m.Get(toSaveContent.Id)).Returns(toSaveContent);

			Mock<IPlaylistRepository> playlistRepository = new Mock<IPlaylistRepository>(MockBehavior.Strict);

			Mock<ICategoryRepository> categoryRepository = new Mock<ICategoryRepository>(MockBehavior.Strict);
			categoryRepository.Setup(m => m.Get(music.Id)).Returns(music);

			ContentLogic contentLogic = new ContentLogic(contentRepositoryMock.Object, playlistRepository.Object, categoryRepository.Object);

			contentLogic.CreateContent(toSaveContent);
			toSaveContent.PlayLists = new List<Playlist>();
			contentLogic.UpdateContent(toSaveContent);
			Content obtainedContent = contentLogic.GetContent(toSaveContent.Id);

			contentRepositoryMock.VerifyAll();
			Assert.AreEqual(toSaveContent, obtainedContent);
		}

		[TestMethod]
		[ExpectedException(typeof(NotFoundException))]
		public void UpdateContent_InvalidCategory_ExceptionThrown()
		{
			Category music = new Category()
			{
				Id = 1,
				Name = "Musica"
			};

			Playlist playlist = new Playlist()
			{
				Id = 1,
				Name = "Besto of Bon Jovi"
			};

			Content toSaveContent = new Content()
			{
				ArtistName = "Bon Jovi",
				Categories = new List<Category>(){
						music
					},
				PlayLists = new List<Playlist>()
				{
					playlist
				},
				Id = 1,
				ContentLength = new TimeSpan(0, 2, 30),
				Name = "It's My Life",
				ImageUrl = "http://www.images.com/image.jpg",
				ContentUrl = "http://www.audios.com/audio.mp3"
			};

			Mock<IContentRepository> contentRepositoryMock = new Mock<IContentRepository>(MockBehavior.Strict);
			contentRepositoryMock.Setup(m => m.Add(toSaveContent));
			contentRepositoryMock.Setup(m => m.Update(toSaveContent));
			contentRepositoryMock.Setup(m => m.Get(toSaveContent.Id)).Returns(toSaveContent);

			Mock<IPlaylistRepository> playlistRepository = new Mock<IPlaylistRepository>(MockBehavior.Strict);
			playlistRepository.Setup(m => m.Get(playlist.Id)).Returns(playlist);

			Mock<ICategoryRepository> categoryRepository = new Mock<ICategoryRepository>(MockBehavior.Strict);
			
			categoryRepository.Setup(m => m.Get(music.Id)).Returns(music);
			Category invalid = new Category()
			{
				Id = 2222,
				Name = "Invalid category"
			};
			Category notFound = null;
			categoryRepository.Setup(m => m.Get(music.Id)).Returns(notFound);

			ContentLogic contentLogic = new ContentLogic(contentRepositoryMock.Object, playlistRepository.Object, categoryRepository.Object);

			contentLogic.CreateContent(toSaveContent);
			toSaveContent.Categories = new List<Category>()
			{
				music,
				notFound
			};
			Content obtainedContent = contentLogic.GetContent(toSaveContent.Id);

			contentRepositoryMock.VerifyAll();
			Assert.AreNotEqual(toSaveContent, obtainedContent);
		}

		[TestMethod]
		[ExpectedException(typeof(UnableToCreatePlaylistException))]
		public void UpdateContent_NewPlaylistMissingData_ExceptionThrown()
		{
			Category music = new Category()
			{
				Id = 1,
				Name = "Musica"
			};

			Playlist playlist = new Playlist()
			{
				Id = 1
			};

			Content toSaveContent = new Content()
			{
				ArtistName = "Bon Jovi",
				Categories = new List<Category>(){
						music
					},
				PlayLists = new List<Playlist>()
				{
					playlist
				},
				Id = 1,
				ContentLength = new TimeSpan(0, 2, 30),
				Name = "It's My Life",
				ImageUrl = "http://www.images.com/image.jpg",
				ContentUrl = "http://www.audios.com/audio.mp3"
			};

			Playlist notFound = new Playlist()
			{
				Id = 2
			};

			Mock<IContentRepository> contentRepositoryMock = new Mock<IContentRepository>(MockBehavior.Strict);
			contentRepositoryMock.Setup(m => m.Add(toSaveContent));
			contentRepositoryMock.Setup(m => m.Update(toSaveContent));
			contentRepositoryMock.Setup(m => m.Get(toSaveContent.Id)).Returns(toSaveContent);

			Mock<IPlaylistRepository> playlistRepository = new Mock<IPlaylistRepository>(MockBehavior.Strict);
			playlistRepository.Setup(m => m.Get(playlist.Id)).Returns(playlist);
			playlistRepository.Setup(m => m.Get(notFound.Id)).Throws(new NotFoundException(notFound.Id.ToString()));

			Mock<ICategoryRepository> categoryRepository = new Mock<ICategoryRepository>(MockBehavior.Strict);
			categoryRepository.Setup(m => m.Get(music.Id)).Returns(music);

			ContentLogic contentLogic = new ContentLogic(contentRepositoryMock.Object, playlistRepository.Object, categoryRepository.Object);

			contentLogic.CreateContent(toSaveContent);
			toSaveContent.PlayLists = new List<Playlist>(){
				playlist,
				notFound
			};
			contentLogic.UpdateContent(toSaveContent);
			Content obtainedContent = contentLogic.GetContent(toSaveContent.Id);

			contentRepositoryMock.VerifyAll();
			Assert.AreNotEqual(obtainedContent, toSaveContent);
		}

		[TestMethod]
		[ExpectedException(typeof(MissingCategoriesException))]
		public void UpdateContent_NoCategoriesEntered_ExceptionThrown()
		{
			Category music = new Category()
			{
				Id = 1
			};

			Playlist playlist = new Playlist()
			{
				Id = 1,
				Name = "Besto of Bon Jovi"
			};

			Content toSaveContent = new Content()
			{
				ArtistName = "Bon Jovi",
				PlayLists = new List<Playlist>()
				{
					playlist
				},
				Id = 1,
				ContentLength = new TimeSpan(0, 2, 30),
				Name = "It's My Life",
				ImageUrl = "http://www.images.com/image.jpg",
				ContentUrl = "http://www.audios.com/audio.mp3"
			};

			Mock<IContentRepository> contentRepositoryMock = new Mock<IContentRepository>(MockBehavior.Strict);
			contentRepositoryMock.Setup(m => m.Add(toSaveContent));
			contentRepositoryMock.Setup(m => m.Get(toSaveContent.Id)).Returns(toSaveContent);

			Mock<IPlaylistRepository> playlistRepository = new Mock<IPlaylistRepository>(MockBehavior.Strict);
			playlistRepository.Setup(m => m.Get(playlist.Id)).Returns(playlist);

			Mock<ICategoryRepository> categoryRepository = new Mock<ICategoryRepository>(MockBehavior.Strict);
			categoryRepository.Setup(m => m.Get(music.Id)).Returns(music);

			ContentLogic contentLogic = new ContentLogic(contentRepositoryMock.Object, playlistRepository.Object, categoryRepository.Object);

			contentLogic.CreateContent(toSaveContent);
			toSaveContent.Categories = new List<Category>();
			contentLogic.UpdateContent(toSaveContent);
			Content obtainedContent = contentLogic.GetContent(toSaveContent.Id);

			contentRepositoryMock.VerifyAll();
			Assert.AreNotEqual(obtainedContent, toSaveContent);
		}

		[TestMethod]
		[ExpectedException(typeof(NotFoundException))]
		public void UpdateContent_ContentNotFound_ExceptionThrown()
		{
			Content content = new Content()
			{
				Id = 1
			};

			Mock<IContentRepository> contentRepositoryMock = new Mock<IContentRepository>(MockBehavior.Strict);
			contentRepositoryMock.Setup(m => m.Get(content.Id)).Throws(new NotFoundException(content.Id.ToString()));

			Mock<IPlaylistRepository> playlistRepository = new Mock<IPlaylistRepository>(MockBehavior.Strict);
			Mock<ICategoryRepository> categoryRepository = new Mock<ICategoryRepository>(MockBehavior.Strict);

			ContentLogic contentLogic = new ContentLogic(contentRepositoryMock.Object, playlistRepository.Object, categoryRepository.Object);

			contentLogic.UpdateContent(content);
			Content obtainedContent = contentLogic.GetContent(content.Id);

			contentRepositoryMock.VerifyAll();
			Assert.IsNull(obtainedContent);
		}

		[TestMethod]
		public void GetAll_ContentsByContentTypeExist_ContentsFetched()
		{
			Content content = new Content()
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
				ContentUrl = "http://www.audios.com/video.mp4",
				ContentType = new ContentType()
				{
					Id = 1,
					Name = "video"
				}
			};

			List<Content> expectedContents = new List<Content>()
			{
				content
			};

			Mock<IContentRepository> contentRepositoryMock = new Mock<IContentRepository>(MockBehavior.Strict);
			contentRepositoryMock.Setup(m => m.GetAll(content.ContentType.Name)).Returns(expectedContents);

			Mock<IPlaylistRepository> playlistRepository = new Mock<IPlaylistRepository>(MockBehavior.Strict);
			Mock<ICategoryRepository> categoryRepository = new Mock<ICategoryRepository>(MockBehavior.Strict);
			ContentLogic contentLogic = new ContentLogic(contentRepositoryMock.Object, playlistRepository.Object, categoryRepository.Object);

			IEnumerable<Content> obtainedContents = contentLogic.GetContents();

			contentRepositoryMock.VerifyAll();
			Assert.IsTrue(obtainedContents.SequenceEqual(expectedContents));
		}

		[TestMethod]
		public void GetAll_ContentsByContentTypeNotExist_ContentsFetched()
		{
			Content content = new Content()
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
				ContentUrl = "http://www.audios.com/video.mp4",
				ContentType = new ContentType()
				{
					Id = 1,
					Name = "video"
				}
			};

			List<Content> expectedContents = new List<Content>()
			{
				content
			};

			Mock<IContentRepository> contentRepositoryMock = new Mock<IContentRepository>(MockBehavior.Strict);
			contentRepositoryMock.Setup(m => m.GetAll(content.ContentType.Name)).Returns(new List<Content>());

			Mock<IPlaylistRepository> playlistRepository = new Mock<IPlaylistRepository>(MockBehavior.Strict);
			Mock<ICategoryRepository> categoryRepository = new Mock<ICategoryRepository>(MockBehavior.Strict);
			ContentLogic contentLogic = new ContentLogic(contentRepositoryMock.Object, playlistRepository.Object, categoryRepository.Object);

			IEnumerable<Content> obtainedContents = contentLogic.GetContents();

			contentRepositoryMock.VerifyAll();
			Assert.IsFalse(obtainedContents.SequenceEqual(expectedContents));
		}
	}
}
