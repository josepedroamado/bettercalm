using DataAccess.Context;
using DataAccess.Repositories;
using Domain;
using Domain.Exceptions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace DataAccess.Test
{
	[TestClass]
	public class ContentRepositoryTest
	{
		private DbContext context;
		private DbConnection connection;

		public ContentRepositoryTest()
		{
			this.connection = new SqliteConnection("Filename=:memory:");
			this.context = new BetterCalmContext(
				new DbContextOptionsBuilder<BetterCalmContext>()
				.UseSqlite(connection)
				.Options);
		}

		[TestInitialize]
		public void Setup()
		{
			this.connection.Open();
			this.context.Database.EnsureCreated();
		}

		[TestCleanup]
		public void TestCleanup()
		{
			this.context.Database.EnsureDeleted();
		}

		[TestMethod]
		public void GetAll_ContentsExist_Fetched()
		{
			List<Content> expectedContents = GetExpectedContents();

			expectedContents.ForEach(content => this.context.Add(content));
			this.context.SaveChanges();
			ContentRepository repository = new ContentRepository(this.context);

			IEnumerable<Content> obtainedContents = repository.GetAll();
			Assert.IsTrue(expectedContents.SequenceEqual(obtainedContents));
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
		[ExpectedException(typeof(CollectionEmptyException))]
		public void GetAll_NoContentsExist_ExceptionThrown()
		{
			ContentRepository repository = new ContentRepository(this.context);
			IEnumerable<Content> obtainedContents = repository.GetAll();
			Assert.IsNull(obtainedContents);
		}

		[TestMethod]
		public void GetAllByPlaylist_ContentsAndPlaylistExist_Fetched()
		{
			List<Content> expectedContents = GetAllByPlaylistExpectedContents();

			expectedContents.ForEach(content => this.context.Add(content));
			this.context.SaveChanges();
			ContentRepository repository = new ContentRepository(this.context);

			Playlist expectedPlaylist = expectedContents.First().PlayLists.First();
			IEnumerable<Content> obtainedContents = repository.GetAll(expectedPlaylist);
			Assert.IsTrue(expectedContents.SequenceEqual(obtainedContents));
		}

		[TestMethod]
		[ExpectedException(typeof(CollectionEmptyException))]
		public void GetAllByPlaylist_NoContentExistsPlaylistExists_ExceptionThrown()
		{
			List<Content> expectedContents = GetAllByPlaylistExpectedContents();
			ContentRepository repository = new ContentRepository(this.context);
			Playlist expectedPlaylist = expectedContents.First().PlayLists.First();
			IEnumerable<Content> obtainedContents = repository.GetAll(expectedPlaylist);
			Assert.IsNull(obtainedContents);
		}

		private static List<Content> GetAllByPlaylistExpectedContents()
		{
			Category rock = new Category()
			{
				Id = 1,
				Name = "Rock"
			};

			Playlist bestOfBonJovi = new Playlist() { };

			bestOfBonJovi = new Playlist()
			{
				Id = 1,
				Name = "Best of Bon Jovi",
				Description = "The Best songs by Bon Jovi",
				ImageUrl = "http://www.images.com/image.jpg",
				Categories = new List<Category>() { rock }
			};

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

			bestOfBonJovi.Contents = new List<Content>() { itsMyLife, livinOnAPrayer };

			List<Content> expectedContent = new List<Content>();
			foreach (Content song in bestOfBonJovi.Contents)
			{
				expectedContent.Add(song);
			}

			return expectedContent;
		}

		[TestMethod]
		public void GetAllByCategory_ContentsAndCategoryExist_Fetched()
		{
			List<Content> expectedContents = GetAllByCategoryExpectedContents();

			expectedContents.ForEach(content => this.context.Add(content));
			this.context.SaveChanges();
			ContentRepository repository = new ContentRepository(this.context);

			Category expectedCategory = expectedContents.First().Categories.First();
			IEnumerable<Content> obtainedContents = repository.GetAll(expectedCategory);
			Assert.IsTrue(expectedContents.SequenceEqual(obtainedContents));
		}

		[TestMethod]
		[ExpectedException(typeof(CollectionEmptyException))]
		public void GetAllByCategory_NoContentsExistsCategoryExists_ExceptionThrown()
		{
			List<Content> expectedContents = GetAllByCategoryExpectedContents();
			ContentRepository repository = new ContentRepository(this.context);
			Category expectedCategory = expectedContents.First().Categories.First();
			IEnumerable<Content> obtainedContents = repository.GetAll(expectedCategory);
			Assert.IsNull(obtainedContents);
		}

		private static List<Content> GetAllByCategoryExpectedContents()
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
				ImageUrl = "http://www.images.com/image.jpg",
				ContentUrl = "http://www.audios.com/audio.mp3"
			};

			Content livinOnAPrayer = new Content()
			{
				ArtistName = "Bon Jovi",
				Categories = new List<Category>() { rock },
				PlayLists = new List<Playlist>() { },
				Id = 2,
				ContentLength = new TimeSpan(0, 4, 10),
				Name = "Livin' On A Prayer",
				ImageUrl = "http://www.images.com/image.jpg",
				ContentUrl = "http://www.audios.com/audio.mp3"
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
		public void Get_ContentFound_Fetched()
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
				ContentUrl = "http://www.audios.com/audio.mp3"
			};

			this.context.Add(expectedContent);
			this.context.SaveChanges();

			ContentRepository repository = new ContentRepository(this.context);

			Content obtainedContent = repository.Get(expectedContent.Id);
			Assert.AreEqual(expectedContent, obtainedContent);
		}

		[TestMethod]
		[ExpectedException(typeof(NotFoundException))]
		public void Get_ContentNotFound_ExceptionThrown()
		{
			Content toSaveContent = new Content()
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
			};

			this.context.Add(toSaveContent);
			this.context.SaveChanges();

			int toGetContentId = 2;
			
			ContentRepository repository = new ContentRepository(this.context);

			Content obtainedContent = repository.Get(toGetContentId);
			Assert.AreNotEqual(toSaveContent, obtainedContent);
		}

		[TestMethod]
		public void Add_DataIsCorrectToNewPlaylist_Added()
		{
			Category music = new Category()
			{
				Id = 1,
				Name = "Musica"
			};
			this.context.Add(music);

			ContentType contentType = new ContentType()
			{
				Id = 1,
				Name = "audio"
			};
			this.context.Add(contentType);

			Content toSaveContent = new Content()
			{
				ArtistName = "Bon Jovi",
				Categories = new List<Category>(){
						music
					},
				PlayLists = new List<Playlist>()
				{
					new Playlist()
					{
						Id = 1,
						Name = "Besto of Bon Jovi",
						Description = "Playlist description"
					}
				},
				Id = 1,
				ContentLength = new TimeSpan(0, 2, 30),
				Name = "It's My Life",
				ImageUrl = "http://www.images.com/image.jpg",
				ContentUrl = "http://www.audios.com/audio.mp3",
				ContentType = contentType
			};
			this.context.SaveChanges();

			ContentRepository repository = new ContentRepository(this.context);

			repository.Add(toSaveContent);
			Content added = repository.Get(toSaveContent.Id);

			Assert.AreEqual(toSaveContent, added);
		}

		[TestMethod]
		public void Add_DataIsCorrect_Added()
		{
			Category music = new Category()
			{
				Id = 1,
				Name = "Musica"
			};
			this.context.Add(music);

			ContentType contentType = new ContentType()
			{
				Id = 1,
				Name = "audio"
			};
			this.context.Add(contentType);

			Playlist playlist = new Playlist()
			{
				Id = 1,
				Name = "Besto of Bon Jovi",
				Description = "Playlist description"
			};
			this.context.Add(playlist);

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
				ContentUrl = "http://www.audios.com/audio.mp3",
				ContentType = contentType
			};
			this.context.SaveChanges();

			ContentRepository repository = new ContentRepository(this.context);

			repository.Add(toSaveContent);
			Content added = repository.Get(toSaveContent.Id);

			Assert.AreEqual(toSaveContent, added);
		}

		[TestMethod]
		public void Add_DataIsCorrectNoPlaylist_Added()
		{
			Category music = new Category()
			{
				Id = 1,
				Name = "Musica"
			};
			this.context.Add(music);

			ContentType contentType = new ContentType()
			{
				Id = 1,
				Name = "audio"
			};
			this.context.Add(contentType);

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
				ContentUrl = "http://www.audios.com/audio.mp3",
				ContentType = contentType
			};
			this.context.SaveChanges();

			ContentRepository repository = new ContentRepository(this.context);

			repository.Add(toSaveContent);
			Content added = repository.Get(toSaveContent.Id);

			Assert.AreEqual(toSaveContent, added);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidInputException))]
		public void Add_NoNameEntered_ExceptionThrown()
		{
			Content content = new Content()
			{
				Id = 1,
				ArtistName = "artist name",
				ContentUrl = "http://audio.com/audio.mp3",
				ContentLength = TimeSpan.Parse("00:00:01")
			};

			ContentRepository repository = new ContentRepository(this.context);

			repository.Add(content);
			Content obtained = repository.Get(content.Id);

			Assert.IsNull(obtained);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidInputException))]
		public void Add_NoArtistNameEntered_ExceptionThrown()
		{
			Content content = new Content()
			{
				Id = 1,
				Name = "name",
				ContentUrl = "http://audio.com/audio.mp3",
				ContentLength = TimeSpan.Parse("00:00:01")
			};

			ContentRepository repository = new ContentRepository(this.context);

			repository.Add(content);
			Content obtained = repository.Get(content.Id);

			Assert.IsNull(obtained);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidInputException))]
		public void Add_NoAudioUrlEntered_ExceptionThrown()
		{
			Content content = new Content()
			{
				Id = 1,
				Name = "name",
				ArtistName = "artist name",
				ContentLength = TimeSpan.Parse("00:00:01")
			};

			ContentRepository repository = new ContentRepository(this.context);

			repository.Add(content);
			Content obtained = repository.Get(content.Id);

			Assert.IsNull(obtained);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidInputException))]
		public void Add_NoContentLengthEntered_ExceptionThrown()
		{
			Content content = new Content()
			{
				Id = 1,
				Name = "name",
				ArtistName = "artist name",
				ContentUrl = "http://audio.com/audio.mp3"
			};

			ContentRepository repository = new ContentRepository(this.context);

			repository.Add(content);
			Content obtained = repository.Get(content.Id);

			Assert.IsNull(obtained);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidInputException))]
		public void Add_NoValidPlaylistEntered_ExceptionThrown()
		{
			Content content = new Content()
			{
				Id = 1,
				Name = "name",
				ArtistName = "artist name",
				ContentUrl = "http://audio.com/audio.mp3",
				ContentLength = TimeSpan.Parse("00:00:01"),
				PlayLists = new List<Playlist>()
				{
					new Playlist()
					{
						Id = 1,
						Name = "playlist name"
					}
				}
			};

			ContentRepository repository = new ContentRepository(this.context);

			repository.Add(content);
			Content obtained = repository.Get(content.Id);

			Assert.IsNull(obtained);
		}

		[TestMethod]
		[ExpectedException(typeof(NotFoundException))]
		public void Delete_ContentFound_Deleted()
		{
			Category music = new Category()
			{
				Id = 1,
				Name = "Musica"
			};
			this.context.Add(music);
			this.context.SaveChanges();

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
				ContentUrl = "http://www.audios.com/audio.mp3",
				ContentType = new ContentType()
				{
					Id = 1,
					Name = "audio"
				}
			};

			ContentRepository repository = new ContentRepository(this.context);
			repository.Add(toSaveContent);

			repository.Delete(toSaveContent.Id);
			
			Content obtained = repository.Get(toSaveContent.Id);

			Assert.AreNotEqual(toSaveContent, obtained);
		}

		[TestMethod]
		public void Update_DataIsValid_Updated()
		{
			Category music = new Category()
			{
				Id = 1,
				Name = "Musica"
			};
			this.context.Add(music);

			Playlist playlist = new Playlist()
			{
				Id = 1,
				Name = "Best of Bon Jovi",
				Description = "Playlist description"
			};
			this.context.Add(playlist);

			Content currentContent = new Content()
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
				ContentUrl = "http://www.audios.com/audio.mp3",
				ContentType = new ContentType()
				{
					Id = 1,
					Name = "audio"
				}
			};
			this.context.Add(currentContent);

			Playlist newPlaylist = new Playlist()
			{
				Id = 2,
				Name = "Trend Jovi",
				Description = "Playlist description"
			};
			this.context.Add(newPlaylist);
			this.context.SaveChanges();

			currentContent.PlayLists = new List<Playlist>()
			{
				playlist,
				newPlaylist
			};

			ContentRepository repository = new ContentRepository(this.context);

			repository.Update(currentContent);
			Content obtained = repository.Get(currentContent.Id);
			Assert.AreEqual(currentContent, obtained);
		}

		[TestMethod]
		public void Update_DataIsValidNoPlaylist_Updated()
		{
			Category music = new Category()
			{
				Id = 1,
				Name = "Musica"
			};
			this.context.Add(music);

			Content currentContent = new Content()
			{
				ArtistName = "Bon Jovi",
				Categories = new List<Category>(){
						music
					},
				PlayLists = new List<Playlist>()
				{
					new Playlist()
					{
						Id = 1,
						Name = "Best of Bon Jovi",
						Description = "Playlist description"
					}
				},
				Id = 1,
				ContentLength = new TimeSpan(0, 2, 30),
				Name = "It's My Life",
				ImageUrl = "http://www.images.com/image.jpg",
				ContentUrl = "http://www.audios.com/audio.mp3",
				ContentType = new ContentType()
				{
					Id = 1,
					Name = "audio"
				}
			};
			this.context.Add(currentContent);
			this.context.SaveChanges();

			currentContent.PlayLists = new List<Playlist>();

			ContentRepository repository = new ContentRepository(this.context);

			repository.Update(currentContent);
			Content obtained = repository.Get(currentContent.Id);
			Assert.AreEqual(currentContent, obtained);
		}

		[TestMethod]
		public void Update_DataIsValidAddCategory_Updated()
		{
			Category music = new Category()
			{
				Id = 1,
				Name = "Musica"
			};
			this.context.Add(music);

			Content currentContent = new Content()
			{
				ArtistName = "Bon Jovi",
				Categories = new List<Category>(){
						music
					},
				PlayLists = new List<Playlist>()
				{
					new Playlist()
					{
						Id = 1,
						Name = "Best of Bon Jovi",
						Description = "Playlist description"
					}
				},
				Id = 1,
				ContentLength = new TimeSpan(0, 2, 30),
				Name = "It's My Life",
				ImageUrl = "http://www.images.com/image.jpg",
				ContentUrl = "http://www.audios.com/audio.mp3",
				ContentType = new ContentType()
				{
					Id = 1,
					Name = "audio"
				}
			};
			this.context.Add(currentContent);

			Category category = new Category()
			{
				Id = 2,
				Name = "Category2"
			};
			this.context.Add(category);
			this.context.SaveChanges();

			currentContent.Categories = new List<Category>()
			{
				music,
				category
			};

			ContentRepository repository = new ContentRepository(this.context);

			repository.Update(currentContent);
			Content obtained = repository.Get(currentContent.Id);
			Assert.AreEqual(currentContent, obtained);
		}

		[TestMethod]
		public void Update_DataIsValidUpdateAllProperties_Updated()
		{
			Category music = new Category()
			{
				Id = 1,
				Name = "Musica"
			};
			this.context.Add(music);

			Playlist playlist = new Playlist()
			{
				Id = 1,
				Name = "Best of Bon Jovi",
				Description = "Playlist description"
			};
			this.context.Add(playlist);

			Content currentContent = new Content()
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
				ContentUrl = "http://www.audios.com/audio.mp3",
				ContentType = new ContentType()
				{
					Id = 1,
					Name = "audio"
				}
			};
			this.context.Add(currentContent);

			Category newCategory = new Category()
			{
				Id = 2,
				Name = "Category2"
			};
			this.context.Add(newCategory);
			Playlist newPlaylist = new Playlist()
			{
				Id = 2,
				Name = "Playlist2",
				Description = "Playlist description"
			};
			this.context.Add(newPlaylist);
			this.context.SaveChanges();

			currentContent.Categories = new List<Category>()
			{
				music,
				newCategory
			};

			currentContent.PlayLists = new List<Playlist>()
			{
				playlist,
				newPlaylist
			};
			currentContent.ArtistName = "new artist name";
			currentContent.ContentUrl = "http://audio.com/a.mp3";
			currentContent.ContentLength = TimeSpan.Parse("00:00:30");
			currentContent.ImageUrl = "http://image.com/a.jpg";
			currentContent.Name = "new content name";

			ContentRepository repository = new ContentRepository(this.context);

			repository.Update(currentContent);
			Content obtained = repository.Get(currentContent.Id);
			Assert.AreEqual(currentContent, obtained);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidInputException))]
		public void Update_NoNameEntered_ExceptionThrown()
		{
			Content content = new Content()
			{
				Id = 1,
				Name = "name",
				ArtistName = "artist name",
				ContentUrl = "http://audio.com/audio.mp3",
				ContentLength = TimeSpan.Parse("00:01:30")
			};

			ContentRepository repository = new ContentRepository(this.context);

			repository.Add(content);

			content.Name = string.Empty;
			repository.Update(content);

			Content obtained = repository.Get(content.Id);

			Assert.AreNotEqual(content.Name, obtained.Name);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidInputException))]
		public void Update_NoArtistNameEntered_ExceptionThrown()
		{
			Content content = new Content()
			{
				Id = 1,
				Name = "name",
				ArtistName = "artist name",
				ContentUrl = "http://audio.com/audio.mp3",
				ContentLength = TimeSpan.Parse("00:01:30")
			};

			ContentRepository repository = new ContentRepository(this.context);

			repository.Add(content);

			content.ArtistName = string.Empty;
			repository.Update(content);

			Content obtained = repository.Get(content.Id);

			Assert.AreNotEqual(content.ArtistName, obtained.ArtistName);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidInputException))]
		public void Update_NoAudioUrlEntered_ExceptionThrown()
		{
			Content content = new Content()
			{
				Id = 1,
				Name = "name",
				ArtistName = "artist name",
				ContentUrl = "http://audio.com/audio.mp3",
				ContentLength = TimeSpan.Parse("00:01:30")
			};

			ContentRepository repository = new ContentRepository(this.context);

			repository.Add(content);

			content.ContentUrl = string.Empty;
			repository.Update(content);

			Content obtained = repository.Get(content.Id);

			Assert.AreNotEqual(content.ContentUrl, obtained.ContentUrl);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidInputException))]
		public void Update_NoContentLengthEntered_ExceptionThrown()
		{
			Content content = new Content()
			{
				Id = 1,
				Name = "name",
				ArtistName = "artist name",
				ContentUrl = "http://audio.com/audio.mp3",
				ContentLength = TimeSpan.Parse("00:01:30")
			};

			ContentRepository repository = new ContentRepository(this.context);

			repository.Add(content);

			content.ContentLength = TimeSpan.Zero;
			repository.Update(content);

			Content obtained = repository.Get(content.Id);

			Assert.AreNotEqual(content.ContentLength, obtained.ContentLength);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidInputException))]
		public void Update_InvalidPlaylistEntered_ExceptionThrown()
		{
			Content content = new Content()
			{
				Id = 1,
				Name = "name",
				ArtistName = "artist name",
				ContentUrl = "http://audio.com/audio.mp3",
				ContentLength = TimeSpan.Parse("00:01:30")
			};

			ContentRepository repository = new ContentRepository(this.context);

			repository.Add(content);

			content.PlayLists = new List<Playlist>()
			{
				new Playlist()
				{
					Id = 1,
					Name = "playlist name"
				}
			};

			repository.Update(content);

			Content obtained = repository.Get(content.Id);

			CollectionAssert.AreNotEqual(content.PlayLists.ToList(), obtained.PlayLists.ToList());
		}
	}
}
