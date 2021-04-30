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
        public void GetContentsOk()
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
		public void GetContentsByPlaylistOk()
		{
			List<Content> expectedContents = GetContentsByPlaylistExpectedContents();
			Playlist expectedPlaylist = expectedContents.First().PlayLists.First();
			Mock<IContentRepository> contentRepositoryMock = new Mock<IContentRepository>(MockBehavior.Strict);
			contentRepositoryMock.Setup(m => m.GetAll(expectedPlaylist)).Returns(expectedContents);

			Mock<IPlaylistRepository> playlistRepository = new Mock<IPlaylistRepository>(MockBehavior.Strict);
			Mock<ICategoryRepository> categoryRepository = new Mock<ICategoryRepository>(MockBehavior.Strict);
			ContentLogic contentLogic = new ContentLogic(contentRepositoryMock.Object, playlistRepository.Object, categoryRepository.Object);

			IEnumerable<Content> obtainedContents = contentLogic.GetContents(expectedPlaylist);
			Assert.IsTrue(expectedContents.SequenceEqual(obtainedContents));
		}

		private static List<Content> GetContentsByPlaylistExpectedContents()
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
				AudioUrl = "http://www.audios.com/audio.mp3"
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
				AudioUrl = "http://www.audios.com/audio.mp3"
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
		public void GetContentsByCategoryOk()
		{
			List<Content> expectedContents = GetContentsByCategoryExpectedContents();
			Category expectedCategory = expectedContents.First().Categories.First();
			Mock<IContentRepository> contentRepositoryMock = new Mock<IContentRepository>(MockBehavior.Strict);
			contentRepositoryMock.Setup(m => m.GetAll(expectedCategory)).Returns(expectedContents);

			Mock<IPlaylistRepository> playlistRepository = new Mock<IPlaylistRepository>(MockBehavior.Strict);
			Mock<ICategoryRepository> categoryRepository = new Mock<ICategoryRepository>(MockBehavior.Strict);
			ContentLogic contentLogic = new ContentLogic(contentRepositoryMock.Object, playlistRepository.Object, categoryRepository.Object);

			IEnumerable<Content> obtainedContents = contentLogic.GetContents(expectedCategory);
			Assert.IsTrue(expectedContents.SequenceEqual(obtainedContents));
		}

		private static List<Content> GetContentsByCategoryExpectedContents()
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
		public void GetContentOk()
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
				AudioUrl = "http://www.audios.com/audio.mp3"
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
		public void GetContentNotFound()
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
		public void CreateContentWithNewPlaylist()
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
				AudioUrl = "http://www.audios.com/audio.mp3"
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
		public void CreateContentWithExistentPlaylist()
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
				AudioUrl = "http://www.audios.com/audio.mp3"
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
		public void CreateContentWithoutPlaylist()
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
				AudioUrl = "http://www.audios.com/audio.mp3"
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
		public void CreateContentInvalidCategory()
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
				AudioUrl = "http://www.audios.com/audio.mp3"
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
		public void CreateContentUnableToCreatePlaylist()
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
				AudioUrl = "http://www.audios.com/audio.mp3"
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
		public void CreateContentMissingCategories()
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
				AudioUrl = "http://www.audios.com/audio.mp3"
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
		public void DeleteContentOk()
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
				AudioUrl = "http://www.audios.com/audio.mp3"
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

		//////////////////////
		//////////////////////
		//////////////////////
		/////////////////////
		///
		[TestMethod]
		public void UpdateContentWithNewPlaylist()
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
				AudioUrl = "http://www.audios.com/audio.mp3"
			};

			Mock<IContentRepository> contentRepositoryMock = new Mock<IContentRepository>(MockBehavior.Strict);
			contentRepositoryMock.Setup(m => m.Add(toSaveContent));

			contentRepositoryMock.Setup(m => m.Update(toSaveContent));
			contentRepositoryMock.Setup(m => m.Get(toSaveContent.Id)).Returns(toSaveContent);

			Mock<IPlaylistRepository> playlistRepository = new Mock<IPlaylistRepository>(MockBehavior.Strict);
			playlistRepository.Setup(m => m.Get(playlist.Id)).Throws(new NotFoundException(playlist.Id.ToString()));

			Mock<ICategoryRepository> categoryRepository = new Mock<ICategoryRepository>(MockBehavior.Strict);
			categoryRepository.Setup(m => m.Get(music.Id)).Returns(music);

			ContentLogic contentLogic = new ContentLogic(contentRepositoryMock.Object, playlistRepository.Object, categoryRepository.Object);

			contentLogic.CreateContent(toSaveContent);
			toSaveContent.PlayLists.Append(new Playlist()
			{
				Id = 2,
				Name = "Playlist2"
			});
			contentLogic.UpdateContent(toSaveContent);
			
			Content obtainedContent = contentLogic.GetContent(toSaveContent.Id);

			contentRepositoryMock.VerifyAll();
			Assert.AreEqual(toSaveContent, obtainedContent);
		}

		[TestMethod]
		public void UpdateContentWithExistentPlaylist()
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
				AudioUrl = "http://www.audios.com/audio.mp3"
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
			toSaveContent.PlayLists.Append(playlist);
			contentLogic.UpdateContent(toSaveContent);
			Content obtainedContent = contentLogic.GetContent(toSaveContent.Id);

			contentRepositoryMock.VerifyAll();
			Assert.AreEqual(toSaveContent, obtainedContent);
		}

		[TestMethod]
		public void UpdateContentWithoutPlaylist()
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
				AudioUrl = "http://www.audios.com/audio.mp3"
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
		public void UpdateContentInvalidCategory()
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
				AudioUrl = "http://www.audios.com/audio.mp3"
			};

			Mock<IContentRepository> contentRepositoryMock = new Mock<IContentRepository>(MockBehavior.Strict);
			contentRepositoryMock.Setup(m => m.Add(toSaveContent));
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
			categoryRepository.Setup(m => m.Get(music.Id)).Returns(invalid);

			ContentLogic contentLogic = new ContentLogic(contentRepositoryMock.Object, playlistRepository.Object, categoryRepository.Object);

			contentLogic.CreateContent(toSaveContent);
			toSaveContent.Categories.Append(invalid);
			Content obtainedContent = contentLogic.GetContent(toSaveContent.Id);

			contentRepositoryMock.VerifyAll();
			Assert.AreNotEqual(toSaveContent, obtainedContent);
		}

		[TestMethod]
		[ExpectedException(typeof(UnableToCreatePlaylistException))]
		public void UpdateContentUnableToCreatePlaylist()
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
				AudioUrl = "http://www.audios.com/audio.mp3"
			};

			Mock<IContentRepository> contentRepositoryMock = new Mock<IContentRepository>(MockBehavior.Strict);
			contentRepositoryMock.Setup(m => m.Add(toSaveContent));
			contentRepositoryMock.Setup(m => m.Get(toSaveContent.Id)).Returns(toSaveContent);

			Mock<IPlaylistRepository> playlistRepository = new Mock<IPlaylistRepository>(MockBehavior.Strict);
			Playlist notFound = new Playlist()
			{
				Id = 2
			};
			playlistRepository.Setup(m => m.Get(playlist.Id)).Returns(notFound);

			Mock<ICategoryRepository> categoryRepository = new Mock<ICategoryRepository>(MockBehavior.Strict);
			categoryRepository.Setup(m => m.Get(music.Id)).Returns(music);

			ContentLogic contentLogic = new ContentLogic(contentRepositoryMock.Object, playlistRepository.Object, categoryRepository.Object);

			contentLogic.CreateContent(toSaveContent);
			toSaveContent.PlayLists.Append(notFound);
			contentLogic.UpdateContent(toSaveContent);
			Content obtainedContent = contentLogic.GetContent(toSaveContent.Id);

			contentRepositoryMock.VerifyAll();
			Assert.AreNotEqual(obtainedContent, toSaveContent);
		}

		[TestMethod]
		[ExpectedException(typeof(MissingCategoriesException))]
		public void UpdateContentMissingCategories()
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
				AudioUrl = "http://www.audios.com/audio.mp3"
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
	}
}
