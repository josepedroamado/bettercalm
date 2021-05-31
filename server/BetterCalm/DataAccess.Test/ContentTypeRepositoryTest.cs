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
using System.Text;

namespace DataAccess.Test
{
	[TestClass]
	public class ContentTypeRepositoryTest
	{
		private DbContext context;
		private DbConnection connection;

		public ContentTypeRepositoryTest()
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
		public void Get_ContentType_Fetched()
		{
			ContentType audio = new ContentType()
			{
				Id = 1,
				Name = "audio"
			};
			ContentType video = new ContentType()
			{
				Id = 2,
				Name = "video"
			};
			this.context.Add(audio);
			this.context.Add(video);
			this.context.SaveChanges();

			ContentTypeRepository contentTypeRepository = new ContentTypeRepository(this.context);

			ContentType result = contentTypeRepository.Get(audio.Name);

			Assert.AreEqual(audio, result);
		}

		[TestMethod]
		[ExpectedException(typeof(NotFoundException))]
		public void Get_ContentType_NotFound()
		{
			ContentType audio = new ContentType()
			{
				Id = 1,
				Name = "audio"
			};
			ContentType video = new ContentType()
			{
				Id = 2,
				Name = "video"
			};
			this.context.Add(video);
			this.context.SaveChanges();

			ContentTypeRepository contentTypeRepository = new ContentTypeRepository(this.context);

			ContentType result = contentTypeRepository.Get(audio.Name);

			Assert.AreNotEqual(audio, result);
		}
	}
}
